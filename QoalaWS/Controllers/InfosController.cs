using QoalaWS.DAO;
using QoalaWS.Filters;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace QoalaWS.Controllers
{
    public class InfosController : ApiController
    {
        [Route("infos/{key}")]
        [HttpGet]
        public IHttpActionResult GetInfoCompany(string key)
        {
            using (var db = new QoalaEntities())
            {

                var info = INFOCOMPANY.findByKey(db, key);

                if (info == null)
                    return NotFound();

                return Ok(new
                {
                    key = info.KEY,
                    value = info.VALUE,
                });
            }
        }

        [Route("infos")]
        [HttpGet]
        [BasicAuthorization(Permission = Permission.Admin)]
        public IHttpActionResult GetInfos(int page = 1)
        {
            using (var db = new QoalaEntities())
            {
                List<object> infos = INFOCOMPANY.All(db, page);

                var totalNumberPage = INFOCOMPANY.totalNumberPage(db);
                return Ok(
                    new
                    {
                        infos = infos,
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
        }

        [Route("infos")]
        [HttpPost]
        [BasicAuthorization(Permission = Permission.Admin)]
        public IHttpActionResult Create(INFOCOMPANY info)
        {
            using (var db = new QoalaEntities())
            {
                info.Add(db);
            }
            return Created("", info);
        }


        [HttpDelete]
        [BasicAuthorization(Permission = Permission.Admin)]
        [Route("infos/{key}")]
        public IHttpActionResult Delete(string key)
        {
            using (var db = new QoalaEntities())
            {
                INFOCOMPANY p = INFOCOMPANY.findByKey(db, key);
                if (p == null)
                    return NotFound();

                p.Delete(db);

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [HttpPut]
        [Route("infos/{key}")]
        [BasicAuthorization(Permission = Permission.Admin)]
        public IHttpActionResult Update(string key, INFOCOMPANY info)
        {
            using (var db = new QoalaEntities())
            {
                
                info.Update(db);

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

    }
}
