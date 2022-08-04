using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Net.Http;
using MySql.Data.MySqlClient;

namespace CrawlerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            startCrawlerasync().Wait();
            Console.ReadLine();
        }
        private static async Task startCrawlerasync()
        {
            carinforContext context = new carinforContext();

            var url = "https://www.audi.com/en/models.html";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var articles = new List<Articles>();

            var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("teaserhighlight parbase")).ToList();

            foreach (var div in divs)
            {
                var Title = div.Descendants("h3").FirstOrDefault().InnerText;
                var Link = div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                var ImageUrl = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                var carinfor = new Carinfor1
                {
                    Title = Title.Length>50?Title.Substring(0,50):Title,
                    Link = Link.Length>50?Link.Substring(0,50):Link,
                    ImageUrl = ImageUrl.Length>50?ImageUrl.Substring(0,50):ImageUrl
                };
                context.Carinfor1s.Add(carinfor);
            }
            context.SaveChanges();

            /*string MyConnection = "DRIVER = {MySQL ODBC 8.0 Driver};Server=localhost;Database=crawlerdemo;User Id=root;Password=";
            //string MyConnection = "datasource=localhost;username=root;password=";
            OdbcConnection con = new OdbcConnection(MyConnection);
            con.Open();

            try
            {
                int count = articles.Count;
                foreach (var item in articles)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string query = "insert into carinfor(Title,Link,ImageUrl) value(?,?,?);";
                        OdbcCommand cmd = new OdbcCommand(query, con);
                        cmd.Parameters.Add("?Title", OdbcType.VarChar).Value = articles[i].Title;
                        cmd.Parameters.Add("?Link", OdbcType.VarChar).Value = articles[i].Link;
                        cmd.Parameters.Add("?ImageUrl", OdbcType.VarChar).Value = articles[i].ImageUrl;
                        OdbcDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
            Console.WriteLine("Successful...");
            Console.WriteLine("Press enter to exit the program...");
            ConsoleKeyInfo keyinfor = Console.ReadKey(true);
            if (keyinfor.Key == ConsoleKey.Enter)
            {
                System.Environment.Exit(0);
            }*/
        }
    }
}