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

//Last modification on: 31/07/2023 14:25:25

namespace MarshallStore.Areas.MarshallStore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 31/07/2023 14:25:25
    /// </summary>
    public partial class PurchaseProductService : IPurchaseProduct
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public PurchaseProductService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public PurchaseProductModel Select1ByPurchaseProductIdToModel(int PurchaseProductId)
        {
            return new PurchaseProductModel().Select1ByPurchaseProductIdToModel(PurchaseProductId);
        }

        public List<PurchaseProductModel> SelectAllToList()
        {
            return new PurchaseProductModel().SelectAllToList();
        }

        public purchaseproductSelectAllPaged SelectAllPagedToModel(purchaseproductSelectAllPaged purchaseproductSelectAllPaged)
        {
            return new PurchaseProductModel().SelectAllPagedToModel(purchaseproductSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(PurchaseProductModel PurchaseProductModel)
        {
            return new PurchaseProductModel().Insert(PurchaseProductModel);
        }

        public int UpdateByPurchaseProductId(PurchaseProductModel PurchaseProductModel)
        {
            return new PurchaseProductModel().UpdateByPurchaseProductId(PurchaseProductModel);
        }

        public int DeleteByPurchaseProductId(int PurchaseProductId)
        {
            return new PurchaseProductModel().DeleteByPurchaseProductId(PurchaseProductId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                PurchaseProductModel PurchaseProductModel = new PurchaseProductModel();
                PurchaseProductModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PurchaseProductModel PurchaseProductModel = new PurchaseProductModel().Select1ByPurchaseProductIdToModel(Convert.ToInt32(RowsChecked[i]));
                    PurchaseProductModel.DeleteByPurchaseProductId(PurchaseProductModel.PurchaseProductId);
                }
            }
        }

        public int CopyByPurchaseProductId(int PurchaseProductId)
        {
            PurchaseProductModel PurchaseProductModel = new PurchaseProductModel().Select1ByPurchaseProductIdToModel(PurchaseProductId);
            int NewEnteredId = new PurchaseProductModel().Insert(PurchaseProductModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<PurchaseProductModel> lstPurchaseProductModel = new List<PurchaseProductModel> { };
                lstPurchaseProductModel = new PurchaseProductModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstPurchaseProductModel.Count];

                for (int i = 0; i < lstPurchaseProductModel.Count; i++)
                {
                    NewEnteredIds[i] = lstPurchaseProductModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PurchaseProductModel PurchaseProductModel = new PurchaseProductModel().Select1ByPurchaseProductIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = PurchaseProductModel.Insert();
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
            List<PurchaseProductModel> lstPurchaseProductModel = new List<PurchaseProductModel> { };

            if (ExportationType == "All")
            {
                lstPurchaseProductModel = new PurchaseProductModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PurchaseProductModel PurchaseProductModel = new PurchaseProductModel().Select1ByPurchaseProductIdToModel(Convert.ToInt32(RowChecked));
                    lstPurchaseProductModel.Add(PurchaseProductModel);
                }
            }

            foreach (PurchaseProductModel row in lstPurchaseProductModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of PurchaseProduct</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PurchaseProductId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PurchaseId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProductId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/MarshallStore/PurchaseProduct/PurchaseProduct_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtPurchaseProduct = new DataTable();
                dtPurchaseProduct.TableName = "PurchaseProduct";

                //We define another DataTable dtPurchaseProductCopy to avoid issue related to DateTime conversion
                DataTable dtPurchaseProductCopy = new DataTable();
                dtPurchaseProductCopy.TableName = "PurchaseProduct";

                #region Define columns for dtPurchaseProductCopy
                DataColumn dtColumnPurchaseProductIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnPurchaseProductIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnPurchaseProductIdFordtPurchaseProductCopy.ColumnName = "PurchaseProductId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnPurchaseProductIdFordtPurchaseProductCopy);

                    DataColumn dtColumnActiveFordtPurchaseProductCopy = new DataColumn();
                    dtColumnActiveFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnActiveFordtPurchaseProductCopy.ColumnName = "Active";
                    dtPurchaseProductCopy.Columns.Add(dtColumnActiveFordtPurchaseProductCopy);

                    DataColumn dtColumnDateTimeCreationFordtPurchaseProductCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPurchaseProductCopy.ColumnName = "DateTimeCreation";
                    dtPurchaseProductCopy.Columns.Add(dtColumnDateTimeCreationFordtPurchaseProductCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPurchaseProductCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPurchaseProductCopy.ColumnName = "DateTimeLastModification";
                    dtPurchaseProductCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPurchaseProductCopy);

                    DataColumn dtColumnUserCreationIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPurchaseProductCopy.ColumnName = "UserCreationId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnUserCreationIdFordtPurchaseProductCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPurchaseProductCopy.ColumnName = "UserLastModificationId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnUserLastModificationIdFordtPurchaseProductCopy);

                    DataColumn dtColumnPurchaseIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnPurchaseIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnPurchaseIdFordtPurchaseProductCopy.ColumnName = "PurchaseId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnPurchaseIdFordtPurchaseProductCopy);

                    DataColumn dtColumnProductIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnProductIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnProductIdFordtPurchaseProductCopy.ColumnName = "ProductId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnProductIdFordtPurchaseProductCopy);

                    
                #endregion

                dtPurchaseProduct = new PurchaseProductModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtPurchaseProduct.Rows)
                {
                    dtPurchaseProductCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtPurchaseProductCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/PurchaseProducting/PurchaseProduct/PurchaseProduct_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsPurchaseProduct = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtPurchaseProductCopy to avoid issue related to DateTime conversion
                    DataTable dtPurchaseProductCopy = new DataTable();
                    dtPurchaseProductCopy.TableName = "PurchaseProduct";

                    #region Define columns for dtPurchaseProductCopy
                    DataColumn dtColumnPurchaseProductIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnPurchaseProductIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnPurchaseProductIdFordtPurchaseProductCopy.ColumnName = "PurchaseProductId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnPurchaseProductIdFordtPurchaseProductCopy);

                    DataColumn dtColumnActiveFordtPurchaseProductCopy = new DataColumn();
                    dtColumnActiveFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnActiveFordtPurchaseProductCopy.ColumnName = "Active";
                    dtPurchaseProductCopy.Columns.Add(dtColumnActiveFordtPurchaseProductCopy);

                    DataColumn dtColumnDateTimeCreationFordtPurchaseProductCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPurchaseProductCopy.ColumnName = "DateTimeCreation";
                    dtPurchaseProductCopy.Columns.Add(dtColumnDateTimeCreationFordtPurchaseProductCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPurchaseProductCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPurchaseProductCopy.ColumnName = "DateTimeLastModification";
                    dtPurchaseProductCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPurchaseProductCopy);

                    DataColumn dtColumnUserCreationIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPurchaseProductCopy.ColumnName = "UserCreationId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnUserCreationIdFordtPurchaseProductCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPurchaseProductCopy.ColumnName = "UserLastModificationId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnUserLastModificationIdFordtPurchaseProductCopy);

                    DataColumn dtColumnPurchaseIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnPurchaseIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnPurchaseIdFordtPurchaseProductCopy.ColumnName = "PurchaseId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnPurchaseIdFordtPurchaseProductCopy);

                    DataColumn dtColumnProductIdFordtPurchaseProductCopy = new DataColumn();
                    dtColumnProductIdFordtPurchaseProductCopy.DataType = typeof(string);
                    dtColumnProductIdFordtPurchaseProductCopy.ColumnName = "ProductId";
                    dtPurchaseProductCopy.Columns.Add(dtColumnProductIdFordtPurchaseProductCopy);

                    
                    #endregion

                    dsPurchaseProduct.Tables.Add(dtPurchaseProductCopy);

                    for (int i = 0; i < dsPurchaseProduct.Tables.Count; i++)
                    {
                        dtPurchaseProductCopy = new PurchaseProductModel().Select1ByPurchaseProductIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtPurchaseProductCopy.Rows)
                        {
                            dsPurchaseProduct.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsPurchaseProduct.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsPurchaseProduct.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/PurchaseProducting/PurchaseProduct/PurchaseProduct_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<PurchaseProductModel> lstPurchaseProductModel = new List<PurchaseProductModel> { };

            if (ExportationType == "All")
            {
                lstPurchaseProductModel = new PurchaseProductModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PurchaseProductModel PurchaseProductModel = new PurchaseProductModel().Select1ByPurchaseProductIdToModel(Convert.ToInt32(RowChecked));
                    lstPurchaseProductModel.Add(PurchaseProductModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/PurchaseProducting/PurchaseProduct/PurchaseProduct_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstPurchaseProductModel);
            }

            return Now;
        }
        #endregion
    }
}