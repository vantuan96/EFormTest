using Admin.Common;
using Admin.Common.Model;
using Admin.CustomAuthen;
using Admin.MemCached;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Action = DataAccess.Models.Action;
using MasterData = DataAccess.Models.MasterData;

namespace Admin.Controllers
{
    public class UploadFileController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        // GET: Account
        public ActionResult Uploadfile()
        {
            return View();
        }
        //[HttpPost]
        //// [ValidateAntiForgeryToken]
        //public JsonResult UploadExcel(HttpPostedFileBase FileUpload)
        //{
        //    dynamic noti = null;
        //    List<string> data = new List<string>();
        //    DataTable dataTableMasterData = new DataTable();
        //    DataTable dataTableAction = new DataTable();
        //    DataTable dataTableForm = new DataTable();
        //    if (FileUpload != null)
        //    {
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            try
        //            {
        //                Stream stream = FileUpload.InputStream;
        //                IExcelDataReader reader = null;
        //                if (FileUpload.FileName.EndsWith(".xls"))
        //                {
        //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
        //                }
        //                else if (FileUpload.FileName.EndsWith(".xlsx"))
        //                {
        //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //                }
        //                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                    {
        //                        UseHeaderRow = true
        //                    }
        //                });
        //                dataTableMasterData = result.Tables[0];
        //                dataTableAction = result.Tables[1];
        //                dataTableForm = result.Tables[2];
        //                if (dataTableMasterData != null)
        //                {
        //                    InsertMasterData(dataTableMasterData);
        //                }
        //                if (dataTableAction != null)
        //                {
        //                    InsertAction(dataTableAction);
        //                }
        //                if (dataTableForm != null)
        //                {
        //                    InsertForm(dataTableForm);
        //                }
        //                reader.Close();
        //                noti = new
        //                {
        //                    Message = "Thành công"
        //                };
        //                return Json(noti);
        //            }
        //            catch
        //            {
        //                noti = new
        //                {
        //                    Message = "Có lỗi xảy ra"
        //                };
        //                return Json(noti);
        //            }
                    
        //        }

        //        else
        //        {
        //            noti = new
        //            {
        //                Message = "Thất bại"
        //            };
        //            return Json(noti);
        //        }
        //    }
        //    else
        //    {
        //        noti = new
        //        {
        //            Message = "Bạn chưa upload file"
        //        };
        //        return Json(noti);
        //    }
        //}
    //    public void InsertMasterData(DataTable importdt)
    //    {
    //        try
    //        {
    //            for (int row = 1; row < importdt.Rows.Count; row++)
    //            {
    //                int col = 0;
    //                var code = importdt.Rows[row][col + 2].ToString();
    //                var checkdata = unitOfWork.MasterDataRepository.FirstOrDefault(e => e.Code == code);
    //                if (checkdata != null)
    //                {
    //                    unitOfWork.MasterDataRepository.Update(checkdata);
    //                    unitOfWork.Commit();
    //                }
    //                else
    //                {
    //                    MasterData mdata = new MasterData();
    //                    mdata.ViName = importdt.Rows[row][col].ToString();
    //                    mdata.EnName = importdt.Rows[row][col + 1].ToString();
    //                    mdata.Code = code;
    //                    mdata.Group = importdt.Rows[row][col + 3].ToString();
    //                    mdata.Form = importdt.Rows[row][col + 4].ToString();
    //                    mdata.Level = Int32.Parse(importdt.Rows[row][col + 5].ToString());
    //                    mdata.Order = Int32.Parse(importdt.Rows[row][col + 6].ToString());
    //                    mdata.DataType = importdt.Rows[row][col + 7].ToString();
    //                    mdata.Version = "1";
    //                    unitOfWork.MasterDataRepository.Add(mdata);
    //                    unitOfWork.Commit();
    //                }
    //            }
    //        }
    //        catch
    //        {

    //        }  
    //    }
    //    public void InsertAction(DataTable importdt)
    //    {
    //        try
    //        {
    //            for (int row = 0; row < importdt.Rows.Count; row++)
    //            {
    //                int col = 0;
    //                var code = importdt.Rows[row][col + 1].ToString();
    //                var type = importdt.Rows[row][col + 2].ToString();
    //                Guid visitgroupId = new Guid();
    //                var visitgroup = unitOfWork.VisitTypeGroupRepository.FirstOrDefault(e => e.Code == type);
    //                if (visitgroup != null)
    //                {
    //                    visitgroupId = visitgroup.Id;
    //                }
    //                var checkdata = unitOfWork.ActionRepository.FirstOrDefault(e => e.Code == code);
    //                if (checkdata != null)
    //                {
    //                    unitOfWork.ActionRepository.Update(checkdata);
    //                    unitOfWork.Commit();
    //                }
    //                else
    //                {
    //                    Action action = new Action();
    //                    action.Name = importdt.Rows[row][col].ToString();
    //                    action.Code = importdt.Rows[row][col + 1].ToString();
    //                    action.VisitTypeGroupId = visitgroupId;
    //                    unitOfWork.ActionRepository.Add(action);
    //                    unitOfWork.Commit();
    //                }
    //            }
    //        }
    //        catch(Exception ex)
    //        {

    //        }
           
    //    }
    //    public void InsertForm(DataTable importdt)
    //    {
    //        try
    //        {
    //            for (int row = 0; row < importdt.Rows.Count; row++)
    //            {
    //                int col = 0;
    //                var code = importdt.Rows[row][col + 1].ToString();
    //                var checkdata = unitOfWork.FormRepository.FirstOrDefault(e => e.Code == code);
    //                if (checkdata != null)
    //                {
    //                    unitOfWork.FormRepository.Delete(checkdata);
    //                    unitOfWork.Commit();
    //                }
    //                else
    //                {
    //                    Form form = new Form();
    //                    form.Name = importdt.Rows[row][col].ToString();
    //                    form.Code = importdt.Rows[row][col + 1].ToString();
    //                    form.VisitTypeGroupCode = importdt.Rows[row][col + 2].ToString();
    //                    form.Version = Int32.Parse(importdt.Rows[row][col + 3].ToString());
    //                    form.Time = Int32.Parse(importdt.Rows[row][col + 4].ToString());
    //                    form.Ispermission = Boolean.Parse(importdt.Rows[row][col + 5].ToString());
    //                    unitOfWork.FormRepository.Add(form);
    //                    unitOfWork.Commit();
    //                }
    //            }
    //        }
    //        catch
    //        {

    //        }
    //    }
    }
}
