using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using QoalaWS.Exceptions;
using System.Data.Entity;
using System.Collections.Generic;

namespace QoalaWS.DAO
{
    public partial class Device
    {
        const int LIMIT = 10;
        public static List<object> All(QoalaEntities context, int page_number)
        {
            var list = context.DEVICES.Where(d => !d.DELETED_AT.HasValue).
                OrderByDescending(p => p.CREATED_AT).
                Skip(page_number == 1 ? 0 : LIMIT * page_number - LIMIT).
                Take(LIMIT).
                ToList();
            List<object> devices = new List<object>();
            foreach (var device in list)
            {
                devices.Add(device.Serializer());
            }
            return devices;
        }

        public static List<object> AllByUser(QoalaEntities context, int page_number, decimal idUser)
        {
            var list = context.DEVICES.Where(d => d.ID_USER == idUser && !d.DELETED_AT.HasValue).
                OrderByDescending(p => p.CREATED_AT).
                Skip(page_number == 1 ? 0 : LIMIT * page_number - LIMIT).
                Take(LIMIT).
                ToList();
            List<object> devices = new List<object>();
            foreach (var device in list)
            {
                devices.Add(device.Serializer());
            }
            return devices;
        }

        public static Device findById(QoalaEntities context, Decimal id_device)
        {
            return context.DEVICES.FirstOrDefault(u => u.ID_DEVICE == id_device && !u.DELETED_AT.HasValue);
        }

        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_DEVICE", typeof(decimal));
            context.SP_INSERT_DEVICE(ALIAS, COLOR, FREQUENCY_UPDATE, ID_USER, outParameter);
            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_DEVICE = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;
            return ID_DEVICE;
        }

        public decimal? Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_UPDATE_DEVICE(
                ID_DEVICE, 
                ALIAS, 
                COLOR, 
                FREQUENCY_UPDATE, 
                ID_USER, 
                outParameter
            );
            return 1;
        }

        public decimal? Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_DELETE_DEVICE(ID_DEVICE, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public decimal? TurnAlarm(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_TURN_ALARM(ID_DEVICE, ((bool)ALARM ? 1 : 0), outParameter);
            return 1;
        }

        public decimal? UpdateLastLocation(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_UPDATE_LAST_LOCATION(ID_DEVICE, LAST_LONGITUDE, LAST_LATITUDE, outParameter);
            return 1;
        }

        public static bool belongsToUser(QoalaEntities context, decimal id_device, decimal id_user)
        {
            return context.DEVICES.Where(
                d => d.ID_USER == id_user && !d.DELETED_AT.HasValue &&
                !d.USER.DELETED_AT.HasValue
            ).Count() > 0;
        }

        public static int totalNumberPage(QoalaEntities context)
        {
            decimal count = context.DEVICES.
                Where(p => !p.DELETED_AT.HasValue).
                Count();
            return (int)Math.Ceiling(count / LIMIT);
        }

        public static int totalNumberPageByUser(QoalaEntities context, decimal idUser)
        {
            decimal count = context.DEVICES.
                Where(p => !p.DELETED_AT.HasValue && p.ID_USER == idUser).
                Count();
            return (int)Math.Ceiling(count / LIMIT);
        }

        public object Serializer()
        {
            return new
            {
                id_device = ID_DEVICE,
                alarm = ALARM,
                alias = ALIAS,
                color = COLOR,
                frequency_update = FREQUENCY_UPDATE,
                id_user = ID_USER
            };
        }
        
        public User GetUser()
        {
            using(QoalaEntities db = new QoalaEntities())
            {
                return db.USERS.FirstOrDefault(u => u.ID_USER == ID_USER);
            }
        }
    }
}