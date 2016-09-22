using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;
using System.Net;

namespace QoalaWS.Controllers
{
    public class DeviceGeoLocationsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();
        
        [ValidateModel]
        [BasicAuthorization]
        [Route("users/{id_user}/devices/{id_device}/geo_location")]
        public IHttpActionResult Create(decimal id_user, decimal id_device, DeviceGeoLocation deviceGeoLocation)
        {
            if (!DAO.Device.belongsToUser(db, id_device, id_user))
                return StatusCode(HttpStatusCode.Unauthorized);

            deviceGeoLocation.ID_DEVICE = id_device;
            deviceGeoLocation.Add(db);
            return Created(
                "",
                new
                {
                    latitude = deviceGeoLocation.LATITUDE,
                    longitude = deviceGeoLocation.LONGITUDE
                }
            );
        }
    }
}