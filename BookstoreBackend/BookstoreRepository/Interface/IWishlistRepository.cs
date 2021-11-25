using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IWishlistRepository
    {
        string AddBookToWishlist(int bookId, int userId);
        List<WishlistModel> GetWishlistItems(int userId);
        bool DeleteBookFromWishlist(int wishlistId);
    }
}
