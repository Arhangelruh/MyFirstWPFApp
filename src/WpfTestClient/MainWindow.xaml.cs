using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfTestClient.Interfaces;
using WpfTestClient.Models;
using WpfTestClient.Services;

namespace WpfTestClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IApiService api = new ApiService();
        DataTable cardsTable;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cardsTable = new DataTable();
            try
            {
                var cardsTable = await api.GetCardsAsync();
                List<BookView> books = new List<BookView>();

                foreach (var card in cardsTable)
                {
                    var image = new BitmapImage();
                    if (card.Picture != null)
                    {
                        var picture = card.Picture;
                        var stream = new MemoryStream(picture);
                        stream.Seek(0, SeekOrigin.Begin);                        
                        image.BeginInit();
                        image.StreamSource = stream;
                        image.EndInit();
                    }

                    var danloadCard = new BookView
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Picture = image
                    };
                    books.Add(danloadCard);
                }
                cardsGrid.ItemsSource = books;
            }
            catch
            {

            }
        }
        private async void updateButton_Click(object sender, RoutedEventArgs e) {
            var book = cardsGrid.SelectedItem as Book;
            if (book != null)
            {
                AddCard addCardWindow = new AddCard(book);
                addCardWindow.Owner = this;
                var dialog = addCardWindow.ShowDialog();
                if (dialog == true)
                {
                    cardsTable = new DataTable();
                    try
                    {
                        var cardsTable = await api.GetCardsAsync();
                        cardsGrid.ItemsSource = cardsTable;
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для изменения");
            }
        }
        private async void deleteButton_Click(object sender, RoutedEventArgs e) {

            var book = cardsGrid.SelectedItem as Book;
            if (book != null)
            {
                try
                {
                    //var book = new Book
                    //{
                    //    Id = bookfield.Id,
                    //    Name = bookfield.Name
                    //};

                    var delbook = await api.DeleteCardAsync(book);
                    if (delbook == "true")
                    {
                        MessageBox.Show("Книга удалена");
                        cardsTable = new DataTable();
                        try
                        {
                            var cardsTable = await api.GetCardsAsync();
                            cardsGrid.ItemsSource = cardsTable;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления");
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления");
            }
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {

            AddCard addCardWindow = new AddCard();
            addCardWindow.Owner = this;
            var dialog = addCardWindow.ShowDialog();
            if (dialog==true)
            {
                cardsTable = new DataTable();
                try
                {
                    var cardsTable = await api.GetCardsAsync();
                    cardsGrid.ItemsSource = cardsTable;
                }
                catch
                {

                }
            }
        }
    }
}
