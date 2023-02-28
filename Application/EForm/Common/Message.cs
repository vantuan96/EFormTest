namespace EForm.Common
{
    public class Message
    {
        #region General
        public readonly static dynamic COCS_IS_EXIST = new { ViMessage = "Tóm tắt ca bệnh phức tạp cho lượt khám này đã tồn tại", EnMessage = "Complex outpatient case summary is exist" };
        public readonly static dynamic COCS_NOT_FOUND = new { ViMessage = "Tóm tắt ca bệnh phức tạp cho lượt khám này không tồn tại", EnMessage = "Complex outpatient case summary is not exist" };
        public readonly static dynamic CONFIRM_UNAUTHORIZED = new { ViMessage = "Không thể xác nhận được do thành viên này không nằm trong danh sách", EnMessage = "Không thể xác nhận được do thành viên này không nằm trong danh sách" };
        public readonly static dynamic CUSTOMER_IS_EXIST = new { ViMessage = "Khách hàng đã tồn tại trong hệ thống", EnMessage = "Customer is exist" };
        public readonly static dynamic CUSTOMER_IS_NOT_CHRONIC = new { ViMessage = "Bệnh nhân không có bệnh mãn tính", EnMessage = "Patient is not chronic" };
        public readonly static dynamic CUSTOMER_NOT_FOUND = new { ViMessage = "Khách hàng không tồn tại", EnMessage = "Customer is not found" };
        public readonly static dynamic CSRF_MISSING = new { ViMessage = "Thiếu CSRF token", EnMessage = "CSRF token missing" };
        public readonly static dynamic DATA_NOT_FOUND = new { ViMessage = "Dữ liệu không tồn tại", EnMessage = "Internal server error" };
        public readonly static dynamic DELETE_FORBIDDEN = new { ViMessage = "Không được phép xóa", EnMessage = "Deletion not allowed" };
        public readonly static dynamic DOCTOR_ACCEPTED = new { ViMessage = "Bác sĩ đã xác nhận", EnMessage = "Doctor accepted" };
        public readonly static dynamic DOCTOR_IS_UNAUTHORIZED = new { ViMessage = "Bác sĩ chưa được phân quyền trên hệ thống", EnMessage = "Doctor is unauthorized" };
        public readonly static dynamic EHOS_ACCOUNT_MISSING = new { ViMessage = "Chưa cập nhật tài khoản EHOS của bác sĩ", EnMessage = "EHOS account is missing" };
        public readonly static dynamic FORBIDDEN = new { ViMessage = "Bạn không có quyền truy cập", EnMessage = "You do NOT have permission to access", ActionCode = "" };
        public readonly static dynamic FORMAT_INVALID = new { ViMessage = "Dữ liệu sai định dạng", EnMessage = "Format is NOT correct" };
        public readonly static dynamic FORMAT_ACCOUNT_SPE_INVALID = new { ViMessage = "Dữ liệu sai định dạng, Bạn đang ở khu vực khám khác", EnMessage = "Format is NOT correct" };
        public readonly static dynamic HOCL_NOT_FOUND = new { ViMessage = "Bảng kiểm bàn giao người bệnh chuyển khoa không tồn tại", EnMessage = "Hand over check list is not found" };
        public readonly static dynamic INFO_INCORRECT = new { ViMessage = "Thông tin xác nhận không đúng", EnMessage = "Information is incorrect" };
        public readonly static dynamic INTERAL_SERVER_ERROR = new { ViMessage = "Có lỗi xảy ra", EnMessage = "Internal server error" };
        public readonly static dynamic LOGIN_ERROR = new { ViMessage = "Thông tin đăng nhập chưa đúng", EnMessage = "Login information is incorrect" };        public readonly static dynamic NOTHING_CHANGE = new { ViMessage = "Không có thay đổi", EnMessage = "Nothing change" };
        public readonly static dynamic NURSE_ACCEPTED = new { ViMessage = "Điều dưỡng đã xác nhận", EnMessage = "Nurse accepted" };
        public readonly static dynamic ORDER_NOT_FOUND = new { ViMessage = "Y lệnh không tồn tại", EnMessage = "Order is not found" };
        public readonly static dynamic OWNER_FORBIDDEN = new { ViMessage = "Bạn không có quyền chỉnh sửa", EnMessage = "You do NOT have permission to update" };
        public readonly static dynamic PID_IS_MISSING = new { ViMessage = "Mã khách hàng(PID) bị thiếu", EnMessage = "PID is missing" };
        public readonly static dynamic PID_INVALID = new { ViMessage = "Mã khách hàng(PID) không hợp lệ", EnMessage = "PID is invalid" };
        public readonly static dynamic PRIMARY_DOCTOR_NOT_FOUND = new { ViMessage = "Chưa gán bác sĩ", EnMessage = "Doctor is not found" };
        public readonly static dynamic POSITION_NOT_FOUND = new { ViMessage = "Chưa có chức vị", EnMessage = "Position is not found" };
        public readonly static dynamic RESULTOFPARACLINICALTESTS_NOT_FOUND = new { ViMessage = "Không có kết quả lâm sàng", EnMessage = "There is no result of paraclinical tests" };
        public readonly static dynamic SIGNIFICANT_MEDICATION_NOT_FOUND = new { ViMessage = "Không có thuốc đã dùng", EnMessage = "There is no significant medications" };
        public readonly static dynamic STATUS_NOT_FOUND = new { ViMessage = "Trạng thái không tồn tại", EnMessage = "Status is not found" };
        public readonly static dynamic SUCCESS = new { ViMessage = "Thành công", EnMessage = "Success" };
        public readonly static dynamic SYNCHRONIZED_ERROR = new { ViMessage = "Bệnh nhân đã đồng bộ", EnMessage = "Customer is synchronized" };
        public readonly static dynamic SYNC_NOT_FOUND = new { ViMessage = "Không có dữ liệu", EnMessage = "Không có dữ liệu gần nhất" };
        public readonly static dynamic SYNC_24H_NOT_FOUND = new { ViMessage = "Không có dữ liệu gần nhất trong vòng 24 giờ", EnMessage = "Không có dữ liệu gần nhất trong vòng 24 giờ" };
        public readonly static dynamic TIME_FORBIDDEN = new { ViMessage = "Đã quá 24h kể từ khi Tạo hồ sơ. Bạn không có quyền chỉnh sửa thông tin của hồ sơ này.", EnMessage = "It has been more than 24 hours since the patient was admitted to emergency. You do not have permission to edit this patient information" };
        public readonly static dynamic IPD_TIME_FORBIDDEN = new { ViMessage = "Đã quá 24h kể từ khi BN ra viện. Bạn không có quyền chỉnh sửa thông tin của hồ sơ này.", EnMessage = "It has been more than 24 hours since the patient was admitted to emergency. You do not have permission to edit this patient information" };
        public readonly static dynamic TRANSFER_ERROR = new { ViMessage = "Yêu cầu nhập nơi chuyển", EnMessage = "Receiving is empty" };
        public readonly static dynamic UNAUTHORIZED = new { ViMessage = "Bạn chưa đăng nhập", EnMessage = "Please login" };
        public readonly static dynamic USER_NOT_FOUND = new { ViMessage = "User không tồn tại", EnMessage = "User is NOT found" };
        public readonly static dynamic VISIT_CODE_IS_MISSING = new { ViMessage = "Mã tiếp nhận(Visit code) bị thiếu", EnMessage = "Visit code is missing" };
        public readonly static dynamic VISIT_NOT_FOUND = new { ViMessage = "Lượt khám này không tồn tại", EnMessage = "Visit is not exist" };
        public readonly static dynamic PLACING_ORDER_VISIT_IS_CLOSED = new { ViMessage = "Lượt khám này đã đóng, vui lòng sử dụng lượt khám khác để thực hiện chỉ định.", EnMessage = "Visit is closed already, please use a new one to place order." };
        public readonly static dynamic CANCELLING_ORDER_VISIT_IS_CLOSED = new { ViMessage = "Bạn không phải là người chỉ định nên không thể thực hiện hiện hủy chỉ định.", EnMessage = "Lượt khám này đã đóng, hoặc bạn không phải là người chỉ định nên không thể thực hiện hiện hủy chỉ định." };
        public readonly static dynamic HAS_OEN_DOCTOR = new { ViMessage = "Bác sĩ đã thực hiện khám bệnh cho bệnh nhân", EnMessage = "Bác sĩ đã thực hiện khám bệnh cho bệnh nhân" };
        public readonly static dynamic TRANSLATION_EXIST = new { ViMessage = "Yêu cầu dịch đang được xử lý", EnMessage = "Yeu cau dich dan duoc xu ly" };
        public readonly static dynamic TRANSLATION_NOT_FOUND = new { ViMessage = "Bản dịch không tồn tại", EnMessage = "Translation is NOT exist" };
        public readonly static dynamic TRANSLATION_IS_WAITING_TRANSLATE = new { ViMessage = "Yêu cầu đã được gửi", EnMessage = "Request sent" };
        public readonly static dynamic TRANSLATION_IS_CONFIRMED = new { ViMessage = "Bản dịch đã phê duyệt", EnMessage = "The translation is confirmed " };
        public readonly static dynamic NOTI_NOT_FOUND = new { ViMessage = "Thông báo không tồn tại", EnMessage = "Notification is not found" };
        public readonly static dynamic FORM_NOT_FOUND = new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found", NeedNew = true };
        public readonly static dynamic FORM_NOT_FOUND_WITH_LOCKED = new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found", IsLocked = true };
        public readonly static dynamic TRANSFER_EXIST = new { ViMessage = "Yêu cầu tiếp nhận tồn tại", EnMessage = "Yêu cầu tiếp nhận tồn tại", TransferExit = true };
        public readonly static dynamic FORM_EXIST = new { ViMessage = "Form đã tồn tại", EnMessage = "Form is exited" };
        public readonly static dynamic HUMAN_RESOURCE_NOT_FOUND = new { ViMessage = "Thông báo không tồn tại", EnMessage = "Notification is not found" };
        public readonly static dynamic FORM_IS_LOCKED = new { ViMessage = "Hồ sơ đã bị khóa", EnMessage = "Form is locked" };
        public readonly static dynamic VISIT_FORBIDDEN = new { ViMessage = "Bạn không có quyền chỉnh sửa, Hồ sơ đã được tiếp nhận bởi cả bác sĩ", EnMessage = "You do NOT have permission to update" };
        public readonly static dynamic VISIT_HAS_FORM = new { ViMessage = "Người bệnh đã được làm hồ sơ bệnh án, bạn không được phép xóa hồ sơ này", EnMessage = "You do NOT have permission to update" };
        public readonly static dynamic SPECIMEN_STATUS_ERROR = new { ViMessage = "Mẫu xét nghiệm đã được tiếp nhận, bạn không thể thực hiện thao tác này", EnMessage = "Mẫu xét nghiệm đã được tiếp nhận, bạn không thể thực hiện thao tác này" };

        #endregion

        #region ED
        public readonly static dynamic ED_NOT_FOUND = new
        {
            ViMessage = "ED không tồn tại",
            EnMessage = "ED is not found"
        };
        public readonly static dynamic ED_ETR_NOT_FOUND = new
        {
            ViMessage = "Phân loại cấp cứu không tồn tại",
            EnMessage = "Emergency triage record is not found"
        };
        public readonly static dynamic ED_ER0_NOT_FOUND = new
        {
            ViMessage = "Bệnh án cấp cứu không tồn tại",
            EnMessage = "Emergency record is not found"
        };
        public readonly static dynamic ED_DI0_NOT_FOUND = new
        {
            ViMessage = "Đánh giá kết thúc không tồn tại",
            EnMessage = "Discharge information is not found"
        };
        public readonly static dynamic ED_OC0_NOT_FOUND = new
        {
            ViMessage = "Bảng theo dõi không tồn tại",
            EnMessage = "Observation chart is not found"
        };
        public readonly static dynamic ED_PPN_NOT_FOUND = new
        {
            ViMessage = "Theo dõi diến biến không tồn tại",
            EnMessage = "Patient progress notes is not found"
        };
        public readonly static dynamic ED_MCA_NOT_FOUND = new
        {
            ViMessage = "Phiếu theo dõi và bàn giao người bệnh vận chuyển không tồn tại",
            EnMessage = "Monitoring chart and handover form for patients being transferred via ambulance is not found"
        };

        public readonly static dynamic ED_AGBT_NOT_FOUND = new
        {
            ViMessage = "Phiếu XN khí máu động mạch không tồn tại",
            EnMessage = "Arterial blood gas test is not found"
        };
        public readonly static dynamic ED_AGBT_EXIST = new
        {
            ViMessage = "Phiếu XN khí máu động mạch đã tồn tại",
            EnMessage = "Arterial blood gas test is exist"
        };
        public readonly static dynamic ED_CBT_NOT_FOUND = new
        {
            ViMessage = "Phiếu XN sinh hóa máu không tồn tại",
            EnMessage = "Chemical Biology test is not found"
        };
        public readonly static dynamic ED_CBT_EXIST = new
        {
            ViMessage = "Phiếu XN sinh hóa máu đã tồn tại",
            EnMessage = "Chemical Biology test is exist"
        };
        public readonly static dynamic ED_CONFIRM_PLS = new
        {
            ViMessage = "Cần xác nhận chỉ định thực hiện thuốc / y lệnh miệng / kết quả test da",
            EnMessage = "You must confirm standing order form"
        };
        public readonly static dynamic CONFIRM_STANDING_ORDER = new
        {
            ViMessage = "Cần xác nhận ghi nhận thực hiện thuốc standing order",
            EnMessage = "You must confirm standing order form"
        };
        public readonly static dynamic CONFIRM_ORDER = new
        {
            ViMessage = "Cần xác nhận ghi nhận y lệnh miệng",
            EnMessage = "You must confirm order form"
        };
        public readonly static dynamic CONFIRM_SKIN_TEST = new
        {
            ViMessage = "Cần xác nhận kết quả test da",
            EnMessage = "You must confirm skin test form"
        };
        public readonly static dynamic ED_ETR_PROHIBIT = new
        {
            ViMessage = "Bác sĩ đã hoàn thành đánh giá kết thúc cho bệnh nhân. Bạn không thể chỉnh sửa phân loại cấp cứu",
            EnMessage = "The doctor has finished discharge information. You can not edit emergency triage record"
        };

        public readonly static dynamic ED_CDWAAM_NOT_FOUND = new
        {
            ViMessage = "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*) không tồn tại",
            EnMessage = "Minutes of consultation for patient using drug with an asterisk mark(*) is not found"
        };
        public readonly static dynamic ED_CDWAAM_EXIST = new
        {
            ViMessage = "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*) đã tồn tại",
            EnMessage = "Minutes of consultation for patient using drug with an asterisk mark(*) is exist"
        };


        public readonly static dynamic ED_MORE_NOT_FOUND = new
        {
            ViMessage = "Kiểm thảo tử vong không tồn tại",
            EnMessage = "Mortality report is not found"
        };
        public readonly static dynamic ED_MORE_EXIST = new
        {
            ViMessage = "Kiểm thảo tử vong đã tồn tại",
            EnMessage = "Mortality report is exist"
        };

        public readonly static dynamic ED_POMC_NOT_FOUND = new
        {
            ViMessage = "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào không tồn tại",
            EnMessage = "Patient’s own medications chart is not found"
        };
        public readonly static dynamic ED_POMC_EXIST = new
        {
            ViMessage = "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào đã tồn tại",
            EnMessage = "Patient’s own medications chart is exist"
        };

        public readonly static dynamic ED_EXTA_NOT_FOUND = new
        {
            ViMessage = "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện không tồn tại",
            EnMessage = "External transportation assessment is not found"
        };
        public readonly static dynamic ED_EXTA_EXIST = new
        {
            ViMessage = "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện đã tồn tại",
            EnMessage = "External transportation assessment is exist"
        };

        public readonly static dynamic ED_ARR_NOT_FOUND = new
        {
            ViMessage = "Bệnh án cấp cứu ngoại viện không tồn tại",
            EnMessage = "Ambulance run report is not found"
        };
        public readonly static dynamic ED_ARR_EXIST = new
        {
            ViMessage = "Bệnh án cấp cứu ngoại viện đã tồn tại",
            EnMessage = "Ambulance run report is exist"
        };

        public readonly static dynamic ED_FALL_NOT_FOUND = new
        {
            ViName = "Đánh giá ngã không tồn tại",
            EnName = "Not found"
        };

        #endregion

        #region OPD
        public readonly static dynamic OPD_NOT_FOUND = new
        {
            ViMessage = "OPD không tồn tại",
            EnMessage = "OPD is not found"
        };
        public readonly static dynamic OPD_IAFST_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh ngoại trú thông thường không tồn tại",
            EnMessage = "Initial assessment for short term out-patient is not found"
        };
        public readonly static dynamic OPD_IAFOG_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh ngoại trú dài hạn không tồn tại",
            EnMessage = "Initial assessment for on-going out patient is not found"
        };
        public readonly static dynamic OPD_IAFTH_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người chăm sóc từ xa không tồn tại",
            EnMessage = "Initial assessment for telehealth patient is not found"
        };
        public readonly static dynamic OPD_IAFTH_EXIST = new
        {
            ViMessage = "Đánh giá ban đầu người chăm sóc từ xa đã tồn tại",
            EnMessage = "Initial assessment for telehealth patient is exist"
        };
        public readonly static dynamic OPD_FRS_NOT_FOUND = new
        {
            ViMessage = "Phiếu sàng lọc nguy cơ ngã dành cho bệnh ngoại trú không tồn tại",
            EnMessage = "Fall risk screening for out patient is not found"
        };
        public readonly static dynamic OPD_OC0_NOT_FOUND = new
        {
            ViMessage = "Bảng theo dõi không tồn tại",
            EnMessage = "Observation chart is not found"
        };
        public readonly static dynamic OPD_OEN_NOT_FOUND = new
        {
            ViMessage = "Phiếu khám ngoại trú không tồn tại",
            EnMessage = "Outpatient examination note is not found"
        };
        public readonly static dynamic OPD_OEN_NOT_TRANSFER = new
        {
            ViMessage = "Bệnh nhân không ở trạng thái chuyển khoa",
            EnMessage = "Outpatient examination note is not found"
        };
        public readonly static dynamic OPD_CONFIRM_PLS = new
        {
            ViMessage = "Cần xác nhận chỉ định thực hiện thuốc",
            EnMessage = "You must confirm standing order form"
        };
        public readonly static dynamic OPD_OEN_PROHIBIT = new
        {
            ViMessage = "Bác sĩ đã hoàn thành phiếu khám ngoại trú cho bệnh nhân. Bạn không thể chỉnh sửa đánh giá ban đầu",
            EnMessage = "The doctor has finished outpatient examination. You can not edit initial assessment"
        };
        public readonly static dynamic OPD_LOCK24h = new
        {
            ViMessage = "OPD đã bị khóa",
            EnMessage = "OPD locked"
        };


        #endregion

        #region IPD
        public readonly static dynamic IPD_NOT_FOUND = new
        {
            ViMessage = "IPD không tồn tại",
            EnMessage = "IPD is not found"
        };

        public readonly static dynamic IPD_IAAU_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh ngoại trú thông thường không tồn tại",
            EnMessage = "Initial assessment for adult inpatient is not found"
        };
        public readonly static dynamic IPD_IAAU_EXIST = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh ngoại trú thông thường đã tồn tại",
            EnMessage = "Initial assessment for adult inpatient is exist"
        };
        public readonly static dynamic IPD_IACP_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh truyền hóa chất không tồn tại",
            EnMessage = "Initial assessment for chemotherapy patient is not found"
        };
        public readonly static dynamic IPD_IACP_EXIST = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh truyền hóa chất đã tồn tại",
            EnMessage = "Initial assessment for chemotherapy patient is exist"
        };
        public readonly static dynamic IPD_IAFE_NOT_FOUND = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh cao tuổi, già yếu/cuối đời không tồn tại",
            EnMessage = "Initial assessment for frail elderly/ end-of-life patient is not found"
        };
        public readonly static dynamic IPD_IAFE_EXIST = new
        {
            ViMessage = "Đánh giá ban đầu người bệnh cao tuổi, già yếu/cuối đời đã tồn tại",
            EnMessage = "Initial assessment for frail elderly/ end-of-life patient is exist"
        };

        public readonly static dynamic IPD_MMFS_NOT_FOUND = new
        {
            ViMessage = "Đánh giá nguy cơ ngã ở NB nội trú người lớn không tồn tại",
            EnMessage = "Modified MFS for fall risk assessment in adultinpatients is not found"
        };

        public readonly static dynamic IPD_MOFR_NOT_FOUND = new
        {
            ViMessage = "Bảng đánh giá nguy cơ ngã của thai phụ áp dụng cho nội trú sản không tồn tại",
            EnMessage = "The modified Obstetric Fall Risk Assessment System is not found"
        };

        public readonly static dynamic IPD_MERE_NOT_FOUND = new
        {
            ViMessage = "Bệnh án nội trú không tồn tại",
            EnMessage = "Medical report for inpatient is not found"
        };
        public readonly static dynamic IPD_MERE_EXIST = new
        {
            ViMessage = "Bệnh án nội trú đã tồn tại",
            EnMessage = "Medical report for inpatient is exist"
        };

        public readonly static dynamic IPD_MRPO_NOT_FOUND = new
        {
            ViMessage = "Bệnh án nội trú: hành chính không tồn tại",
            EnMessage = "Medical report for inpatient part 1 is not found"
        };
        public readonly static dynamic IPD_MRPO_EXIST = new
        {
            ViMessage = "Bệnh án nội trú: hành chính đã tồn tại",
            EnMessage = "Medical report for inpatient part 1 is exist"
        };

        public readonly static dynamic IPD_MRPT_NOT_FOUND = new
        {
            ViMessage = "Bệnh án nội trú: bệnh án không tồn tại",
            EnMessage = "Medical report for inpatient part 2 is not found"
        };
        public readonly static dynamic IPD_MRPT_EXIST = new
        {
            ViMessage = "Bệnh án nội trú: bệnh án đã tồn tại",
            EnMessage = "Medical report for inpatient part 2 is exist"
        };

        public readonly static dynamic IPD_MRPE_NOT_FOUND = new
        {
            ViMessage = "Bệnh án nội trú: tóm tắt bệnh án không tồn tại",
            EnMessage = "Medical report for inpatient part 3 is not found"
        };
        public readonly static dynamic IPD_DOC_DISCHA_NOT_FOUND = new
        {
            ViMessage = "Người bệnh không thuộc đối tượng ra viện. Vui lòng hoàn thành bệnh án",
            EnMessage = "Người bệnh không thuộc đối tượng ra viện. Vui lòng hoàn thành bệnh án"
        };
        public readonly static dynamic IPD_MRPE_EXIST = new
        {
            ViMessage = "Bệnh án nội trú: tóm tắt bệnh án đã tồn tại",
            EnMessage = "Medical report for inpatient part 3 is exist"
        };

        public readonly static dynamic IPD_GUSS_NOT_FOUND = new
        {
            ViMessage = "Đánh giá rối loạn nuốt không tồn tại",
            EnMessage = "GUSS is not found"
        };
        public readonly static dynamic IPD_GUSS_EXIST = new
        {
            ViMessage = "Đánh giá rối loạn nuốt đã tồn tại",
            EnMessage = "GUSS is exist"
        };
        public readonly static dynamic IPD_DISCHARGE_PRECHECKLIST_EXIST = new
        {
            ViMessage = "Bảng kiểm chuẩn bị ra viện đã tồn tại",
            EnMessage = "Discharge preparation checklist is existed"
        };
        public readonly static dynamic IPD_DISCHARGE_PRECHECKLIST_NOT_FOUND = new
        {
            ViMessage = "Bảng kiểm chuẩn bị ra viện chưa được tạo",
            EnMessage = "Discharge preparation checklist is not created"
        };

        public readonly static dynamic IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_NOT_FOUND = new
        {
            ViMessage = "Đánh giá nguy cơ thuyên tắc mạch cho BN nội khoa chưa được tạo",
            EnMessage = "Thrombosis risk factor assessment for medical patients is not created"
        };
        public readonly static dynamic IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_FOR_GENERAL_SURGERY_NOT_FOUND = new
        {
            ViMessage = "Đánh giá nguy cơ thuyên tắc mạch cho BN ngoại khoa chưa được tạo",
            EnMessage = "Thrombosis risk factor assessment for general surgery patients is not created"
        };
        public readonly static dynamic IPD_GLAMORGAN_NOT_FOUND = new
        {
            ViMessage = "Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh không tồn tại",
            EnMessage = "Glamorgan Scale for Screening Pressure Score in Neonate and Pediatrics is not found"
        };
        #endregion

        #region EIO
        public readonly static dynamic EIO_JSCM_EXIST = new
        {
            ViMessage = "Biên bản hội chẩn đã tồn tại",
            EnMessage = "Joint consultation group minutes is exists"
        };
        public readonly static dynamic EIO_JSCM_NOT_FOUND = new
        {
            ViMessage = "Biên bản hội chẩn không tồn tại",
            EnMessage = "Joint consultation group minutes is not found"
        };

        public readonly static dynamic EIO_SPSC_EXIST = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật đã tồn tại",
            EnMessage = "Surgical/procedure safety checklist is exists"
        };
        public readonly static dynamic EIO_SPSC_NOT_FOUND = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật không tồn tại",
            EnMessage = "Surgical/procedure safety checklist is not found"
        };

        public readonly static dynamic EIO_SPSCSI_EXIST = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi gây mê/gây tê) đã tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before induction of anaesthesia) is exists"
        };
        public readonly static dynamic EIO_SPSCSI_NOT_FOUND = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi gây mê/gây tê) không tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before induction of anaesthesia) is not found"
        };

        public readonly static dynamic EIO_SPSCTO_EXIST = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi rạch da) đã tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before skin incision) is exists"
        };
        public readonly static dynamic EIO_SPSCTO_NOT_FOUND = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi rạch da) không tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before skin incision) is not found"
        };
        public readonly static dynamic EIO_SPSCTO_CANT_CREATE = new
        {
            ViMessage = "Bạn cần hoàn thiện thông tin 'trước khi gây mê/gây tê' trước khi chuyển sang đánh giá 'trước khi rạch da'",
            EnMessage = "Bạn cần hoàn thiện thông tin 'trước khi gây mê/gây tê' trước khi chuyển sang đánh giá 'trước khi rạch da'",
        };

        public readonly static dynamic EIO_SPSCSO_EXIST = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi người bệnh rời phòng phẫu thuật/thủ thuật) đã tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before patient leaves operating room) is exists"
        };
        public readonly static dynamic EIO_SPSCSO_NOT_FOUND = new
        {
            ViMessage = "Bảng kiểm an toàn phẫu thuật/thủ thuật (trước khi người bệnh rời phòng phẫu thuật/thủ thuật) không tồn tại",
            EnMessage = "Surgical/procedure safety checklist (before patient leaves operating room) is not found"
        };
        public readonly static dynamic EIO_SPSCSO_CANT_CREATE = new
        {
            ViMessage = "Bạn cần hoàn thiện thông tin 'trước khi rạch da' trước khi chuyển sang đánh giá 'trước khi người bệnh rời phòng phẫu thuật/thủ thuật'",
            EnMessage = "Bạn cần hoàn thiện thông tin 'trước khi rạch da' trước khi chuyển sang đánh giá 'trước khi người bệnh rời phòng phẫu thuật/thủ thuật'"
        };

        public readonly static dynamic EIO_BRSAC_EXIST = new
        {
            ViMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu đã tồn tại",
            EnMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu is exists"
        };
        public readonly static dynamic EIO_BRSAC_NOT_FOUND = new
        {
            ViMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu không tồn tại",
            EnMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu is not found"
        };

        public readonly static dynamic EIO_BTC_NOT_FOUND = new
        {
            ViMessage = "Phiếu truyền máu không tồn tại",
            EnMessage = "Blood transfusion checklist is not found"
        };

        public readonly static dynamic EIO_CAARRE_EXIST = new
        {
            ViMessage = "Bảng hồi sinh tim phổi đã tồn tại",
            EnMessage = "Cardiac arrest record is exists"
        };
        public readonly static dynamic EIO_CAARRE_NOT_FOUND = new
        {
            ViMessage = "Bảng hồi sinh tim phổi không tồn tại",
            EnMessage = "Cardiac arrest record is not found"
        };

        public readonly static dynamic EIO_PRSU_NOT_FOUND = new
        {
            ViMessage = "Phiếu phẫu thuật/ thủ thuật không tồn tại",
            EnMessage = "Surgery and procedure Note is not found"
        };
        public readonly static dynamic EIO_PRSU_EXIST = new
        {
            ViMessage = "Phiếu phẫu thuật/ thủ thuật đã tồn tại",
            EnMessage = "Surgery and procedure Note is exists"
        };

        public readonly static dynamic EIO_JCFAOS_NOT_FOUND = new
        {
            ViMessage = "Biên bản hội chẩn thông qua mổ không tồn tại",
            EnMessage = "Joint-Consultation for approval of surgery is not found"
        };
        public readonly static dynamic EIO_JCFAOS_EXIST = new
        {
            ViMessage = "Biên bản hội chẩn thông qua mổ đã tồn tại",
            EnMessage = "Joint-Consultation for approval of surgery is exist"
        };

        public readonly static dynamic EIO_PHC_EXIST = new
        {
            ViMessage = "Bản Kiểm bàn giao người bệnh trước mổ đã tồn tại",
            EnMessage = "Pre-Operative/Procedure handover checklist is exist"
        };
        public readonly static dynamic EIO_PHC_NOT_FOUND = new
        {
            ViMessage = "Bản Kiểm bàn giao người bệnh trước mổ không tồn tại",
            EnMessage = "Pre-Operative/Procedure handover checklist is not found"
        };
        public readonly static dynamic EIO_SSA_EXIST = new
        {
            ViMessage = "Phiếu kiểm gạc và dụng cụ phẫu thuật đã tồn tại",
            EnMessage = "Sponge, shards and instruments counts sheet is exist"
        };
        public readonly static dynamic EIO_SSA_NOT_FOUND = new
        {
            ViMessage = "Phiếu kiểm gạc và dụng cụ phẫu thuật không tồn tại",
            EnMessage = "Sponge, shards and instruments counts sheet is not found"
        };


        public readonly static dynamic EIO_STR_NOT_FOUND = new
        {
            ViMessage = "Kết quả test da không tồn tại",
            EnMessage = "Skin test result is not found"
        };
        public readonly static dynamic EIO_STR_EXIST = new
        {
            ViMessage = "Kết quả test da đã tồn tại",
            EnMessage = "Skin test result is exist"
        };

        public readonly static dynamic EIO_AFRS_NOT_FOUND = new
        {
            ViMessage = "Đánh giá NB dịch vụ lẻ không tồn tại",
            EnMessage = "Assessment for retail service patient is not found"
        };
        public readonly static dynamic EIO_TESTCOVID_NOT_FOUND = new
        {
            ViMessage = "Đây không phải là NB test SARS-CoV-2",
            EnMessage = "Đây không phải là NB test SARS-CoV-2"
        };
        public readonly static dynamic EIO_AFRS_EXIST = new
        {
            ViMessage = "Đánh giá NB dịch vụ lẻ đã tồn tại",
            EnMessage = "Assessment for retail service patient is exist"
        };
        public readonly static dynamic EIO_SOFRS_NOT_FOUND = new
        {
            ViMessage = "Ghi nhận thực hiện thuốc NB dịch vụ lẻ không tồn tại",
            EnMessage = "Standing order for retail service patient is not found"
        };
        public readonly static dynamic EIO_PN_NOT_FOUND = new
        {
            ViMessage = "Phiếu điều trị không tồn tại",
            EnMessage = "Physician note is not found"
        };
        public readonly static dynamic EIO_CN_NOT_FOUND = new
        {
            ViMessage = "Phiếu chăm sóc không tồn tại",
            EnMessage = "Care note is not found"
        };

        public readonly static dynamic IPD_MEWS_NOT_FOUND = new
        {
            ViMessage = "Thông tin không tồn tại",
            EnMessage = "Mews can not be found"
        };

        public readonly static dynamic IPD_SURCER_NOT_FOUND = new
        {
            ViMessage = "Giấy chứng nhận phẫu thuật không tồn tại",
            EnMessage = "Surgery certificate can not be found"
        };
        #endregion

        //public readonly static dynamic VISIT_CODE_NOT_FOUNT = new
        //{
        //    ViMessage = "Vui lòng tiếp nhận lượt khám {visit} vào hệ thống EFORM!"
        //}
        public readonly static dynamic PATIENT_NOT_CONFIRM_EMERGENCY = new
        {
            ViMessage = "Bệnh nhân không có xác nhận cấp cứu!",
            EnMessage = "The patient has not confirmed an emergency!"
        };
        public readonly static dynamic PATIENT_NOT_CONFIRM_INJURY = new
        {
            ViMessage = "Bệnh nhân không có xác nhận thương tích!",
            EnMessage = "The patient has not confirmed any injury!"
        };
        public readonly static dynamic NOT_FOUND_MEDICAL_RECORD = new
        {
            ViMessage = "Bệnh án chưa được setup",
            EnMessage = "The medical record has not been set up"
        };
        public readonly static dynamic NOT_FOUND_MEDICAL_RECORD_OF_PATIENT = new
        {
            ViMessage = "Bệnh nhân không có bệnh án",
            EnMessage = "The patient is not sick"
        };
        public readonly static dynamic CONTENT_NOT_FOUND = new { ViMessage = "Nội dung không tồn tại", EnMessage = "Content is not found", NeedNew = true };
    }
}