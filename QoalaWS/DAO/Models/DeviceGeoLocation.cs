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
                VERIFIED_AT, 
                LATITUDE, 
                LONGITUDE, 
                outParameter
            );

            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_DEVICE_GEO_LOCATION = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;

            DEVICE.LAST_LATITUDE = LATITUDE;
            DEVICE.LAST_LONGITUDE = LONGITUDE;
            DEVICE.UpdateLastLocation(context);

            return ID_DEVICE_GEO_LOCATION;
        }
    }
}