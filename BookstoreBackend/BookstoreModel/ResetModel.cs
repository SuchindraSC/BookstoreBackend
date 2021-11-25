using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class ResetModel
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string token { get; set; }
    }
}
