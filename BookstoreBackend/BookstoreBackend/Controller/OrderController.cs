using BookstoreManager.Interface;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreBackend.Controller
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;

        public OrderController(IOrderManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/placeorders")]
        public IActionResult PlaceOrders([FromBody] List<Order> orderDetails)
        {
            try
            {
                var result = this.manager.PlaceTheOrder(orderDetails);
                if (result)
                {

                    return this.Ok(new { success = true, Message = "Order placed successfully" });
                }
                else
                {

                    return this.BadRequest(new { success = false, Message = "Failed to place order, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { success = false, Message = ex.Message });

            }
        }

        [HttpGet]
        [Route("api/getorderlist")]
        public IActionResult GetOrderList(int userId)
        {
            var result = this.manager.GetOrderList(userId);
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Wish List successfully retrived", Data = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "No WishList available" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { success = false, Message = e.Message });
            }
        }
    }
}
