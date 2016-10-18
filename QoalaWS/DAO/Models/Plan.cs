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
                var list = qe.PLANS.
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
                return qe.PLANS.FirstOrDefault(u => u.ID_PLAN == id_plan);
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
                qe.Entry(this).State = System.Data.Entity.EntityState.Modified;
                qe.Entry(this.REWARDS).State = System.Data.Entity.EntityState.Modified;

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