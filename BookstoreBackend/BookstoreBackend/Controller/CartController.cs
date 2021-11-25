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
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/cartaddbook")]
        public IActionResult AddBookToCart(int bookId, int userId)
        {
            try
            {
                string resultMessage = this.manager.AddBookToCart(bookId, userId);

                if (resultMessage.Equals("Add Book To Cart Successful"))
                {
                    return this.Ok(new { success = true, message = resultMessage });
                }
                else if (resultMessage.Equals("Add Book To Cart Failed, Book already Exists in Cart"))
                {
                    return this.Ok(new { success = false, message = resultMessage });
                }

                return this.Ok(new { success = false, message = resultMessage });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/cartgetbooks")]
        public IActionResult GetCartItems(int userId)
        {
            List<CartModel> cart = this.manager.GetCartItems(userId);
            try
            {
                if (cart.Count > 0)
                {
                    return this.Ok(new { success = true, message = "Get Cart Items Successful", data = cart });

                }
                return this.Ok(new { success = false, message = "Get Cart Item Failed, Cart list is Empty" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });
            }
        }

        [HttpPut]
        [Route("api/cartupdatebook")]
        public IActionResult UpdateCartItem(int cartId, int quantityToBuy)
        {
            try
            {
                bool result = this.manager.UpdateCartItem(cartId, quantityToBuy);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Update Cart Item Successful" });
                }

                return this.Ok(new { success = false, message = "Update Cart Item Failed, Book doesnot exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/cartdeletebook")]
        public IActionResult DeleteBookFromCart(int cartId)
        {
            try
            {
                bool result = this.manager.DeleteBookFromCart(cartId);

                if (result)
                {
                    return this.Ok(new { success = true, message = "Delete Book From Cart Successful" });
                }

                return this.Ok(new { success = false, message = "Delete Book From Cart Failed, Book doesnot exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
