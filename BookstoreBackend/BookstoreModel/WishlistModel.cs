using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class WishlistModel
    {
        [Key]
        public int WishlistId { get; set; }

        public BookModel BookId { get; set; }

        public int UserId { get; set; }
    }
}
