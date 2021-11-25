using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface IBooksManager
    {
        List<BookModel> GetAllBooks();
        BookModel GetBookDetails(int BookId);
        List<FeedbackModel> GetCustomerFeedBack(int bookid);
    }
}
