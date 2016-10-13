﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QoalaWS.DAO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QoalaEntities : DbContext
    {
        public QoalaEntities()
            : base("name=QoalaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comment> COMMENTS { get; set; }
        public virtual DbSet<DeviceGeoLocation> DeviceGeoLocations { get; set; }
        public virtual DbSet<Device> DEVICES { get; set; }
        public virtual DbSet<Post> POSTS { get; set; }
        public virtual DbSet<User> USERS { get; set; }
        public virtual DbSet<AccessControl> ACCESSCONTROLs { get; set; }
        public virtual DbSet<INFOCOMPANY> INFOCOMPANies { get; set; }
        public virtual DbSet<Plan> PLANS { get; set; }
        public virtual DbSet<Reward> REWARDS { get; set; }
        public virtual DbSet<Sponsor> SPONSORS { get; set; }
    
        public virtual int SP_DELETE_USER(Nullable<decimal> iD, ObjectParameter rOWCOUNT)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_USER", iDParameter, rOWCOUNT);
        }
    
        public virtual int SP_UPDATE_USER(Nullable<decimal> pID, string pNAME, string pPASSWORD, string pEMAIL, Nullable<decimal> pPERMISSION, string pADDRESS, string pDISTRICT, string pCITY, string pSTATE, string pZIPCODE, ObjectParameter pROWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pNAMEParameter = pNAME != null ?
                new ObjectParameter("PNAME", pNAME) :
                new ObjectParameter("PNAME", typeof(string));
    
            var pPASSWORDParameter = pPASSWORD != null ?
                new ObjectParameter("PPASSWORD", pPASSWORD) :
                new ObjectParameter("PPASSWORD", typeof(string));
    
            var pEMAILParameter = pEMAIL != null ?
                new ObjectParameter("PEMAIL", pEMAIL) :
                new ObjectParameter("PEMAIL", typeof(string));
    
            var pPERMISSIONParameter = pPERMISSION.HasValue ?
                new ObjectParameter("PPERMISSION", pPERMISSION) :
                new ObjectParameter("PPERMISSION", typeof(decimal));
    
            var pADDRESSParameter = pADDRESS != null ?
                new ObjectParameter("PADDRESS", pADDRESS) :
                new ObjectParameter("PADDRESS", typeof(string));
    
            var pDISTRICTParameter = pDISTRICT != null ?
                new ObjectParameter("PDISTRICT", pDISTRICT) :
                new ObjectParameter("PDISTRICT", typeof(string));
    
            var pCITYParameter = pCITY != null ?
                new ObjectParameter("PCITY", pCITY) :
                new ObjectParameter("PCITY", typeof(string));
    
            var pSTATEParameter = pSTATE != null ?
                new ObjectParameter("PSTATE", pSTATE) :
                new ObjectParameter("PSTATE", typeof(string));
    
            var pZIPCODEParameter = pZIPCODE != null ?
                new ObjectParameter("PZIPCODE", pZIPCODE) :
                new ObjectParameter("PZIPCODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_USER", pIDParameter, pNAMEParameter, pPASSWORDParameter, pEMAILParameter, pPERMISSIONParameter, pADDRESSParameter, pDISTRICTParameter, pCITYParameter, pSTATEParameter, pZIPCODEParameter, pROWCOUNT);
        }
    
        public virtual int SP_USER_LOG(string lOG, Nullable<decimal> uSER_ID)
        {
            var lOGParameter = lOG != null ?
                new ObjectParameter("LOG", lOG) :
                new ObjectParameter("LOG", typeof(string));
    
            var uSER_IDParameter = uSER_ID.HasValue ?
                new ObjectParameter("USER_ID", uSER_ID) :
                new ObjectParameter("USER_ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_USER_LOG", lOGParameter, uSER_IDParameter);
        }
    
        public virtual int SP_INSERT_USER(string nAME, string pASSWORD, string eMAIL, Nullable<decimal> pERMISSION, string aDDRESS, string dISTRICT, string cITY, string sTATE, string zIPCODE, ObjectParameter oUT_ID_USER)
        {
            var nAMEParameter = nAME != null ?
                new ObjectParameter("NAME", nAME) :
                new ObjectParameter("NAME", typeof(string));
    
            var pASSWORDParameter = pASSWORD != null ?
                new ObjectParameter("PASSWORD", pASSWORD) :
                new ObjectParameter("PASSWORD", typeof(string));
    
            var eMAILParameter = eMAIL != null ?
                new ObjectParameter("EMAIL", eMAIL) :
                new ObjectParameter("EMAIL", typeof(string));
    
            var pERMISSIONParameter = pERMISSION.HasValue ?
                new ObjectParameter("PERMISSION", pERMISSION) :
                new ObjectParameter("PERMISSION", typeof(decimal));
    
            var aDDRESSParameter = aDDRESS != null ?
                new ObjectParameter("ADDRESS", aDDRESS) :
                new ObjectParameter("ADDRESS", typeof(string));
    
            var dISTRICTParameter = dISTRICT != null ?
                new ObjectParameter("DISTRICT", dISTRICT) :
                new ObjectParameter("DISTRICT", typeof(string));
    
            var cITYParameter = cITY != null ?
                new ObjectParameter("CITY", cITY) :
                new ObjectParameter("CITY", typeof(string));
    
            var sTATEParameter = sTATE != null ?
                new ObjectParameter("STATE", sTATE) :
                new ObjectParameter("STATE", typeof(string));
    
            var zIPCODEParameter = zIPCODE != null ?
                new ObjectParameter("ZIPCODE", zIPCODE) :
                new ObjectParameter("ZIPCODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERT_USER", nAMEParameter, pASSWORDParameter, eMAILParameter, pERMISSIONParameter, aDDRESSParameter, dISTRICTParameter, cITYParameter, sTATEParameter, zIPCODEParameter, oUT_ID_USER);
        }
    
        public virtual int SP_DELETE_POST(Nullable<decimal> iD, ObjectParameter rOWCOUNT)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_POST", iDParameter, rOWCOUNT);
        }
    
        public virtual int SP_INSERT_POST(string tITLE, string cONTENT, Nullable<decimal> iD_USER, ObjectParameter oUT_ID_POST)
        {
            var tITLEParameter = tITLE != null ?
                new ObjectParameter("TITLE", tITLE) :
                new ObjectParameter("TITLE", typeof(string));
    
            var cONTENTParameter = cONTENT != null ?
                new ObjectParameter("CONTENT", cONTENT) :
                new ObjectParameter("CONTENT", typeof(string));
    
            var iD_USERParameter = iD_USER.HasValue ?
                new ObjectParameter("ID_USER", iD_USER) :
                new ObjectParameter("ID_USER", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERT_POST", tITLEParameter, cONTENTParameter, iD_USERParameter, oUT_ID_POST);
        }
    
        public virtual int SP_POST_LOG(string lOG, Nullable<decimal> pOST_ID)
        {
            var lOGParameter = lOG != null ?
                new ObjectParameter("LOG", lOG) :
                new ObjectParameter("LOG", typeof(string));
    
            var pOST_IDParameter = pOST_ID.HasValue ?
                new ObjectParameter("POST_ID", pOST_ID) :
                new ObjectParameter("POST_ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_POST_LOG", lOGParameter, pOST_IDParameter);
        }
    
        public virtual int SP_PUBLISH_POST(Nullable<decimal> iD, ObjectParameter rOWCOUNT)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_PUBLISH_POST", iDParameter, rOWCOUNT);
        }
    
        public virtual int SP_UPDATE_POST(Nullable<decimal> pID, string pTITLE, string pCONTENT, Nullable<decimal> pID_USER, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pTITLEParameter = pTITLE != null ?
                new ObjectParameter("PTITLE", pTITLE) :
                new ObjectParameter("PTITLE", typeof(string));
    
            var pCONTENTParameter = pCONTENT != null ?
                new ObjectParameter("PCONTENT", pCONTENT) :
                new ObjectParameter("PCONTENT", typeof(string));
    
            var pID_USERParameter = pID_USER.HasValue ?
                new ObjectParameter("PID_USER", pID_USER) :
                new ObjectParameter("PID_USER", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_POST", pIDParameter, pTITLEParameter, pCONTENTParameter, pID_USERParameter, rOWCOUNT);
        }
    
        public virtual int SP_APPROVE_COMMENT(Nullable<decimal> pID, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_APPROVE_COMMENT", pIDParameter, rOWCOUNT);
        }
    
        public virtual int SP_COMMENT_LOG(string pLOG, Nullable<decimal> pCOMMENT_ID)
        {
            var pLOGParameter = pLOG != null ?
                new ObjectParameter("PLOG", pLOG) :
                new ObjectParameter("PLOG", typeof(string));
    
            var pCOMMENT_IDParameter = pCOMMENT_ID.HasValue ?
                new ObjectParameter("PCOMMENT_ID", pCOMMENT_ID) :
                new ObjectParameter("PCOMMENT_ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_COMMENT_LOG", pLOGParameter, pCOMMENT_IDParameter);
        }
    
        public virtual int SP_DELETE_COMMENT(Nullable<decimal> pID, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_COMMENT", pIDParameter, rOWCOUNT);
        }
    
        public virtual int SP_INSERT_COMMENT(string pCONTENT, Nullable<decimal> pID_USER, Nullable<decimal> pID_POST, ObjectParameter oUT_ID_COMMENT)
        {
            var pCONTENTParameter = pCONTENT != null ?
                new ObjectParameter("PCONTENT", pCONTENT) :
                new ObjectParameter("PCONTENT", typeof(string));
    
            var pID_USERParameter = pID_USER.HasValue ?
                new ObjectParameter("PID_USER", pID_USER) :
                new ObjectParameter("PID_USER", typeof(decimal));
    
            var pID_POSTParameter = pID_POST.HasValue ?
                new ObjectParameter("PID_POST", pID_POST) :
                new ObjectParameter("PID_POST", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERT_COMMENT", pCONTENTParameter, pID_USERParameter, pID_POSTParameter, oUT_ID_COMMENT);
        }
    
        public virtual int SP_UPDATE_COMMENT(Nullable<decimal> pID, string pCONTENT, Nullable<decimal> pID_USER, Nullable<decimal> pID_POST, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pCONTENTParameter = pCONTENT != null ?
                new ObjectParameter("PCONTENT", pCONTENT) :
                new ObjectParameter("PCONTENT", typeof(string));
    
            var pID_USERParameter = pID_USER.HasValue ?
                new ObjectParameter("PID_USER", pID_USER) :
                new ObjectParameter("PID_USER", typeof(decimal));
    
            var pID_POSTParameter = pID_POST.HasValue ?
                new ObjectParameter("PID_POST", pID_POST) :
                new ObjectParameter("PID_POST", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_COMMENT", pIDParameter, pCONTENTParameter, pID_USERParameter, pID_POSTParameter, rOWCOUNT);
        }
    
        public virtual int SP_DELETE_DEVICE(Nullable<decimal> pID, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_DEVICE", pIDParameter, rOWCOUNT);
        }
    
        public virtual int SP_DEVICE_LOG(string pLOG, Nullable<decimal> pDEVICE_ID)
        {
            var pLOGParameter = pLOG != null ?
                new ObjectParameter("PLOG", pLOG) :
                new ObjectParameter("PLOG", typeof(string));
    
            var pDEVICE_IDParameter = pDEVICE_ID.HasValue ?
                new ObjectParameter("PDEVICE_ID", pDEVICE_ID) :
                new ObjectParameter("PDEVICE_ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DEVICE_LOG", pLOGParameter, pDEVICE_IDParameter);
        }
    
        public virtual int SP_INSERT_DEVICE(string pALIAS, string pCOLOR, Nullable<decimal> pFREQUENCY_UPDATE, Nullable<decimal> pID_USER, ObjectParameter oUT_ID_DEVICE)
        {
            var pALIASParameter = pALIAS != null ?
                new ObjectParameter("PALIAS", pALIAS) :
                new ObjectParameter("PALIAS", typeof(string));
    
            var pCOLORParameter = pCOLOR != null ?
                new ObjectParameter("PCOLOR", pCOLOR) :
                new ObjectParameter("PCOLOR", typeof(string));
    
            var pFREQUENCY_UPDATEParameter = pFREQUENCY_UPDATE.HasValue ?
                new ObjectParameter("PFREQUENCY_UPDATE", pFREQUENCY_UPDATE) :
                new ObjectParameter("PFREQUENCY_UPDATE", typeof(decimal));
    
            var pID_USERParameter = pID_USER.HasValue ?
                new ObjectParameter("PID_USER", pID_USER) :
                new ObjectParameter("PID_USER", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERT_DEVICE", pALIASParameter, pCOLORParameter, pFREQUENCY_UPDATEParameter, pID_USERParameter, oUT_ID_DEVICE);
        }
    
        public virtual int SP_TURN_ALARM(Nullable<decimal> pID, Nullable<decimal> pALARM, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pALARMParameter = pALARM.HasValue ?
                new ObjectParameter("PALARM", pALARM) :
                new ObjectParameter("PALARM", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_TURN_ALARM", pIDParameter, pALARMParameter, rOWCOUNT);
        }
    
        public virtual int SP_UPDATE_DEVICE(Nullable<decimal> pID, string pALIAS, string pCOLOR, Nullable<decimal> pFREQUENCY_UPDATE, Nullable<decimal> pID_USER, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pALIASParameter = pALIAS != null ?
                new ObjectParameter("PALIAS", pALIAS) :
                new ObjectParameter("PALIAS", typeof(string));
    
            var pCOLORParameter = pCOLOR != null ?
                new ObjectParameter("PCOLOR", pCOLOR) :
                new ObjectParameter("PCOLOR", typeof(string));
    
            var pFREQUENCY_UPDATEParameter = pFREQUENCY_UPDATE.HasValue ?
                new ObjectParameter("PFREQUENCY_UPDATE", pFREQUENCY_UPDATE) :
                new ObjectParameter("PFREQUENCY_UPDATE", typeof(decimal));
    
            var pID_USERParameter = pID_USER.HasValue ?
                new ObjectParameter("PID_USER", pID_USER) :
                new ObjectParameter("PID_USER", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_DEVICE", pIDParameter, pALIASParameter, pCOLORParameter, pFREQUENCY_UPDATEParameter, pID_USERParameter, rOWCOUNT);
        }
    
        public virtual int SP_UPDATE_LAST_LOCATION(Nullable<decimal> pID, Nullable<decimal> pLAST_LONGITUDE, Nullable<decimal> pLAST_LATITUDE, ObjectParameter rOWCOUNT)
        {
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(decimal));
    
            var pLAST_LONGITUDEParameter = pLAST_LONGITUDE.HasValue ?
                new ObjectParameter("PLAST_LONGITUDE", pLAST_LONGITUDE) :
                new ObjectParameter("PLAST_LONGITUDE", typeof(decimal));
    
            var pLAST_LATITUDEParameter = pLAST_LATITUDE.HasValue ?
                new ObjectParameter("PLAST_LATITUDE", pLAST_LATITUDE) :
                new ObjectParameter("PLAST_LATITUDE", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_LAST_LOCATION", pIDParameter, pLAST_LONGITUDEParameter, pLAST_LATITUDEParameter, rOWCOUNT);
        }
    
        public virtual int SP_DEVICE_GEO_LOCATION_LOG(string pLOG, Nullable<decimal> pDEVICE_GEO_LOCATION_ID)
        {
            var pLOGParameter = pLOG != null ?
                new ObjectParameter("PLOG", pLOG) :
                new ObjectParameter("PLOG", typeof(string));
    
            var pDEVICE_GEO_LOCATION_IDParameter = pDEVICE_GEO_LOCATION_ID.HasValue ?
                new ObjectParameter("PDEVICE_GEO_LOCATION_ID", pDEVICE_GEO_LOCATION_ID) :
                new ObjectParameter("PDEVICE_GEO_LOCATION_ID", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DEVICE_GEO_LOCATION_LOG", pLOGParameter, pDEVICE_GEO_LOCATION_IDParameter);
        }
    
        public virtual int SP_INSERT_DEVICE_GEO_LOCATION(Nullable<decimal> pID_DEVICE, Nullable<decimal> pLATITUDE, Nullable<decimal> pLONGITUDE, ObjectParameter oUT_ID_GEO)
        {
            var pID_DEVICEParameter = pID_DEVICE.HasValue ?
                new ObjectParameter("PID_DEVICE", pID_DEVICE) :
                new ObjectParameter("PID_DEVICE", typeof(decimal));
    
            var pLATITUDEParameter = pLATITUDE.HasValue ?
                new ObjectParameter("PLATITUDE", pLATITUDE) :
                new ObjectParameter("PLATITUDE", typeof(decimal));
    
            var pLONGITUDEParameter = pLONGITUDE.HasValue ?
                new ObjectParameter("PLONGITUDE", pLONGITUDE) :
                new ObjectParameter("PLONGITUDE", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERT_DEVICE_GEO_LOCATION", pID_DEVICEParameter, pLATITUDEParameter, pLONGITUDEParameter, oUT_ID_GEO);
        }
    }
}
