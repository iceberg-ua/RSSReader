using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace RSSReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFeedButtonClick(object sender, RoutedEventArgs e)
        {
            ParseResponse(urlTB.Text);
        }

        private async Task<string> LoadFeedWithHttpRequest(string uri)
        {
            string resultStream = string.Empty;

            if (uri != string.Empty)
            {
                HttpWebRequest rssRequest = (HttpWebRequest)WebRequest.Create(uri);
                rssRequest.KeepAlive = false;
                var response = await rssRequest.GetResponseAsync();

                var strmReader = new StreamReader(response.GetResponseStream());
                resultStream = strmReader.ReadToEnd();
                response.Close();
            }
            else
            {
                MessageBox.Show("RSS feed URL is empty", "Error");
            }

            return resultStream;
        }

        private async Task<Stream> LoadFeedWithHttpClient(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStreamAsync();
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        private async void ParseResponse(string uri)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var content = await LoadFeedWithHttpRequest(uri);
            XDocument doc = XDocument.Parse(content);

            //var content = await LoadFeedWithHttpClient(uri);
            //XDocument doc = XDocument.Load(content);

            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());

            var rss = doc.Element("rss");
            var channel = rss.Element("channel");

            string chTitle = channel.Element("title").Value;

            TreeViewItem channelItem = new TreeViewItem
            {
                Header = chTitle
            };

            foreach (var item in channel.Elements("item"))
            {
                string title = item.Element("title").Value;

                TreeViewItem itemBranch = new TreeViewItem
                {
                    Header = title
                };

                channelItem.Items.Add(itemBranch);
            }

            chanelsTree.Items.Add(channelItem);
        }
    }
}
