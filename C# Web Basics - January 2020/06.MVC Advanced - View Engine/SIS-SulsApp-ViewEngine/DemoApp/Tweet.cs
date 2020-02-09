using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp
{
    public class Tweet
    {
       // [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; } 

        [Required]
        public string Creator { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
