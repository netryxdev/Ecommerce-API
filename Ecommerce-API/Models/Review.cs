﻿namespace Ecommerce_API.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId{ get; set;}
        public int ProductId { get; set;}
        public int Rating { get; set;}
        public string Comment { get; set;}
    }
}
