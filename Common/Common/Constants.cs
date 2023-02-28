using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public readonly static string[] MALE_SAMPLE = { "Nam", "nam", "Male", "male", "M", "Trai", "T" };
        public readonly static string[] FEMALE_SAMPLE = { "Nữ", "nữ", "Female", "female", "F", "Gái", "G" };

        #region Log
        public readonly static string Log_Type_Info = "Log_Info";
        public readonly static string Log_Type_Debug = "Log_Debug";
        public readonly static string Log_Type_Error = "Log_Error";
        #endregion
        #region Datetime format
        public readonly static string DATE_SQL = "yyyy-MM-dd";
        public readonly static string DATETIME_SQL = "yyyy-MM-dd HH:mm:ss";
        public readonly static string TIME_FORMAT = "HH:mm:ss";
        public readonly static string TIME_FORMAT_WITHOUT_SECOND = "HH:mm";
        public readonly static string TIME_DATE_FORMAT = "HH:mm:ss dd/MM/yyyy";
        public readonly static string TIME_DATE_FORMAT_WITHOUT_SECOND = "HH:mm dd/MM/yyyy";
        public readonly static string MONTH_YEAR_FORMAT = "MM/yyyy";
        public readonly static string YEAR_MONTH_FORMAT = "yyyyMM";
        public readonly static string DATE_FORMAT = "dd/MM/yyyy";
        public readonly static string DATE_TIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        public readonly static string DATE_TIME_FORMAT_WITHOUT_SECOND = "dd/MM/yyyy HH:mm";
        #endregion
        #region DataFormatType
        public readonly static string FM_TABLE = "TABLE";
        public readonly static string FM_EXP_EXCEL = "EXP_EXCEL";
        #endregion .DataFormatType
        #region HIS
        public readonly static Dictionary<string, int> HIS_CODE = new Dictionary<string, int> {
            { "OH", 0 },
            { "EHos", 1 }
        };
        #endregion
        public class ChargeItemType
        {
            public static string Lab = "Lab";
            public static string Rad = "Rad";
            public static string Allies = "Allies";
        };
        public readonly static string VMHC_CODE = "VIHC";

        public readonly static string SERVICE_APIGW = "APIGW";
        public class ChargeItemStatus
        {
            public static string Cancelled = "Cancelled";
            public static string Failed = "Failed";
            public static string Placing = "Placing";
            public static string Placed = "Placed";
            public static string Cancelling = "Cancelling";
            public static string DRSFailed = "DRSFailed";
        };


        public class AlliesRadRequestStatus
        {
            public static string New = "NW";
            public static string Cancel = "CA";
        };

        public class AlliesRadResponseStatus
        {
            public static string Placed = "OK";
            public static string Placing = "PL";
            public static string Cancelling = "CA";
            public static string Cancelled = "CR";
            public static string PlaceFailed = "UA";
            public static string CancelFailed = "UC";
        }

        #region Order
        public readonly static string EOC_STANDING_ORDER = "EOC-StandingOrder";
        public readonly static string OPD_STANDING_ORDER = "OPD-StandingOrder";
        public readonly static string OPD_PATIENT_MEDICATION_LIST = "OPD-PatientMedicationList";
        public readonly static string OPD_RETAIL_SERVICE_ORDER = "OPD-RetailServiceOrder";
        public readonly static string ED_ORDER = "ED-Order";
        public readonly static string ED_PATIENT_MEDICATION_LIST = "ED-PatientMedicationList";
        public readonly static string ED_STANDING_ORDER = "ED-StandingOrder";
        public readonly static string ED_RETAIL_SERVICE_ORDER = "ED-RetailServiceOrder";
        public readonly static string ED_DRUG_WITH_AN_ASTERISK_MARK = "ED-DrugWithAnAsteriskMark";
        public readonly static string ED_PATIENT_MANAGEMENT = "ED-PatientManagement";
        public readonly static string ED_PATIENT_HANDOVER = "ED-PatientHandover";
        public readonly static string IPD_PATIENT_MEDICATION_LIST = "IPD-PatientMedicationList";
        public readonly static string IPD_DRUG_WITH_AN_ASTERISK_MARK = "IPD-DrugWithAnAsteriskMark";
        public readonly static string IPD_ORDER = "IPD-Order";
        public readonly static string IPD_STANDING_ORDER = "IPD-StandingOrder";
        #endregion

        #region Blood purchase supply and transfusion
        public readonly static string EIO_BLOOD_PURCHASE = "EIOBloodPurchaseRequest";
        public readonly static string EIO_BLOOD_SUPPLY = "EIOBloodSupply";
        public readonly static string EIO_BLOOD_TRANS = "EIOBloodTransfusionConfirmation";
        #endregion

        #region Cardiac Arrest Record
        public readonly static string EIO_CAARRE_IF = "Information";
        public readonly static string EIO_CAARRE_VS = "VitalSign";
        public readonly static string EIO_CAARRE_DE = "DefibEnergy";
        public readonly static string EIO_CAARRE_MB = "MedicationBolus";
        public readonly static string EIO_CAARRE_MI = "MedicationInfusion";
        public readonly static string EIO_CAARRE_OT = "Other";
        #endregion

        #region Ambulance Run Report
        public readonly static string ED_ARR_PAMA = "PatientManagement";
        public readonly static string ED_ARR_TRPA = "TransferPatient";
        public readonly static string ED_ARR_PAHA = "PatientHandover";
        #endregion

        #region Translation
        public readonly static Dictionary<int, dynamic> TRANSLATION_STATUS = new Dictionary<int, dynamic>()
        {
            { 0, new { Id = 0, ViName = "Yêu cầu dịch", EnName = "Translation request" } },
            { 1, new { Id = 1, ViName = "Yêu cầu chỉnh sửa", EnName = "Change request" } },
            { 2, new { Id = 2, ViName = "Đang phê duyệt", EnName = "Waiting confirm" } },
            { 3, new { Id = 3, ViName = "Đã phê duyệt", EnName = "Approved" } }
        };
        #endregion

        #region Allergic
        public readonly static Dictionary<string, string> CUSTOMER_ALLERGY_SWITCH = new Dictionary<string, string> {
            {"ETRALLYES", "YES" },
            {"ETRALLNO", "NOO" },
            {"ETRALLNPA", "NPA" },
            {"ETRALLKOA", "KOA" },
            {"ETRALLANS", "ANS" },

            {"EDAFRSPALLYES", "YES" },
            {"EDAFRSPALLNOO", "NOO" },
            {"EDAFRSPALLNPA", "NPA" },
            {"EDAFRSPALLKOA", "KOA" },
            {"EDAFRSPALLANS", "ANS" },

            {"OPDIAFSTOPALLYES", "YES" },
            {"OPDIAFSTOPALLNOO", "NOO" },
            {"OPDIAFSTOPALLNPA", "NPA" },
            {"OPDIAFSTOPALLKOA", "KOA" },
            {"OPDIAFSTOPALLANS", "ANS" },

            {"OPDIAFTPALLYES", "YES" },
            {"OPDIAFTPALLNOO", "NOO" },
            {"OPDIAFTPALLNPA", "NPA" },
            {"OPDIAFTPALLKOA", "KOA" },
            {"OPDIAFTPALLANS", "ANS" },

            {"IPDIAAUALLEYES", "YES" },
            {"IPDIAAUALLENOO", "NOO" },
            {"IPDIAAUALLENPA", "NPA" },
            {"IPDIAAUALLEKOA", "KOA" },
            {"IPDIAAUALLEANS", "ANS" },
        };


        public readonly static Dictionary<string, string> KIND_OF_ALLERGIC = new Dictionary<string, string>()
        {
            { "1", "thực phẩm" },
            { "2", "thời tiết" },
            { "3", "thuốc" },
            { "4", "khác" },
        };

        public readonly static string[] OPD_IAFST_ALLERGIC_CODE = new string[] {
            "OPDIAFSTOPALLYES", "OPDIAFSTOPALLNOO", "OPDIAFSTOPALLNPA", "OPDIAFSTOPALLKOA",  "OPDIAFSTOPALLANS",
        };
        public readonly static string[] OPD_IAFTP_ALLERGIC_CODE = new string[] {
            "OPDIAFTPALLYES",  "OPDIAFTPALLNOO", "OPDIAFTPALLNPA", "OPDIAFTPALLKOA", "OPDIAFTPALLANS",
        };
        public readonly static Dictionary<string, string> OPD_HOC_ALLERGIC_CODE_SWITCH = new Dictionary<string, string>() {
            {"OPDIAFSTOPALLYES", "OPDHOCALLYES" },
            {"OPDIAFSTOPALLNOO", "OPDHOCALLNOO" },
            {"OPDIAFSTOPALLNPA", "OPDHOCALLNPA" },
            {"OPDIAFSTOPALLKOA", "OPDHOCALLKOA" },
            {"OPDIAFSTOPALLANS", "OPDHOCALLANS" },
            {"OPDIAFTPALLYES", "OPDHOCALLYES" },
            {"OPDIAFTPALLNOO", "OPDHOCALLNOO" },
            {"OPDIAFTPALLNPA", "OPDHOCALLNPA" },
            {"OPDIAFTPALLKOA", "OPDHOCALLKOA" },
            {"OPDIAFTPALLANS", "OPDHOCALLANS" },
        };

        public readonly static string[] ED_ETR_ALLERGIC_CODE = new string[] {
            "ETRALLYES", "ETRALLNO", "ETRALLNPA", "ETRALLKOA",  "ETRALLANS",
        };
        public readonly static string[] ED_AFRSP_ALLERGIC_CODE = new string[] {
            "EDAFRSPALLYES",  "EDAFRSPALLNOO", "EDAFRSPALLNPA", "EDAFRSPALLKOA", "EDAFRSPALLANS",
        };
        public readonly static Dictionary<string, string> ED_HOC_ALLERGIC_CODE_SWITCH = new Dictionary<string, string>() {
            {"ETRALLYES", "HOCALLYES" },
            {"ETRALLNO", "HOCALLNOO" },
            {"ETRALLNPA", "HOCALLNPA" },
            {"ETRALLKOA", "HOCALLKOA" },
            {"ETRALLANS", "HOCALLANS" },
            {"EDAFRSPALLYES", "HOCALLYES" },
            {"EDAFRSPALLNOO", "HOCALLNOO" },
            {"EDAFRSPALLNPA", "HOCALLNPA" },
            {"EDAFRSPALLKOA", "HOCALLKOA" },
            {"EDAFRSPALLANS", "HOCALLANS" },
        };

        public readonly static string[] IPD_IAAU_ALLERGIC_CODE = new string[] {
            "IPDIAAUALLEYES",  "IPDIAAUALLENOO", "IPDIAAUALLENPA", "IPDIAAUALLEKOA", "IPDIAAUALLEANS",
        };
        public readonly static Dictionary<string, string> IPD_HOC_ALLERGIC_CODE_SWITCH = new Dictionary<string, string>() {
            {"IPDIAAUALLEYES", "IPDHOCALLYES" },
            {"IPDIAAUALLENOO", "IPDHOCALLNOO" },
            {"IPDIAAUALLENPA", "IPDHOCALLNPA" },
            {"IPDIAAUALLEKOA", "IPDHOCALLKOA" },
            {"IPDIAAUALLEANS", "IPDHOCALLANS" },
        };
        #endregion

        #region Vital sign
        public readonly static string[] OPD_IAFST_VITAL_SIGN_CODE = new string[] {
            "OPDIAFSTOPPULANS", "OPDIAFSTOPTEMANS", "OPDIAFSTOPSPOANS", "OPDIAFSTOPBP0ANS", "OPDIAFSTOPRR0ANS",  "OPDIAFSTOPHEIANS", "OPDIAFSTOPWEIANS"
        };

        public readonly static string[] OPD_IAFTP_VITAL_SIGN_CODE = new string[] {
            "OPDIAFTPPULANS", "OPDIAFTPTEMANS", "OPDIAFTPSPOANS", "OPDIAFTPBP0ANS", "OPDIAFTPRR0ANS",  "OPDIAFTPHEIANS", "OPDIAFTPWEIANS"
        };

        public readonly static Dictionary<string, string> OPD_OEN_VITAL_SIGN_CODE_SWITCH = new Dictionary<string, string>()
        {
            {"OPDIAFSTOPPULANS", "OPDOENPULANS" },
            {"OPDIAFSTOPTEMANS", "OPDOENTEMANS" },
            {"OPDIAFSTOPSPOANS", "OPDOENSPOANS" },
            {"OPDIAFSTOPBP0ANS", "OPDOENBP0ANS" },
            {"OPDIAFSTOPRR0ANS", "OPDOENRR0ANS" },
            {"OPDIAFSTOPHEIANS", "OPDOENHEIANS" },
            {"OPDIAFSTOPWEIANS", "OPDOENWEIANS" },

            {"OPDIAFTPPULANS", "OPDOENPULANS" },
            {"OPDIAFTPTEMANS", "OPDOENTEMANS" },
            {"OPDIAFTPSPOANS", "OPDOENSPOANS" },
            {"OPDIAFTPBP0ANS", "OPDOENBP0ANS" },
            {"OPDIAFTPRR0ANS", "OPDOENRR0ANS" },
            {"OPDIAFTPHEIANS", "OPDOENHEIANS" },
            {"OPDIAFTPWEIANS", "OPDOENWEIANS" },
        };

        #endregion

        #region ED Discharge infomation
        public readonly static string[] ED_UNCHECK_FIELD_STATUS = new string[]
        {
            "OPDWR","EDWR","IPDWR","EOCWR","EDIH","OPDIH","IPDIH","EOCIH","EDNE","OPDNE","EOCNE"
        };
        public readonly static string[] CompleteTreatment = new string[] { "IPDCOTM" };
        public readonly static string[] InterHospitalTransfer = new string[] { "IPDIHT", "EDTFIH", "EOCIHT", "OPDIHT" };
        public readonly static string[] UpstreamDownstreamTransfer = new string[] { "EOCUD", "OPDUDT", "EDUDT", "IPDUDT" };
        public readonly static string[] Discharged = new string[] { "OPDDC", "EOCDD", "EDDC", "IPDDC" };
        public readonly static string[] WaitingResults = new string[] { "OPDWR", "EDWR", "IPDWR", "EOCWR" };
        public readonly static string[] TransferToOPD = new string[] { "EDTFOPD" };
        public readonly static string[] InHospital = new string[] { "EDIH", "OPDIH", "IPDIH", "EOCIH" };
        public readonly static string[] Transfer = new string[] { "IPDTF" };
        public readonly static string[] Admitted = new string[] { "EOCA0", "OPDAM", "EDAM" };
        public readonly static string[] Dead = new string[] { "IPDDD", "EDDD" };
        public readonly static string[] NoExamination = new string[] { "EDNE", "OPDNE", "EOCNE" };
        public readonly static string[] Nonhospitalization = new string[] { "IPDNOEX" };
        public readonly static string[] TransferToED = new string[] { "OPDTTE", "EOCTE" };
        public readonly static string[] DischargedED = new string[] { "DI0DT1", "DI0DT2", "DI0DT3", "DI0DT4" };

        public readonly static string[] ED_DI0_TEXT_CODE = new string[] {
            "DI0RPTANS2", "DI0DIAANS", "DI0DIAICD", "DI0TAPANS", "DI0SM0ANS2", "DI0CS0ANS", "DI0FCPANS", "DI0DR0ANS"
        };
        public readonly static Dictionary<string, dynamic> ED_DI0_XNCC_CODE = new Dictionary<string, dynamic> {
            {"QUES", "DI0COEM"},
            {"ANS", new string[] { "DI0COEMNOO", "DI0COEMYES" } }
        };
        public readonly static Dictionary<string, dynamic> ED_DI0_XNTT_CODE = new Dictionary<string, dynamic> {
            {"QUES", "DI0COIN"},
            {"ANS", new string[] { "DI0COINNO", "DI0COINYES" } }
        };
        public readonly static Dictionary<string, dynamic> ED_DI0_XNTT_IPD_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPECOIN"},
            {"ANS", new string[] { "IPDMRPECOINYES", "IPDMRPECOINNO" } }
        };
        public readonly static string[] ED_DI0_ADMITTED_CODE = new string[] { "DI0RFAANS", "DI0REC1ANS" };
        public readonly static string[] ED_DI0_TRANTOPD_CODE = new string[] { "DI0RFT2ANS", "DI0REC2ANS" };
        public readonly static string[] ED_DI0_INHOTRAN_CODE = new string[] { "DI0RH0ANS", "DI0RFTANS", "DI0NRHANS", "DI0TM0ANS", "DI0NATANS" };
        public readonly static string[] ED_DI0_UPDOTRAN_CODE = new string[] { "DI0RH1ANS", "DI0TM1ANS", "DI0NTMANS", "DI0TD0ANS" };
        public readonly static Dictionary<string, dynamic> ED_DI0_REFOTRAN_CODE = new Dictionary<string, dynamic> {
            {"QUES", "DI0RFT1"},
            {"ANS", new string[] { "DI0RFT1SHT", "DI0RFT1LOG" } }
        };
        #endregion

        #region IPD Medical report
        public readonly static string[] IPD_MERE_PART_2_TEXT_CODE = new string[] {
            "IPDMRPTLDVVANS", "IPDMRPTVANTANS", "IPDMRPTQTBLANS", "IPDMRPTBATHANS", "IPDMRPTGIDIANS", "IPDMRPTTTYTANS",
            "IPDMRPTTUHOANS", "IPDMRPTHOHAANS", "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS", "IPDMRPTCXNCANS", "IPDMRPTTTBAANS",
            "IPDMRPTICDCANS", "IPDMRPTCDBCANS",
        };

        public readonly static string[] IPD_MERE_PART_3_TEXT_CODE = new string[] {
            "IPDMRPEQTBLANS", "IPDMRPETTKQANS", "IPDMRPEPPDTANS", "IPDMRPETCDDANS", "IPDMRPETTNBANS", "IPDMRPEICDCANS",
            "IPDMRPECDBCANS", "IPDMRPEHDTVANS","IPDMRPE1110"
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_PART_3_KQDT_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPEKQDT"},
            {"ANS", new string[] { "IPDMRPEKQDTKHO", "IPDMRPEKQDTDOG", "IPDMRPEKQDTKTD", "IPDMRPEKQDTNAH", "IPDMRPEKQDTCV", "IPDMRPEKQDTTUV" } }
        };

        public readonly static string[] IPD_MERE_GEN_TRANSFER_CODE = new string[] { "IPDMRPTLDCKANS", "IPDMRPTNONHANS" };
        public readonly static string[] IPD_MERE_GEN_DISCHARGE_CODE = new string[] { "IPDMRPTCDRVANS" };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_HTRV_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPTHTRV"},
            {"ANS", new string[] { "IPDMRPTHTRVRAV", "IPDMRPTHTRVXIV", "IPDMRPTHTRVBOV", "IPDMRPTHTRVRVE" } }
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_HTRV2_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPGRV01"},
            {"ANS", new string[] { "IPDMRPGRV02", "IPDMRPGRV03", "IPDMRPGRV04", "IPDMRPGRV05" } }
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_UD_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDRFT1"},
            {"ANS", new string[] { "IPDRFT1LOG", "IPDRFT1SHT"} }
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_UD_CODE2 = new Dictionary<string, dynamic> {
            {"QUES", "IPDRFT1"},
            {"ANS", new string[] { "IPDMRPTCHVITTR", "IPDMRPTCHVICKH", "IPDMRPTCHVITDU" } }
        };
        public readonly static string[] IPD_MERE_GEN_INTER_CODE = new string[] { "IPDNRHANS", "IPDRFTANS", "IPDRH0ANS", "IPDTM0ANS", "IPDNATANS", "IPDMRPTCHVHANS" };
        public readonly static string[] IPD_MERE_GEN_UPDOWN_TRANSFER_CODE = new string[] { "IPDNTMANS", "IPDRH1ANS", "IPDTD0ANS", "IPDTM1ANS" };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_CV_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPTCHVI"},
            {"ANS", new string[] { "IPDMRPTCHVITTR", "IPDMRPTCHVITDU", "IPDMRPTCHVICKH" } }
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_LDCV_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPTLDCV"},
            {"ANS", new string[] { "IPDMRPTLDCVDDK", "IPDMRPTLDCVTYC" } }
        };
        public readonly static string[] IPD_MERE_GEN_DEAD_CODE = new string[] { "IPDMRPTNGTVANS", "IPDMRPTICDTANS", "IPDMRPTBCTVANS" };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_LDTV_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPTLDTV"},
            {"ANS", new string[] { "IPDMRPTLDTVDBE", "IPDMRPTLDTVDTB", "IPDMRPTLDTVKHA" } }
        };
        public readonly static Dictionary<string, dynamic> IPD_MERE_GEN_TTTV_CODE = new Dictionary<string, dynamic> {
            {"QUES", "IPDMRPTTTTV"},
            {"ANS", new string[] { "IPDMRPTTTTVTRC", "IPDMRPTTTTVSAU", "IPDMRPT952", "IPDMRPT951"} }
        };
        public readonly static string[] IPD_MONITOR_FOR_PATIENT = new string[] {
            "IPDMRPTLDVVANS", "IPDMRPTVANTANS"
        };
        #endregion
        #region EOC Code MasterDatas
        public readonly static string[] EOC_DATA_FILL_OEN_MEDICAL_REPORT = { "OPDOENKCKANS", "OPDOENKHHLV2", "OPDOENKTTLV2", "OPDOENKBPKLV2" };
        #endregion

        #region Human Resource Assessment
        public readonly static string[] HUMAN_RESOURCE_STAFF_TYPE = new string[] { "C", "P", "TC" };
        #endregion

        #region Code
        public readonly static string[] ED_FRS_CODE = new string[] {
            "ETRDPH","ETRDPN","ETREDF","ETRFRS","ETRHPS","ETRINT","ETRIOE","ETRIOT",
            "ETRLOC","ETRLUA","ETRPAA","ETRPIO","ETRRBR","ETRRM0","ETRSMA","ETRUTY",
            "ETRDPH1","ETRDPH2","ETRDPH3","ETRDPN1","ETRDPN2","ETRDPN3",
            "ETREDF1","ETREDF2","ETREDF3","ETRFRS1","ETRFRS2","ETRFRS3",
            "ETRHPS1","ETRHPS2","ETRHPS3","ETRIOE1","ETRIOE2","ETRIOE3",
            "ETRIOT0","ETRIOT1","ETRIOT2","ETRIOT3","ETRLOC1","ETRLOC2","ETRLOC3",
            "ETRLUA1","ETRLUA2","ETRLUA3","ETRPAA1","ETRPAA2","ETRPAA3",
            "ETRPIO1","ETRPIO2","ETRPIO3","ETRRBR1","ETRRBR2","ETRRBR3",
            "ETRRM01","ETRRM02","ETRRM03","ETRSMA1","ETRSMA2","ETRSMA3",
            "ETRUTY1","ETRUTY2","ETRUTY3"
        };
        public readonly static string[] ED_ER0_ASS_CODE = new string[] {
            "ER0KTTANS",
            "ER0KTMANS",
            "ER0KHHANS",
            "ER0KCKANS",
            "ER0KCBPKANS"
        };


        public readonly static string[] OPD_OEN_IN_WAITING_PRINCIPAL_TEST_ACCEPT = new string[] {
            "OPDOENPT0ROO","OPDOENPT0ANS","OPDOENDD0ANS","OPDOENICDANS","OPDOENICDOPT","OPDOENTP0ANS","OPDOENRFUANS",
            "OPDOENDORANS","OPDOENRFTANS","OPDOENRH0ANS","OPDOENMTUANS","OPDOENPS0ANS","OPDOENRFT1SHT","OPDOENRFT1LOG",
            "OPDOENTM0ANS","OPDOENNTMANS","OPDOENTD0ANS","OPDOENRECANS","OPDOENRFT2ANS","OPDOENREC2ANS","OPDOENPS2ANS",
            "OPDOENFP2ANS","OPDOENPS1ANS","OPDOENFP1ANS", "TFTEOCANS"
        };
        public readonly static string[] OPD_OEN_READ_ONLY = new string[] {
            "OPDOENPULANS","OPDOENTEMANS","OPDOENBP0ANS","OPDOENRR0ANS","OPDOENHEIANS","OPDOENWEIANS"
        };


        public readonly static string[] IPD_SPECIAL_REQUEST_REMOVEABLE_GROUP = new string[] {
            "IPDIAAUPASC",
            "IPDIAFEHASN",
            "IPDIAFEHAFC",
            "IPDIAFEDICF",
            "IPDIAFEAFPT",
            "IPDIAFEAAIC",
        };
        public readonly static string[] IPD_MEDICAL_RECORD_ASSESSMENT = new string[] {
            "IPDMRPTTTYTANS",
            "IPDMRPTCACQANS",
            "IPDMRPTTUHOANS",
            "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS",
            "IPDMRPTTTNSANS",
            "IPDMRPTTHKIANS",
            "IPDMRPTCOXKANS",
            "IPDMRPTTAMHANS",
            "IPDMRPTRAHMANS",
            "IPDMRPTMMATANS",
            "IPDMRPTNTDDANS"
        };


        public readonly static string[] EIO_AEFET_NURSE_RED_CODE = new string[] {
            "EIOAEFETNTDDRED",
            "EIOAEFETFHDDRED",
            "EIOAEFETDTDDRED",
            "EIOAEFETTMDDRED",
            "EIOAEFETTKDDRED",
            "EIOAEFETPHDDRED",
            "EIOAEFETOBDDRED"
        };
        public readonly static string[] EIO_AEFET_DOCTOR_CODE = new string[] {
            "EIOAEFETNTBS" ,"EIOAEFETNTBSBLU" ,"EIOAEFETNTBSRED",
            "EIOAEFETFHBS" ,"EIOAEFETFHBSNAA" ,"EIOAEFETFHBSBLU", "EIOAEFETFHBSRED",
            "EIOAEFETDTBS" ,"EIOAEFETDTBSBLU" ,"EIOAEFETDTBSRED",
            "EIOAEFETTMBS" ,"EIOAEFETTMBSBLU", "EIOAEFETTMBSRED",
            "EIOAEFETTKBS" ,"EIOAEFETTKBSBLU" ,"EIOAEFETTKBSRED",
            "EIOAEFETPHBS" ,"EIOAEFETPHBSNAA" ,"EIOAEFETPHBSBLU", "EIOAEFETPHBSRED",
            "EIOAEFETOBBS", "EIOAEFETOBBSNAA", "EIOAEFETOBBSBLU", "EIOAEFETOBBSRED"
        };

        public readonly static string[] IPD_MR_CODE_A01_037_050919_V = new string[]
        {
            "IPDMRPTTTYTANS", "IPDMRPTCACQANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS"
        };

        public readonly static string[] IPD_MR_CODE_A01_038_050919_V = new string[]
        {
            "IPDMRPT104", "IPDMRPT105", "IPDMRPT106", "IPDMRPT108",
            "IPDMRPT111", "IPDMRPT113", "IPDMRPT120", "IPDMRPT115",
            "IPDMRPT116", "IPDMRPT117", "IPDMRPT118", "IPDMRPT119",
            "IPDMRPTCACQ", "IPDMRPTHOHAANS", "IPDMRPT124", "IPDMRPT126",
            "IPDMRPT128", "IPDMRPT130", "IPDMRPT132", "IPDMRPT133",
            "IPDMRPT135", "IPDMRPT137"
        };

        public readonly static string[] IPD_MR_CODE_A01_035_050919_V = new string[]
        {
            "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS", "IPDMRPTTIHOANS",
            "IPDMRPTTTNSANS", "IPDMRPT831"
        };

        public readonly static string[] IPD_HISTORY_MR_A01_035_050919_V = new string[]
        {
            "IPDMRPTBATHANS", "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPT831"
        };

        public readonly static string[] IPD_HISTORY_MR_A01_036_050919_V = new string[]
        {
            "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPT929", "IPDMRPT930", "IPDMRPT931",
            "IPDMRPTCACQ", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS", "IPDMRPTTIHOANS",
            "IPDMRPTTHKIANS", "IPDMRPTCOXKANS", "IPDMRPTTTNSANS", "IPDMRPT831"
        };

        public readonly static string[] IPD_COPPY_PARA_FROM_A02_069_080121_VE = new string[]
        {
            "IPDIAAUSPPPARAFULL", "IPDIAAUSPPPARAEARLY",
            "IPDIAAUSPPPARAMISS", "IPDIAAUSPPPARALIVE"
        };

        public readonly static string[] IPD_COPPY_PARA_FROM_A02_016_050919_VE = new string[]
        {
            "IPDOAGIANM219", "IPDOAGIANM221", "IPDOAGIANM223", "IPDOAGIANM225"
        };
        public readonly static string[] IPD_HISTORY_MR_A01_039_050919_V = new string[]
        {
            "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPT10001", "IPDMRPT10037", "IPDMRPTTHKIANS", "IPDMRPTTUHOANS",
            "IPDMRPTHOHAANS", "IPDMRPTTIHOANS", "IPDMRPT10022", "IPDMRPTCOXKANS",
            "IPDMRPTTTNSANS", "IPDMRPTNTDDANS",
            "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };
        public readonly static string[] IPD_HISTORY_MR_A01_040_050919_V = new string[]
        {
            "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPT10024", "IPDMRPT10037", "IPDMRPTTHKIANS", "IPDMRPTTUHOANS",
            "IPDMRPTHOHAANS", "IPDMRPTTIHOANS", "IPDMRPT10022", "IPDMRPTCOXKANS",
            "IPDMRPTTTNSANS", "IPDMRPTNTDDANS",
            "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };
        public readonly static string[] IPD_HISTORY_MR_A01_195_050919_V = new string[]
        {
            "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPTBNK00010001", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS",
            "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };

        public readonly static string[] IPD_REMOVEVALIDATE_A01_036_050919_V = new string[]
        {
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS",
            "IPDMRPTVANTANS"
        };

        public readonly static string[] IPD_HISTORY_MR_A01_196_050919_V = new string[]
        {
            "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPT1003", "IPDMRPT1007",
            "IPDMRPTCACQ", "IPDMRPTTHKIANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS", "IPDMRPTCOXKANS", "IPDMRPT1027", "IPDMRPT1001",
            "IPDMRPT831", "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };

        public readonly static string[] IPD_REMOVEVALIDATE_A01_196_050919_V = new string[]
        {
                "IPDMRPTVANTANS", "IPDMRPTTTNSANS", "IPDMRPTTAMHANS", "IPDMRPTRAHMANS",
                "IPDMRPTMMATANS", "IPDMRPTNTDDANS", "IPDMRPEPPDTANS"
        };

        public readonly static string[] IPD_REMOVEVALIDATE_A01_040_050919_V = new string[]
        {
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS"
        };

        public readonly static string[] IPD_HISTORY_MR_A01_041_050919_V = new string[]
        {
            "IPDMRPT1050", "IPDMRPT1052", "IPDMRPT1056",
            "IPDMRPT1064", "IPDMRPTR1080", "IPDMRPTR1082", "IPDMRPTR1084",
            "IPDMRPTR1086", "IPDMRPTL1080", "IPDMRPTL1082", "IPDMRPTL1084",
            "IPDMRPTL1086", "IPDMRPT1065", "IPDMRPTR1088", "IPDMRPTL1088",
            "IPDMRPT1066", "IPDMRPTR1090", "IPDMRPTL1090", "IPDMRPTR1091",
            "IPDMRPTR1092", "IPDMRPTL1091", "IPDMRPTL1092", "IPDMRPT1067",
            "IPDMRPTR1094", "IPDMRPTR1095", "IPDMRPTL1094", "IPDMRPTL1095",
            "IPDMRPT1068", "IPDMRPTR1097", "IPDMRPTR1098", "IPDMRPTR1099",
            "IPDMRPTR1100", "IPDMRPTR1101", "IPDMRPTR1102", "IPDMRPTR1103",
            "IPDMRPTR1104", "IPDMRPTR1110", "IPDMRPTR1111", "IPDMRPTR1112",
            "IPDMRPTR1113", "IPDMRPTR1105", "IPDMRPTR1106", "IPDMRPTR1107",
            "IPDMRPTR1108", "IPDMRPTR1114", "IPDMRPTR1115", "IPDMRPTR1116",
            "IPDMRPTR1117", "IPDMRPTR1118", "IPDMRPTR1120", "IPDMRPTR1121",
            "IPDMRPTL1097", "IPDMRPTL1098", "IPDMRPTL1099", "IPDMRPTL1100",
            "IPDMRPTL1101", "IPDMRPTL1102", "IPDMRPTL1103", "IPDMRPTL1104",
            "IPDMRPTL1110", "IPDMRPTL1111", "IPDMRPTL1112", "IPDMRPTL1113",
            "IPDMRPTL1105", "IPDMRPTL1106", "IPDMRPTL1107", "IPDMRPTL1108",
            "IPDMRPTL1114", "IPDMRPTL1115", "IPDMRPTL1116", "IPDMRPTL1117",
            "IPDMRPTL1118", "IPDMRPTL1120", "IPDMRPTL1121", "IPDMRPT1069",
            "IPDMRPTR1122", "IPDMRPTR1123", "IPDMRPTR1124", "IPDMRPTR1125",
            "IPDMRPTR1126", "IPDMRPTR1127", "IPDMRPTR1128", "IPDMRPTR1129",
            "IPDMRPTR1130", "IPDMRPTR1132", "IPDMRPTR1133", "IPDMRPTR1134",
            "IPDMRPTR1135", "IPDMRPTR1136", "IPDMRPTR1137", "IPDMRPTR1138",
            "IPDMRPTR1139", "IPDMRPTR1141", "IPDMRPTR1142", "IPDMRPTL1122",
            "IPDMRPTL1123", "IPDMRPTL1124", "IPDMRPTL1125", "IPDMRPTL1126",
            "IPDMRPTL1127", "IPDMRPTL1128", "IPDMRPTL1129", "IPDMRPTL1130",
            "IPDMRPTL1132", "IPDMRPTL1133", "IPDMRPTL1134", "IPDMRPTL1135",
            "IPDMRPTL1136", "IPDMRPTL1137", "IPDMRPTL1138", "IPDMRPTL1139",
            "IPDMRPTL1141", "IPDMRPTL1142", "IPDMRPT1070", "IPDMRPTR1662",
            "IPDMRPTR1663", "IPDMRPTR1664", "IPDMRPTR1665", "IPDMRPTR1666",
            "IPDMRPTR1667", "IPDMRPTR1668", "IPDMRPTR1669", "IPDMRPTR1154",
            "IPDMRPTR1155", "IPDMRPTR1156", "IPDMRPTR1157", "IPDMRPTR1159",
            "IPDMRPTR1160", "IPDMRPTR1161", "IPDMRPTR1162", "IPDMRPTR1163",
            "IPDMRPTR1164", "IPDMRPTR1165", "IPDMRPTR1166", "IPDMRPTR1168",
            "IPDMRPTR1169", "IPDMRPTR1158", "IPDMRPTR1170", "IPDMRPTR1171",
            "IPDMRPTR1172",  "IPDMRPTR1173", "IPDMRPTR1174", "IPDMRPTR1177",
            "IPDMRPTR1178", "IPDMRPTR1179", "IPDMRPTR1181", "IPDMRPTR1182",
            "IPDMRPTR1183", "IPDMRPTR1174", "IPDMRPTR1175", "IPDMRPTL1662",
            "IPDMRPTL1663", "IPDMRPTL1664", "IPDMRPTL1665", "IPDMRPTL1666",
            "IPDMRPTL1667", "IPDMRPTL1668", "IPDMRPTL1669", "IPDMRPTL1154",
            "IPDMRPTL1155", "IPDMRPTL1156", "IPDMRPTL1157", "IPDMRPTL1159",
            "IPDMRPTL1160", "IPDMRPTL1161", "IPDMRPTL1162", "IPDMRPTL1163",
            "IPDMRPTL1164", "IPDMRPTL1165", "IPDMRPTL1166", "IPDMRPTL1168",
            "IPDMRPTL1169", "IPDMRPTL1158", "IPDMRPTL1170", "IPDMRPTL1171",
            "IPDMRPTL1172", "IPDMRPTL1173", "IPDMRPTL1174", "IPDMRPTL1177",
            "IPDMRPTL1178", "IPDMRPTL1179", "IPDMRPTL1181", "IPDMRPTL1182",
            "IPDMRPTL1183", "IPDMRPTL1174", "IPDMRPTL1175", "IPDMRPTR1184",
            "IPDMRPTR1185", "IPDMRPTR1186", "IPDMRPTR1187", "IPDMRPTR1188",
            "IPDMRPTR1189", "IPDMRPTR1190", "IPDMRPTR1191", "IPDMRPTR1192",
            "IPDMRPTL1184", "IPDMRPTL1185", "IPDMRPTL1186", "IPDMRPTL1187",
            "IPDMRPTL1188", "IPDMRPTL1189", "IPDMRPTL1190", "IPDMRPTL1191",
            "IPDMRPTL1192", "IPDMRPTR1194", "IPDMRPTR1195", "IPDMRPTR1196",
            "IPDMRPTR1198", "IPDMRPTR1199", "IPDMRPTR1200", "IPDMRPTR1202",
            "IPDMRPTR1203", "IPDMRPTR1204", "IPDMRPTR1205", "IPDMRPTL1194",
            "IPDMRPTL1195", "IPDMRPTL1196", "IPDMRPTL1198", "IPDMRPTL1199",
            "IPDMRPTL1200", "IPDMRPTL1202", "IPDMRPTL1203", "IPDMRPTL1204",
            "IPDMRPTL1205", "IPDMRPTR1206", "IPDMRPTR1207", "IPDMRPTR1208",
            "IPDMRPTR1209", "IPDMRPTR1210", "IPDMRPTR1211", "IPDMRPTR1212",
            "IPDMRPTR1213", "IPDMRPTR1214", "IPDMRPTR1215", "IPDMRPTR1216",
            "IPDMRPTR1218", "IPDMRPTR1219", "IPDMRPTL1206", "IPDMRPTL1207",
            "IPDMRPTL1208", "IPDMRPTL1209", "IPDMRPTL1210", "IPDMRPTL1211",
            "IPDMRPTL1212", "IPDMRPTL1213", "IPDMRPTL1214", "IPDMRPTL1215",
            "IPDMRPTL1216", "IPDMRPTL1218", "IPDMRPTL1219", "IPDMRPT1071",
            "IPDMRPTR1221", "IPDMRPTR1228", "IPDMRPTR1229", "IPDMRPTR1230",
            "IPDMRPTR1231", "IPDMRPTR1232", "IPDMRPTR1222", "IPDMRPTR1223",
            "IPDMRPTR1224", "IPDMRPTR1225", "IPDMRPTR1226", "IPDMRPTR1648",
            "IPDMRPTL1221", "IPDMRPTL1228", "IPDMRPTL1229", "IPDMRPTL1230",
            "IPDMRPTL1231", "IPDMRPTL1232", "IPDMRPTL1222", "IPDMRPTL1223",
            "IPDMRPTL1224", "IPDMRPTL1225","IPDMRPTL1226", "IPDMRPTL1648",
            "IPDMRPT1072", "IPDMRPTR1651", "IPDMRPTR1652", "IPDMRPTR1653",
            "IPDMRPTR1654", "IPDMRPTR1655", "IPDMRPTR1678", "IPDMRPTR1242",
            "IPDMRPTR1244", "IPDMRPTR1245", "IPDMRPTR1247", "IPDMRPTR1248",
            "IPDMRPTR1250", "IPDMRPTR1252", "IPDMRPTL1651", "IPDMRPTL1652",
            "IPDMRPTL1653", "IPDMRPTL1654", "IPDMRPTL1655", "IPDMRPTL1678",
            "IPDMRPTL1242", "IPDMRPTL1244", "IPDMRPTL1245", "IPDMRPTL1247",
            "IPDMRPTL1248", "IPDMRPTL1250", "IPDMRPTL1252", "IPDMRPT1073",
            "IPDMRPTR1257", "IPDMRPTR1258", "IPDMRPTR1259", "IPDMRPTR1260",
            "IPDMRPTR1261", "IPDMRPTR1262", "IPDMRPTL1257", "IPDMRPTL1258",
            "IPDMRPTL1259", "IPDMRPTL1260", "IPDMRPTL1261", "IPDMRPTL1262",
            "IPDMRPT1074", "IPDMRPTR1264", "IPDMRPTR1266", "IPDMRPTR1267",
            "IPDMRPTR1268", "IPDMRPTR1269", "IPDMRPTR1270", "IPDMRPTR1271",
            "IPDMRPTR1272", "IPDMRPTR1273", "IPDMRPTR1274", "IPDMRPTR1275",
            "IPDMRPTR1276", "IPDMRPTR1277", "IPDMRPTR1279", "IPDMRPTR1280",
            "IPDMRPTL1264", "IPDMRPTL1266", "IPDMRPTL1267", "IPDMRPTL1268",
            "IPDMRPTL1269", "IPDMRPTL1270", "IPDMRPTL1271", "IPDMRPTL1272",
            "IPDMRPTL1273", "IPDMRPTL1274", "IPDMRPTL1275", "IPDMRPTL1276",
            "IPDMRPTL1277", "IPDMRPTL1279", "IPDMRPTL1280", "IPDMRPT1075",
            "IPDMRPTR1282", "IPDMRPTR1284", "IPDMRPTR1285", "IPDMRPTR1287",
            "IPDMRPTR1289", "IPDMRPTR1290", "IPDMRPTR1291", "IPDMRPTR1292",
            "IPDMRPTR1293", "IPDMRPTR1294", "IPDMRPTR1296", "IPDMRPTR1297",
            "IPDMRPTL1282", "IPDMRPTL1284", "IPDMRPTL1285", "IPDMRPTL1287",
            "IPDMRPTL1289", "IPDMRPTL1290", "IPDMRPTL1291", "IPDMRPTL1292",
            "IPDMRPTL1293", "IPDMRPTL1294", "IPDMRPTL1296", "IPDMRPTL1297",
            "IPDMRPT1076", "IPDMRPTR1299", "IPDMRPTR1300", "IPDMRPTR1301",
            "IPDMRPTR1302", "IPDMRPTR1303", "IPDMRPTR1305", "IPDMRPTR1306",
            "IPDMRPTR1307", "IPDMRPTR1308", "IPDMRPTR1309", "IPDMRPTL1299",
            "IPDMRPTL1300", "IPDMRPTL1301", "IPDMRPTL1302", "IPDMRPTL1303",
            "IPDMRPTL1305", "IPDMRPTL1306", "IPDMRPTL1307", "IPDMRPTL1308",
            "IPDMRPTL1309", "IPDMRPT1077", "IPDMRPTR1310", "IPDMRPTR1311",
            "IPDMRPTR1312", "IPDMRPTR1313", "IPDMRPTR1314", "IPDMRPTR1315",
            "IPDMRPTR1316", "IPDMRPTR1317", "IPDMRPTR1318", "IPDMRPTR1319",
            "IPDMRPTR1320", "IPDMRPTR1321", "IPDMRPTR1323", "IPDMRPTR1324",
            "IPDMRPTR1325", "IPDMRPTR1326", "IPDMRPTR1327", "IPDMRPTR1328",
            "IPDMRPTR1329", "IPDMRPTR1330", "IPDMRPTR1331", "IPDMRPTR1333",
            "IPDMRPTR1334", "IPDMRPTR1336", "IPDMRPTR1337", "IPDMRPTL1310",
            "IPDMRPTL1311", "IPDMRPTL1312", "IPDMRPTL1313", "IPDMRPTL1314",
            "IPDMRPTL1315", "IPDMRPTL1316", "IPDMRPTL1317", "IPDMRPTL1318",
            "IPDMRPTL1319", "IPDMRPTL1320", "IPDMRPTL1321", "IPDMRPTL1323",
            "IPDMRPTL1324", "IPDMRPTL1325", "IPDMRPTL1326", "IPDMRPTL1327",
            "IPDMRPTL1328", "IPDMRPTL1329", "IPDMRPTL1330", "IPDMRPTL1331",
            "IPDMRPTL1333", "IPDMRPTL1334", "IPDMRPTL1336", "IPDMRPTL1337",
            "IPDMRPT1078", "IPDMRPTR1339", "IPDMRPTR1340", "IPDMRPTR1341",
            "IPDMRPTL1339", "IPDMRPTL1340", "IPDMRPTL1341", "IPDMRPT1612",
            "IPDMRPT1613", "IPDMRPT1614", "IPDMRPT1615", "IPDMRPT1616",
            "IPDMRPT1617", "IPDMRPT1618", "IPDMRPT1619", "IPDMRPT1620",
            "IPDMRPT1621", "IPDMRPT1622", "IPDMRPT1623", "IPDMRPT1624",
            "IPDMRPT1625", "IPDMRPT1626", "IPDMRPT1627", "IPDMRPT1628",
            "IPDMRPT1629", "IPDMRPT1630", "IPDMRPT1631", "IPDMRPT1632",
            "IPDMRPT1633", "IPDMRPT1634", "IPDMRPT1635", "IPDMRPT1636",
            "IPDMRPT1637", "IPDMRPT1638", "IPDMRPT1639", "IPDMRPT1641",
            "IPDMRPT1607", "IPDMRPT1609", "IPDMRPT1611", "IPDMRPT1612",
            "IPDMRPT1613", "IPDMRPT1614", "IPDMRPT1615", "IPDMRPT1616",
            "IPDMRPT1617", "IPDMRPT1618", "IPDMRPT1619", "IPDMRPT1620",
            "IPDMRPT1621", "IPDMRPT1622", "IPDMRPT1623", "IPDMRPT1624",
            "IPDMRPT1625", "IPDMRPT1626", "IPDMRPT1627", "IPDMRPT1628",
            "IPDMRPT1629", "IPDMRPT1630", "IPDMRPT1631", "IPDMRPT1632",
            "IPDMRPT1633", "IPDMRPT1634", "IPDMRPT1635", "IPDMRPT1636",
            "IPDMRPT1637", "IPDMRPT1638", "IPDMRPT1639", "IPDMRPT1641",
            "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };

        public readonly static string[] IPD_HISTORY_MR_A01_041_050919_V_VESION2 = new string[]
        {

            "IPDMRPT1050", "IPDMRPT1052", "IPDMRPT1054", "IPDMRPT1056",
            "IPDMRPT1064", "IPDMRPTR1080", "IPDMRPTR1082", "IPDMRPTR1084",
            "IPDMRPTR1086", "IPDMRPTL1080", "IPDMRPTL1082", "IPDMRPTL1084",
            "IPDMRPTL1086", "IPDMRPT1065", "IPDMRPTR1088", "IPDMRPTL1088",
            "IPDMRPT1066", "IPDMRPTR1090", "IPDMRPTL1090", "IPDMRPTR1091",
            "IPDMRPTR1092", "IPDMRPTL1091", "IPDMRPTL1092", "IPDMRPT1067",
            "IPDMRPTR1094", "IPDMRPTR1095", "IPDMRPTL1094", "IPDMRPTL1095",
            "IPDMRPT1068", "IPDMRPTR1097", "IPDMRPTR1098", "IPDMRPTR1099",
            "IPDMRPTR1100", "IPDMRPTR1101", "IPDMRPTR1102", "IPDMRPTR1103",
            "IPDMRPTR1104", "IPDMRPTR1110", "IPDMRPTR1111", "IPDMRPTR1112",
            "IPDMRPTR1113", "IPDMRPTR1105", "IPDMRPTR1106", "IPDMRPTR1107",
            "IPDMRPTR1108", "IPDMRPTR1114", "IPDMRPTR1115", "IPDMRPTR1116",
            "IPDMRPTR1117", "IPDMRPTR1118", "IPDMRPTR1120", "IPDMRPTR1121",
            "IPDMRPTL1097", "IPDMRPTL1098", "IPDMRPTL1099", "IPDMRPTL1100",
            "IPDMRPTL1101", "IPDMRPTL1102", "IPDMRPTL1103", "IPDMRPTL1104",
            "IPDMRPTL1110", "IPDMRPTL1111", "IPDMRPTL1112", "IPDMRPTL1113",
            "IPDMRPTL1105", "IPDMRPTL1106", "IPDMRPTL1107", "IPDMRPTL1108",
            "IPDMRPTL1114", "IPDMRPTL1115", "IPDMRPTL1116", "IPDMRPTL1117",
            "IPDMRPTL1118", "IPDMRPTL1120", "IPDMRPTL1121", "IPDMRPT1069",
            "IPDMRPTR1122", "IPDMRPTR1123", "IPDMRPTR1124", "IPDMRPTR1125",
            "IPDMRPTR1126", "IPDMRPTR1127", "IPDMRPTR1128", "IPDMRPTR1129",
            "IPDMRPTR1130", "IPDMRPTR1132", "IPDMRPTR1133", "IPDMRPTR1134",
            "IPDMRPTR1135", "IPDMRPTR1136", "IPDMRPTR1137", "IPDMRPTR1138",
            "IPDMRPTR1139", "IPDMRPTR1141", "IPDMRPTR1142", "IPDMRPTL1122",
            "IPDMRPTL1123", "IPDMRPTL1124", "IPDMRPTL1125", "IPDMRPTL1126",
            "IPDMRPTL1127", "IPDMRPTL1128", "IPDMRPTL1129", "IPDMRPTL1130",
            "IPDMRPTL1132", "IPDMRPTL1133", "IPDMRPTL1134", "IPDMRPTL1135",
            "IPDMRPTL1136", "IPDMRPTL1137", "IPDMRPTL1138", "IPDMRPTL1139",
            "IPDMRPTL1141", "IPDMRPTL1142", "IPDMRPT1070", "IPDMRPTR1662",
            "IPDMRPTR1663", "IPDMRPTR1664", "IPDMRPTR1665", "IPDMRPTR1666",
            "IPDMRPTR1667", "IPDMRPTR1668", "IPDMRPTR1669", "IPDMRPTR1154",
            "IPDMRPTR1155", "IPDMRPTR1156", "IPDMRPTR1157", "IPDMRPTR1159",
            "IPDMRPTR1160", "IPDMRPTR1161", "IPDMRPTR1162", "IPDMRPTR1163",
            "IPDMRPTR1164", "IPDMRPTR1165", "IPDMRPTR1166", "IPDMRPTR1168",
            "IPDMRPTR1169", "IPDMRPTR1158", "IPDMRPTR1170", "IPDMRPTR1171",
            "IPDMRPTR1172",  "IPDMRPTR1173", "IPDMRPTR1174", "IPDMRPTR1177",
            "IPDMRPTR1178", "IPDMRPTR1179", "IPDMRPTR1181", "IPDMRPTR1182",
            "IPDMRPTR1183", "IPDMRPTR1174", "IPDMRPTR1175", "IPDMRPTL1662",
            "IPDMRPTL1663", "IPDMRPTL1664", "IPDMRPTL1665", "IPDMRPTL1666",
            "IPDMRPTL1667", "IPDMRPTL1668", "IPDMRPTL1669", "IPDMRPTL1154",
            "IPDMRPTL1155", "IPDMRPTL1156", "IPDMRPTL1157", "IPDMRPTL1159",
            "IPDMRPTL1160", "IPDMRPTL1161", "IPDMRPTL1162", "IPDMRPTL1163",
            "IPDMRPTL1164", "IPDMRPTL1165", "IPDMRPTL1166", "IPDMRPTL1168",
            "IPDMRPTL1169", "IPDMRPTL1158", "IPDMRPTL1170", "IPDMRPTL1171",
            "IPDMRPTL1172", "IPDMRPTL1173", "IPDMRPTL1174", "IPDMRPTL1177",
            "IPDMRPTL1178", "IPDMRPTL1179", "IPDMRPTL1181", "IPDMRPTL1182",
            "IPDMRPTL1183", "IPDMRPTL1174", "IPDMRPTL1175", "IPDMRPTR1184",
            "IPDMRPTR1185", "IPDMRPTR1186", "IPDMRPTR1187", "IPDMRPTR1188",
            "IPDMRPTR1189", "IPDMRPTR1190", "IPDMRPTR1191", "IPDMRPTR1192",
            "IPDMRPTL1184", "IPDMRPTL1185", "IPDMRPTL1186", "IPDMRPTL1187",
            "IPDMRPTL1188", "IPDMRPTL1189", "IPDMRPTL1190", "IPDMRPTL1191",
            "IPDMRPTL1192", "IPDMRPTR1194", "IPDMRPTR1195", "IPDMRPTR1196",
            "IPDMRPTR1198", "IPDMRPTR1199", "IPDMRPTR1200", "IPDMRPTR1202",
            "IPDMRPTR1203", "IPDMRPTR1204", "IPDMRPTR1205", "IPDMRPTL1194",
            "IPDMRPTL1195", "IPDMRPTL1196", "IPDMRPTL1198", "IPDMRPTL1199",
            "IPDMRPTL1200", "IPDMRPTL1202", "IPDMRPTL1203", "IPDMRPTL1204",
            "IPDMRPTL1205", "IPDMRPTR1206", "IPDMRPTR1207", "IPDMRPTR1208",
            "IPDMRPTR1209", "IPDMRPTR1210", "IPDMRPTR1211", "IPDMRPTR1212",
            "IPDMRPTR1213", "IPDMRPTR1214", "IPDMRPTR1215", "IPDMRPTR1216",
            "IPDMRPTR1218", "IPDMRPTR1219", "IPDMRPTL1206", "IPDMRPTL1207",
            "IPDMRPTL1208", "IPDMRPTL1209", "IPDMRPTL1210", "IPDMRPTL1211",
            "IPDMRPTL1212", "IPDMRPTL1213", "IPDMRPTL1214", "IPDMRPTL1215",
            "IPDMRPTL1216", "IPDMRPTL1218", "IPDMRPTL1219", "IPDMRPT1071",
            "IPDMRPTR1221", "IPDMRPTR1228", "IPDMRPTR1229", "IPDMRPTR1230",
            "IPDMRPTR1231", "IPDMRPTR1232", "IPDMRPTR1222", "IPDMRPTR1223",
            "IPDMRPTR1224", "IPDMRPTR1225", "IPDMRPTR1226", "IPDMRPTR1648",
            "IPDMRPTL1221", "IPDMRPTL1228", "IPDMRPTL1229", "IPDMRPTL1230",
            "IPDMRPTL1231", "IPDMRPTL1232", "IPDMRPTL1222", "IPDMRPTL1223",
            "IPDMRPTL1224", "IPDMRPTL1225","IPDMRPTL1226", "IPDMRPTL1648",
            "IPDMRPT1072", "IPDMRPTR1651", "IPDMRPTR1652", "IPDMRPTR1653",
            "IPDMRPTR1654", "IPDMRPTR1655", "IPDMRPTR1678", "IPDMRPTR1242",
            "IPDMRPTR1244", "IPDMRPTR1245", "IPDMRPTR1247", "IPDMRPTR1248",
            "IPDMRPTR1250", "IPDMRPTR1252", "IPDMRPTL1651", "IPDMRPTL1652",
            "IPDMRPTL1653", "IPDMRPTL1654", "IPDMRPTL1655", "IPDMRPTL1678",
            "IPDMRPTL1242", "IPDMRPTL1244", "IPDMRPTL1245", "IPDMRPTL1247",
            "IPDMRPTL1248", "IPDMRPTL1250", "IPDMRPTL1252", "IPDMRPT1073",
            "IPDMRPTR1257", "IPDMRPTR1258", "IPDMRPTR1259", "IPDMRPTR1260",
            "IPDMRPTR1261", "IPDMRPTR1262", "IPDMRPTL1257", "IPDMRPTL1258",
            "IPDMRPTL1259", "IPDMRPTL1260", "IPDMRPTL1261", "IPDMRPTL1262",
            "IPDMRPT1074", "IPDMRPTR1264", "IPDMRPTR1266", "IPDMRPTR1267",
            "IPDMRPTR1268", "IPDMRPTR1269", "IPDMRPTR1270", "IPDMRPTR1271",
            "IPDMRPTR1272", "IPDMRPTR1273", "IPDMRPTR1274", "IPDMRPTR1275",
            "IPDMRPTR1276", "IPDMRPTR1277", "IPDMRPTR1279", "IPDMRPTR1280",
            "IPDMRPTL1264", "IPDMRPTL1266", "IPDMRPTL1267", "IPDMRPTL1268",
            "IPDMRPTL1269", "IPDMRPTL1270", "IPDMRPTL1271", "IPDMRPTL1272",
            "IPDMRPTL1273", "IPDMRPTL1274", "IPDMRPTL1275", "IPDMRPTL1276",
            "IPDMRPTL1277", "IPDMRPTL1279", "IPDMRPTL1280", "IPDMRPT1075",
            "IPDMRPTR1282", "IPDMRPTR1284", "IPDMRPTR1285", "IPDMRPTR1287",
            "IPDMRPTR1289", "IPDMRPTR1290", "IPDMRPTR1291", "IPDMRPTR1292",
            "IPDMRPTR1293", "IPDMRPTR1294", "IPDMRPTR1296", "IPDMRPTR1297",
            "IPDMRPTL1282", "IPDMRPTL1284", "IPDMRPTL1285", "IPDMRPTL1287",
            "IPDMRPTL1289", "IPDMRPTL1290", "IPDMRPTL1291", "IPDMRPTL1292",
            "IPDMRPTL1293", "IPDMRPTL1294", "IPDMRPTL1296", "IPDMRPTL1297",
            "IPDMRPT1076", "IPDMRPTR1299", "IPDMRPTR1300", "IPDMRPTR1301",
            "IPDMRPTR1302", "IPDMRPTR1303", "IPDMRPTR1305", "IPDMRPTR1306",
            "IPDMRPTR1307", "IPDMRPTR1308", "IPDMRPTR1309", "IPDMRPTL1299",
            "IPDMRPTL1300", "IPDMRPTL1301", "IPDMRPTL1302", "IPDMRPTL1303",
            "IPDMRPTL1305", "IPDMRPTL1306", "IPDMRPTL1307", "IPDMRPTL1308",
            "IPDMRPTL1309", "IPDMRPT1077", "IPDMRPTR1310", "IPDMRPTR1311",
            "IPDMRPTR1312", "IPDMRPTR1313", "IPDMRPTR1314", "IPDMRPTR1315",
            "IPDMRPTR1316", "IPDMRPTR1317", "IPDMRPTR1318", "IPDMRPTR1319",
            "IPDMRPTR1320", "IPDMRPTR1321", "IPDMRPTR1323", "IPDMRPTR1324",
            "IPDMRPTR1325", "IPDMRPTR1326", "IPDMRPTR1327", "IPDMRPTR1328",
            "IPDMRPTR1329", "IPDMRPTR1330", "IPDMRPTR1331", "IPDMRPTR1333",
            "IPDMRPTR1334", "IPDMRPTR1336", "IPDMRPTR1337", "IPDMRPTL1310",
            "IPDMRPTL1311", "IPDMRPTL1312", "IPDMRPTL1313", "IPDMRPTL1314",
            "IPDMRPTL1315", "IPDMRPTL1316", "IPDMRPTL1317", "IPDMRPTL1318",
            "IPDMRPTL1319", "IPDMRPTL1320", "IPDMRPTL1321", "IPDMRPTL1323",
            "IPDMRPTL1324", "IPDMRPTL1325", "IPDMRPTL1326", "IPDMRPTL1327",
            "IPDMRPTL1328", "IPDMRPTL1329", "IPDMRPTL1330", "IPDMRPTL1331",
            "IPDMRPTL1333", "IPDMRPTL1334", "IPDMRPTL1336", "IPDMRPTL1337",
            "IPDMRPT1078", "IPDMRPTR1339", "IPDMRPTR1340", "IPDMRPTR1341",
            "IPDMRPTL1339", "IPDMRPTL1340", "IPDMRPTL1341", "IPDMRPT1612",
            "IPDMRPT1613", "IPDMRPT1614", "IPDMRPT1615", "IPDMRPT1616",
            "IPDMRPT1617", "IPDMRPT1618", "IPDMRPT1619", "IPDMRPT1620",
            "IPDMRPT1621", "IPDMRPT1622", "IPDMRPT1623", "IPDMRPT1624",
            "IPDMRPT1625", "IPDMRPT1626", "IPDMRPT1627", "IPDMRPT1628",
            "IPDMRPT1629", "IPDMRPT1630", "IPDMRPT1631", "IPDMRPT1632",
            "IPDMRPT1633", "IPDMRPT1634", "IPDMRPT1635", "IPDMRPT1636",
            "IPDMRPT1637", "IPDMRPT1638", "IPDMRPT1639", "IPDMRPT1641",
            "IPDMRPT1607", "IPDMRPT1609", "IPDMRPT1611", "IPDMRPT1612",
            "IPDMRPT1613", "IPDMRPT1614", "IPDMRPT1615", "IPDMRPT1616",
            "IPDMRPT1617", "IPDMRPT1618", "IPDMRPT1619", "IPDMRPT1620",
            "IPDMRPT1621", "IPDMRPT1622", "IPDMRPT1623", "IPDMRPT1624",
            "IPDMRPT1625", "IPDMRPT1626", "IPDMRPT1627", "IPDMRPT1628",
            "IPDMRPT1629", "IPDMRPT1630", "IPDMRPT1631", "IPDMRPT1632",
            "IPDMRPT1633", "IPDMRPT1634", "IPDMRPT1635", "IPDMRPT1636",
            "IPDMRPT1637", "IPDMRPT1638", "IPDMRPT1639", "IPDMRPT1641",
            "IPDMRPTCXNCANS", "IPDMRPTTTBAANS"
        };

        public readonly static string[] IPD_REMOVEVALIDATE_A01_039_050919_V = new string[]
        {
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS"
        };

        public readonly static string[] IPD_REMOVEVALIDATE_A01_041_050919_V = new string[]
        {
            "IPDMRPTVANTANS", "IPDMRPTQTBLANS", "IPDMRPTBATHANS", "IPDMRPTGIDIANS",
            "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS", "IPDMRPTTIHOANS",
            "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS", "IPDMRPTTAMHANS",
            "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS"
        };
        public readonly static string[] IPD_HISTORY_MR_BMTIMMACH = new string[]
        {
            "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
            "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
            "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS"
        };
        #endregion

        #region session
        public readonly static string[] IGNORE_EXTEND_SESSION_PATH = { "/api/notification/", };
        public readonly static string[] IGNORE_UPDATE_SESSION_PATH = {
            "/api/account/login/",
            "/api/account/logout/",
            "/api/user/choosesite/",
            "/api/user/info/"
        };
        #endregion

        #region Log
        public readonly static string[] LOG_CHANGE_SENSITIVE = {
            "login", "/confirm", "/accept", "/confirms", "nurseconfirm", "doctorconfirm"
        };
        public readonly static string[] LOG_CHANGE_FILE = {
            "/sync", "/public",
        };
        public readonly static string[] IGNORE_WRITE_LOG =
        {
            "notification", "icd10", "/logs", "/uploadfile", "api/doctorplacediagnosticsorder/getupdate",
            "doctorplacediagnosticsorder/services/getprice", "doctorplacediagnosticsorder/services/getdetailservice",
            "customer/allergy", "customer/heightandweight", "user/choosesite"
        };
        #endregion

        #region Service
        #endregion

        #region DiseasesScreening
        public readonly static Dictionary<int, dynamic> DISEASES_SCREENING = new Dictionary<int, dynamic> {
            { 1, new Dictionary<string, string[]>{
                    {
                        "EDEmergencyTriageRecord", new string[] {
                            "ETRAF0YES", "ETRAF0NO",
                            "ETRAROYES", "ETRARONO", "ETRAROOTH",
                            "ETRTOCYES", "ETRTOCNO", "ETRTOCOTH",
                            "ETRSOBYES", "ETRSOBNOO", "ETRSOBOTH",
                            "ETRACCYES", "ETRACCNOO",
                            "ETRDGLNEG", "ETRDGLPOS",
                            "ETRLCLKK0", "ETRLCLGB0", "ETRLCLALA", "ETRLCLOTH", "ETRLCLANS"
                        }
                    },
                    {
                        "EDRetailService", new string[] {
                            "EDAFRSPAFIYES", "EDAFRSPAFINOO",
                            "EDAFRSPAROYES", "EDAFRSPARONOO",
                            "EDAFRSPTOCYES", "EDAFRSPTOCNOO",
                            "EDAFRSPSOBYES", "EDAFRSPSOBNOO",
                            "EDAFRSPACCYES", "EDAFRSPACCNOO",
                            "EDAFRSPDGLNEG", "EDAFRSPDGLPOS",
                            "EDAFRSPLCLKK0", "EDAFRSPLCLGB0", "EDAFRSPLCLALA", "EDAFRSPLCLOTH", "EDAFRSPLCLANS"
                        }
                    },
                    {
                        "OPDShortTerm", new string[] {
                            "OPDIAFSTOPAFIYES", "OPDIAFSTOPAFINOO",
                            "OPDIAFSTOPAROYES", "OPDIAFSTOPARONOO",
                            "OPDIAFSTOPTOCYES", "OPDIAFSTOPTOCNOO",
                            "OPDIAFSTOPSOBYES", "OPDIAFSTOPSOBNOO",
                            "OPDIAFSTOPACCYES", "OPDIAFSTOPACCNOO",
                            "OPDIAFSTOPDGLNEG", "OPDIAFSTOPDGLPOS",
                            "OPDIAFSTOPLCLKK0", "OPDIAFSTOPLCLGB0", "OPDIAFSTOPLCLALA", "OPDIAFSTOPLCLOTH", "OPDIAFSTOPLCLANS"
                        }
                    },
                    {
                        "OPDTelehealth", new string[] {
                            "OPDIAFTPAFIYES", "OPDIAFTPAFINOO",
                            "OPDIAFTPAROYES", "OPDIAFTPARONOO",
                            "OPDIAFTPTOCYES", "OPDIAFTPTOCNOO",
                            "OPDIAFTPSOBYES", "OPDIAFTPSOBNOO",
                            "OPDIAFTPACCYES", "OPDIAFTPACCNOO",
                            "OPDIAFTPDGLNEG", "OPDIAFTPDGLPOS",
                            "OPDIAFTPLCLKK0", "OPDIAFTPLCLGB0", "OPDIAFTPLCLALA", "OPDIAFTPLCLOTH", "OPDIAFTPLCLANS"
                        }
                    },
                    {
                        "IPDAdult", new string[] {
                            "IPDIAAUFERVYES", "IPDIAAUFERVNOO",
                            "IPDIAAURUSLYES", "IPDIAAURUSLNOO",
                            "IPDIAAUTRAVYES", "IPDIAAUTRAVNOO",
                            "IPDIAAUCOUGYES", "IPDIAAUCOUGNOO",
                            "IPDIAAUCCWSYES", "IPDIAAUCCWSNOO",
                            "IPDIAAUTSIPNEG", "IPDIAAUTSIPPOS",
                            "IPDIAAULOCLCLK", "IPDIAAULOCLGIB", "IPDIAAULOCLCLA", "IPDIAAULOCLOTH", "IPDIAAULOCLANS"
                        }
                    }
                }
            },
            { 2, new Dictionary<string, string[]>{
                    {
                        "EDEmergencyTriageRecord", new string[] {
                            "EDETNOAPYES",
                            "EDETCODOYES", "EDETCODONOO",
                            "EDETCODIYES", "EDETCODINOO",
                            "EDETFOSYFEV", "EDETFOSYCOU", "EDETFOSYRAS", "EDETFOSYDIA", "EDETFOSYNOO",
                            "EDETSCRENEG", "EDETSCREPOS",
                        }
                    },
                    {
                        "EDRetailService", new string[] {
                            "EDRSNOAPYES",
                            "EDRSCODOYES", "EDRSCODONOO",
                            "EDRSCODIYES", "EDRSCODINOO",
                            "EDRSFOSYFEV", "EDRSFOSYCOU", "EDRSFOSYRAS", "EDRSFOSYDIA", "EDRSFOSYNOO",
                            "EDRSSCRENEG", "EDRSSCREPOS",
                        }
                    },
                    {
                        "OPDShortTerm", new string[] {
                            "ODSTNOAPYES",
                            "ODSTCODOYES", "ODSTCODONOO",
                            "ODSTCODIYES", "ODSTCODINOO",
                            "ODSTFOSYFEV", "ODSTFOSYCOU", "ODSTFOSYRAS", "ODSTFOSYDIA", "ODSTFOSYNOO",
                            "ODSTSCRENEG", "ODSTSCREPOS",
                        }
                    },
                    {
                        "OPDTelehealth", new string[] {
                            "ODTHNOAPYES",
                            "ODTHCODOYES", "ODTHCODONOO",
                            "ODTHCODIYES", "ODTHCODINOO",
                            "ODTHFOSYFEV", "ODTHFOSYCOU", "ODTHFOSYRAS", "ODTHFOSYDIA", "ODTHFOSYNOO",
                            "ODTHSCRENEG", "ODTHSCREPOS",
                        }
                    },
                    {
                        "IPDAdult", new string[] {
                            "IDAUNOAPYES",
                            "IDAUCODOYES", "IDAUCODONOO",
                            "IDAUCODIYES", "IDAUCODINOO",
                            "IDAUFOSYFEV", "IDAUFOSYCOU", "IDAUFOSYRAS", "IDAUFOSYDIA", "IDAUFOSYNOO",
                            "IDAUSCRENEG", "IDAUSCREPOS",
                        }
                    }
                }
            },
        };

        #endregion

        #region Ma loai KCB
        public readonly static string[] ML_KCB_NGOAI_TRU = new string[] { "556", "1722", "1723", "1724", "1765", "2372" };
        public readonly static string[] ML_KCB_NOI_TRU = new string[] { "558", "1828" };
        public readonly static string[] ML_KCB_CAP_CUU = new string[] { "557" };
        public readonly static string[] ML_KCB_HC = new string[] { "1330" };
        #endregion

        public readonly static List<dynamic> DIAGNOSIS_OF_INFECTION = new List<dynamic>()
        {
            new { value = 0, ViName = "Sốc nhiễm khuẩn (đường vào)", EnName = "Septic shock (sources)" },
            new { value = 1, ViName = "Viêm phổi", EnName = "Pneumonia" },
            new { value = 2, ViName = "Viêm màng não/NK TKTƯ", EnName = "Meningitis/CNS infection" },
            new { value = 3, ViName = "NK tiết niệu", EnName = "UTI (1)" },
            new { value = 4, ViName = "NK da – mô mềm", EnName = "Skin, Soft – tissue infection" },
            new { value = 5, ViName = "NK ổ bụng", EnName = "Abdominal infection" },
            new { value = 6, ViName = "Nhiễm khuẩn khác", EnName = "Others (detail)" },
        };
        public readonly static List<dynamic> INDICATIONS_OF_HIGHLY_RESTRICTED_ANTIMICROBIALS = new List<dynamic>()
        {
            new { value = 0, ViName = "Nhiễm khuẩn mắc phải tại bệnh viện", EnName = "Hospital-acquired infections" },
            new { value = 1, ViName = "Nhiễm khuẩn nặng/nguy cơ vi khuẩn kháng thuốc ", EnName = "Severe infections/Suspected multidrug resistant pathogens" },
            new { value = 2, ViName = "Poor response to current antibiotics ", EnName = "Meningitis/CNS infection" },
            new { value = 3, ViName = "Điều chỉnh theo kháng sinh đồ ", EnName = "Definitive therapy " },
            new { value = 4, ViName = "Khác (ghi rõ)", EnName = "Others (in detail)" }
        };
        public readonly static string[] EOC_WITH_TRANFER_STATUS_CODE = { "EOCWR", "EOCIH", "EOCNE", "EOCDD" };
        public readonly static string[] EOC_WITH_WITHOUT_OEN_FORM = { "EOCDD", "EOCNE", "EOCWR", "EOCIH" };

        #region IPD Form Code
        public struct IPDFormCode
        {
            public const string DanhGiaBanDau = "IPDIE";
            public const string ChamSocBNCovid = "IPDCSCVID";

            public const string DanhGiaNguyCoNga = "IPDFRE";
            public const string BenhAnNoiTru = "IPDMR";

            public const string KeHoachDieuTriChamSoc = "IPDTCP";
            public const string GDSKchoNBvaThanNhan = "IPDGDSK";
            public const string PhieuDieuTri = "IPDTT";
            public const string PhieuChamSoc = "IPDCT";
            //
            public const string TheoDoiDienTien = "IPDPPN";
            public const string TomTatThuThuat = "IPDPS";
            public const string TomTatThuThuatV2 = "IPDPSV2";
            public const string TomTatThuThuatV3 = "IPDPSV3";
            public const string BangHoiSinhTimPhoi = "IPDCAR";
            public const string BienBanHoiChan = "IPDJCGM";
            public const string BienBanHoiChanThongQuaMo = "IPDJCFAOS";
            public const string ThangDiemGUSSRoiLoanNuot = "IPDGSS";
            public const string PhieuChamSocNVCovid = "IPDTOPWC";
            public const string BienBanHoiChanSuDungKSQL = "IPDHRAC";
            //
            public const string BangKiemChuanBiRaVien = "IPDDPC";
            public const string BienBanKiemThaoTuVong = "IPDMORE";


            public const string PhieuDuTruMau = "IPDPDTM";
            public const string PhieuTruyenMau = "IPDPTM";
            public const string BangKiemChuanBiBanGiaoTruocPhauThuat = "IPDBKTPT";
            public const string BienBanHoiChanBenhNhanSuDungThuocCoDauSao = "IPDBBHCTDS";
            public const string BangKiemAnToanPhauThuatThuThuat = "IPDBK";
            public const string PhieuGhiNhanSuDungThuocNguoiBenhMangVao = "IPDMK";
            public const string BienBanBangKiemNguoiBenhChuyenKhoa = "IPDBBBKNBCK";
            public const string BangDanhGiaNhuCauTrangThietBi = "IPDEXTA";


            public const string BangDanhGiaSinhTonNguoiLon = "IPDMEWS";
            public const string GiayXacNhanRaVienKhongTheoChiDinh = "IPDDWC";
            public const string IPDGiayChungNhanThuongTich = "IPDINCERT";
            public const string BradenDanhGiaNguyCoLoet = "IPDBRADENSCALE";
            public const string IPDSurgeryCertificate = "IPDSURCER";
            public const string PhieuSoKet15NgayDieuTri = "IPDSO15DT";
            public const string PhieuKhaiThacTienSuDungThuoc = "IPDMEDHIS";
            public const string IPDGiayChungNhanPhauThuat = "IPDSURCER";
            public const string IPDDanhGiaBanDauTreNoiTruNhi = "IPDINITASSFPI";
            public const string DanhGiaBanDauChoTreVuaSinh = "A02_016_050919_VE";
            public const string DanhGiaBanDauSanPhuChuyenDa = "A02_069_080121_VE";
            public const string DanhGiaNguyCoThuyenTacMach = "IPDTRFA";
            public const string PhieuTheoDoiNguoiBenhThoatMachThuocDieuTriUngThu = "IPDMONITORFPWEOCD";
            public const string DanhGiaNguyCoThuyenTacMachNgoaiKhoa = "A01_049_050919_VE";
            public const string BangTheoDoiDauHieuSinhTonDanhChoSanPhu = "VSFPW";
            public const string DanhGiaNguyCoNgaNBNhi = "A02_047_301220_VE";
            public const string BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh = "IPDNOC";
            public const string PhieuCamKetTruyenMau = "IPDCFTOB";
        }
        #endregion
        public readonly static string[] API_PUBLIC_LOG = {
            "/api/publicapi",
        };
        public class OrderType
        {
            public static string MedicationHistory = "IPD-MedicationHistory";
            public static string TPCN = "IPD-TPCN";
        };
    }
    public class UnlockVipType
    {
        public static string HSBA = "1";
        public static string PlaceDiagnosticsOrder = "2";
        public static string Prescription = "3";
        public static string MedicationAdministration = "4";
        public static string LabAndXrayResults = "5";

    };
}
