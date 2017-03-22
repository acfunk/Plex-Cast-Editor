using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Linq;

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
            sb.AppendLine("	,mi.user_thumb_url");
            sb.AppendLine("	,mi.user_fields");
            sb.AppendLine("	,COUNT(t.id) AS actor_count");
            sb.AppendLine("FROM metadata_items mi");
            sb.AppendLine("LEFT JOIN taggings ti");
            sb.AppendLine("	ON mi.id = ti.metadata_item_id");
            sb.AppendLine("LEFT JOIN tags t");
            sb.AppendLine("	ON ti.tag_id = t.id");
            sb.AppendLine("	AND t.tag_type = 6");
            sb.AppendLine("WHERE mi.library_section_id = @library_section_id");
            sb.AppendLine(" AND mi.metadata_type IN (1,2)");
            sb.AppendLine("GROUP BY mi.id;");

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

        public static long CreateActor(string actor)
        {
            long row_id = 0;

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO tags (tag,tag_type,created_at,updated_at)");
                sb.AppendLine("VALUES(");
                sb.AppendLine(" @tag");
                sb.AppendLine(" ,6");
                sb.AppendLine(" ,datetime(current_timestamp, 'localtime')");
                sb.AppendLine(" ,datetime(current_timestamp, 'localtime')");
                sb.AppendLine(" );");

                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@tag", actor);
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT last_insert_rowid()";
                    row_id = (long)command.ExecuteScalar();
                    connection.Close();
                }
            }

            return row_id;
        }
    }
}
