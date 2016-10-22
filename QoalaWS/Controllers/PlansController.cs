using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;

namespace QoalaWS.Controllers
{
    public class PlansController : ApiController
    {
        [HttpGet]
        [Route("plans/")]
        public IHttpActionResult All()
        {
            return Ok(new
            {
                plans = Plan.All()
            });
        }

        [HttpGet]
        [Route("plans/reports")]
        public IHttpActionResult Reports(string name = "", 
            int id_plan=0, int id_plan2=0,
            int plan_left = 0, int plan_left2 = 0,
            int plan_sold = 0, int plan_sold2 = 0
            )
        {
            return Ok(new
            {
                report = Plan.Report(name, id_plan, id_plan2, plan_left, plan_left2, plan_sold, plan_sold2)
            });
        }

        [HttpGet]
        [Route("plans/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Show(decimal id)
        {
            Plan plan = Plan.Find(id);
            if (plan == null)
                return NotFound();

            return Ok(plan.Serializer());
        }

        [HttpPut]
        [Route("plans/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Update(decimal id, Plan plan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Plan p = Plan.Find(id);

            if (p == null)
                return NotFound();

            plan.ID_PLAN = id;

            if (plan.NAME == null)
                plan.NAME = p.NAME;
            if (plan.PRICE_CENTS == 0)
                plan.PRICE_CENTS = p.PRICE_CENTS;
            if (plan.REWARDS == null)
                plan.REWARDS = p.REWARDS;

            plan.Update();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("plans")]
        [BasicAuthorization]
        public IHttpActionResult Create(Plan plan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            plan.Add();

            return Created(
                "",
                plan.Serializer()
            );
        }

        [HttpDelete]
        [Route("plans/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Delete(decimal id)
        {
            Plan plan = Plan.Find(id);
            if (plan == null)
                return NotFound();

            plan.Delete();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}