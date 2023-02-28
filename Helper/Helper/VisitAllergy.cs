using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class EMRVisitAllergy
    {
        public static VisitAllergyModel GetVisitAllergy(dynamic visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy
                };
            }
            return new VisitAllergyModel()
            {
                Allergy = visit.Allergy
            };
        }
        public static VisitAllergyModel GetIPDVisitAllergy(IPD visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy
                };
            }
            return new VisitAllergyModel() {
                Allergy = visit.Allergy
            };
        }
        public static void SetIpdVisitAllergy(Dictionary<string, string> all_dct, ref IPD visit)
        {
            try
            {
                if (all_dct.ContainsKey("YES") && all_dct["YES"] != null && all_dct["YES"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = true;
                    visit.KindOfAllergy = all_dct["KOA"];
                    visit.Allergy = all_dct["ANS"];
                }
                else if (all_dct.ContainsKey("NOO") && all_dct["NOO"] != null && all_dct["NOO"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = false;
                    visit.KindOfAllergy = "";
                    visit.Allergy = "Không";
                }
                else if (all_dct.ContainsKey("NPA") && all_dct["NPA"] != null && all_dct["NPA"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = false;
                    visit.KindOfAllergy = "";
                    visit.Allergy = "Không xác định";
                }

            }
            catch (Exception)
            {
            }
        }
        public static VisitAllergyModel GetOPDVisitAllergy(OPD visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy
                };
            }
            return new VisitAllergyModel()
            {
                Allergy = visit.Allergy
            };
        }
        public static void SetOPDVisitAllergy(Dictionary<string, string> all_dct, ref OPD visit)
        {
            try
            {
                if (all_dct.ContainsKey("YES") && all_dct["YES"] != null && all_dct["YES"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = true;
                    visit.KindOfAllergy = all_dct["KOA"];
                    visit.Allergy = all_dct["ANS"];
                }
                else if (all_dct.ContainsKey("NOO") && all_dct["NOO"] != null && all_dct["NOO"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = false;
                    visit.KindOfAllergy = "";
                    visit.Allergy = "Không";
                }
                else if (all_dct.ContainsKey("NPA") && all_dct["NPA"] != null && all_dct["NPA"].Trim().ToLower() == "true")
                {
                    visit.IsAllergy = false;
                    visit.KindOfAllergy = "";
                    visit.Allergy = "Không xác định";
                }

            }
            catch (Exception)
            {
            }
        }
        public static VisitAllergyModel GetEDVisitAllergy(ED visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy
                };
            }
            return new VisitAllergyModel()
            {
                Allergy = visit.Allergy
            };
        }
        public static VisitAllergyModel GetEOCVisitAllergy(EOC visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy
                };
            }
            return new VisitAllergyModel()
            {
                Allergy = visit.Allergy
            };
        }
    }
}
