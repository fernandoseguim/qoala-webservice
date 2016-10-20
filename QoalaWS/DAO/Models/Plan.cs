using System;
using System.Linq;
using System.Collections.Generic;

namespace QoalaWS.DAO
{
    public partial class Plan
    {
        public static List<object> All()
        {
            using (QoalaEntities qe = new QoalaEntities())
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

        public static List<object> Report(int id_plan)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                List<object> items = new List<object>();

                if (id_plan > 0)
                {
                    var list = qe.PLANS.
                            Join(qe.USERS,
                            plan => plan.ID_PLAN,
                            user => user.ID_USER,
                            (plan, user) => new { Plan = plan, User = user }
                    ).Where(i => i.Plan.ID_PLAN == id_plan).OrderByDescending(p => p.Plan.CREATED_AT).ToList();

                    foreach (var item in list)
                    {
                        items.Add(
                            new
                            {
                                id_plan = item.Plan.ID_PLAN,
                                id_user = item.User.ID_USER,
                                user_name = item.User.NAME,
                                plan_qnt_left = item.Plan.LEFT
                            }
                        );
                    }
                } else
                {
                    var list = qe.PLANS.
                            Join(qe.USERS,
                            plan => plan.ID_PLAN,
                            user => user.ID_USER,
                            (plan, user) => new { Plan = plan, User = user }
                    ).OrderByDescending(p => p.Plan.CREATED_AT).ToList();

                    foreach (var item in list)
                    {
                        items.Add(
                            new
                            {
                                id_plan = item.Plan.ID_PLAN,
                                id_user = item.User.ID_USER,
                                user_name = item.User.NAME,
                                left_qnt_plan = item.Plan.LEFT
                            }
                        );
                    }
                }
                
                return items;
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
                this.CREATED_AT = DateTime.Now;
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
                qe.SaveChanges();
            }
        }

        public object Serializer()
        {
            return new
            {
                id_plan = ID_PLAN,
                name = NAME,
                left = LEFT,
                rewards = REWARDS,
                price_cents = PRICE_CENTS
            };
        }
    }
}