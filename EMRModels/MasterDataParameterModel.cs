namespace EMRModels
{
    public class MasterDataParameterModel
    {
        public string Code { get; set; }
        public string Group { get; set; }
        public string Form { get; set; }
        public string Clinic { get; set; }
    }

    public class MasterDataValue
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string GroupCode { get; set; }
        public string CreatedBy { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string UpdatedBy { get; set; }
        public string Note { get; set; }
        public int? Lv { get; set; }
    }
    public class MasterDataValueDto
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string GroupCode { get; set; }
        public string CreatedBy { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string UpdatedBy { get; set; }
        public string Note { get; set; }
        public int? Lv { get; set; }
    }
}