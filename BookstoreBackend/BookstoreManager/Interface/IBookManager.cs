using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface IBookManager
    {
        BookModel AddBook(BookModel book);
        BookModel UpdateBook(BookModel book);
        bool DeleteBook(int bookId);
        bool AddCustomerFeedBack(FeedbackModel feedbackModel);
    }
}
