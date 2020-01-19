using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        string nikolaefff_link = "https://kttc.ru/wot/ru/user/nikolaefff/";
        string ctakah_link = "https://kttc.ru/wot/ru/user/ctakah_kofee4ku/";
        string sql_connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\developer\VisualStudio\Gamers-stat\Stata\history.mdf;Integrated Security=True;Connect Timeout=30";

        SqlCommand command;
        SqlDataAdapter adapt;
        SqlConnection connection;

        private string Check_stat(string link)
        {
            System.Net.WebClient wc = new WebClient();
            string responce = wc.DownloadString(link);
            //"currentBattles":"39664"
            string boi = System.Text.RegularExpressions.Regex.Match(responce, @"""currentBattles"":""([0-9]+)""").Groups[1].Value;
            //"WN8":1913.78
            string wn = System.Text.RegularExpressions.Regex.Match(responce, @"""WN8"":([0-9]+\.[0-9]+)").Groups[1].Value;
            if (link == nikolaefff_link)
            {

                label2.Text = "WN8 " + wn + " / " + boi + " боев";
                progressBar1.Value = 50;
                return wn + " " + boi;

            }
            else
            {

                label4.Text = "WN8 " + wn + " / " + boi + " боев";
                progressBar1.Value = 100;
                return wn + " " + boi;
            }

        }
        async private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            string nikolaefff = Check_stat(nikolaefff_link);
            string ctakah = Check_stat(ctakah_link);
            await Task.Delay(1000);
            progressBar1.Visible = false;

            DateTime time = DateTime.Now;
            connection = new SqlConnection(sql_connect);
            string query = "INSERT INTO [Table] (DateTime, Nikolaefff, CTakaH_KOFEe4kU) VALUES (@DateTime, @Nikolaefff, @CTakaH_KOFEe4kU)";
            await connection.OpenAsync();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("DateTime", time);
                command.Parameters.AddWithValue("Nikolaefff", nikolaefff);
                command.Parameters.AddWithValue("CTakaH_KOFEe4kU", ctakah);
                await command.ExecuteNonQueryAsync();

            }

            connection.Close();

            Load_data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "historyDataSet.Table". При необходимости она может быть перемещена или удалена.
            //this.tableTableAdapter.Fill(this.historyDataSet.Table);
            Load_data();

        }

        void Load_data()
        {
            connection = new SqlConnection(sql_connect);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT * FROM [Table]", connection);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dateTimeDataGridViewTextBoxColumn, System.ComponentModel.ListSortDirection.Descending);

            }
            connection.Close();
        }
    }
}









