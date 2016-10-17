using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QoalaWS.DAO;
using QoalaWS.Filters;

namespace QoalaWS.Controllers
{
    public class RewardsController : ApiController
    {
        [HttpGet]
        [Route("rewards/")]
        [BasicAuthorization]
        public IHttpActionResult All()
        {
            return Ok(new
            {
                rewards = Reward.All()
            });
        }

        [HttpGet]
        [Route("rewards/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Show(decimal id)
        {
            Reward reward = Reward.Find(id);
            if (reward == null)
                return NotFound();

            return Ok(reward.Serializer());
        }

        [HttpPut]
        [Route("rewards/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Update(decimal id, Reward reward)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Reward r = Reward.Find(id);

            if (r == null)
                return NotFound();

            reward.ID_REWARD = id;

            if (reward.NAME == null)
                reward.NAME = r.NAME;
            
            reward.Update();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("rewards")]
        [BasicAuthorization]
        public IHttpActionResult Create(Reward reward)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            reward.Add();

            return Created(
                "",
                reward.Serializer()
            );
        }

        [HttpDelete]
        [Route("rewards/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Delete(decimal id)
        {
            Reward reward = Reward.Find(id);
            if (reward== null)
                return NotFound();

            reward.Delete();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}