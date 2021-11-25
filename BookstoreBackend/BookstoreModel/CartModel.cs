using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }

        public BookModel BookId { get; set; }

        public int UserId { get; set; }

        public int quantityToBuy { get; set; }
    }
}
