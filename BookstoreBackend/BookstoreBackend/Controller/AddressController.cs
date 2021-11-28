using BookstoreManager.Interface;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreBackend.Controller
{
    //[Authorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addaddress")]
        public IActionResult AddUserAddress([FromBody] AddressModel userDetails)
        {
            try
            {
                var result = this.manager.AddUserAddress(userDetails);
                if (result)
                {
                    return this.Ok(new { success = true, Message = "Added New UserDetails Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { success = false, Message = "Failed to add user Details, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { success = false, Message = ex.Message });

            }
        }

        [HttpPut]
        [Route("api/editaddress")]
        public IActionResult EditAddress([FromBody] AddressModel details)
        {
            var result = this.manager.EditAddress(details);
            try
            {
                if (result)
                {
                    return this.Ok(new { success = true, Message = "Address updated successfully" });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address updation fails" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { success = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("api/getaddress")]
        public IActionResult getUserAddress(int userId)
        {
            var result = this.manager.GetUserDetails(userId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address successfully retrived", Data = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "No address available" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { success = false, Message = e.Message });
            }
        }


        [HttpDelete]
        [Route("api/removeaddress")]
        public IActionResult RemoveFromUserDetails(int addressId)
        {
            try
            {
                var result = this.manager.RemoveFromUserDetails(addressId);
                if (result)
                {

                    return this.Ok(new { success = true, Message = "Removed User Address Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { success = false, Message = "Failed to Remove User Address, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { success = false, Message = ex.Message });

            }
        }
    }
}
