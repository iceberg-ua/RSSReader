using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RSSReader
{
   class ArticleItem : TreeViewItem
   {
      protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
      {
         //base.OnMouseDoubleClick(e);
         var aWindow = new ArticleWindow();
         aWindow.Title = Header.ToString();
         aWindow.Show();
      }
   }
}
