using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QoalaWS.DAO
{
    public partial class Reward
    {
        public static List<object> All()
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                var list = qe.REWARDS.Where(d => !d.DELETED_AT.HasValue).
                OrderByDescending(p => p.CREATED_AT).
                ToList();
                List<object> rewards = new List<object>();
                foreach (var reward in list)
                {
                    rewards.Add(reward.Serializer());
                }
                return rewards;
            }
        }


        public static Reward Find(Decimal id_reward)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                return qe.REWARDS.FirstOrDefault(u => u.ID_REWARD == id_reward && !u.DELETED_AT.HasValue);
            }
        }

        public void Add()
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                qe.REWARDS.Add(this);
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
                var reward = Find(ID_REWARD);
                reward.NAME = this.NAME;

                qe.Entry(reward).State = System.Data.Entity.EntityState.Modified;

                qe.SaveChanges();
            }
        }

        public object Serializer()
        {
            return new
            {
                id = ID_REWARD,
                name = NAME
            };
        }
    }
}