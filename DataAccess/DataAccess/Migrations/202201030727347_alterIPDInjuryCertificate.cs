﻿namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterIPDInjuryCertificate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IPDInjuryCertificates", "PID", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IPDInjuryCertificates", "PID", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
