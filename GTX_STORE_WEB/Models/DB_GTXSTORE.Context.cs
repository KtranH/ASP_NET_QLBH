﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTX_STORE_WEB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_GTXSTORE : DbContext
    {
        public DB_GTXSTORE()
            : base("name=DB_GTXSTORE")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual DbSet<CT_GIOHANG> CT_GIOHANG { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<HANGSX> HANGSXes { get; set; }
        public virtual DbSet<KHACH> KHACHes { get; set; }
        public virtual DbSet<LOAISP> LOAISPs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
    }
}
