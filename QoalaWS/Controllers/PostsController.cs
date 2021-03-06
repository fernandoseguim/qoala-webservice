﻿using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;
using System.Collections.Generic;

namespace QoalaWS.Controllers
{
    public class PostsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [Route("posts/{id}")]
        [HttpGet]
        public IHttpActionResult Get(decimal id)
        {
            Post post = Post.findById(db, id);
            if (post == null)
            {
                return NotFound();
            }
            
            return Ok(post.Serializer());
        }

        [Route("users/{idUser}/posts")]
        [HttpGet]
        public IHttpActionResult GetUserPosts(int idUser, int page = 1)
        {
            List<object> posts = Post.FindByIdUser(db, idUser, page);
            var totalNumberPage = Post.TotalNumberPageFromIdUser(db, idUser);
            return Ok(
                new
                {
                    posts = posts,
                    pagination = new
                    {
                        total_number_pages = totalNumberPage,
                        next_page = totalNumberPage > page,
                        current_page = page,
                        previous_page = page > 1 && page <= totalNumberPage
                    }
                }
            );
        }

        [Route("posts")]
        [HttpGet]
        public IHttpActionResult GetPosts(int page = 1)
        {
            List<object> posts = Post.All(db, page);
            
            var totalNumberPage = Post.totalNumberPage(db);
            return Ok(
                new
                {   
                    posts = posts,
                    pagination = new
                    {
                        total_number_pages = totalNumberPage,
                        next_page = totalNumberPage > page,
                        current_page = page,
                        previous_page = page > 1 && page <= totalNumberPage
                    }
                }
            );
        }

        [Route("posts/{id}")]
        [HttpPut]
        [BasicAuthorization(Permission = Permission.Editor)]
        [ValidateModel]
        public IHttpActionResult Update(decimal id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post p = Post.findById(db, id);

            if (p == null)
                return NotFound();

            post.ID_POST = id;

            if (post.TITLE == null)
                post.TITLE = p.TITLE;
            if (post.CONTENT == null)
                post.CONTENT = p.CONTENT;
            if (post.ID_USER == 0)
                post.ID_USER = p.ID_USER;

            post.Update(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [BasicAuthorization(Permission = Permission.Editor)]
        [ValidateModel]
        [Route("posts/")]
        public IHttpActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.Add(db);

            return Created("", post.Serializer());
        }

        [HttpDelete]
        [BasicAuthorization(Permission = Permission.Editor)]
        [Route("posts/{id}")]
        public IHttpActionResult Delete(decimal id)
        {
            Post p = Post.findById(db, id);
            if (p == null)
                return NotFound();

            p.Delete(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [BasicAuthorization(Permission = Permission.Editor)]
        [Route("posts/{id}/publish")]
        public IHttpActionResult Publish(decimal id)
        {
            Post p = Post.findById(db, id);
            if (p == null)
                return NotFound();

            p.Publish(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}