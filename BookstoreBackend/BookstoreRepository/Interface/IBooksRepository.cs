using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IBooksRepository
    {
        List<BookModel> GetAllBooks();
        BookModel GetBookDetails(int BookId);
        List<FeedbackModel> GetCustomerFeedBack(int bookid);
    }
}
