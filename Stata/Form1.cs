using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
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
        string nikolaefff_link = "http://wotskill.ru/players/analysis_wn8/ru-5807219";
        string ctakah_link = "http://wotskill.ru/players/analysis_wn8/ru-7923121";
        async private void Check_stat(string link, string number)
        {
            await Task.Run(() =>
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
                    string wn8 = UpdExp.Match(html).Groups["upd"].Value;
                    string count_boi = UpdEx.Match(html).Groups["upd"].Value;
                    if (number == "4"){

                        label2.Text = $"WN8 {wn8}, {count_boi} боев";
                        progressBar1.Value = 50;
                                               
                    }
                    else{

                        label4.Text = $"WN8 {wn8}, {count_boi} боев";
                        progressBar1.Value = 100;
                        
                    }
                      
                    
                }
            }
            });
        }
        async private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            Check_stat(nikolaefff_link, "4");
            Check_stat(ctakah_link, "3");
            await Task.Delay(1000);
            progressBar1.Visible = false;
        }

        
    }
}




        

        
        

