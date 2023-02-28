using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOMortalityReportController : BaseApiController
    {
        protected EIOMortalityReport GetEIOMortalityReport(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOMortalityReportRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected dynamic GetMortalityReportResult(EIOMortalityReport mortality, dynamic visit, string visit_type = "ED")
        {
            var specialty = GetSpecialty();
            var customer = visit.Customer;

            var gender = new CustomerUtil(customer).GetGender();
            var chairman = mortality.Chairman;
            var secretary = mortality.Secretary;
            var members = mortality.EDMortalityReportMembers.Where(e => !e.IsDeleted).Select(e => new
            {
                e.Member?.Id,
                ConfirmTime = e.ConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Member?.Username,
                e.Member?.Fullname,
                e.Member?.DisplayName,
                e.Member?.Title,
                e.IsNotMember
            });

            DiagnosisAndICDModel diagnosis_ed = new DiagnosisAndICDModel();

            if (visit != null)
            {

                if (visit_type == "IPD")
                {
                    IPD ipd = (IPD)visit;
                    var medicalRecord = ipd.IPDMedicalRecord;
                    if (medicalRecord != null)
                    {
                        var medicalRecordData = medicalRecord.IPDMedicalRecordDatas;
                        var medicalRecordPart2 = medicalRecord.IPDMedicalRecordPart2;
                        if (medicalRecordPart2 != null)
                        {
                            var medicalRecordPart2Datas = medicalRecordPart2.IPDMedicalRecordPart2Datas;
                            if (medicalRecordPart2Datas != null)
                            {
                                //if (mortality.PastMedicalHistory == null || mortality.PastMedicalHistory == "")
                                //{
                                //    mortality.PastMedicalHistory = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTBATHANS")?.Value;
                                //}
                                mortality.PastMedicalHistory = GetPersonalHistory(ipd);
                                string toanThan = "1.Toàn thân: (ý thức, da niêm mạc, hệ thống hạch, tuyến giáp, vị trí, kích thước, số lượng,...)";
                                string mach = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTMACHANS")?.Value ?? string.Empty;
                                if (mach != "")
                                {
                                    mach = $"\n\tMạch: {mach} nhịp/phút";
                                }
                                string nhietDo = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTNHDOANS")?.Value ?? string.Empty;
                                if (nhietDo != "")
                                {
                                    nhietDo = $"\n\tNhiệt độ: {nhietDo} độ C";
                                }
                                string huyetAp = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTHUAPANS")?.Value ?? string.Empty;
                                if (huyetAp != "")
                                {
                                    huyetAp = $"\n\tHuyết áp: {huyetAp} mmHg";
                                }
                                string nhipTho = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTNHTHANS")?.Value ?? string.Empty;
                                if (nhipTho != "")
                                {
                                    nhipTho = $"\n\tNhịp thở: {nhipTho} lần/phút";
                                }
                                string chieuCao = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTCICAANS")?.Value ?? string.Empty;
                                if (chieuCao != "")
                                {
                                    chieuCao = $"\n\tChiều cao: {chieuCao} cm";
                                }
                                string canNang = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTCANAANS")?.Value ?? string.Empty;
                                if (canNang != "")
                                {
                                    canNang = $"\n\tCân nặng: {canNang} kg";
                                }
                                string bmi = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTBBMIANS")?.Value ?? string.Empty;
                                if (bmi != "")
                                {
                                    bmi = $"\n\tBMI: {bmi}";
                                }
                                string toanThanText = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTTYTANS")?.Value ?? string.Empty;
                                if (toanThanText != "")
                                {
                                    toanThanText = $"\n\t{toanThanText}";
                                }
                                toanThan = $"{toanThan}{mach}{nhietDo}{huyetAp}{nhipTho}{chieuCao}{canNang}{bmi}{toanThanText}";


                                string cacCoQuan = $"2. Các cơ quan:";

                                string tuanHoan = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTUHOANS")?.Value ?? string.Empty;
                                if (tuanHoan != "")
                                {
                                    tuanHoan = $"\n\tTuần hoàn: {tuanHoan}";
                                }

                                string hohap = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTHOHAANS")?.Value ?? string.Empty;
                                if (hohap != "")
                                {
                                    hohap = $"\n\tHô hấp: {hohap}";
                                }

                                string tieuHoa = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTIHOANS")?.Value ?? string.Empty;
                                if (tieuHoa != "")
                                {
                                    tieuHoa = $"\n\tTiêu hóa: {tieuHoa}";
                                }

                                string thanTietNieu = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTTNSANS")?.Value ?? string.Empty;
                                if (thanTietNieu != "")
                                {
                                    thanTietNieu = $"\n\tThận - Tiết niệu: {thanTietNieu}";
                                }

                                string thanKinh = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTHKIANS")?.Value ?? string.Empty;
                                if (thanKinh != "")
                                {
                                    thanKinh = $"\n\tThần kinh: {thanKinh}";
                                }

                                string coXuongKhop = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTCOXKANS")?.Value ?? string.Empty;
                                if (coXuongKhop != "")
                                {
                                    coXuongKhop = $"\n\tCơ - Xương - Khớp: {coXuongKhop}";
                                }

                                string taiMuiHong = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTAMHANS")?.Value ?? string.Empty;
                                if (taiMuiHong != "")
                                {
                                    taiMuiHong = $"\n\tTai - Mũi - Họng: {taiMuiHong}";
                                }

                                string rangHamMat = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTRAHMANS")?.Value ?? string.Empty;
                                if (rangHamMat != "")
                                {
                                    rangHamMat = $"\n\tRăng - Hàm - Mặt: {rangHamMat}";
                                }

                                string mat = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTMMATANS")?.Value ?? string.Empty;
                                if (mat != "")
                                {
                                    mat = $"\n\tMắt: {mat}";
                                }
                                string noiTietVaKhac = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTNTDDANS")?.Value ?? string.Empty;
                                if (noiTietVaKhac != "")
                                {
                                    noiTietVaKhac = $"\n\tNội tiết, dinh dưỡng và các bệnh lý khác: {noiTietVaKhac}";
                                }
                                cacCoQuan += $"{tuanHoan}{hohap}{tieuHoa}{thanTietNieu}{thanKinh}{coXuongKhop}{taiMuiHong}{rangHamMat}{mat}{noiTietVaKhac}";

                                string xetNghiem = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTCXNCANS")?.Value ?? string.Empty;
                                if (xetNghiem != "")
                                {
                                    xetNghiem = $"3. Các xét nghiệm cận lâm sàng cần làm: {xetNghiem}";
                                }

                                //IPDMRPTTTBA
                                string tomTatBenhAn = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPTTTBAANS")?.Value ?? string.Empty;
                                if (tomTatBenhAn != "")
                                {
                                    tomTatBenhAn = $"4. Tóm tắt bệnh án: {tomTatBenhAn}";
                                }

                                if (mortality.Status == null || mortality.Status == "")
                                {
                                    mortality.Status = $"{toanThan}\n{cacCoQuan}\n{xetNghiem}\n{tomTatBenhAn}";
                                }
                            }
                        }

                        if (medicalRecordData != null)
                        {
                            var diagnosis = new DiagnosisAndICDModel
                            {
                                ICD = medicalRecordData.FirstOrDefault(e => e.Code == "IPDMRPTICDTANS")?.Value,
                                Diagnosis = medicalRecordData.FirstOrDefault(e => e.Code == "IPDMRPTBCTVANS")?.Value,
                            };

                            if (mortality.Diagnosis == null || mortality.Diagnosis == "" || mortality.Diagnosis == ",  (, )" || mortality.Diagnosis == " ()")
                            {
                                mortality.Diagnosis = GenerateStringDiagnosis(diagnosis);
                            }
                        }
                    }
                }
                if (visit_type == "ED")
                    if (visit.EDStatus?.Code == "EDDD")
                        diagnosis_ed = GetVisitDiagnosisAndICD(visit.Id, "ED", false);

            }
            Guid siteId = visit.SiteId;
            var site = unitOfWork.SiteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == siteId);

            return new
            {
                mortality.Id,
                Time = mortality.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Site = new {site?.ViName, site?.EnName, site?.Location, site?.Province, site?.Name, site?.LocationUnit},
                Specialty = new { visit.Specialty?.ViName, visit.Specialty?.EnName,  },
                Customer = new
                {
                    customer?.Fullname,
                    DateOfBirth = customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    Gender = gender,
                    Ethnic = customer?.Fork,
                    customer?.Nationality,
                    customer?.AgeFormated,
                    customer?.Job,
                    customer?.WorkPlace,
                    customer?.Address,
                    customer?.PID,
                    customer?.IdentificationCard,
                    IssueDate = customer?.IssueDate?.ToString(Constant.DATE_FORMAT),
                    customer?.IssuePlace,
                    customer?.MOHJob,
                    customer?.MOHEthnic,
                    customer?.MOHNationality,
                    customer?.MOHJobCode,
                    customer?.MOHEthnicCode,
                    customer?.MOHNationalityCode,
                    customer?.MOHAddress,
                    customer?.MOHProvince,
                    customer?.MOHProvinceCode,
                    customer?.MOHDistrict,
                    customer?.MOHDistrictCode,
                    customer?.MOHObject,
                    customer?.MOHObjectOther,
                },
                visit.VisitCode,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DeathAt = mortality.DeathAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                mortality.Reason,
                PastMedicalHistory = mortality.PastMedicalHistory,
                mortality.Status,
                Diagnosis = mortality.Diagnosis,
                mortality.Progress,
                mortality.Welcome,
                mortality.Assessment,
                mortality.TreatmentAndProcedures,
                mortality.Care,
                mortality.RelationShip,
                mortality.Extra,
                mortality.Conclusion,
                ChairmanTime = mortality.ChairmanTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Chairman = new { chairman?.Id, chairman?.Username, chairman?.Fullname, chairman?.DisplayName, chairman?.Title },
                SecretaryTime = mortality.SecretaryTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Secretary = new { secretary?.Id, secretary?.Username, secretary?.Fullname, secretary?.DisplayName, secretary?.Title },
                Members = members,
                IsLocked = visit_type == "ED" ? false : IPDIsBlock(visit, Constant.IPDFormCode.BienBanKiemThaoTuVong, mortality.Id),
                mortality.ICD10,
                Version = visit?.Version,
                mortality.UpdatedAt,
                mortality.UpdatedBy,
                IsNew = IsNew(mortality.CreatedAt, mortality.UpdatedAt),
                mortality.CreatedAt,
                mortality.CreatedBy,
                ICDFromDisChagreInformation = diagnosis_ed
            };
        }

        protected EIOMortalityReport CreateEIOMortalityReport(Guid visit_id, string visit_type)
        {
            List<IPDMedicalRecordPart2Data> part2Data = new List<IPDMedicalRecordPart2Data>();
            var chanDoan = GetVisitDiagnosisAndICD(visit_id, "IPD", true);

            if (visit_type == "IPD")
            {
                var ipd = GetIPD(visit_id);

                if (ipd != null)
                {
                    if (ipd.IPDMedicalRecord != null)
                    {
                        if (ipd.IPDMedicalRecord.IPDMedicalRecordPart2 != null)
                        {
                            if (ipd.IPDMedicalRecord.IPDMedicalRecordPart2.IPDMedicalRecordPart2Datas != null)
                            {
                                part2Data = ipd.IPDMedicalRecord.IPDMedicalRecordPart2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).ToList();
                            }
                        }
                    }
                }
            }

            var mortality = new EIOMortalityReport()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                //Diagnosis = chanDoan
            };
            unitOfWork.EIOMortalityReportRepository.Add(mortality);
            unitOfWork.Commit();
            return mortality;
        }

        protected void UpdateMortalityReport(EIOMortalityReport mortality, IEnumerable<EIOMortalityReportMember> members, JObject request, int app_version = 1)
        {
            var time = request["Time"]?.ToString();
            if (!string.IsNullOrEmpty(time))
                mortality.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                mortality.Time = null;

            var death_at = request["DeathAt"]?.ToString();
            if (!string.IsNullOrEmpty(death_at))
                mortality.DeathAt = DateTime.ParseExact(death_at, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                mortality.DeathAt = null;

            mortality.Reason = request["Reason"]?.ToString();
            mortality.PastMedicalHistory = request["PastMedicalHistory"]?.ToString();
            mortality.Status = request["Status"]?.ToString();
            mortality.Diagnosis = request["Diagnosis"]?.ToString();
            mortality.Progress = request["Progress"]?.ToString();
            mortality.Welcome = request["Welcome"]?.ToString();
            mortality.Assessment = request["Assessment"]?.ToString();
            mortality.TreatmentAndProcedures = request["TreatmentAndProcedures"]?.ToString();
            mortality.Care = request["Care"]?.ToString();
            mortality.RelationShip = request["RelationShip"]?.ToString();
            mortality.Extra = request["Extra"]?.ToString();
            mortality.Conclusion = request["Conclusion"]?.ToString();
            mortality.ICD10 = request["ICD10"]?.ToString() ?? string.Empty;
            var chairman_id = request["Chairman"]?["Id"]?.ToString();
            var currenVisitType = GetCurrentVisitType();
            unitOfWork.EIOMortalityReportMemberRepository.HardDeleteRange(members.AsQueryable());
            if (!string.IsNullOrEmpty(chairman_id))
            {
                mortality.ChairmanId = new Guid(chairman_id);
                if(app_version >= 9 && currenVisitType == "ED")
                {
                    var mem = new EIOMortalityReportMember()
                    {
                        MemberId = mortality.ChairmanId,
                        EDMortalityReportId = mortality.Id,
                        IsNotMember = true
                    };
                    unitOfWork.EIOMortalityReportMemberRepository.Add(mem);
                }
                else if (app_version >= 11 && currenVisitType == "IPD")
                {
                    var mem = new EIOMortalityReportMember()
                    {
                        MemberId = mortality.ChairmanId,
                        EDMortalityReportId = mortality.Id,
                        IsNotMember = true
                    };
                    unitOfWork.EIOMortalityReportMemberRepository.Add(mem);
                }
            }    
            else
                mortality.ChairmanId = null;

            var secret_id = request["Secretary"]?["Id"]?.ToString();
            if (!string.IsNullOrEmpty(secret_id))
            {
                mortality.SecretaryId = new Guid(secret_id);
                if (app_version >= 7 && currenVisitType == "ED")
                {
                    var mem = new EIOMortalityReportMember()
                    {
                        MemberId = mortality.SecretaryId,
                        EDMortalityReportId = mortality.Id,
                        IsNotMember = true
                    };
                    unitOfWork.EIOMortalityReportMemberRepository.Add(mem);
                }
                else if (app_version >= 11 && currenVisitType == "IPD")
                {
                    var mem = new EIOMortalityReportMember()
                    {
                        MemberId = mortality.SecretaryId,
                        EDMortalityReportId = mortality.Id,
                        IsNotMember = true
                    };
                    unitOfWork.EIOMortalityReportMemberRepository.Add(mem);
                }
            }
            else
                mortality.SecretaryId = null;
            
            foreach (var item in request["Members"])
            {
                var item_id = item["Id"]?.ToString();
                if (string.IsNullOrEmpty(item_id)) continue;

                var mem = new EIOMortalityReportMember()
                {
                    MemberId = new Guid(item_id),
                    EDMortalityReportId = mortality.Id,
                };
                unitOfWork.EIOMortalityReportMemberRepository.Add(mem);
            }
            unitOfWork.EIOMortalityReportRepository.Update(mortality);
            unitOfWork.Commit();
        }

        protected dynamic ConfirmMortalityReport(EIOMortalityReport mortality, JObject request)
        {
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Message.INFO_INCORRECT;
            var member = mortality.EDMortalityReportMembers.FirstOrDefault(
                   e => !e.IsDeleted && e.MemberId != null && e.MemberId == user.Id
               );
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (kind == "Chairman" && positions.Contains("Doctor") && mortality.ChairmanTime == null && mortality.ChairmanId == user.Id)
                mortality.ChairmanTime = DateTime.Now;
            else if (kind == "Secretary" && (positions.Contains("Doctor") || positions.Contains("Nurse")) && mortality.SecretaryTime == null && mortality.SecretaryId == user.Id)
                mortality.SecretaryTime = DateTime.Now;
            else if (kind == "Members")
            {
                if (member == null)
                    return Message.CONFIRM_UNAUTHORIZED;

                if (member.ConfirmTime != null)
                    return Message.OWNER_FORBIDDEN;

                member.ConfirmTime = DateTime.Now;
                unitOfWork.EIOMortalityReportMemberRepository.Update(member);
            }
            else
                return Message.CONFIRM_UNAUTHORIZED;

            unitOfWork.EIOMortalityReportRepository.Update(mortality);
            unitOfWork.Commit();
            return null;
        }

        protected string GenerateStringDiagnosis(DiagnosisAndICDModel diagnosisObject)
        {
            string diagnosis = "";

            if (diagnosisObject != null && diagnosisObject.Diagnosis != "" && diagnosisObject.Diagnosis != null)
            {
                string icds = "";
                string icdOptions = "";
                if (diagnosisObject.ICD != null && diagnosisObject.ICD.Length > 0)
                {
                    List<DiagnosisModel> listICDs = JsonConvert.DeserializeObject<List<DiagnosisModel>>(diagnosisObject.ICD);
                    if (listICDs != null)
                    {
                        icds = string.Join(",", listICDs.Select(e => e.code));
                    }
                }
                if (diagnosisObject.ICDOption != null && diagnosisObject.ICDOption.Length > 0)
                {
                    List<DiagnosisModel> listICDOptions = JsonConvert.DeserializeObject<List<DiagnosisModel>>(diagnosisObject.ICDOption);
                    icdOptions = string.Join(",", listICDOptions.Select(e => e.code));
                }

                diagnosis = $"{(diagnosisObject.Diagnosis ?? "")} ({(icds != "" ? icds : "")})";
            }
            else
            {
                diagnosis = "";
            }

            return diagnosis;
        }
    }
}
