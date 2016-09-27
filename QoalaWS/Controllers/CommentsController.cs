using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;
using System.Collections.Generic;

namespace QoalaWS.Controllers
{
    public class CommentsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [HttpGet]
        [Route("posts/{id_post}/comments")]
        public IHttpActionResult GetComments(decimal id_post)
        {
            List<object> comments = Comment.findByIdPost(db, id_post);

            return Ok(comments);
        }

        [HttpGet]
        [Route("posts/{id_post}/comments/{id_comment}")]
        public IHttpActionResult Get(decimal id_post, decimal id_comment)
        {
            if (!DAO.Comment.belongsToPost(db, id_comment, id_post))
                return NotFound();

            Comment comment = Comment.findById(db, id_comment);
            if (comment == null)
                return NotFound();

            return Ok(
                new {
                    content = comment.CONTENT,
                    approved_at = comment.APPROVED_AT,
                    id_post = comment.ID_POST,
                    id_user = comment.ID_USER
                }
            );
        }

        
        [HttpPut]
        [BasicAuthorization]
        [ValidateModel]
        [Route("posts/{id_post}/comments/{id_comment}")]
        public IHttpActionResult Update(decimal id_post, decimal id_comment, Comment comment)
        {
            if (!DAO.Comment.belongsToPost(db, id_comment, id_post))
                return NotFound();

            Comment c = DAO.Comment.findById(db, id_comment);

            if (c == null)
                return NotFound();

            comment.ID_COMMENT = id_comment;

            if (comment.CONTENT == null)
                comment.CONTENT = c.CONTENT;
            if (comment.ID_USER == 0)
                comment.ID_USER = c.ID_USER;
            if (comment.APPROVED_AT == null)
                comment.APPROVED_AT = c.APPROVED_AT;

            comment.Update(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpPost]
        [BasicAuthorization]
        [ValidateModel]
        [Route("posts/{id_post}/comments")]
        public IHttpActionResult Create(decimal id_post, Comment comment)
        {
            comment.ID_POST = id_post;
            comment.Add(db);

            return Created("", 
                new {
                    content = comment.CONTENT,
                    approved_at = comment.APPROVED_AT,
                    id_post = comment.ID_POST,
                    id_user = comment.ID_USER
                }
            );
        }

        [HttpDelete]
        [BasicAuthorization]
        [Route("posts/{id_post}/comments/{id_comment}")]
        public IHttpActionResult Delete(decimal id_post, decimal id_comment)
        {
            if (!DAO.Comment.belongsToPost(db, id_comment, id_post))
                return NotFound();

            Comment comment = DAO.Comment.findById(db, id_comment);
            if (comment == null)
                return NotFound();

            comment.Delete(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [BasicAuthorization]
        [Route("posts/{id_post}/comments/{id_comment}/approve")]
        public IHttpActionResult Approve(decimal id_post, decimal id_comment)
        {
            if (!DAO.Comment.belongsToPost(db, id_comment, id_post))
                return NotFound();

            Comment comment = DAO.Comment.findById(db, id_comment);
            if (comment == null)
                return NotFound();

            comment.Approve(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}