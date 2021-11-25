using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int QuantityToBuy { get; set; }
        public string DateOfOrder { get; set; }
        public BookModel Books { get; set; }
    }

    public class Order
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int QuantityToBuy { get; set; }
    }
}
