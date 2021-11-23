using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfTestClient.Interfaces;
using WpfTestClient.Models;
using WpfTestClient.Services;

namespace WpfTestClient
{
    /// <summary>
    /// Логика взаимодействия для AddCard.xaml
    /// </summary>
    public partial class AddCard : Window
    {
        private byte[] danloadFile;
        private IApiService api = new ApiService();
        private Book _book { get; set; }

        public AddCard()
        {
            InitializeComponent();
        }

        public AddCard(Book book)
        {
            _book = book;
            InitializeComponent();
        }

        private void danloadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All Image Files|*.jpg";

            if ((bool)fileDialog.ShowDialog())
            {
                BitmapImage images = new BitmapImage(new Uri(fileDialog.FileName));
                photos_show.Source = images;

                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(images));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    danloadFile = ms.ToArray();
                }

            }
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_book is null)
            {
                var nameBook = textBox.Text;

                var book = new Book
                {
                    Name = nameBook,
                    Picture = danloadFile
                };

                try
                {
                    var result = await api.AddCardAsync(book);

                    if (result == "ok")
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        label.Content = "Ошибка";
                    }
                }
                catch
                {
                    label.Content = "Ошибка";
                }
            }
            else
            {
                var nameBook = textBox.Text;
                var book = new Book
                {
                    Id = _book.Id,
                    Name = nameBook,
                    Picture = danloadFile
                };

                try
                {
                    var result = await api.EditCardAsync(book);

                    if (result == "true")
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        label.Content = "Ошибка";
                    }
                }
                catch
                {
                    label.Content = "Ошибка";
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_book != null)
            {
                textBox.Text = _book.Name;
                if (_book.Picture != null)
                {
                    var picture = _book.Picture;
                    var stream = new MemoryStream(picture);
                    stream.Seek(0, SeekOrigin.Begin);
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    photos_show.Source = image;
                }
            }
        }
    }
}
