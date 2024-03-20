using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="A required field")]
        [MaxLength(50, ErrorMessage ="max length is 50 characters")]
        [MinLength(2, ErrorMessage ="min length is 2 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        //navigations
        public virtual List<AuthorPost> AuthorPosts { get; set; }
    }
}
