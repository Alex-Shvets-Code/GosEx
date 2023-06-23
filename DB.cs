using MySql.Data.MySqlClient;
using System;

namespace DB
{
    internal class DB
    {
        static string database = "test";
        MySqlConnection connection = new MySqlConnection(
           "server=localhost;port=3306;username=root;password=;database=" + database);

        public void openConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connect success");
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1045:
                        Console.WriteLine("Error: Incorrect password. Please check the credentials and try again.");
                        break;
                    case 1049:
                        Console.WriteLine("Error: Incorrect database name. Please check the database name and try again.");
                        break;
                    default:
                        Console.WriteLine("Error: " + ex.Message);
                        break;
                }
            }
        }

        public void closeConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public MySqlConnection getConnection() { return connection; }

        public int Test(MySqlCommand command)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return result;
        }
    }
}
