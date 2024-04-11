using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using articles01.Data;
using articles01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace articles01.Pages
{
    [Authorize]
    public class AdminIndexModel : PageModel
    {

         private readonly IDataHelper<AuthorPost> dataHelper;
        public int AllPosts { get; set; }
        public int PostsLastMonth { get; set; }
        public int PostThisYear { get; set; }
        public AdminIndexModel(IDataHelper<AuthorPost> dataHelper)
        {
            this.dataHelper = dataHelper;
        }
        public void OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var datem = DateTime.Now.AddMonths(-1);
            var datey = DateTime.Now.AddYears(-1);

            AllPosts = dataHelper.GetDataByUser(userid).Count();
            PostsLastMonth = dataHelper.GetDataByUser(userid).Where(x => x.AddedDate >= datem).Count();
            PostThisYear = dataHelper.GetDataByUser(userid).Where(x => x.AddedDate >= datey).Count();

        }
    }
}
