using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class FeedbackModel
    {
        [Key]
        public int feedbackId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public string feedback { get; set; }
        public double rating { get; set; }
        public string customerName { get; set; }
    }
}
