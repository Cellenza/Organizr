using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Organizr.Models
{
    public class Idea
    {
        public int Id { get; set; }
        
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public User Submitter { get; set; }

        [Required]
        public DateTime Submitted { get; set; }
    }
}