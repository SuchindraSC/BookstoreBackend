using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface ICartManager
    {
        string AddBookToCart(int bookId, int userId);
        List<CartModel> GetCartItems(int userId);
        bool UpdateCartItem(int cartId, int quantityToBuy);
        bool DeleteBookFromCart(int cartId);
    }
}
