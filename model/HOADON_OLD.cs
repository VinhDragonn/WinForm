//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADON_OLD
    {
        public int IDHD_OLD { get; set; }
        public int IDBAN { get; set; }
        public string TENNV { get; set; }
        public string TENMONAN { get; set; }
        public Nullable<System.DateTime> NGAYLAP { get; set; }
        public int TONGTIEN { get; set; }
        public string TENKH { get; set; }
    
        public virtual TableFood TableFood { get; set; }
    }
}
