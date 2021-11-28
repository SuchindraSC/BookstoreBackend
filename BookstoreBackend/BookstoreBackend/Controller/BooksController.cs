using BookstoreManager.Interface;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreBackend.Controller
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksManager manager;

        public BooksController(IBooksManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        [Route("api/getbooks")]
        public IActionResult GetAllBooks()
        {
            List<BookModel> books = this.manager.GetAllBooks();
            try
            {
                if (books.Count > 0)
                {
                    return this.Ok(new { success = true, message = "Get Books Successful", data = books });
                }
                return this.Ok(new { success = false, message = "Book list is Empty" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });
            }
        }

        [HttpGet]
        [Route("api/getbookdetails")]
        public IActionResult GetBookDetail(int bookId)
        {
            var result = this.manager.GetBookDetails(bookId);
            try
            {
                if (result.BookId != 0)
                {
                    return this.Ok(new { success = true, Message = "Book is retrived", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "BookId Doesnt Exist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { success = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFeedback")]
        public IActionResult GetFeedback(int bookid)
        {
            try
            {
                var result = this.manager.GetCustomerFeedBack(bookid);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Feedbackertrived", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed to add to wish list, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, Message = ex.Message });
            }
        }
    }
}
