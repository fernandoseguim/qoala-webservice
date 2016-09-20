using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace QoalaWS.DAO
{
    public partial class Post
    {
        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("oUT_ID_POST", typeof(decimal));
            
            //if (outParameter.Value == DBNull.Value)
            //    throw new CreateRecordException();

            //ID_USER = (Decimal)outParameter.Value;
            //context.Entry(this).State = EntityState.Unchanged;
            //return ID_USER;
            return null;
        }
    }
}