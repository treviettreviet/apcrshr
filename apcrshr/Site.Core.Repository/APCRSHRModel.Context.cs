﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Site.Core.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APCRSHREntities : DbContext
    {
        public APCRSHREntities()
            : base("name=APCRSHREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ConferenceData> ConferenceDatas { get; set; }
        public DbSet<DownloadCode> DownloadCodes { get; set; }
        public DbSet<ImportantDeadline> ImportantDeadlines { get; set; }
        public DbSet<MailingAddress> MailingAddresses { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<OfficeAddress> OfficeAddresses { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Upload> Uploads { get; set; }
    }
}