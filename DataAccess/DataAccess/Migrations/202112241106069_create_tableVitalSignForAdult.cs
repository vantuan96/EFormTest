namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_tableVitalSignForAdult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VitalSignForAdults",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    VISIT_ID = c.Guid(),
                    IPD_ID = c.Guid(),
                    PID = c.String(maxLength: 20),
                    BREATH_RATE = c.Decimal(precision: 18, scale: 2),
                    COPD = c.Boolean(nullable: false),
                    SPO2 = c.Decimal(precision: 18, scale: 2),
                    LOW_BP = c.Decimal(precision: 18, scale: 2),
                    HIGH_BP = c.Decimal(precision: 18, scale: 2),
                    PULSE = c.Decimal(precision: 18, scale: 2),
                    TEMPERATURE = c.Decimal(precision: 18, scale: 2),
                    SENSE = c.String(),
                    RESPIRATORY_SUPPORT = c.String(),
                    PAIN_SCORE = c.Decimal(precision: 18, scale: 2),
                    CAPILLARY_BLOOD = c.Decimal(precision: 18, scale: 2),
                    VIP_SCORE = c.Int(),
                    FLUID_IN_T = c.Boolean(nullable: false),
                    FLUID_IN_T_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_IN_P = c.Boolean(nullable: false),
                    FLUID_IN_P_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_IN_M = c.Boolean(nullable: false),
                    FLUID_IN_M_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_IN_S = c.Boolean(nullable: false),
                    FLUID_IN_S_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_IN_AN = c.Boolean(nullable: false),
                    FLUID_IN_AN_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_IN_D = c.Boolean(nullable: false),
                    FLUID_IN_D_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_OUT_N = c.Boolean(nullable: false),
                    FLUID_OUT_N_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_OUT_PH = c.Boolean(nullable: false),
                    FLUID_OUT_PH_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_OUT_NT = c.Boolean(nullable: false),
                    FLUID_OUT_NT_VALUE = c.Decimal(precision: 18, scale: 2),
                    FLUID_OUT_DL = c.Boolean(nullable: false),
                    FLUID_OUT_DL_VALUE = c.Decimal(precision: 18, scale: 2),
                    NOTE = c.String(maxLength: 100),
                    TRANS_DATE = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    DeletedAt = c.DateTime(),
                    CreatedBy = c.String(),
                    CreatedAt = c.DateTime(),
                    UpdatedBy = c.String(),
                    UpdatedAt = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.VitalSignForAdults");
        }
    }
}
