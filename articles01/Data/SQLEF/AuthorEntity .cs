using articles01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Data.SQLEF
{
    public class AuthorEntity : IDataHelper<Author>
    {
        private DBContext db;
        private Author _table;
        public AuthorEntity()
        {
            db = new DBContext();
        }
        public int Add(Author table)
        {
            if (db.Database.CanConnect())
            {
                db.Author.Add(table);
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
                db.Author.Remove(_table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Edit(int id, Author table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.Author.Update(table);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Author Find(int id)
        {
            if (db.Database.CanConnect())
            {
                return db.Author.Where(x=>x.Id==id).First();
            }
            else
                return null;
        }

        public List<Author> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.Author.ToList() ;
            }
            else
                return null;
        }

        public List<Author> GetDataByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Author> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {
                searchItem = searchItem.ToLower();
                var query = db.Author.AsQueryable();

                // Construct dynamic OR conditions for search
                query = query.Where(x =>
                    x.FullName.ToLower().Contains(searchItem) ||
                    x.Bio.ToLower().Contains(searchItem) ||
                    x.UserName.ToLower().Contains(searchItem) ||
                    x.Facebook.ToLower().Contains(searchItem) ||
                    x.Twitter.ToLower().Contains(searchItem) ||
                    x.Instagram.ToLower().Contains(searchItem) ||
                    x.Id.ToString().Contains(searchItem)
                );

                return query.ToList();
            }
            else
            {
                return null;
            }
        }

    }
}
