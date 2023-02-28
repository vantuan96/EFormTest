using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.DTOs;
using EForm.Authentication;
using EForm.BaseControllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class TableDatasController : BaseApiController
    {
        [HttpPost]
        [Route("api/TableData/{code}/{id}")]
        public IHttpActionResult CreateOrTables(string code, Guid id, [FromBody] JObject request)
        {
            HandleUpdateOrCreateTableData(id, code, request["Table"]);
            HandleDeleteTableData(request["TableDelete"]);
            return Content(HttpStatusCode.OK, "Process Table Success");
        }
        [HttpGet]
        [Route("api/TableData/{code}/{id}")]
        public IHttpActionResult GetTables(string code, Guid id)
        {

            var datas = unitOfWork.TableDataRepository.Find(x => x.Id2 == id && x.FormCode == code && !x.IsDeleted).OrderBy(x => x.Order).GroupBy(x => x.IdRow).Select(x => new
            {
                IdRow = x.Key,
                RowData = unitOfWork.TableDataRepository.Find(e => e.IdRow == x.Key).OrderBy(e => e.Order).Distinct().Select(p => { return new { p.Id, p.Code, p.Value }; }).ToList()
            }); ;



            return Content(HttpStatusCode.OK, new
            {
                Id = id,
                Name = code,
                Datas = datas
            });
        }
        [HttpGet]
        [Route("api/TableData/{id}")]
        public IHttpActionResult GetTables(Guid id)
        {

            var tables = unitOfWork.TableDataRepository.Find(x => x.Id2 == id && !x.IsDeleted).OrderBy(x => x.Order).GroupBy(x => x.FormCode).Select(x => new
            {
                Id = id,
                Name = x.Key,
                Datas = unitOfWork.TableDataRepository.Find(e => e.Id2 == id && e.FormCode == x.Key).OrderBy(e => e.Order).GroupBy(e => e.IdRow).Select(e => new
                {
                    IdRow = e.Key,
                    RowData = unitOfWork.TableDataRepository.Find(t => t.IdRow == e.Key).OrderBy(t => t.Order).Distinct().Select(p => { return new { p.Id, p.Code, p.Value }; }).ToList()

                })
            });
            return Content(HttpStatusCode.OK, new
            {
                Tables = tables
            });

        }
        private void HandleUpdateOrCreateTableData(Guid id2, string code, JToken table)
        {
            if (table != null)
            {
                List<TableData> listInsert = new List<TableData>();
                List<TableData> listUpdate = new List<TableData>();
                foreach (var row in table)
                {

                    Guid gid = Guid.NewGuid();
                    DateTime createRowAt = DateTime.Now;
                    foreach (var column in row["RowData"])
                    {
                        string column_id = column["Id"]?.ToString();
                        if (string.IsNullOrEmpty(column_id))
                        {
                            CreateRowTableData(id2, code, gid, createRowAt, column, ref listInsert);
                        }
                        else
                        {
                            // Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)
                            bool isGuid = Guid.TryParse(column["Id"].ToString(), out Guid table_id);
                            if (!isGuid)
                                continue;
                            TableData tableData = unitOfWork.TableDataRepository.GetById(table_id);
                            UpdateRowTableData(tableData, column, ref listUpdate);

                        }


                    }

                }
                if (listInsert.Count > 0)
                {
                    unitOfWorkDapper.TableDataDtoRepository.Adds(Mapper.Map<List<TableDataDto>>(listInsert));
                }
                if (listUpdate.Count > 0)
                {
                    unitOfWorkDapper.TableDataDtoRepository.Updates(Mapper.Map<List<TableDataDto>>(listUpdate));
                }
            }
        }
        protected void CreateRowTableData(Guid id2, string code, Guid id_row, DateTime CreateRowAt, JToken item, ref List<TableData> listInsert)
        {
            TableData tableData = new TableData();
            tableData.Code = item["Code"]?.ToString();
            tableData.Value = item["Value"]?.ToString(); ;
            tableData.Id2 = id2;
            tableData.FormCode = code;
            tableData.IdRow = id_row;
            tableData.CreatedRowAt = CreateRowAt;

            listInsert.Add(tableData);
        }
        protected void UpdateRowTableData(TableData tableData, JToken item, ref List<TableData> listUpdate)
        {
            var old = new
            {
                tableData.Code,
                tableData.Value

            };
            var _new = new
            {
                Code = item["Code"]?.ToString(),
                Value = item["Value"]?.ToString(),
            };

            if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
            {
                tableData.Code = _new.Code;
                tableData.Value = _new.Value;

                listUpdate.Add(tableData);
            }
        }
        private void HandleDeleteTableData(JToken request_datas)
        {
            if (request_datas != null)
            {
                foreach (var item in request_datas)
                {
                    // Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)
                    bool isGuid = Guid.TryParse(item.ToString(), out Guid row_id);
                    if (!isGuid)
                        continue;
                    var columns = unitOfWork.TableDataRepository.Find(x => x.IdRow == row_id).ToList();
                    foreach (var colum in columns)
                    {
                        unitOfWork.TableDataRepository.Delete(colum);
                    }


                }
                unitOfWork.Commit();
            }

        }
    }
}