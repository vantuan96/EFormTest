using System.Configuration;

namespace Common
{
    public static class ConfigHelper 
    {
        public static string AppName { get { return ConfigurationManager.AppSettings["AppName"] != null ? ConfigurationManager.AppSettings["AppName"].ToString() : string.Empty; } }
        #region CF_SyncOHHCService_C
        public static string CF_SyncOHHCService_C { get { return ConfigurationManager.AppSettings["CF_SyncOHHCService_C"] != null ? ConfigurationManager.AppSettings["CF_SyncOHHCService_C"].ToString() : "0 30 0/1 ? * * *"; } }
        public static bool CF_SyncOHHCService_C_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_SyncOHHCService_C_is_off"] != null && ConfigurationManager.AppSettings["CF_SyncOHHCService_C_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SyncOHHCService_C
        #region CF_ClearOldNotifications_CS
        public static string CF_ClearOldNotifications_CS { get { return ConfigurationManager.AppSettings["CF_ClearOldNotifications_CS"] != null ? ConfigurationManager.AppSettings["CF_ClearOldNotifications_CS"].ToString() : "0 0/15 0-6 * * ?"; } }
        public static bool CF_ClearOldNotifications_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_ClearOldNotifications_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_ClearOldNotifications_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_ClearOldNotifications_CS
        #region CF_MoveLogData_CS
        public static string CF_MoveLogData_CS { get { return ConfigurationManager.AppSettings["CF_MoveLogData_CS"] != null ? ConfigurationManager.AppSettings["CF_MoveLogData_CS"].ToString() : "0 0/5 0-6,18-23 * * ?"; } }
        public static bool CF_MoveLogData_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_MoveLogData_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_MoveLogData_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_MoveLogData_CS
        #region CF_LockVipPatientService_CS
        public static string CF_LockVipPatientService_CS { get { return ConfigurationManager.AppSettings["CF_LockVipPatientService_CS"] != null ? ConfigurationManager.AppSettings["CF_LockVipPatientService_CS"].ToString() : "0 0/45 0/1 ? * * *"; } }
        public static bool CF_LockVipPatientService_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_LockVipPatientService_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_LockVipPatientService_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_LockVipPatientService_CS
        #region CF_SendMailNotifications_CS
        public static string CF_SendMailNotifications_CS { get { return ConfigurationManager.AppSettings["CF_SendMailNotifications_CS"] != null ? ConfigurationManager.AppSettings["CF_SendMailNotifications_CS"].ToString() : "0 0/5 0/1 ? * * *"; } }
        public static bool CF_SendMailNotifications_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_SendMailNotifications_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_SendMailNotifications_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SendMailNotifications_CS
        #region CF_SendNotiToMyVinmec_CS
        public static string CF_SendNotiToMyVinmec_CS { get { return ConfigurationManager.AppSettings["CF_SendNotiToMyVinmec_CS"] != null ? ConfigurationManager.AppSettings["CF_SendNotiToMyVinmec_CS"].ToString() : "0 0/5 0/1 ? * * *"; } }
        public static bool CF_SendNotiToMyVinmec_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_SendNotiToMyVinmec_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_SendNotiToMyVinmec_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SendNotiToMyVinmec_CS
        #region CF_NullData_CS
        public static string CF_NullData_CS { get { return ConfigurationManager.AppSettings["CF_NullData_CS"] != null ? ConfigurationManager.AppSettings["CF_NullData_CS"].ToString() : "0 0/5 0-6,18-23 * * ?"; } }
        //
        public static bool CF_NullData_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_NullData_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_NullData_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_NullData_CS
        #region CF_NotifyAPIGW
        public static string CF_NotifyAPIGW { get { return ConfigurationManager.AppSettings["CF_NotifyAPIGW"] != null ? ConfigurationManager.AppSettings["CF_NotifyAPIGW"].ToString() : "0 1 0 ? * * *"; } }

        public static bool CF_NotifyAPIGW_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_NotifyAPIGW_is_off"] != null && ConfigurationManager.AppSettings["CF_NotifyAPIGW_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_NotifyAPIGW
        #region CF_SyncOHArea
        public static string CF_SyncOHArea { get { return ConfigurationManager.AppSettings["CF_SyncOHArea"] != null ? ConfigurationManager.AppSettings["CF_SyncOHArea"].ToString() : "0 15 6,12,18,22 * * ?"; } }

        public static bool CF_SyncOHArea_is_off { 
            get {
                return ConfigurationManager.AppSettings["CF_SyncOHArea_is_off"] != null && ConfigurationManager.AppSettings["CF_SyncOHArea_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SyncOHArea
        #region CF_SyncOHService_CS
        public static string CF_SyncOHService_CS { get { return ConfigurationManager.AppSettings["SyncOHService_CS"] != null ? ConfigurationManager.AppSettings["SyncOHService_CS"].ToString() : "0 0/5 0/1 ? * * *"; } }
        public static bool CF_SyncOHService_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_SyncOHService_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_SyncOHService_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SyncOHService_CS
        #region CF_SyncOHHCPathologyMicrobiologyService_C
        public static string CF_SyncOHHCPathologyMicrobiologyService_C { get { return ConfigurationManager.AppSettings["CF_SyncOHHCPathologyMicrobiologyService_C"] != null ? ConfigurationManager.AppSettings["CF_SyncOHHCPathologyMicrobiologyService_C"].ToString() : "0 30 0/1 ? * * *"; } }
        public static bool CF_SyncOHHCPathologyMicrobiologyService_CS_is_off
        {
            get
            {
                return ConfigurationManager.AppSettings["CF_SyncOHHCPathologyMicrobiologyService_CS_is_off"] != null && ConfigurationManager.AppSettings["CF_SyncOHHCPathologyMicrobiologyService_CS_is_off"].ToString() == "Off";
            }
        }
        #endregion CF_SyncOHHCPathologyMicrobiologyService_C
    }
}
