using EForm.Models.IPDModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models
{
    public class VitalSignsForPregnantWomanItem
    {
        public Guid? Id { get; set; }
        public Guid? VisitId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public List<string> Content { get; set; }
        public string MEWSTotal { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? IsYellow { get; set; }
        public VitalSign VitalSign { get; set; }
    }
    public class VitalSign
    {
        private string _NhipTho { get; set; } = "";
        private string _Spo2 { get; set; } = "";
        private string _HuyetApToiThieu { get; set; } = "";
        private string _HuyetApToiDa { get; set; } = "";
        private string _Mach { get; set; } = "";
        private string _ThanNhiet { get; set; } = "";
        private string _TriGiac { get; set; } = "";
        private string _HoTroHoHap { get; set; } = "";
        private string _DiemDau1 { get; set; } = "";
        private string _DiemDau2 { get; set; } = "";
        private string _DuongMauToanPhan1 { get; set; } = "";
        private string _DuongMauToanPhan2 { get; set; } = "";
        private string _DanhGiaVeinTruyen { get; set; } = "";
        private string _DanhGiaVeinTruyenDesc { get; set; } = "";
        private string _TanSoCoTuCung { get; set; } = "";
        private string _TanSoTimThai { get; set; } = "";
        private string _MatDoCoHoiTuCung { get; set; } = "";
        private string _SanDich { get; set; } = "";
        private string _SoLuongDichVaoT { get; set; } = "";
        private string _SoLuongDichVaoP { get; set; } = "";
        private string _SoLuongDichVaoM { get; set; } = "";
        private string _SoLuongDichVaoS { get; set; } = "";
        private string _SoLuongDichVaoAN { get; set; } = "";
        private string _SoLuongDichVaoD { get; set; } = "";
        private string _TongDichVao { get; set; } = "";
        private string _SoLuongDichRaSD { get; set; } = "";
        private string _SoLuongDichRaN { get; set; } = "";
        private string _SoLuongDichRaPh { get; set; } = "";
        private string _SoLuongDichRaNT { get; set; } = "";
        private string _SoLuongDichRaDL { get; set; } = "";
        private string _TongDichRa { get; set; } = "";
        private string _BilanDich { get; set; } = "";
        private string _SuyHoHap { get; set; } = "";
        private string _CanNang { get; set; } = "";
        private string _AnMieng { get; set; } = "";
        private string _AnSonde { get; set; } = "";
        private string _NuocTieu { get; set; } = "";
        private string _Phan { get; set; } = "";
        private string _TruyenDich1 { get; set; } = "";
        private string _TruyenDich2 { get; set; } = "";
        private string _DanhGiaSuyHoHapCoCoRutNgucTren { get; set; } = "";
        private string _DanhGiaSuyHoHapCoCoRutNgucDuoi { get; set; } = "";
        private string _DanhGiaSuyHoHapRutLomHomUc { get; set; } = "";
        private string _DanhGiaSuyHoHapNoCanhMui { get; set; } = "";
        private string _DanhGiaSuyHoHapRen { get; set; } = "";
        private string _MucDoSuyHoHap { get; set; } = "";

        public string NhipTho
        {
            get
            {
                return _NhipTho;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _NhipTho = value;
                }

            }
        }
        public string Spo2
        {
            get
            {
                return _Spo2;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Spo2 = value;
                }
            }
        }
        public string HuyetApToiThieu
        {
            get
            {
                return _HuyetApToiThieu;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _HuyetApToiThieu = value;
                }
            }
        }
        public string HuyetApToiDa
        {
            get
            {
                return _HuyetApToiDa;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _HuyetApToiDa = value;
                }
            }
        }
        public string Mach
        {
            get
            {
                return _Mach;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Mach = value;
                }
            }
        }
        public string ThanNhiet
        {
            get
            {
                return _ThanNhiet;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ThanNhiet = value;
                }
            }
        }
        public string TriGiac
        {
            get
            {
                return _TriGiac;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TriGiac = value;
                }
            }
        }

        public string HoTroHoHap
        {
            get
            {
                return _HoTroHoHap;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _HoTroHoHap = value;
                }
            }
        }
        public string DiemDau1
        {
            get
            {
                return _DiemDau1;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DiemDau1 = value;
                }
            }
        }
        public string DiemDau2
        {
            get
            {
                return _DiemDau2;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DiemDau2 = value;
                }
            }
        }
        public string DuongMauToanPhan1
        {
            get
            {
                return _DuongMauToanPhan1;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DuongMauToanPhan1 = value;
                }
            }
        }
        public string DuongMauToanPhan2
        {
            get
            {
                return _DuongMauToanPhan2;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DuongMauToanPhan2 = value;
                }
            }
        }
        public string DanhGiaVeinTruyen
        {
            get
            {
                return _DanhGiaVeinTruyen;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaVeinTruyen = value;
                }
            }
        }
        public string DanhGiaVeinTruyenDesc
        {
            get
            {
                return _DanhGiaVeinTruyenDesc;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaVeinTruyenDesc = value;
                }
            }
        }
        public string TanSoCoTuCung
        {
            get
            {
                return _TanSoCoTuCung;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TanSoCoTuCung = value;
                }
            }
        }
        public string TanSoTimThai
        {
            get
            {
                return _TanSoTimThai;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TanSoTimThai = value;
                }
            }
        }
        public string MatDoCoHoiTuCung
        {
            get
            {
                return _MatDoCoHoiTuCung;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _MatDoCoHoiTuCung = value;
                }
            }
        }
        public string SanDich
        {
            get
            {
                return _SanDich;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SanDich = value;
                }
            }
        }
        public string SoLuongDichVaoT
        {
            get
            {
                return _SoLuongDichVaoT;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoT = value;
                }
            }
        }
        public string SoLuongDichVaoP
        {
            get
            {
                return _SoLuongDichVaoP;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoP = value;
                }
            }
        }
        public string SoLuongDichVaoM
        {
            get
            {
                return _SoLuongDichVaoM;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoM = value;
                }
            }
        }
        public string SoLuongDichVaoS
        {
            get
            {
                return _SoLuongDichVaoS;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoS = value;
                }
            }
        }
        public string SoLuongDichVaoAN
        {
            get
            {
                return _SoLuongDichVaoAN;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoAN = value;
                }
            }
        }
        public string SoLuongDichVaoD
        {
            get
            {
                return _SoLuongDichVaoD;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichVaoD = value;
                }
            }
        }
        public string TongDichVao
        {
            get
            {
                return _TongDichVao;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TongDichVao = value;
                }
            }
        }
        public string SoLuongDichRaSD
        {
            get
            {
                return _SoLuongDichRaSD;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichRaSD = value;
                }
            }
        }
        public string SoLuongDichRaN
        {
            get
            {
                return _SoLuongDichRaN;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichRaN = value;
                }
            }
        }
        public string SoLuongDichRaPh
        {
            get
            {
                return _SoLuongDichRaPh;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichRaPh = value;
                }
            }
        }
        public string SoLuongDichRaNT
        {
            get
            {
                return _SoLuongDichRaNT;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichRaNT = value;
                }
            }
        }
        public string SoLuongDichRaDL
        {
            get
            {
                return _SoLuongDichRaDL;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SoLuongDichRaDL = value;
                }
            }
        }
        public string TongDichRa
        {
            get
            {
                return _TongDichRa;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TongDichRa = value;
                }
            }
        }
        public string BilanDich
        {
            get
            {
                return _BilanDich;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _BilanDich = value;
                }
            }
        }
        public string SuyHoHap
        {
            get
            {
                return _SuyHoHap;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SuyHoHap = value;
                }
            }
        }
        public string CanNang
        {
            get
            {
                return _CanNang;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _CanNang = value;
                }
            }
        }
        public string AnMieng
        {
            get
            {
                return _AnMieng;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _AnMieng = value;
                }
            }
        }
        public string AnSonde
        {
            get
            {
                return _AnSonde;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _AnSonde = value;
                }
            }
        }

        public string NuocTieu
        {
            get
            {
                return _NuocTieu;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _NuocTieu = value;
                }
            }
        }
        public string Phan
        {
            get
            {
                return _Phan;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Phan  = value;
                }
            }
        }
        public string TruyenDich1
        {
            get
            {
                return _TruyenDich1;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TruyenDich1 = value;
                }
            }
        }
        public string TruyenDich2
        {
            get
            {
                return _TruyenDich2;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TruyenDich2 = value;
                }
            }
        }
        public string DanhGiaSuyHoHapCoCoRutNgucTren
        {
            get
            {
                return _DanhGiaSuyHoHapCoCoRutNgucTren;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaSuyHoHapCoCoRutNgucTren = value;
                }
            }
        }
        public string DanhGiaSuyHoHapCoCoRutNgucDuoi
        {
            get
            {
                return _DanhGiaSuyHoHapCoCoRutNgucDuoi;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaSuyHoHapCoCoRutNgucDuoi = value;
                }
            }
        }
        public string DanhGiaSuyHoHapRutLomHomUc
        {
            get
            {
                return _DanhGiaSuyHoHapRutLomHomUc;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaSuyHoHapRutLomHomUc = value;
                }
            }
        }
        public string DanhGiaSuyHoHapNoCanhMui
        {
            get
            {
                return _DanhGiaSuyHoHapNoCanhMui;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaSuyHoHapNoCanhMui = value;
                }
            }
        }
        public string DanhGiaSuyHoHapRen
        {
            get
            {
                return _DanhGiaSuyHoHapRen;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _DanhGiaSuyHoHapRen = value;
                }
            }
        }
        public string MucDoSuyHoHap
        {
            get
            {
                return _MucDoSuyHoHap;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _MucDoSuyHoHap = value;
                }
            }
        }

    }
}
