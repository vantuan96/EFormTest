namespace EForm.Models
{
    public class MedicationMasterdataModel: PagingParameterModel
    {
        public string Search { get; set; }
        public string ConvertedSearch
        {
            get
            {
                if (string.IsNullOrEmpty(this.Search))
                {
                    return "";
                }
                return this.Search.Replace("\r", "")
                    .Replace("\t", "")
                    .Replace("\n", "")
                    .Trim()
                    .ToLower();
            }
        }
    }
}