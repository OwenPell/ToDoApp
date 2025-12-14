using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace ToDoApp
{
    /// <summary>
    /// Manages SQLite database operations for storing and retrieving tasks.
    /// </summary>
    public class DatabaseManager
    {
        /// <summary>
        /// Connection string used to access the SQLite database.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Active SQLite connection instance.
        /// </summary>
        private readonly SqliteConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManager"/> class
        /// and ensures the database schema exists.
        /// </summary>
        /// <param name="dbPath">Path to the SQLite database file.</param>
        public DatabaseManager(string dbPath = "tasks.db")
        {
            connectionString = $"Data Source={dbPath};";
            connection = new SqliteConnection(connectionString);
            InitializeDatabase();
        }

        /// <summary>
        /// Creates required database tables if they do not already exist.
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                connection.Open();

                string createTable = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        List TEXT NOT NULL,
                        CreatedAt TEXT NOT NULL,
                        Completed INTEGER NOT NULL DEFAULT 0
                    );";

                using var cmd = new SqliteCommand(createTable, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization error: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves a task to the database.
        /// </summary>
        /// <param name="title">Task title.</param>
        /// <param name="description">Task description.</param>
        /// <param name="list">Task list category.</param>
        /// <param name="createdAt">Task creation timestamp.</param>
        /// <param name="completed">Completion status.</param>
        public void SaveTask(string title, string description, string list, DateTime createdAt, bool completed)
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                string query = @"
                    INSERT INTO Tasks (Title, Description, List, CreatedAt, Completed)
                    VALUES (@title, @description, @list, @createdAt, @completed);";

                using var cmd = new SqliteCommand(query, connection);

                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@list", list);
                cmd.Parameters.AddWithValue("@createdAt", createdAt.ToString("o"));
                cmd.Parameters.AddWithValue("@completed", completed ? 1 : 0);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads all incomplete tasks from the database.
        /// </summary>
        /// <returns>A list of task data objects.</returns>
        public List<TaskData> LoadTasks()
        {
            var tasks = new List<TaskData>();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                string query = "SELECT Title, Description, List, CreatedAt, Completed FROM Tasks WHERE Completed = 0";
                using var cmd = new SqliteCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new TaskData
                    {
                        Title = reader.GetString(0),
                        Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        List = reader.GetString(2),
                        CreatedAt = DateTime.Parse(reader.GetString(3)),
                        Completed = reader.GetInt32(4) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}");
            }

            return tasks;
        }

        /// <summary>
        /// Removes all tasks from the database.
        /// </summary>
        public void ClearAllTasks()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                string query = "DELETE FROM Tasks";
                using var cmd = new SqliteCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing tasks: {ex.Message}");
            }
        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        public void Close()
        {
            connection?.Close();
        }

        /// <summary>
        /// Represents a single task record loaded from the database.
        /// </summary>
        public class TaskData
        {
            /// <summary>
            /// Task title.
            /// </summary>
            public string Title { get; set; } = string.Empty;

            /// <summary>
            /// Task description.
            /// </summary>
            public string Description { get; set; } = string.Empty;

            /// <summary>
            /// Task list category.
            /// </summary>
            public string List { get; set; } = "All Tasks";

            /// <summary>
            /// Task creation timestamp.
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// Indicates whether the task is completed.
            /// </summary>
            public bool Completed { get; set; }
        }
    }
}
