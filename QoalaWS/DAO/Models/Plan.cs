using System;
using System.Linq;
using System.Collections.Generic;

namespace QoalaWS.DAO
{
    public partial class Plan
    {
         public static List<object> All()
        {
            using(QoalaEntities qe = new QoalaEntities())
            {
                var list = qe.PLANS.Where(d => !d.DELETED_AT.HasValue).
                OrderByDescending(p => p.CREATED_AT).
                ToList();
                List<object> plans = new List<object>();
                foreach (var plan in list)
                {
                    plans.Add(plan.Serializer());
                }
                return plans;
            }
        }

        
        public static Plan Find(Decimal id_plan)
        {
            using(QoalaEntities qe = new QoalaEntities())
            {
                return qe.PLANS.FirstOrDefault(u => u.ID_PLAN == id_plan && !u.DELETED_AT.HasValue);
            }
        }

        public void Add()
        {
            using(QoalaEntities qe = new QoalaEntities())
            {
                qe.PLANS.Add(this);
                qe.SaveChanges();
            }
        }

        public void Delete()
        {
            using(QoalaEntities qe = new QoalaEntities())
            {
                qe.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                qe.SaveChanges();
            }
        }
        public void Update()
        {
            using(QoalaEntities qe = new QoalaEntities())
            {
                var plan = Find(ID_PLAN);
                plan.NAME = this.NAME;
                plan.LEFT = this.LEFT;
                plan.PRICE_CENTS = this.PRICE_CENTS;

                qe.Entry(plan).State = System.Data.Entity.EntityState.Modified;

                qe.SaveChanges();
            }
        }

        public object Serializer()
        {
            return new
            {
                id = ID_PLAN,
                name = NAME,
                left = LEFT,
                prince_cents = PRICE_CENTS
            };
        }
    }
}