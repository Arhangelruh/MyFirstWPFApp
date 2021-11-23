using System.Collections.Generic;
using System.Threading.Tasks;
using WpfTestClient.Models;

namespace WpfTestClient.Interfaces
{
    public interface IApiService
    {
        /// <summary>
        /// Add card.
        /// </summary>
        /// <returns></returns>
        Task<string> AddCardAsync(Book book);

        /// <summary>
        /// Edit card.
        /// </summary>
        /// <returns></returns>
        Task<string> EditCardAsync(Book book);

        /// <summary>
        /// Delete card.
        /// </summary>
        /// <returns></returns>
        Task<string> DeleteCardAsync(Book book);

        /// <summary>
        /// Get cards.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetCardsAsync();
    }
}
