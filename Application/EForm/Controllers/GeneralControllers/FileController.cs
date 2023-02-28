using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class FileController : BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/UploadFile/Image")]
        [Permission(Code = "GUPLO1")]
        public IHttpActionResult UploadImageFile()
        {
            var httpRequest = HttpContext.Current.Request;
            var content = httpRequest.Params["ViName"];
            var title = httpRequest.Params["EnName"];
            var visit_type = httpRequest.Params["visittype"];
            var visitid = httpRequest.Params["visitid"];
            var formid = httpRequest.Params["formid"];
            var formcode = httpRequest.Params["formcode"];
            if (httpRequest.Files.Count < 1)
                return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);            
            var upload_path = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
            var success_file = new List<string>();
            var date_now = DateTime.Now.ToString("dd-MM-yyyy");
            var folder_virtual_path = $"/UploadFiles/Images/Temp/{date_now}";
            var folder_physic_path = $"{upload_path}{folder_virtual_path}";
            bool exists = System.IO.Directory.Exists(folder_physic_path);
            if (!exists)
                System.IO.Directory.CreateDirectory(folder_physic_path);           
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                string[] name_parts = postedFile.FileName.Split('.');
                string originname = name_parts[name_parts.Length - 2];
                string ext = name_parts[name_parts.Length - 1];
                long file_size = postedFile.ContentLength;
                if (!IsValidImageExtend(ext) || !IsValidImageSize(file_size)) continue;
                string image_name = $"{Guid.NewGuid().ToString()}.{ext}"; 
                string image_virtual_path = $"{folder_virtual_path}/{image_name}";
                string image_physic_path = $"{upload_path}{image_virtual_path}";
                postedFile.SaveAs(image_physic_path);
                success_file.Add(image_virtual_path);
                if (success_file.Count > 0)
                {
                    Guid id = Guid.Empty;
                    Guid form_id = Guid.Empty;
                    if (visitid != null)
                    {
                        id = Guid.Parse(visitid);
                    }
                    if(formid != null)
                    {
                        form_id = Guid.Parse(formid);
                    }
                    var user = GetUser();
                    var image_data = new UploadImage
                    {
                        Name = content == null ? originname : content,
                        Title = title == null ? originname : title,
                        Path = image_physic_path,
                        Url = image_virtual_path,
                        FileType = ext,
                        FileSize = file_size.ToString(),
                        VisitType = visit_type == null ? "ảnh upload từ form" : visit_type,
                        VisitId = visitid == null ? Guid.Empty : id,
                        FormId = formid == null ? Guid.Empty : form_id,
                        CreatedBy = user.Username,
                        CreatedAt = DateTime.Now,
                        FormCode = formcode != null? formcode: "",
                    };
                    unitOfWork.UploadImageRepository.Add(image_data);                
                    image_data.IsDeleted = true;
                    unitOfWork.Commit();
                }
            }
            if (success_file.Count > 0)
                return Content(HttpStatusCode.OK, success_file);           
           
            return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/UploadFile/File")]
        [Permission(Code = "GUPLO1")]
        public IHttpActionResult UploadFile()
        {
            var httpRequest = HttpContext.Current.Request;
            var content = httpRequest.Params["content"];
            var title = httpRequest.Params["title"];
            var visit_type = httpRequest.Params["recordcode"];
            var visit_id = httpRequest.Params["visitid"];
            var form_id = httpRequest.Params["formid"];
            bool isvisitid = Guid.TryParse(visit_id, out var id);
            if (!isvisitid) return Content(HttpStatusCode.BadRequest, "Sai định dạng");
            if (httpRequest.Files.Count < 1)
                return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
            var upload_path = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
            var success_file = new List<string>();
            var date_now = DateTime.Now.ToString("dd-MM-yyyy");
            var folder_virtual_path = $"/UploadFiles/Images/Temp/{date_now}";
            var folder_physic_path = $"{upload_path}{folder_virtual_path}";
            bool exists = System.IO.Directory.Exists(folder_physic_path);
            if (!exists)
                System.IO.Directory.CreateDirectory(folder_physic_path);
           
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                string[] name_parts = postedFile.FileName.Split('.');
                string originname = name_parts[name_parts.Length - 2];
                string ext = name_parts[name_parts.Length - 1];
                long file_size = postedFile.ContentLength;
                if (!IsValidFileExtend(ext) || !IsValidFileSize(file_size)) continue;
                string file_name = $"{Guid.NewGuid().ToString()}.{ext}";
                string file_virtual_path = $"{folder_virtual_path}/{file_name}";
                string file_physic_path = $"{upload_path}{file_virtual_path}";
                postedFile.SaveAs(file_physic_path);
                success_file.Add(file_virtual_path);
                #region không lưu thông tin lúc upload file 
                if (success_file.Count > 0)
                {
                    var file_data = new UploadImage
                    {
                        Name = originname,
                        Title = "UPFROMLOADFILE",
                        Path = file_physic_path,
                        Url = file_virtual_path,
                        FileType = ext,
                        FileSize = file_size.ToString(),
                        VisitType = visit_type == null ? "file upload từ UploadFile" : visit_type,
                        VisitId = id,
                        FormId = form_id == null? Guid.Empty: Guid.Parse(form_id),
                    };
                    unitOfWork.UploadImageRepository.Add(file_data);
                    unitOfWork.Commit();
                }
                #endregion
            }

            if (success_file.Count > 0)
                return Content(HttpStatusCode.OK, success_file);

            return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
        }   
        [HttpGet]
        [Route("UploadFiles/Images/{folder}/{sub}/{image}/{ext}")]
        [Permission(Code = "GUPLO2")]
        public HttpResponseMessage ReadFile(string folder, string sub, string image, string ext)
        {
            var upload_path = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
            string image_virtual_path = $"UploadFiles/Images/{folder}/{sub}/{image}.{ext}";
            string image_physic_path = $"{upload_path}/{image_virtual_path}";

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            System.IO.FileStream stream = System.IO.File.OpenRead(image_physic_path);
            result.Content = new StreamContent(stream);
            if ("PDF,pdf".Contains(ext))
            {
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                return result;
            }else if ("JPG,jpg,JPEG,jpeg".Contains(ext))
            {
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");                
                return result;
            }            
            else if ("PNG,png".Contains(ext))
            {
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                return result;
            }           
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;

        }
        
        [HttpPost]
        [Route("UploadFiles/File/DeleteFile")]
        //[Permission(Code = "VIEWFILEDEL")]
        public IHttpActionResult DeleteFile(string strgurl)
        {
            try
            {
                if (!strgurl.Contains("."))
                {
                    strgurl = strgurl.Remove(72, 1).Insert(72, ".");
                }
                var file = unitOfWork.UploadImageRepository.FirstOrDefault(e => !e.IsDeleted && e.Url == strgurl);
                if (file != null)
                {
                    file.IsDeleted = true;
                    unitOfWork.UploadImageRepository.Update(file);
                    unitOfWork.Commit();
                    return Content(HttpStatusCode.OK, new { ViMessage = "Xóa thành công", EnMessage = "" });
                }
                return Content(HttpStatusCode.BadRequest, new { ViMessage = "Không tồn tại", EnMessage = "" });
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, new { ViMessage = "Có lỗi xảy ra", EnMessage = "" });
            }           
        }
        [HttpPost]
        [Route("UploadFiles/File/DeleteFileFromForms/{FormId}")]
        //[Permission(Code = "VIEWFILEDEL")]
        public IHttpActionResult DeleteFileFromForms(Guid FormId,[FromBody] Param param)
        {
            try
            {
                if (!String.IsNullOrEmpty(param.FormCodeUrl))
                {
                    var list_image = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == FormId && e.FormCode == param.FormCodeUrl).ToList();
                    if (list_image != null && list_image.Count > 0)
                    {
                        foreach (var image in list_image)
                        {
                            image.IsDeleted = true;
                            unitOfWork.UploadImageRepository.Update(image);
                            unitOfWork.Commit();
                        }
                    }
                }
                else
                {
                    var list_image = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == FormId).ToList();
                    if (list_image != null && list_image.Count > 0)
                    {
                        foreach (var image in list_image)
                        {
                            image.IsDeleted = true;
                            unitOfWork.UploadImageRepository.Update(image);
                            unitOfWork.Commit();
                        }
                    }
                }
                if (!String.IsNullOrEmpty(param.Urls))
                {
                    var urls = param.Urls.Split(',');
                    var user = GetUser();
                    
                    foreach (var url in urls)
                    {
                        string sub_url = "";
                        if (!url.Contains("."))
                        {
                            sub_url = url.Remove(72, 1).Insert(72, ".");
                        }
                        var sub_file = unitOfWork.UploadImageRepository.FirstOrDefault(e => e.Url == sub_url && e.FormId == FormId);
                        if (sub_file != null && String.IsNullOrEmpty(param.VisitId))
                        {
                            sub_file.UpdatedAt = DateTime.Now;
                            sub_file.UpdatedBy = user.Username;
                            sub_file.IsDeleted = false;
                            unitOfWork.UploadImageRepository.Update(sub_file);
                            unitOfWork.Commit();
                        }
                        // phần xử lý riêng upload ảnh xác nhận ra viện không theo chỉ định của bác sỹ cho anh Đức
                        if (!String.IsNullOrEmpty(param.VisitId))
                        {
                            Guid id = Guid.Parse(param.VisitId);
                            var form = unitOfWork.IPDConfirmDischargeWithoutDirectRepository.FirstOrDefault(e => e.VisitId == id);
                            if(form != null)
                            {
                                Guid formId = form.Id;
                                var file = unitOfWork.UploadImageRepository.FirstOrDefault(e => e.Url == sub_url && e.VisitId == id);
                                if (file != null)
                                {
                                    file.UpdatedAt = DateTime.Now;
                                    file.UpdatedBy = user.Username;
                                    file.IsDeleted = false;
                                    file.FormId = formId;
                                    unitOfWork.UploadImageRepository.Update(sub_file);
                                    unitOfWork.Commit();
                                }
                            }
                        }
                    }
                }               
                return Content(HttpStatusCode.OK, new { ViMessage = "Xóa thành công", EnMessage = "" }); ;
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, new { ViMessage = "Có lỗi xảy ra", EnMessage = "" });
            }
        }
        [HttpGet]
        [Route("api/GetListWithPaging/{type}/{formcode}/{visitId}")]
        [Permission(Code = "VIEWFILEDT")]
        public IHttpActionResult GetListWithPaging(string type, string formcode, Guid visitId, [FromUri] UploadImageModel param)
        {
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);           
            var forms = GetForms(visitId, formcode, type);
            var fromForms = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && !e.Title.Contains("UPFROMLOADFILE")).ToList();
            if (forms.Count == 0 && fromForms.Count == 0)
                return Content(HttpStatusCode.NotFound, new
                {
                    visitId = visitId,
                    Message.FORM_NOT_FOUND
                });
            List<UploadModel> result_list = new List<UploadModel>();
            var datas = forms.Select(form => new UploadModel
            {
                Id = form?.Id,
                CreatedAt = form?.CreatedAt,
                CreatedBy = form?.CreatedBy,
                UpdatedAt = form?.UpdatedAt,
                UpdatedBy = form?.UpdatedBy,
                Title = unitOfWork.FormDatasRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "UPPLOADFILECD2" && e.FormId == form.Id)?.Value,
                Content = unitOfWork.FormDatasRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "UPPLOADFILECD3" && e.FormId == form.Id)?.Value,
                IsCheckFile = false,
                IsDeleted = form.IsDeleted,
                EnName = unitOfWork.FormDatasRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "UPPLOADFILECD2" && e.FormId == form.Id)?.Value,
                ViName = unitOfWork.FormDatasRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "UPPLOADFILECD2" && e.FormId == form.Id)?.Value,
                FormCodeUrl = form.FormCode
            }).OrderByDescending(e => e.CreatedAt).ToList();
            foreach(var data in datas)
            {
                if (String.IsNullOrEmpty(data.Title))
                {
                    data.IsDeleted = true;
                }
            }
            datas = (from r in datas where !r.IsDeleted select r).ToList();
            result_list.AddRange(datas);
            
            var list_groupb = fromForms.GroupBy(p => new { p.FormId, p.FormCode }, (key, g) => new { Id = key, Data = g.OrderByDescending(e => e.CreatedAt).ToList() });
         
            foreach (var i in list_groupb)
            {
                UploadModel sub_item = new UploadModel();
                var b = i.Data;
                sub_item.Id = i.Id.FormId;
                sub_item.FormCodeUrl = i.Id.FormCode;
                sub_item.CreatedAt = b.LastOrDefault()?.CreatedAt;
                sub_item.CreatedBy = b.LastOrDefault()?.CreatedBy;
                sub_item.EnName = b.LastOrDefault()?.Title;
                sub_item.ViName = b.LastOrDefault()?.Name;
                b = b.OrderByDescending(e => e.UpdatedAt).ToList();
                sub_item.UpdatedAt = b.FirstOrDefault()?.UpdatedAt;
                sub_item.UpdatedBy = b.FirstOrDefault()?.UpdatedBy;
                sub_item.IsCheckFile = true;               
                result_list.Add(sub_item);
            }            
            if (!string.IsNullOrEmpty(param.Title))   
                result_list = (from r in result_list where (r.ViName.ToUpper().Contains(param.Title.ToUpper()) || r.EnName.ToUpper().Contains(param.Title.ToUpper())) select r).ToList();      

            if (!string.IsNullOrEmpty(param.FromDate))
            {
                var startDate = DateTime.ParseExact(param.FromDate, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                result_list = (from r in result_list where r.CreatedAt >= startDate select r).ToList();
            }
            if (!string.IsNullOrEmpty(param.ToDate))
            {
                var endDate = DateTime.ParseExact(param.ToDate, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = endDate.AddSeconds(59);
                result_list = (from r in result_list where r.CreatedAt <= endDate select r).ToList();
            }
            if (!string.IsNullOrEmpty(param.CreatedBy))
                result_list = (from r in result_list where r.CreatedBy == param.CreatedBy select r).ToList();
            int count = result_list.Count();
            int numberPage = count % param.PageSize == 0 ? count / param.PageSize : count / param.PageSize + 1;
            var items = result_list.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize)
            .ToList();
            items = items.OrderByDescending(e => e.CreatedAt).ToList();
            var lastUpdate = result_list.OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
            return Content(HttpStatusCode.OK, new
            {
                VisitId = visitId,
                Datas = items,
                LastInfo = new
                {
                    lastUpdate?.CreatedAt,
                    lastUpdate?.CreatedBy,
                    lastUpdate?.UpdatedAt,
                    lastUpdate?.UpdatedBy
                },
                Paging = new { numberPage, param.PageNumber }
            });
        }
        [HttpPost]
        [Route("api/DeleteForm/{type}/{formcode}/{visitId}/{id}")]
        [Permission(Code = "UPLOADFILEUPDA")]
        public IHttpActionResult DeleteForm(string type, string formcode, Guid visitId, Guid id)
        {
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcode);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViMessage = "Không tìm thấy formcode! ", EnMessage = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formcode && e.Id == id).FirstOrDefault(); ;
            //var fromForms = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == id).ToList();            
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            if(form != null)
            {
                form.IsDeleted = true;
                unitOfWork.EIOFormRepository.Update(form);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { ViMessage = "Xoá thành công", EnMessage = "" });
            }          
            return Content(HttpStatusCode.OK, new { ViMessage = "Xoá thành công", EnMessage = "" });
        }
        [HttpGet]
        [Route("api/GetDetailFile/{type}/{formcode}/{visitId}/{id}")]
        [Permission(Code = "VIEWFILEDT")]
        public IHttpActionResult GetFormByVisitId(string type, string formcode, Guid visitId, Guid id, [FromUri] Param param)
        {
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.Id ==id && e.FormCode == "UPLOADFILE");
            List<UploadImage> form2 = new List<UploadImage>();
            if (!string.IsNullOrEmpty(param.FormCodeUrl))
            {
                 form2 = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == id && !e.Title.Contains("UPFROMLOADFILE") && e.FormCode == param.FormCodeUrl).ToList();
            }
            else
            {
               form2 = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == id && !e.Title.Contains("UPFROMLOADFILE")).ToList();
            }
            
            if (form == null && form2.Count < 0)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            dynamic data = null;
            List<VisitUploadModel> list_fileData = new List<VisitUploadModel>();            
            if (form != null && form2.Count < 1)
            {
                var title = unitOfWork.FormDatasRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "UPPLOADFILECD2" && e.FormId == form.Id)?.Value;
                if(title != null && !title.Contains("UPFROMLOADFILE"))
                {
                    var list_file = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == id).ToList();
                    foreach (var file in list_file)
                    {
                        VisitUploadModel fileData1 = new VisitUploadModel();
                        fileData1.FileName = file.Name;
                        fileData1.Url = file.Url;
                        fileData1.FileSize = file.FileSize;
                        fileData1.FileType = file.FileType;
                        list_fileData.Add(fileData1);
                    }
                    data = new
                    {
                        Id = form.Id,
                        CreatedBy = form.CreatedBy,
                        CreatedAt = form.CreatedAt,
                        UpdatedAt = form.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        UpdatedBy = form.UpdatedBy,
                        IsCheckFile = false,
                        IsCheckUpdate = form.CreatedAt < form.UpdatedAt ? true : false,
                        Datas = GetFormDatas(visitId, id, "UPLOADFILE"),
                        ListFileData = list_fileData,
                    };
                }
            }
            if (form2.Count > 0 && form == null)
            {
                var first_item =  form2.FirstOrDefault();
                List<string> list_url = new List<string>();

                foreach (var item in form2)
                {
                    VisitUploadModel fileData2 = new VisitUploadModel();
                    var converstring =  item.Url.Replace(".", "/");
                    list_url.Add(converstring);
                    fileData2.FileName = item.Name;
                    fileData2.Url = item.Url;
                    fileData2.FileSize = item.FileSize;  
                    list_fileData.Add(fileData2);
                }
                var json = new JavaScriptSerializer().Serialize(list_url);
                
                List<FormDataValue> list_value = new List<FormDataValue>()
                {
                    new FormDataValue { Code = "UPPLOADFILECD1", Value = json},
                    new FormDataValue { Code = "UPPLOADFILECD2", Value = first_item.Title},
                    new FormDataValue { Code = "UPPLOADFILECD3", Value = first_item.Name},
                };               
                data = new
                {
                    Id = first_item.Id,
                    CreatedBy = first_item.CreatedBy,
                    CreatedAt = first_item.CreatedAt,
                    UpdatedAt = first_item.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = first_item.UpdatedBy,
                    IsCheckFile = true,
                    IsCheckUpdate = first_item.CreatedAt < first_item.UpdatedAt ? true : false,
                    Datas = list_value,
                    ListFileData = list_fileData,
                };
            }
            return Content(HttpStatusCode.OK, new { data });
        }
        [HttpPost]
        [Route("api/UpdateFile/{type}/{formcode}/{visitId}/{id}")]
        [Permission(Code = "UPLOADFILEUPDA")]
        public IHttpActionResult UpdateForm(string type, string formcode, Guid visitId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formcode && e.Id == id).FirstOrDefault(); ;
            var form2 = unitOfWork.UploadImageRepository.Find(e => !e.IsDeleted && e.FormId == id).ToList();
            if (form == null && form2.Count == 0)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            if(form != null)
            {
                var user = GetUser();
                HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formcode, request["Datas"]);
                UpdateVisit(visit, type);
                form.UpdatedAt = DateTime.Now;
                form.UpdatedBy = user.Username;
                unitOfWork.EIOFormRepository.Update(form);
                unitOfWork.Commit();
            }
            else if(form2.Count> 0)
            {
                return Content(HttpStatusCode.BadRequest, new { ViMessage = "File không được chỉnh sửa", EnMessage = "File can not edit" });
                #region không cho sửa 
                //string content = string.Empty;
                //string title = string.Empty;
                //string url = string.Empty;

                //foreach (var item in request["Datas"])
                //{
                //    var code = item["Code"]?.ToString();
                //    var value = item["Value"]?.ToString();
                //    if (code.Contains("UPPLOADFILECD3"))
                //    {
                //        content = value;
                //    }
                //    if (code.Contains("UPPLOADFILECD2"))
                //    {
                //        title = value;
                //    }
                //    if (code.Contains("UPPLOADFILECD1"))
                //    {
                //        url = value;
                //        var obj = new JavaScriptSerializer().Deserialize<dynamic>(url);
                //        foreach(var item2 in obj)
                //        {

                //            if (!item2.Contains("."))
                //            {
                //                string url_obj = item2.Remove(72, 1).Insert(72, ".");
                //                var file = unitOfWork.UploadImageRepository.FirstOrDefault(e => !e.IsDeleted && e.Url == url_obj);
                //                if (file != null)
                //                {
                //                    file.Url = url_obj;
                //                    unitOfWork.UploadImageRepository.Update(file);
                //                    unitOfWork.Commit();
                //                }
                //            }
                //        }
                //    }
                //}

                //foreach (var frm in form2)
                //{
                //    frm.Name = content;
                //    frm.Title = title;                   
                //    unitOfWork.UploadImageRepository.Update(frm);
                //    unitOfWork.Commit();
                //}
                #endregion
            }
            return Content(HttpStatusCode.OK, new { Id = id, });
        }       
        private bool IsValidImageExtend(string ext)
        {
            var valid_file = System.Configuration.ConfigurationManager.AppSettings["ValidFileType"].Split(',');
            foreach(var file in valid_file)
            {
                if (file.Equals(ext, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        private bool IsValidImageSize(long file_size)
        {
            return file_size <= 5242880;
        }
        private bool IsValidFileExtend(string ext)
        {
            var valid_file = System.Configuration.ConfigurationManager.AppSettings["ValidFileUploadType"].Split(',');
            foreach (var file in valid_file)
            {
                if (file.Equals(ext, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        private bool IsValidFileSize(long file_size)
        {
            var file_sizestrg = System.Configuration.ConfigurationManager.AppSettings["ValidFileSize"];
            bool isfile_size = int.TryParse(file_sizestrg, out int filesize);
            if (!isfile_size) return false;
            return file_size <= filesize;
        }
        private List<FormDataValue> GetFormDatas(Guid visitId, Guid formId, string formCode)
        {
          return  unitOfWorkDapper.FormDatasRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.VisitId == visitId &&
                    e.FormCode == formCode &&
                    e.FormId == formId
            ).Select(f => new FormDataValue {Code = f.Code, Value = f.Value }).ToList();
        }
        public class Param
        {
            public string Urls { get; set; }
            public string FormCodeUrl { get; set; }
            public string VisitId { get; set; }
        }       
    }
}
