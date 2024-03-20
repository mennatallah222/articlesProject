using articles01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Data.SQLEF
{
    public class CategoryEntity : IDataHelper<Category>
    {
        private DBContext db;
        private Category _table;
        public CategoryEntity()
        {
            db = new DBContext();
        }
        public int Add(Category table)
        {
            if (db.Database.CanConnect())
            {
                db.Category.Add(table);
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
                db.Category.Remove(_table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Edit(int id, Category table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.Category.Update(table);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Category Find(int id)
        {
            if (db.Database.CanConnect())
            {
                return db.Category.Where(x=>x.Id==id).First();
            }
            else
                return null;
        }

        public List<Category> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.Category.ToList() ;
            }
            else
                return null;
        }

        public List<Category> GetDataByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Category> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {
                return db.Category.Where(x=>x.Name.Contains(searchItem) || x.Id.ToString().Contains(searchItem)).ToList();
            }
            else
                return null;
        }
    }
}
