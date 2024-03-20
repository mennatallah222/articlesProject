using articles01.Data;
using articles01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace articles01.Controllers
{
    [Authorize]
    public class PostController : Controller
    {

        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly IDataHelper<Author> dataHelperForAuthor;
        private readonly IDataHelper<Category> dataHelperForCategory;
        private readonly Code.FileHelper fileHelper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorizationService authorizationService;
        private int pageItems;
        private Task<AuthorizationResult> result;
        private string UserId;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public PostController(IDataHelper<AuthorPost> dataHelper,
            IDataHelper<Author> dataHelperForAuthor,
            IDataHelper<Category> dataHelperForCategory,
            IWebHostEnvironment webHostEnvironment,
            IAuthorizationService authorizationService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForCategory = dataHelperForCategory;
            this.webHostEnvironment = webHostEnvironment;
            this.authorizationService = authorizationService;
            fileHelper = new Code.FileHelper(this.webHostEnvironment);
            pageItems = 10;
            
            this.signInManager = signInManager;
            this.userManager = userManager;

        }




        // GET: PostController
        public ActionResult Index(int? id)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                // Admin
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetAllData().Take(pageItems));
                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItems);
                    return View(data);
                }
            }
            else
            {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(pageItems));
                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(pageItems);
                    return View(data);
                }
            }

        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = fileHelper.UploadFile(collection.PostImageUrl, "Images")
                };
                dataHelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorPost = dataHelper.Find(id);
            ModelView.AuthorPostView authorPostView = new ModelView.AuthorPostView
            {
                AddedDate = authorPost.AddedDate,
                Author = authorPost.Author,
                AuthorId = authorPost.AuthorId,
                Category = authorPost.Category,
                CategoryId = authorPost.CategoryId,
                FullName = authorPost.FullName,
                PostCategory = authorPost.PostCategory,
                PostDescription = authorPost.PostDescription,
                PostTitle = authorPost.PostTitle,
                UserId = authorPost.UserId,
                UserName = authorPost.UserName,
                Id = authorPost.Id
            };
            return View(authorPostView);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ModelView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = fileHelper.UploadFile(collection.PostImageUrl, "Images"),
                    Id=collection.Id
                };
                dataHelper.Edit(id, Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
