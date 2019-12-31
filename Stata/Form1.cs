using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Stata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void nikolaefff()
        {
            var request = WebRequest.Create("http://wotskill.ru/players/analysis_wn8/ru-5807219");
            using (var responses = request.GetResponse())
            {
                using (var streams = responses.GetResponseStream())
                using (var readers = new StreamReader(streams))
                {
                    //в переменной html наш сайт
                    string html = readers.ReadToEnd();
                    //ищем определенное место
                    var UpdExp = new Regex(@"<td class=""Aggregated2Value AggregatedValue4"">(?<upd>\d.*)</td>");
                    //в переменной upDate наша искомая дата обновления
                    string upDate = UpdExp.Match(html).Groups["upd"].Value; // дата
                    label2.Text = upDate; //выводим значение на форму
                }
            }

        }

        void ctakah()
        {
            var request = WebRequest.Create("http://wotskill.ru/players/analysis_wn8/ru-7923121");
            using (var responses = request.GetResponse())
            {
                using (var streams = responses.GetResponseStream())
                using (var readers = new StreamReader(streams))
                {
                    //в переменной html наш сайт
                    string html = readers.ReadToEnd();
                    //ищем определенное место
                    var UpdExp = new Regex(@"<td class=""Aggregated2Value AggregatedValue3"">(?<upd>\d.*)</td>");
                    //в переменной upDate наша искомая дата обновления
                    string upDate = UpdExp.Match(html).Groups["upd"].Value; // дата
                    label4.Text = upDate; //выводим значение на форму
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            nikolaefff();
            ctakah();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Width = 440;
            label5.Text = label2.Text;
            label6.Text = label4.Text;
            label7.Text = "Было...";
            nikolaefff();
            ctakah();
        }
    }
}
