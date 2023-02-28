//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using Microsoft.Office.Interop.Word;

//namespace Helper.ESign
//{
//    public class ExportWordFile
//    {
//        private Application _wordApp;
//        private Guid _visitId;
//        private Guid? _formId;
//        private string _name;
//        private string _filenameOutput;
//        /// <summary>
//        /// Khởi tạo các thuộc tính của đối tượng
//        /// <br></br>
//        /// <strong>Tham số 1 - Tên Của Biểu mẫu cần export, 2 - visit Id lượt khám của bệnh nhân, 3 - form id của biểu mẫu nếu có</strong>
//        /// </summary>
//        /// <param name="namedocx"></param>
//        /// <param name="visitId"></param>
//        /// <param name="formId"></param>
//        public ExportWordFile(string namedocx, Guid visitId, Guid? formId = null)
//        {
//            this._wordApp = new Application();
//            this._visitId = visitId;
//            this._formId = formId;
//            this._name = namedocx;
//            this._filenameOutput = this._visitId.ToString().ToUpper() + "_" + (formId == null ? "" : formId.ToString() + "_") + this._name  + ".docx";
//        }
//        /// <summary>
//        /// Export ra file .docx theo template có sãn vả replace các keywords trong template. 
//        /// <br></br>
//        /// Tham số vào sẽ là 1 - đường dẫn tuyệt đối của file temlate, 2 - là đường dẫn tuyệt đối đến folder cần lưu file kết quả, 3 - obecj Dictionary gồm key và vaule cần thay thế
//        /// <br></br>
//        /// <strong>Lưu ý tham số thứ 2 directory_destination_folder là đường dẫn tuyết đối đến folder muốn lưu file kết quả, không phải đường dẫn của cả file</strong>        
//        /// </summary>
//        /// <param name="pathTemplateFile"></param>
//        /// <param name="directory_destination_folder"></param>
//        /// <param name="finAndRelpaces"></param>
//        /// <returns><strong>Trả về một đường dẫn tuyệt đối của file đã export nếu thành công và trả về null nếu file template không tồn tại</strong></returns>
//        /// <exception cref="Exception"></exception>
//        public string ExcuteReport(object pathTemplateFile, object directory_destination_folder, Dictionary<object, object> finAndRelpaces)
//        {
//            object missing = Missing.Value;
//            Document myWordDoc = null;
//            if (File.Exists((string)pathTemplateFile))
//            {
//                try
//                {
                    
//                    object readOnly = false;
//                    this._wordApp.Visible = false;
//                    myWordDoc = this._wordApp.Documents.Open(ref pathTemplateFile, ref missing, ref readOnly,
//                                                        ref missing, ref missing, ref missing,
//                                                        ref missing, ref missing, ref missing,
//                                                        ref missing, ref missing, ref missing,
//                                                         ref missing, ref missing, ref missing, ref missing);
//                    myWordDoc.Activate();
//                    FindAndReplace(this._wordApp, finAndRelpaces);

//                    object pathOutFile = (string)directory_destination_folder + "\\" + this._filenameOutput;
//                    myWordDoc.SaveAs2(ref pathOutFile, ref missing, ref missing, ref missing,
//                                                                    ref missing, ref missing, ref missing,
//                                                                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
//                                                                    ref missing, ref missing, ref missing);

//                    myWordDoc.Close();
//                    this._wordApp.Quit();
//                    return (string)pathOutFile;
//                }
//                catch (Exception e)
//                {
//                    if (myWordDoc != null)
//                    {
//                        myWordDoc.Close(SaveChanges: WdSaveOptions.wdDoNotSaveChanges);
//                        myWordDoc = null;
//                    }
//                    this._wordApp.Quit(SaveChanges: WdSaveOptions.wdDoNotSaveChanges);
//                    throw new Exception(e.Message, e);
//                }
//            }
//            return null;
//        }
//        private void FindAndReplace(Application wordApp, Dictionary<object, object> finAndRelpaces)
//        {
//            object matchCase = true;

//            object matchwholeWord = true;

//            object matchwildCards = false;

//            object matchSoundLike = false;

//            object nmatchAllforms = false;

//            object forward = true;

//            object format = false;

//            object matchKashida = false;

//            object matchDiactitics = false;

//            object matchAlefHamza = false;

//            object matchControl = false;

//            object read_only = false;

//            object visible = true;

//            object replace = -2;

//            object wrap = 1;


//            foreach (KeyValuePair<object, object> keyvalue in finAndRelpaces)
//            {
//                object key = keyvalue.Key;
//                object va = keyvalue.Value;
//                wordApp.Selection.Find.Execute(ref key, ref matchCase,
//                                           ref matchwholeWord, ref matchwildCards, ref matchSoundLike,

//                                           ref nmatchAllforms, ref forward,

//                                           ref wrap, ref format, ref va,

//                                               ref replace, ref matchKashida,

//                                           ref matchDiactitics, ref matchAlefHamza,

//                                            ref matchControl);
//            }
//        }
//    }
//}
