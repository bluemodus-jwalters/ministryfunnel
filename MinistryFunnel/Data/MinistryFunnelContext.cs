using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Data
{
    public class MinistryFunnelContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MinistryFunnelContext() : base("name=MinistryFunnelContext")
        {
            Database.SetInitializer<MinistryFunnelContext>(null);
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public System.Data.Entity.DbSet<MinistryFunnel.Models.MinistryOwner> MinistryOwner { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Practice> Practice { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Funnel> Funnel { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Campus> Campus { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.ResourceInvolvement> ResourceInvolvement { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Location> Location { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.LevelOfImportance> LevelOfImportance { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.UpInOut> UpInOut { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Approval> Approval { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.Frequency> Frequency { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.UpInOutRelationship> UpInOutRelaionship { get; set; }

        public System.Data.Entity.DbSet<MinistryFunnel.Models.ResourceInvolvementRelationship> ResourceInvolvementRelaionship { get; set; }

        public System.Data.Entity.DbSet<MinistryFunnel.Models.Ministry> Ministry { get; set; }
        public System.Data.Entity.DbSet<MinistryFunnel.Models.LogEvent> LogEvent { get; set; }
    }
}
