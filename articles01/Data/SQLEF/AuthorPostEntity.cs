using articles01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Data.SQLEF
{
    public class AuthorPostEntity : IDataHelper<AuthorPost>
    {
        private DBContext db;
        private AuthorPost _table;
        public AuthorPostEntity()
        {
            db = new DBContext();
        }
        public int Add(AuthorPost table)
        {
            if (db.Database.CanConnect())
            {
                db.AuthorPosts.Add(table);
                db.SaveChanges();

                return 1;
            }
            else
            {
                return 0;
            }
            
        }

        public int Delete(int id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(id);
                db.AuthorPosts.Remove(_table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Edit(int id, AuthorPost table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.AuthorPosts.Update(table);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public AuthorPost Find(int id)
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPosts.Where(x=>x.Id==id).First();
            }
            else
                return null;
        }

        public List<AuthorPost> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPosts.ToList() ;
            }
            else
                return null;
        }

        public List<AuthorPost> GetDataByUser(string userId)
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPosts.Where(x=>x.UserId==userId).ToList();
            }
            else
                return null;
        }

        public List<AuthorPost> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPosts.Where(x=>x.FullName.Contains(searchItem) 
                                         || x.Id.ToString().Contains(searchItem)
                                         || x.UserId.Contains(searchItem)
                                         || x.UserName.Contains(searchItem)
                                         || x.PostTitle.Contains(searchItem)
                                         || x.PostImageUrl.Contains(searchItem)
                                         || x.AuthorId.ToString().Contains(searchItem)
                                         || x.CategoryId.ToString().Contains(searchItem)
                                         || x.AddedDate.ToString().Contains(searchItem)
                                         || x.PostDescription.Contains(searchItem)
                                         ).ToList();
            }
            else
                return null;
        }
    }
}
