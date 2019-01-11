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

        private void LoadFeedButtonClick(object sender, RoutedEventArgs e)
        {
            string rssURL = urlTB.Text;

            if(rssURL != string.Empty)
            {
                HttpWebRequest rssRequest = WebRequest.Create(rssURL) as HttpWebRequest;
                rssRequest.KeepAlive = false;

                HttpWebResponse response = rssRequest.GetResponse() as HttpWebResponse;
                outputTB.Text = response.ContentType;

                XDocument doc = XDocument.Load(response.GetResponseStream());
            }
            else
            {
                MessageBox.Show("RSS feed URL is empty", "Error");
            }
        }
    }
}
