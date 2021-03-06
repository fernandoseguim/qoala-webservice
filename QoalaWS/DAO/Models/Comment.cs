﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using QoalaWS.Exceptions;
using System.Data.Entity;

namespace QoalaWS.DAO
{
    public partial class Comment
    {
        const int LIMIT = 10;
        public static Comment findById(QoalaEntities context, Decimal id_comment)
        {
            return context.COMMENTS.FirstOrDefault(u => u.ID_COMMENT == id_comment && !u.DELETED_AT.HasValue);
        }

        public static List<object> findByIdPost(QoalaEntities context, Decimal id_post)
        {
            var list = context.COMMENTS.Where(
                    u => u.ID_POST == id_post && 
                    !u.DELETED_AT.HasValue && 
                    u.APPROVED_AT.HasValue
                ).ToList();
            List<object> comments = new List<object>();
            foreach (var comment in list)
            {
                comments.Add(comment.Serializer());
            }
            return comments;
        }

        public static int totalNumberPageByAuthorId(QoalaEntities context, Decimal authorId)
        {
            var query = context.COMMENTS.
                Join(context.POSTS,
                    comment => comment.ID_POST,
                    post => post.ID_POST,
                    (comment, post) => new { Comment = comment, Post = post }
                );

            User user = User.findById(context, authorId);


            if (user.PERMISSION == 3)
            {
                query = query.Where(
                    commentAndPost =>
                        !commentAndPost.Comment.DELETED_AT.HasValue &&
                        !commentAndPost.Post.DELETED_AT.HasValue
                    );
            }
            else
            {
                query = query.Where(commentAndPost => commentAndPost.Post.ID_USER == authorId &&
                        !commentAndPost.Comment.DELETED_AT.HasValue &&
                        !commentAndPost.Post.DELETED_AT.HasValue);
            }

            decimal count = query.Count();
            return (int)Math.Ceiling(count / LIMIT);
        }
        public static List<object> FindByAuthorId(QoalaEntities context, Decimal authorId, int page = 1)
        {
            User user = User.findById(context, authorId);

            var query = context.COMMENTS.
                Join(context.POSTS,
                    comment => comment.ID_POST,
                    post => post.ID_POST,
                    (comment, post) => new { Comment = comment, Post = post }
                ).
                OrderByDescending(p => p.Comment.CREATED_AT).
                Skip(page == 1 ? 0 : LIMIT * page - LIMIT).
                Take(LIMIT);

            if (user.PERMISSION == 3)
            {
                query = query.Where(
                    commentAndPost => 
                        !commentAndPost.Comment.DELETED_AT.HasValue &&
                        !commentAndPost.Post.DELETED_AT.HasValue
                    );
            } else
            {
                query = query.Where(commentAndPost => commentAndPost.Post.ID_USER == authorId &&
                        !commentAndPost.Comment.DELETED_AT.HasValue &&
                        !commentAndPost.Post.DELETED_AT.HasValue);
            }

            var list = query.ToList();
            List<object> comments = new List<object>();
            foreach (var commentAndPost in list)
            {
                comments.Add(commentAndPost.Comment.Serializer());
            }
            return comments;
        }

        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_COMMENT", typeof(decimal));
            context.SP_INSERT_COMMENT(CONTENT, ID_USER, ID_POST, outParameter);
            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_COMMENT = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;
            return ID_COMMENT;
        }

        public decimal? Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_UPDATE_COMMENT(ID_COMMENT, CONTENT, ID_USER, ID_POST, outParameter);
            return 1;
        }

        public decimal? Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_DELETE_COMMENT(ID_COMMENT, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public decimal? Approve(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_APPROVE_COMMENT(ID_COMMENT, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public static bool belongsToPost(QoalaEntities context, decimal id_comment, decimal id_post)
        {
            return context.COMMENTS.Where(
                c => c.ID_COMMENT == id_comment  && !c.DELETED_AT.HasValue
                && !c.POST.DELETED_AT.HasValue
            ).Count() > 0;
        }

        public object Serializer()
        {
            return new
            {
                id_comment = ID_COMMENT,
                content = CONTENT,
                id_post = ID_POST,
                id_user = ID_USER,
                created_at = CREATED_AT,
                approved_at = APPROVED_AT,
                user_name = GetUser().NAME
            };
        }

        public User GetUser()
        {
            using(var db = new QoalaEntities())
            {
                return db.USERS.FirstOrDefault(u => u.ID_USER == ID_USER);
            }
        }
    }
}


