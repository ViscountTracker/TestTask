using Microsoft.Data.Sqlite;
using System.Data.Entity;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Windows.Media.Media3D;


namespace TestTask
{
    public class DatabaseHandler
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = "";
        public string Comment { get; set; } = "";
        public string ApplicationName { get; set; } = "";

        private readonly string _connectionString = "Data Source=MyDatabase.sqlite;Version=3;";

        public DatabaseHandler(int id, string userName, string comment, string applicationName)
        {
            Id = id;
            UserName = userName;
            Comment = comment;
            ApplicationName = applicationName;
        }

        public void Record()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(connection))
                {
                    command.CommandText = "INSERT INTO MyDatabase(Id, UserName, Comment, ApplicationName) VALUES (@id, @userName, @comment, @appName)";

                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.AddWithValue("@comment", Comment);
                    command.Parameters.AddWithValue("@appName", ApplicationName);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Insert(DatabaseHandler record)
        {
            if (!File.Exists("MyDatabase.sqlite"))
            {
                SqliteConnection.Create("MyDatabase.sqlite");
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqliteCommand(connection))
                    {
                        command.CommandText = "CREATE TABLE MyDatabase(Id INTEGER PRIMARY KEY AUTOINCREMENT, UserName TEXT, Comment TEXT, ApplicationName TEXT)";

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }


            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(connection))
                {
                    command.CommandText = "INSERT INTO MyDatabase(UserName, Comment, ApplicationName) VALUES (@userName, @comment, @appName)";

                    command.Parameters.AddWithValue("@userName", record.UserName);
                    command.Parameters.AddWithValue("@comment", record.Comment);
                    command.Parameters.AddWithValue("@appName", record.ApplicationName);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void UpdateRecord(DatabaseHandler record)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(connection))
                {
                    command.CommandText = "UPDATE MyDatabase SET UserName = @userName, Comment = @comment, ApplicationName = @appName WHERE Id = @id";

                    command.Parameters.AddWithValue("@userName", record.UserName);
                    command.Parameters.AddWithValue("@comment", record.Comment);
                    command.Parameters.AddWithValue("@appName", record.ApplicationName);
                    command.Parameters.AddWithValue("@id", record.Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void DeleteRecord(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(connection))
                {
                    command.CommandText = "DELETE FROM MyDatabase WHERE Id = @id";

                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}



