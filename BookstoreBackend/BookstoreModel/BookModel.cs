using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "bookName is required")]
        public string bookName { get; set; }

        [Required(ErrorMessage = "author is required")]
        public string author { get; set; }

        public string description { get; set; }

        public string bookImage { get; set; }

        [Required(ErrorMessage = "quantity is required")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "price is required")]
        public int price { get; set; }

        [Required(ErrorMessage = "discountPrice is required")]
        public int discountPrice { get; set; }
    }
}
