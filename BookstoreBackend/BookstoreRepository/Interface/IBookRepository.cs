using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IBookRepository
    {
        BookModel AddBook(BookModel book);
        BookModel UpdateBook(BookModel book);
        bool DeleteBook(int bookId);
        bool AddCustomerFeedBack(FeedbackModel feedbackModel);
    }
}
