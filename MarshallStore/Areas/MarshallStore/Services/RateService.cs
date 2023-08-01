using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using MarshallStore.Areas.MarshallStore.Models;
using MarshallStore.Areas.MarshallStore.DTOs;
using MarshallStore.Areas.MarshallStore.Interfaces;
using MarshallStore.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

//Last modification on: 31/07/2023 14:25:35

namespace MarshallStore.Areas.MarshallStore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 31/07/2023 14:25:35
    /// </summary>
    public partial class RateService : IRate
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RateService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RateModel Select1ByRateIdToModel(int RateId)
        {
            return new RateModel().Select1ByRateIdToModel(RateId);
        }

        public List<RateModel> SelectAllToList()
        {
            return new RateModel().SelectAllToList();
        }

        public rateSelectAllPaged SelectAllPagedToModel(rateSelectAllPaged rateSelectAllPaged)
        {
            return new RateModel().SelectAllPagedToModel(rateSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RateModel RateModel)
        {
            return new RateModel().Insert(RateModel);
        }

        public int UpdateByRateId(RateModel RateModel)
        {
            return new RateModel().UpdateByRateId(RateModel);
        }

        public int DeleteByRateId(int RateId)
        {
            return new RateModel().DeleteByRateId(RateId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RateModel RateModel = new RateModel();
                RateModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RateModel RateModel = new RateModel().Select1ByRateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RateModel.DeleteByRateId(RateModel.RateId);
                }
            }
        }

        public int CopyByRateId(int RateId)
        {
            RateModel RateModel = new RateModel().Select1ByRateIdToModel(RateId);
            int NewEnteredId = new RateModel().Insert(RateModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RateModel> lstRateModel = new List<RateModel> { };
                lstRateModel = new RateModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRateModel.Count];

                for (int i = 0; i < lstRateModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRateModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RateModel RateModel = new RateModel().Select1ByRateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RateModel.Insert();
                }

                return NewEnteredIds;
            }
        }
        #endregion

        #region Other services
        public DateTime ExportAsPDF(Ajax Ajax, string ExportationType)
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<RateModel> lstRateModel = new List<RateModel> { };

            if (ExportationType == "All")
            {
                lstRateModel = new RateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RateModel RateModel = new RateModel().Select1ByRateIdToModel(Convert.ToInt32(RowChecked));
                    lstRateModel.Add(RateModel);
                }
            }

            foreach (RateModel row in lstRateModel)
            {
                RowsAsHTML += $@"{row.ToStringOnlyValuesForHTML()}";
            }

            Renderer.RenderHtmlAsPdf($@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">Mikromatica</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Rate</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RateId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProductId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Score&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {Now}</span>
</font>
").SaveAs($@"wwwroot/PDFFiles/MarshallStore/Rate/Rate_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRate = new DataTable();
                dtRate.TableName = "Rate";

                //We define another DataTable dtRateCopy to avoid issue related to DateTime conversion
                DataTable dtRateCopy = new DataTable();
                dtRateCopy.TableName = "Rate";

                #region Define columns for dtRateCopy
                DataColumn dtColumnRateIdFordtRateCopy = new DataColumn();
                    dtColumnRateIdFordtRateCopy.DataType = typeof(string);
                    dtColumnRateIdFordtRateCopy.ColumnName = "RateId";
                    dtRateCopy.Columns.Add(dtColumnRateIdFordtRateCopy);

                    DataColumn dtColumnActiveFordtRateCopy = new DataColumn();
                    dtColumnActiveFordtRateCopy.DataType = typeof(string);
                    dtColumnActiveFordtRateCopy.ColumnName = "Active";
                    dtRateCopy.Columns.Add(dtColumnActiveFordtRateCopy);

                    DataColumn dtColumnDateTimeCreationFordtRateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRateCopy.ColumnName = "DateTimeCreation";
                    dtRateCopy.Columns.Add(dtColumnDateTimeCreationFordtRateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRateCopy.ColumnName = "DateTimeLastModification";
                    dtRateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRateCopy);

                    DataColumn dtColumnUserCreationIdFordtRateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRateCopy.ColumnName = "UserCreationId";
                    dtRateCopy.Columns.Add(dtColumnUserCreationIdFordtRateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRateCopy.ColumnName = "UserLastModificationId";
                    dtRateCopy.Columns.Add(dtColumnUserLastModificationIdFordtRateCopy);

                    DataColumn dtColumnProductIdFordtRateCopy = new DataColumn();
                    dtColumnProductIdFordtRateCopy.DataType = typeof(string);
                    dtColumnProductIdFordtRateCopy.ColumnName = "ProductId";
                    dtRateCopy.Columns.Add(dtColumnProductIdFordtRateCopy);

                    DataColumn dtColumnScoreFordtRateCopy = new DataColumn();
                    dtColumnScoreFordtRateCopy.DataType = typeof(string);
                    dtColumnScoreFordtRateCopy.ColumnName = "Score";
                    dtRateCopy.Columns.Add(dtColumnScoreFordtRateCopy);

                    
                #endregion

                dtRate = new RateModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRate.Rows)
                {
                    dtRateCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRateCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Rateing/Rate/Rate_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRate = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRateCopy to avoid issue related to DateTime conversion
                    DataTable dtRateCopy = new DataTable();
                    dtRateCopy.TableName = "Rate";

                    #region Define columns for dtRateCopy
                    DataColumn dtColumnRateIdFordtRateCopy = new DataColumn();
                    dtColumnRateIdFordtRateCopy.DataType = typeof(string);
                    dtColumnRateIdFordtRateCopy.ColumnName = "RateId";
                    dtRateCopy.Columns.Add(dtColumnRateIdFordtRateCopy);

                    DataColumn dtColumnActiveFordtRateCopy = new DataColumn();
                    dtColumnActiveFordtRateCopy.DataType = typeof(string);
                    dtColumnActiveFordtRateCopy.ColumnName = "Active";
                    dtRateCopy.Columns.Add(dtColumnActiveFordtRateCopy);

                    DataColumn dtColumnDateTimeCreationFordtRateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRateCopy.ColumnName = "DateTimeCreation";
                    dtRateCopy.Columns.Add(dtColumnDateTimeCreationFordtRateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRateCopy.ColumnName = "DateTimeLastModification";
                    dtRateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRateCopy);

                    DataColumn dtColumnUserCreationIdFordtRateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRateCopy.ColumnName = "UserCreationId";
                    dtRateCopy.Columns.Add(dtColumnUserCreationIdFordtRateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRateCopy.ColumnName = "UserLastModificationId";
                    dtRateCopy.Columns.Add(dtColumnUserLastModificationIdFordtRateCopy);

                    DataColumn dtColumnProductIdFordtRateCopy = new DataColumn();
                    dtColumnProductIdFordtRateCopy.DataType = typeof(string);
                    dtColumnProductIdFordtRateCopy.ColumnName = "ProductId";
                    dtRateCopy.Columns.Add(dtColumnProductIdFordtRateCopy);

                    DataColumn dtColumnScoreFordtRateCopy = new DataColumn();
                    dtColumnScoreFordtRateCopy.DataType = typeof(string);
                    dtColumnScoreFordtRateCopy.ColumnName = "Score";
                    dtRateCopy.Columns.Add(dtColumnScoreFordtRateCopy);

                    
                    #endregion

                    dsRate.Tables.Add(dtRateCopy);

                    for (int i = 0; i < dsRate.Tables.Count; i++)
                    {
                        dtRateCopy = new RateModel().Select1ByRateIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRateCopy.Rows)
                        {
                            dsRate.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRate.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRate.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Rateing/Rate/Rate_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RateModel> lstRateModel = new List<RateModel> { };

            if (ExportationType == "All")
            {
                lstRateModel = new RateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RateModel RateModel = new RateModel().Select1ByRateIdToModel(Convert.ToInt32(RowChecked));
                    lstRateModel.Add(RateModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Rateing/Rate/Rate_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRateModel);
            }

            return Now;
        }
        #endregion
    }
}