//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class POST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POST()
        {
            this.COMMENTS = new HashSet<COMMENT>();
        }
    
        public decimal ID_POST { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public System.DateTime CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
        public Nullable<System.DateTime> PUBLISHED_AT { get; set; }
        public Nullable<System.DateTime> DELETED_AT { get; set; }
        public decimal USER_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTS { get; set; }
        public virtual USER USER { get; set; }
    }
}
