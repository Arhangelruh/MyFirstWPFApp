using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfTestClient.Models
{
    public class BookView
    {
        //id.
        public string Id { get; set; }

        //name book.
        public string Name { get; set; }

        //picture
        public BitmapImage Picture { get; set; }
    }
}
