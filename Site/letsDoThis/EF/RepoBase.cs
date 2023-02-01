using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace letsDoThis.EF
{
    public class RepoBase
    {
        private static MyDB _db;
        private static object _lockAsync = new object();
        protected RepoBase()
        {

        }

        public static MyDB CreateContext()
        {
            if (_db == null)
            {
                lock (_lockAsync)
                {
                    if (_db == null)
                    {
                        _db = new MyDB();

                        if (_db.Database.Exists() == false)
                        {
                            _db = new MyDB();
                            _db.Users.ToList();
                        }
                    }

                }
            }
            return _db;
        }
    }
}
