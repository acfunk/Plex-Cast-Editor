using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Linq;

namespace PlexCastEditor
{
    public static class Database
    {
        public static string DBFile { get; set; }
        public static DataTable GetLibrarySections()
        {
            var table = new DataTable();

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand("select id, name from library_sections;", connection))
                {
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            DataRow row = table.NewRow();
            row["id"] = -1;
            row["name"] = "-- Select a library --";
            table.Rows.InsertAt(row, 0);

            return table;
        }

        public static DataTable GetMetadataItems(int librarySectionId)
        {
            var table = new DataTable();

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand("select id, title, user_thumb_url from metadata_items where library_section_id = @library_section_id;", connection))
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

        public static DataTable GetActors(int metadataItemId)
        {
            var table = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select");
            sb.AppendLine("    t.id");
            sb.AppendLine("    , t.tag");
            sb.AppendLine("    , ti.[index]");
            sb.AppendLine("from metadata_items mi");
            sb.AppendLine("join taggings ti");
            sb.AppendLine("    on mi.id = ti.metadata_item_id");
            sb.AppendLine("join tags t");
            sb.AppendLine("    on ti.tag_id = t.id");
            sb.AppendLine("where mi.id = @metadata_item_id");
            sb.AppendLine("    and t.tag_type = 6");
            sb.AppendLine("order by ti.[index];");

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

        public static string[] GetAllActors()
        {
            var table = new DataTable();

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                using (SQLiteCommand command = new SQLiteCommand("select tag from tags where tag_type = 6;", connection))
                {
                    connection.Open();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                    connection.Close();
                }
            }

            return table.AsEnumerable().Select(r => r.Field<string>("tag")).ToArray();
        }

        public static long CreateActor(string actor)
        {
            long row_id = 0;

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", DBFile)))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("insert into tags (tag,tag_type,created_at,updated_at)");
                sb.AppendLine("values(");
                sb.AppendLine("    @tag");
                sb.AppendLine("    , 6");
                sb.AppendLine("    , datetime(current_timestamp, 'localtime')");
                sb.AppendLine("    , datetime(current_timestamp, 'localtime')");
                sb.AppendLine("    );");

                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@tag", actor);
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.CommandText = "select last_insert_rowid()";
                    row_id = (long)command.ExecuteScalar();
                    connection.Close();
                }
            }

            return row_id;
        }
    }
}
