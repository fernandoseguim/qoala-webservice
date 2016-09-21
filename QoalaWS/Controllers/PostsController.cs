using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;

namespace QoalaWS.Controllers
{
    public class PostsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [Route("posts/{id}")]
        [HttpGet]
        public IHttpActionResult Get(decimal id)
        {
            Post post = DAO.Post.findById(db, id);
            if (post == null)
            {
                return NotFound();
            }
            
            return Ok(
                new
                {
                    title = post.TITLE,
                    content = post.CONTENT,
                    published_at = post.PUBLISHED_AT
                }
            );
        }


        [Route("posts/{id}")]
        [HttpPut]
        [BasicAuthorization]
        public IHttpActionResult Put(decimal id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post p = DAO.Post.findById(db, id);

            if (p == null)
                return NotFound();

            post.ID_POST = id;

            if (post.TITLE == null)
                post.TITLE = p.TITLE;
            if (post.CONTENT == null)
                post.CONTENT = p.CONTENT;
            if (post.ID_USER.GetType() != null)
                post.ID_USER = p.ID_USER;

            post.Update(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [BasicAuthorization]
        [Route("posts/")]
        public IHttpActionResult Post(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.Add(db);

            return Created("", new {
                title = post.TITLE,
                content = post.CONTENT,
                id_user = post.ID_USER,
                created_at = post.CREATED_AT
            });
        }

        [HttpDelete]
        [BasicAuthorization]
        [Route("posts/{id}")]
        public IHttpActionResult Delete(decimal id)
        {
            Post p = DAO.Post.findById(db, id);
            if (p == null)
                return NotFound();

            p.Delete(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [BasicAuthorization]
        [Route("posts/{id}/publish")]
        public IHttpActionResult Publish(decimal id)
        {
            Post p = DAO.Post.findById(db, id);
            if (p == null)
                return NotFound();

            p.Publish(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}