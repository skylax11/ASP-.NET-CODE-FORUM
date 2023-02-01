using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace letsDoThis.EF
{
    public class RepoCommand<T> where T : class
    {
        
        private MyDB db;
        private DbSet<T> GottenTable;

        public RepoCommand()
        {
            db = RepoBase.CreateContext();
            GottenTable = db.Set<T>();
        }
        public int Insert(T obj)
        {
            GottenTable.Add(obj);
            return Save();
        }
        public int Delete(T obj)
        {
            GottenTable.Remove(obj);
            return Save();
        }
        public void Update(T obj)
        {
            // Later
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return GottenTable.FirstOrDefault(where);
        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public List<T> List()
        {
            return GottenTable.ToList();

        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return GottenTable.Where(where).ToList();

        }
    }
}