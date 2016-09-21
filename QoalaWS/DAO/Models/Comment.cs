using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using QoalaWS.Exceptions;
using System.Data.Entity;

namespace QoalaWS.DAO
{
    public partial class Comment
    {
        public static Comment findById(QoalaEntities context, Decimal id_comment)
        {
            return context.COMMENTS.FirstOrDefault(u => u.ID_COMMENT == id_comment && !u.DELETED_AT.HasValue);
        }

        public decimal? Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_COMMENT", typeof(decimal));
            context.SP_INSERT_COMMENT(CONTENT, ID_USER, ID_POST, outParameter);
            if (outParameter.Value == DBNull.Value)
                throw new CreateRecordException();

            ID_COMMENT = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;
            return ID_COMMENT;
        }

        public decimal? Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_UPDATE_COMMENT(ID_COMMENT, CONTENT, ID_USER, ID_POST, outParameter);
            return 1;
        }

        public decimal? Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_DELETE_COMMENT(ID_COMMENT, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }

        public decimal? Approve(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            context.SP_APPROVE_COMMENT(ID_COMMENT, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return 1;
        }
    }
}


