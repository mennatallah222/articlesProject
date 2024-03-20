using articles01.Data;
using articles01.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IDataHelper<Author> dataHelper;
        private readonly Code.FileHelper fileHelper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private int pageItems;

        public AuthorController(IDataHelper<Author> dataHelper, IWebHostEnvironment webHostEnvironment)
        {
            this.dataHelper = dataHelper;
            this.webHostEnvironment = webHostEnvironment;
            fileHelper = new Code.FileHelper(this.webHostEnvironment);
            pageItems = 5;
        }
        // GET: AuthorController
        public ActionResult Index(int? id)
        {
            if (id == 0 ||id==null)
            {
                return View(dataHelper.GetAllData().Take(pageItems));
            }
            else
            {
                var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItems);
                return View(data);
            }
        }

        public ActionResult Search(string searchItem)
        {
            if (searchItem == null)
                return View(dataHelper.GetAllData());
            else
            {
                return View("Index", dataHelper.Search(searchItem));
            }

        }


        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = dataHelper.Find(id);
            ModelView.AuthorView authorView = new ModelView.AuthorView
            {
                Id = author.Id,
                Bio = author.Bio,
                Facebook = author.Facebook,
                Instagram = author.Instagram,
                FullName = author.FullName,
                Twitter = author.Twitter,
                UserId = author.UserId,
                UserName = author.UserName,
                
            };
            return View(authorView);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ModelView.AuthorView collection)
        {
            try
            {
                var author = new Author
                {
                    Id = collection.Id,
                    Bio = collection.Bio,
                    Facebook = collection.Facebook,
                    Instagram = collection.Instagram,
                    FullName = collection.FullName,
                    Twitter = collection.Twitter,
                    UserId = collection.UserId,
                    UserName = collection.UserName,
                    ProfilePicURL = fileHelper.UploadFile(collection.ProfilePicURL, "Images")
                };

                dataHelper.Edit(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = dataHelper.Find(id);
            ModelView.AuthorView authorView = new ModelView.AuthorView
            {
                Id = author.Id,
                Bio = author.Bio,
                Facebook = author.Facebook,
                Instagram = author.Instagram,
                FullName = author.FullName,
                Twitter = author.Twitter,
                UserId = author.UserId,
                UserName = author.UserName,

            };
            return View(authorView);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.ProfilePicURL;
                if (System.IO.File.Exists(filePath))
                {
                     System.IO.File.Delete(filePath);
                }
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
