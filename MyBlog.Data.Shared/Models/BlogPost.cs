using MyBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Models
{
    public class BlogPost : IMyBlogItem
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        
        public DateTime PublishDate { get; set; }
        
        public Category? Category { get; set; }

        public ICollection<Tag> Tags { get; set; } = new Collection<Tag>();
    }
}
