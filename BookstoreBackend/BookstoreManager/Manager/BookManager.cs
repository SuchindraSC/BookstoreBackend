using BookstoreManager.Interface;
using BookstoreModel;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repository;

        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.repository.AddBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return this.repository.UpdateBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.repository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddCustomerFeedBack(FeedbackModel feedbackModel)
        {
            try
            {
                return this.repository.AddCustomerFeedBack(feedbackModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
