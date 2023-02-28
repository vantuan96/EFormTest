using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Utils
{
    public class VisitAllergy
    {
        private dynamic Visit;

        public VisitAllergy(dynamic visit)
        {
            this.Visit = visit;
        }
        public void UpdateAllergy(Dictionary<string, string> all_dct)
        {
            if(all_dct.Count > 0)
            {
                if (all_dct["YES"].Trim().ToLower() == "true")
                {
                    Visit.IsAllergy = true;
                    Visit.KindOfAllergy = all_dct["KOA"];
                    Visit.Allergy = all_dct["ANS"];
                }
                else if (all_dct["NOO"].Trim().ToLower() == "true")
                {
                    Visit.IsAllergy = false;
                    Visit.KindOfAllergy = "";
                    Visit.Allergy = "Không";
                }
                else if (all_dct["NPA"].Trim().ToLower() == "true")
                {
                    Visit.IsAllergy = null;
                    Visit.KindOfAllergy = "";
                    Visit.Allergy = "Không xác định";
                }
                else
                {
                    Visit.IsAllergy = null;
                    Visit.KindOfAllergy = "";
                    Visit.Allergy = "";
                }
            }     
        }
    }
}