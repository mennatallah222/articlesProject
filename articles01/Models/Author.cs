using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="ID")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Profile Pic")]
        public string ProfilePicURL { get; set; }
        public string Bio { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }


        public virtual List<AuthorPost> AuthorPosts { get; set; }

    }
}
