using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Models
{
    public class AuthorPost
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Post Category")]
        [DataType(DataType.Text)]
        public string PostCategory { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.MultilineText)]
        public string PostDescription{ get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Profile Pic")]
        [DataType(DataType.Upload)]
        public string PostImageUrl { get; set; }

        public DateTime AddedDate { get; set; }



        public int AuthorId { get; set; }
        public Author Author  { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
