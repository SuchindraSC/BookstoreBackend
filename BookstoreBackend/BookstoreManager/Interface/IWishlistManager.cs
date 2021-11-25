using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface IWishlistManager
    {
        string AddBookToWishlist(int bookId, int userId);
        List<WishlistModel> GetWishlistItems(int userId);
        bool DeleteBookFromWishlist(int wishlistId);
    }
}
