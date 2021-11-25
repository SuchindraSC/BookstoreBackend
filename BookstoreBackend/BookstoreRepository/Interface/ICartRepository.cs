using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface ICartRepository
    {
        string AddBookToCart(int bookId, int userId);
        List<CartModel> GetCartItems(int userId);
        bool UpdateCartItem(int cartId, int quantityToBuy);
        bool DeleteBookFromCart(int cartId);
    }
}
