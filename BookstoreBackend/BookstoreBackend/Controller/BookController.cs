using BookstoreManager.Interface;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreBackend.Controller
{
    //[Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        // Constructor Dependency Injection
        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addbook")]
        public IActionResult AddBook([FromBody] BookModel book)
        {
            try
            {
                BookModel bookData = this.manager.AddBook(book);

                if (bookData != null)
                {
                    return this.Ok(new { success = true, message = "Add Book Successful", result = bookData });
                }

                return this.Ok(new { success = false, message = "Add Book Failed, Book with the same name already exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/updatebook")]
        public IActionResult UpdateBook([FromBody] BookModel book)
        {
            try
            {
                BookModel bookData = this.manager.UpdateBook(book);
                if (bookData != null)
                {
                    return this.Ok(new { success = true, message = "Update Book Successful", result = bookData });
                }

                return this.Ok(new { success = false, message = "Update Book Failed, Book doesnot exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deletebook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                bool result = this.manager.DeleteBook(bookId);

                if (result)
                {
                    return this.Ok(new { success = true, message = "Delete Book Successful" });
                }

                return this.Ok(new { success = false, message = "Delete Book Failed, Book doesnot exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/addcustomerfeedBack")]
        public IActionResult AddCustomerFeedBack([FromBody] FeedbackModel feedbackModel)
        {
            try
            {
                var result = this.manager.AddCustomerFeedBack(feedbackModel);
                if (result)
                {

                    return this.Ok(new { success = true, Message = "Added FeedBack Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { success = false, Message = "Failed to add feedback, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { success = false, Message = ex.Message });

            }
        }
    }
}
