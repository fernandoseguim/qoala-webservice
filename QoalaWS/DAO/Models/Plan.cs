﻿using System;
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
                    Where(i => i.LEFT > 0).
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

        public static List<object> Report(string name = "",
            int id_plan = 0, int id_plan2 = 0,
            int plan_sold = 0, int plan_sold2 = 0,
            int plan_left = 0, int plan_left2 = 0)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                List<object> items = new List<object>();

                var list = qe.PLANS.AsParallel().AsQueryable();

                if (name != null && name.Length > 0)
                    foreach (var nome in name.ToUpper().Trim().Split(new string[] { ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (nome.Length > 2)
                            list = list.Where(i => i.NAME.ToUpper().Contains(nome));
                    }


                if (id_plan > 0 && id_plan2 == 0)
                    list = list.Where(i => i.ID_PLAN == id_plan);
                else if (id_plan > 0 && id_plan2 > 0)
                    list = list.Where(i => i.ID_PLAN >= id_plan && i.ID_PLAN <= id_plan2);
                else if (id_plan == 0 && id_plan2 > 0)
                    list = list.Where(i => i.ID_PLAN <= id_plan2);

                if (plan_left > 0 && plan_left2 == 0)
                    list = list.Where(i => i.LEFT == plan_left);
                else if (plan_left > 0 && plan_left2 > 0)
                    list = list.Where(i => i.LEFT >= plan_left && i.LEFT <= plan_left2);
                else if (plan_left == 0 && plan_left2 > 0)
                    list = list.Where(i => i.LEFT <= plan_left2);

                list = list.OrderByDescending(p => p.CREATED_AT);
                
                foreach (var item in list)
                {
                    int sold = qe.USERS.Count(u => u.ID_PLAN == item.ID_PLAN);

                    if (plan_sold > 0 && plan_sold2 == 0)
                    {
                        if (sold != plan_sold) continue;
                    }
                    else if (plan_sold > 0 && plan_sold2 > 0)
                    {
                        if (sold < plan_sold || sold > plan_sold2) continue;
                    }
                    else if (plan_sold == 0 && plan_sold2 > 0)
                    {
                        if (sold > plan_sold2) continue;
                    }

                    items.Add(
                        new
                        {
                            id_plan = item.ID_PLAN,
                            name_plan = item.NAME,
                            plan_left = item.LEFT,
                            plan_solds = sold,
                            price_cents = item.PRICE_CENTS,
                        }
                    );
                }

                return items;
            }
        }


        public static Plan Find(Decimal id_plan)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                return qe.PLANS.FirstOrDefault(u => u.ID_PLAN == id_plan);
            }
        }

        public void Add()
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                this.CREATED_AT = DateTime.Now;
                this.TOTAL = LEFT;
                qe.PLANS.Add(this);
                qe.SaveChanges();
            }
        }

        public void Delete()
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                qe.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                qe.SaveChanges();
            }
        }
        public void Update()
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                qe.Entry(this).State = System.Data.Entity.EntityState.Modified;
                qe.SaveChanges();
            }
        }

        public object Serializer()
        {
            QoalaEntities qe = new QoalaEntities();
            return new
            {
                id_plan = ID_PLAN,
                name = NAME,
                left = LEFT,
                rewards = REWARDS,
                price_cents = PRICE_CENTS,
                sold = TOTAL
            };
        }
    }
}