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
        string nikolaefff_link = "http://wotskill.ru/players/analysis_wn8/ru-5807219";
        string ctakah_link = "http://wotskill.ru/players/analysis_wn8/ru-7923121";
        async string check_stat(string link, string number)
        {
            var request = WebRequest.Create(link);
            using (var responses = request.GetResponse())
            {
                using (var streams = responses.GetResponseStream())
                using (var readers = new StreamReader(streams))
                {
                    //в переменной html наш сайт
                    string html = readers.ReadToEnd();
                    //ищем определенное место
                    var UpdExp = new Regex($@"<td class=""Aggregated2Value AggregatedValue{number}"">(?<upd>\d.*)</td>");
                    var UpdEx = new Regex(@"<td class=""Aggregated2Value AggregatedValue0"">(?<upd>\d.*)</td>");
                    //в переменной upDate наша искомая дата обновления
                    string wn8 = UpdExp.Match(html).Groups["upd"].Value; // дата
                    string count_boi = UpdEx.Match(html).Groups["upd"].Value;
                    


                    return null;

                }
            }
        }
        

        
        

        private void button1_Click(object sender, EventArgs e)
        {

            check_stat(nikolaefff_link, "4");
            check_stat(ctakah_link, "3");
        }

        
    }
}
