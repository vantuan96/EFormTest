using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDObservationChartData : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string SysBP { get; set; }
        public string DiaBP { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string Resp { get; set; }
        public string SpO2 { get; set; }
        public string RestPainScore { get; set; }
        public string MovePainScore { get; set; }
        public DateTime? NoteAt { get; set; }
        public string Other { get; set; }
        public Nullable<Guid> ObservationChartId { get; set; }
        [ForeignKey("ObservationChartId")]
        public virtual EDObservationChart ObservationChart { get; set; }
    }
}
