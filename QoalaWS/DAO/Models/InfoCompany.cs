using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using QoalaWS.Exceptions;
using System.Data.Entity;
using System.Collections.Generic;

namespace QoalaWS.DAO
{
    public partial class INFOCOMPANY
    {
        const int LIMIT = 10;
        public static List<object> All(QoalaEntities context, int page_number)
        {
            var list = context.INFOCOMPANies.
                OrderBy(i=>i.KEY).
                Skip(page_number == 1 ? 0 : LIMIT * page_number - LIMIT).
                Take(LIMIT).
                ToList();
            List<object> infos = new List<object>();
            foreach (var post in list)
            {
                infos.Add(new
                {
                    key = post.KEY,
                    value = post.VALUE
                });
            }
            return infos;
        }

        public static int totalNumberPage(QoalaEntities context)
        {
            decimal count = context.INFOCOMPANies.Count();
            return (int)Math.Ceiling(count / LIMIT);
        }

        public static INFOCOMPANY findByKey(QoalaEntities db, string key)
        {
            return db.INFOCOMPANies.FirstOrDefault(i => i.KEY.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(QoalaEntities db)
        {
            db.INFOCOMPANies.Add(this);
            db.SaveChanges();
        }

        public void Delete(QoalaEntities db)
        {
            db.INFOCOMPANies.Remove(this);
            db.SaveChanges();
        }
        public void Update(QoalaEntities db)
        {
            var info = findByKey(db, this.KEY);
            info.VALUE = this.VALUE;
            db.SaveChanges();
        }
    }
}