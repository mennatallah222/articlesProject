using articles01.Data;
using articles01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Category> dataHelperCategory;
        private readonly IDataHelper<AuthorPost> dataHelperAuthorPost;
        private readonly int ItemsNum;

        public IndexModel(ILogger<IndexModel> logger, IDataHelper<Category> dataHelperCategory,
            IDataHelper<AuthorPost> dataHelperAuthorPost)
        {
            _logger = logger;
            this.dataHelperCategory = dataHelperCategory;
            this.dataHelperAuthorPost = dataHelperAuthorPost;
            ItemsNum = 6;

            ListForCategory = new List<Category>();
            ListForPost = new List<AuthorPost>();
        }


        public List<Category> ListForCategory { get; set; }
        public List<AuthorPost> ListForPost { get; set; }


        public void OnGet(string LoadState, string categoryName)
        {
            GetAllCategory();

            if (LoadState == null || LoadState == "All")
            {
                GetAllPosts();

            }
            else if (LoadState == "ByCategory")
            {
                GetDataByCategoryName(categoryName);
            }
        }

        //get for category
        private void GetAllCategory()
        {
            ListForCategory = dataHelperCategory.GetAllData();

        }

        private void GetAllPosts()
        {
            ListForPost = dataHelperAuthorPost.GetAllData().Take(ItemsNum).ToList();

        }

        private void GetDataByCategoryName(string categoryName)
        {
            ListForPost = dataHelperAuthorPost.GetAllData().Where(x=>x.PostCategory==categoryName).Take(ItemsNum).ToList();

        }
    }
}
