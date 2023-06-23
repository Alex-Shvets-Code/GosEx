using MySql.Data.MySqlClient;
using System.Windows.Forms;
using DB;

namespace GosEx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string query = "";
        public int count = 0;
        DB db = new DB();
        List<string[]> data = new List<string[]>();

        private void SendData(MySqlCommand command)
        {
            try
            {
                db.openConnection();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new string[dataGridView1.ColumnCount]);

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        Console.WriteLine(reader[i].ToString());
                        data[data.Count - 1][i] = reader[i].ToString();
                    }
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            finally { db.closeConnection(); }

            foreach (string[] rows in data)
            {
                dataGridView1.Rows.Add(rows);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            query = "SELECT P.Назва, COUNT(S.Товар_ID) AS Кількість " +
                    "FROM Товари AS P " +
                    "LEFT JOIN Магазини AS S ON P.ID = S.Товар_ID " +
                    "GROUP BY P.Назва; ";

            Console.WriteLine(query);
            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].HeaderText = "Назва";
            dataGridView1.Columns[1].HeaderText = "Кількість";

            SendData(command);
        }
    }
}