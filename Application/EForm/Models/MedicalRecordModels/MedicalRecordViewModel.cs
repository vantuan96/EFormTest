using EForm.Common;
using System;
using System.Collections.Generic;

namespace EForm.Models.MedicalRecordModels
{
    public class MedicalRecordViewModel
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<MedicalRecordDataViewModel> Datas { get; set; }

        public MedicalRecordViewModel(string vi_name, string en_name, string type)
        {
            this.ViName = vi_name;
            this.EnName = en_name;
            this.Type = type;
            this.Datas = null;
        }
        public MedicalRecordViewModel(string vi_name, string en_name, string type, Guid? form_id)
        {
            this.ViName = vi_name;
            this.EnName = en_name;
            this.Type = type;
            this.Datas = new List<MedicalRecordDataViewModel> { 
                new MedicalRecordDataViewModel {
                    ViName = vi_name,
                    EnName = en_name,
                    FormId = form_id,
                }   
            };
        }
        public MedicalRecordViewModel(string vi_name, string en_name, string type, dynamic form)
        {
            this.ViName = vi_name;
            this.EnName = en_name;
            this.Type = type;
            this.CreatedAt = form.CreatedAt;
            this.UpdatedAt = form.UpdatedAt;
                //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            this.UpdatedBy = form.UpdatedBy;
            this.Datas = new List<MedicalRecordDataViewModel> { 
                new MedicalRecordDataViewModel {
                    ViName = vi_name,
                    EnName = en_name,
                    FormId = form.Id,
                    UpdatedAt = form.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = form.UpdatedBy
                }
            };

        }
        public MedicalRecordViewModel(string vi_name, string en_name, string type, dynamic form, string code)
        {
            this.ViName = vi_name;
            this.Code = code;
            this.EnName = en_name;
            this.Type = type;
            this.CreatedAt = form.CreatedAt;
            this.UpdatedAt = form.UpdatedAt;
            //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            this.UpdatedBy = form.UpdatedBy;
            this.Datas = new List<MedicalRecordDataViewModel> {
                new MedicalRecordDataViewModel {
                    ViName = vi_name,
                    EnName = en_name,
                    FormId = form.Id,
                    UpdatedAt = form.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = form.UpdatedBy,
                }
            };
        }
        public MedicalRecordViewModel(string vi_name, string en_name, string type, dynamic form, List<MedicalRecordDataViewModel> datas)
        {
            this.ViName = vi_name;
            this.EnName = en_name;
            this.Type = type;
            this.Datas = datas;
            this.CreatedAt = form.CreatedAt;
            this.UpdatedAt = form.UpdatedAt;
                //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            this.UpdatedBy = form.UpdatedBy;
        }  
    }

    public class MedicalRecordDataViewModel
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
        public Guid? FormId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Version { get; set; }
    }
}