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
    public class DevicesController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();


        [HttpGet]
        [BasicAuthorization]
        [Route("users/{user_id}/devices/{device_id}")]
        public IHttpActionResult Get(decimal user_id, decimal device_id)
        {
            Device device = DAO.Device.findById(db, device_id);

            if (device == null)
                return NotFound();

            return Ok(
                new
                {
                    alarm = device.ALARM,
                    alias = device.ALIAS,
                    color = device.COLOR,
                    frequency_update = device.FREQUENCY_UPDATE,
                    id_user = device.ID_USER
                }
            );
        }

        [HttpPost]
        [BasicAuthorization]
        [Route("users/{user_id}/devices")]
        public IHttpActionResult Create(decimal user_id, Device device)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            device.ID_USER = user_id;

            device.Add(db);

            return Created(
                "",
                new
                {
                    alarm = device.ALARM,
                    alias = device.ALIAS,
                    color = device.COLOR,
                    frequency_update = device.FREQUENCY_UPDATE,
                    id_user = device.ID_USER
                }
            );
        }

        [HttpPut]
        [BasicAuthorization]
        [ValidateModel]
        [Route("users/{user_id}/devices/{device_id}")]
        public IHttpActionResult Update(decimal user_id, decimal device_id, Device device)
        {
            Device d = DAO.Device.findById(db, device_id);

            if (d == null)
                return NotFound();

            device.ID_DEVICE = device_id;

            if (device.ALIAS == null)
                device.ALIAS = d.ALIAS;
            if (device.COLOR == null)
                device.COLOR = d.COLOR;
            if (device.FREQUENCY_UPDATE == 0)
                device.FREQUENCY_UPDATE = d.FREQUENCY_UPDATE;
            if (device.ID_USER == 0)
                device.ID_USER = d.ID_USER;
            
            device.Update(db);
            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpDelete]
        [BasicAuthorization]
        [Route("users/{user_id}/devices/{device_id}")]
        public IHttpActionResult Delete(decimal user_id, decimal device_id)
        {
            Device device = DAO.Device.findById(db, device_id);
            if (device == null)
                return NotFound();

            device.Delete(db);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPut]
        [BasicAuthorization]
        [ValidateModel]
        [Route("users/{user_id}/devices/{device_id}/turn_alarm")]
        public IHttpActionResult TurnAlarm(decimal user_id, decimal device_id, Device device)
        {
            Device d = DAO.Device.findById(db, device_id);

            if (d == null)
                return NotFound();

            device.ID_DEVICE = device_id;

            if (device.ALARM == null)
                device.ALARM = d.ALARM;

            device.TurnAlarm(db);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}