using System.Data.Entity.Core.Objects;
using QoalaWS.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace QoalaWS.DAO
{
    public partial class Post
    {
        const int LIMIT = 10;
        public static List<object> All(QoalaEntities context, int page_number)
        {
            var list = context.POSTS.Where(p => !p.DELETED_AT.HasValue && p.PUBLISHED_AT.HasValue).
                OrderByDescending(p => p.PUBLISHED_AT).
                Skip(page_number == 1 ? 0 : LIMIT * page_number - 1).
                Take(LIMIT).
                ToList();
            List<object> posts = new List<object>();
            foreach(var post in list)
            {
                posts.Add(post.Serializer());
            }
            return posts;
        }

        public static Post findById(QoalaEntities context, Decimal id_post)
        {
            return context.POSTS.FirstOrDefault(u => u.ID_POST == id_post && !u.DELETED_AT.HasValue);
        }

        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_POST", typeof(decimal));
            int ret = context.SP_INSERT_POST(TITLE, CONTENT, ID_USER, outParameter);
            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_POST = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;
            return ID_POST;
        }

        public decimal? Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_UPDATE_POST(ID_POST, TITLE, CONTENT, ID_USER, outParameter);
            return 1;
        }

        public decimal? Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_DELETE_POST(ID_POST, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public decimal? Publish(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_PUBLISH_POST(ID_POST, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public object Serializer()
        {
            return new
            {
                id_post = ID_POST,
                title = TITLE,
                content = CONTENT,
                published_at = PUBLISHED_AT,
                id_user = ID_USER,
            };
        }

        public static int totalNumberPage(QoalaEntities context)
        {
            decimal count = context.POSTS.
                Where(p => !p.DELETED_AT.HasValue && p.PUBLISHED_AT.HasValue).
                Count();
            return (int)Math.Ceiling(count / LIMIT);
        }
    }
}