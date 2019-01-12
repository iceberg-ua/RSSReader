using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private async void LoadFeedButtonClick(object sender, RoutedEventArgs e)
        {
            string rssURL = urlTB.Text;

            if(rssURL != string.Empty)
            {
                XDocument doc = null;

                await Task.Run(() => 
                {
                    HttpWebRequest rssRequest = (HttpWebRequest)WebRequest.Create(rssURL);
                    rssRequest.KeepAlive = false;
                    HttpWebResponse response = (HttpWebResponse)rssRequest.GetResponse();
                    doc = XDocument.Load(response.GetResponseStream());
                    response.Close();
                });

                ParseResponse(doc);
            }
            else
            {
                MessageBox.Show("RSS feed URL is empty", "Error");
            }
        }

        public void ParseResponse(XDocument doc)
        {
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
