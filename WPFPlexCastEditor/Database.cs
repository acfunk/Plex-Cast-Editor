using System.Data;
using System.Data.SQLite;
using System.Text;
using WPFPlexCastEditor.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace WPFPlexCastEditor
{
    public static class Database
    {
        public static string DBFile { get; set; }
        public static DataTable GetLibrarySections()
        {
            var table = new DataTable();

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT id, name FROM library_sections WHERE section_type IN (1,2);", connection))
                {
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            return table;
        }

        public static DataTable GetMetadataItems(long librarySectionId)
        {
            var table = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("	mi.id");
            sb.AppendLine("	,mi.title");
            sb.AppendLine("	,mi.originally_available_at AS release_date");
            sb.AppendLine("	,mi.added_at AS date_added");
            sb.AppendLine("	,COUNT(t.id) AS actor_count");
            sb.AppendLine("	,mi.user_fields");
            sb.AppendLine("FROM metadata_items mi");
            sb.AppendLine("LEFT JOIN taggings ti");
            sb.AppendLine("	ON mi.id = ti.metadata_item_id");
            sb.AppendLine("LEFT JOIN tags t");
            sb.AppendLine("	ON ti.tag_id = t.id");
            sb.AppendLine("	AND t.tag_type = 6");
            sb.AppendLine("WHERE mi.library_section_id = @library_section_id");
            sb.AppendLine(" AND mi.metadata_type IN (1,2)");
            sb.AppendLine("GROUP BY mi.id");
            sb.AppendLine("ORDER BY mi.title;");

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@library_section_id", librarySectionId);
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            return table;
        }

        public static DataTable GetActors(long metadataItemId)
        {
            var table = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine(" t.id");
            sb.AppendLine(" ,t.tag");
            sb.AppendLine(" ,ti.[index]");
            sb.AppendLine("FROM taggings ti");
            sb.AppendLine("JOIN tags t");
            sb.AppendLine(" ON ti.tag_id = t.id");
            sb.AppendLine("WHERE ti.metadata_item_id = @metadata_item_id");
            sb.AppendLine(" AND t.tag_type = 6");
            sb.AppendLine("ORDER by ti.[index];");

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@metadata_item_id", metadataItemId);
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            return table;
        }

        public static DataTable GetAllActors()
        {
            var table = new DataTable();

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT id, tag FROM tags WHERE tag_type = 6;", connection))
                {
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            return table;
        }

        public static void ResetTaggings(MetadataItem metadataItem, ActorCollection cast)
        {
            // metadata_items.userfields must have lockedFields with 19 in "|" piped delimited list
            string user_fields = "lockedFields=19";

            // If there is an equal sign, assume that everything after is pipe delitemed ints
            // Don't throw an exception
            try
            {
                if (!string.IsNullOrWhiteSpace(metadataItem.user_fields) && metadataItem.user_fields.Contains("="))
                {
                    List<int> lockedFieldList = metadataItem.user_fields.Substring(metadataItem.user_fields.IndexOf("=") + 1).Split('|').Select(Int32.Parse).ToList();
                    if (!lockedFieldList.Contains(19))
                    {
                        lockedFieldList.Add(19);
                        lockedFieldList.Sort();
                    }

                    user_fields = "lockedFields=" + string.Join("|", lockedFieldList);
                }
            }
            catch { }

            StringBuilder deleteTaggings = new StringBuilder();
            deleteTaggings.AppendLine("DELETE FROM taggings");
            deleteTaggings.AppendLine("WHERE id in (");
            deleteTaggings.AppendLine("    SELECT ti.id");
            deleteTaggings.AppendLine("    FROM taggings ti");
            deleteTaggings.AppendLine("    JOIN tags t");
            deleteTaggings.AppendLine("        ON ti.tag_id = t.id");
            deleteTaggings.AppendLine("        AND t.tag_type = 6");
            deleteTaggings.AppendLine("    WHERE ti.metadata_item_id = @metadata_item_id");
            deleteTaggings.AppendLine("    );");

            StringBuilder insertTag = new StringBuilder();
            insertTag.AppendLine("INSERT INTO tags (tag,tag_type,created_at,updated_at)");
            insertTag.AppendLine("VALUES(");
            insertTag.AppendLine(" @tag");
            insertTag.AppendLine(" ,6");
            insertTag.AppendLine(" ,datetime(current_timestamp, 'localtime')");
            insertTag.AppendLine(" ,datetime(current_timestamp, 'localtime')");
            insertTag.AppendLine(" );");

            StringBuilder updateUserFields = new StringBuilder();
            updateUserFields.AppendLine("UPDATE metadata_items");
            updateUserFields.AppendLine("SET user_fields = @user_fields");
            updateUserFields.AppendLine("WHERE id = @metadata_item_id");
            
            string comma = "";
            int index = 0;
            StringBuilder insertTaggging = new StringBuilder();
            insertTaggging.AppendLine("insert into taggings(metadata_item_id, tag_id, [index], created_at)");
            insertTaggging.AppendLine("values");

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        foreach (Actor actor in cast)
                        {
                            // Create tag for actors that don't exist
                            if (actor.id < 0)
                            {
                                command.CommandText = insertTag.ToString();
                                command.Parameters.AddWithValue("@tag", actor.tag);
                                command.ExecuteNonQuery();
                                command.CommandText = "SELECT last_insert_rowid()";
                                actor.id = (long)command.ExecuteScalar();
                            }
                            
                            insertTaggging.AppendLine(string.Format("    {0}({1}, {2}, {3}, datetime(current_timestamp, 'localtime'))", comma, metadataItem.id, actor.id, index));
                            comma = ",";
                            index++;
                        }

                        // Delete all existing actor taggings
                        command.CommandText = deleteTaggings.ToString();
                        command.Parameters.AddWithValue("@metadata_item_id", metadataItem.id);
                        command.ExecuteNonQuery();

                        if (cast.Count > 0)
                        {
                            // Insert all new actor taggings
                            command.CommandText = insertTaggging.ToString();
                            command.ExecuteNonQuery();
                        }

                        // Updating user_fileds with value of 19 in pipe list
                        command.CommandText = updateUserFields.ToString();
                        command.Parameters.AddWithValue("@user_fields", user_fields);
                        command.Parameters.AddWithValue("@metadata_item_id", metadataItem.id);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                connection.Close();
            }

        }
    }
}