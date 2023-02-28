using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.EDModels
{
    public class InfoCartridgeCG4Model
    {
        public string Name { get; set; }
       // public List<ForPerson> ForPersons { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string ViAge { get; set; }
        public string EnAge { get; set; }
        public float? LowerLimit { get; set; }
        public float? HigherLimit { get; set; }
        public float? LowerAlert { get; set; }
        public float? HigherAlert { get; set; }
    }
    public class ForPerson
    {
       
    }
}