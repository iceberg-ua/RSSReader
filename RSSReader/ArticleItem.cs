using System.Windows.Controls;
using System.Windows.Input;

namespace RSSReader
{
   class ArticleItem : TreeViewItem
   {
      public string Link { get; set; }

      protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
      {
         //base.OnMouseDoubleClick(e);
         var aWindow = new ArticleWindow
         {
            Title = Header.ToString()
         };

         aWindow.Show();
         aWindow.webViewer.Navigate(Link);
      }
   }
}
