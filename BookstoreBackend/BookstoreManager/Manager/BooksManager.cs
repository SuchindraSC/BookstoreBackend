using BookstoreManager.Interface;
using BookstoreModel;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Manager
{
    public class BooksManager : IBooksManager
    {
        private readonly IBooksRepository repository;

        public BooksManager(IBooksRepository repository)
        {
            this.repository = repository;
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.repository.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookModel GetBookDetails(int BookId)
        {
            try
            {
                return this.repository.GetBookDetails(BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FeedbackModel> GetCustomerFeedBack(int bookid)
        {
            try
            {
                return this.repository.GetCustomerFeedBack(bookid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
