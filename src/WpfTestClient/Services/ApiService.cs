using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfTestClient.Constants;
using WpfTestClient.Interfaces;
using WpfTestClient.Models;

namespace WpfTestClient.Services
{
    public class ApiService : IApiService
    {
        public async Task<string> AddCardAsync(Book book)
        {

            try
            {
                var result = await Paths.apipath
                    .WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(book).ReceiveJson<Book>();

                if (result is null)
                {
                    return "error";
                }
                else
                {
                    return "ok";
                }
            }
            catch
            {
                return "error";
            }
        }

        public async Task<string> DeleteCardAsync(Book book)
        {
            var responce = await Paths.apipath
                .AppendPathSegment(book.Id)
                .DeleteAsync()
                .ReceiveJson<string>();

            return responce;
        }

        public async Task<string> EditCardAsync(Book book)
        {

            var responce = await Paths.apipath
                .AppendPathSegment(book.Id)
                .PutJsonAsync(book).ReceiveJson<string>();
            return responce;
        }

        public async Task<IEnumerable<Book>> GetCardsAsync()
        {
            try
            {
                var responce = await Paths.apipath
                        .GetJsonAsync<IEnumerable<Book>>();
                return responce;
            }
            catch
            {
                return new List<Book>();
            }

        }
    }
}
