using System;
using System.Data.Entity.Core.Objects;
using QoalaWS.Exceptions;
using System.Data.Entity;

namespace QoalaWS.DAO
{
    public partial class DeviceGeoLocation
    {
        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_GEO", typeof(decimal));
            context.SP_INSERT_DEVICE_GEO_LOCATION(
                ID_DEVICE, 
                LATITUDE, 
                LONGITUDE, 
                outParameter
            );

            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_DEVICE_GEO_LOCATION = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;

            Device device = DAO.Device.findById(context, ID_DEVICE);
            device.LAST_LATITUDE = LATITUDE;
            device.LAST_LONGITUDE = LONGITUDE;
            device.UpdateLastLocation(context);

            return ID_DEVICE_GEO_LOCATION;
        }
    }
}