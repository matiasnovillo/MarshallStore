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

//Last modification on: 31/07/2023 14:45:20

namespace MarshallStore.Areas.MarshallStore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 31/07/2023 14:45:20
    /// </summary>
    public partial class ShoppingCartService : IShoppingCart
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ShoppingCartModel Select1ByShoppingCartIdToModel(int ShoppingCartId)
        {
            return new ShoppingCartModel().Select1ByShoppingCartIdToModel(ShoppingCartId);
        }

        public List<ShoppingCartModel> SelectAllToList()
        {
            return new ShoppingCartModel().SelectAllToList();
        }

        public shoppingcartSelectAllPaged SelectAllPagedToModel(shoppingcartSelectAllPaged shoppingcartSelectAllPaged)
        {
            return new ShoppingCartModel().SelectAllPagedToModel(shoppingcartSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ShoppingCartModel ShoppingCartModel)
        {
            return new ShoppingCartModel().Insert(ShoppingCartModel);
        }

        public int UpdateByShoppingCartId(ShoppingCartModel ShoppingCartModel)
        {
            return new ShoppingCartModel().UpdateByShoppingCartId(ShoppingCartModel);
        }

        public int DeleteByShoppingCartId(int ShoppingCartId)
        {
            return new ShoppingCartModel().DeleteByShoppingCartId(ShoppingCartId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ShoppingCartModel ShoppingCartModel = new ShoppingCartModel();
                ShoppingCartModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ShoppingCartModel ShoppingCartModel = new ShoppingCartModel().Select1ByShoppingCartIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ShoppingCartModel.DeleteByShoppingCartId(ShoppingCartModel.ShoppingCartId);
                }
            }
        }

        public int CopyByShoppingCartId(int ShoppingCartId)
        {
            ShoppingCartModel ShoppingCartModel = new ShoppingCartModel().Select1ByShoppingCartIdToModel(ShoppingCartId);
            int NewEnteredId = new ShoppingCartModel().Insert(ShoppingCartModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel> { };
                lstShoppingCartModel = new ShoppingCartModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstShoppingCartModel.Count];

                for (int i = 0; i < lstShoppingCartModel.Count; i++)
                {
                    NewEnteredIds[i] = lstShoppingCartModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ShoppingCartModel ShoppingCartModel = new ShoppingCartModel().Select1ByShoppingCartIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ShoppingCartModel.Insert();
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
            List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel> { };

            if (ExportationType == "All")
            {
                lstShoppingCartModel = new ShoppingCartModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ShoppingCartModel ShoppingCartModel = new ShoppingCartModel().Select1ByShoppingCartIdToModel(Convert.ToInt32(RowChecked));
                    lstShoppingCartModel.Add(ShoppingCartModel);
                }
            }

            foreach (ShoppingCartModel row in lstShoppingCartModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of ShoppingCart</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ShoppingCartId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Counter&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/MarshallStore/ShoppingCart/ShoppingCart_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtShoppingCart = new DataTable();
                dtShoppingCart.TableName = "ShoppingCart";

                //We define another DataTable dtShoppingCartCopy to avoid issue related to DateTime conversion
                DataTable dtShoppingCartCopy = new DataTable();
                dtShoppingCartCopy.TableName = "ShoppingCart";

                #region Define columns for dtShoppingCartCopy
                DataColumn dtColumnShoppingCartIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnShoppingCartIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnShoppingCartIdFordtShoppingCartCopy.ColumnName = "ShoppingCartId";
                    dtShoppingCartCopy.Columns.Add(dtColumnShoppingCartIdFordtShoppingCartCopy);

                    DataColumn dtColumnActiveFordtShoppingCartCopy = new DataColumn();
                    dtColumnActiveFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnActiveFordtShoppingCartCopy.ColumnName = "Active";
                    dtShoppingCartCopy.Columns.Add(dtColumnActiveFordtShoppingCartCopy);

                    DataColumn dtColumnDateTimeCreationFordtShoppingCartCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtShoppingCartCopy.ColumnName = "DateTimeCreation";
                    dtShoppingCartCopy.Columns.Add(dtColumnDateTimeCreationFordtShoppingCartCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtShoppingCartCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtShoppingCartCopy.ColumnName = "DateTimeLastModification";
                    dtShoppingCartCopy.Columns.Add(dtColumnDateTimeLastModificationFordtShoppingCartCopy);

                    DataColumn dtColumnUserCreationIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnUserCreationIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtShoppingCartCopy.ColumnName = "UserCreationId";
                    dtShoppingCartCopy.Columns.Add(dtColumnUserCreationIdFordtShoppingCartCopy);

                    DataColumn dtColumnUserLastModificationIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtShoppingCartCopy.ColumnName = "UserLastModificationId";
                    dtShoppingCartCopy.Columns.Add(dtColumnUserLastModificationIdFordtShoppingCartCopy);

                    DataColumn dtColumnProductIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnProductIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnProductIdFordtShoppingCartCopy.ColumnName = "ProductId";
                    dtShoppingCartCopy.Columns.Add(dtColumnProductIdFordtShoppingCartCopy);

                    DataColumn dtColumnCounterFordtShoppingCartCopy = new DataColumn();
                    dtColumnCounterFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnCounterFordtShoppingCartCopy.ColumnName = "Counter";
                    dtShoppingCartCopy.Columns.Add(dtColumnCounterFordtShoppingCartCopy);

                    
                #endregion

                dtShoppingCart = new ShoppingCartModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtShoppingCart.Rows)
                {
                    dtShoppingCartCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtShoppingCartCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/ShoppingCarting/ShoppingCart/ShoppingCart_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsShoppingCart = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtShoppingCartCopy to avoid issue related to DateTime conversion
                    DataTable dtShoppingCartCopy = new DataTable();
                    dtShoppingCartCopy.TableName = "ShoppingCart";

                    #region Define columns for dtShoppingCartCopy
                    DataColumn dtColumnShoppingCartIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnShoppingCartIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnShoppingCartIdFordtShoppingCartCopy.ColumnName = "ShoppingCartId";
                    dtShoppingCartCopy.Columns.Add(dtColumnShoppingCartIdFordtShoppingCartCopy);

                    DataColumn dtColumnActiveFordtShoppingCartCopy = new DataColumn();
                    dtColumnActiveFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnActiveFordtShoppingCartCopy.ColumnName = "Active";
                    dtShoppingCartCopy.Columns.Add(dtColumnActiveFordtShoppingCartCopy);

                    DataColumn dtColumnDateTimeCreationFordtShoppingCartCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtShoppingCartCopy.ColumnName = "DateTimeCreation";
                    dtShoppingCartCopy.Columns.Add(dtColumnDateTimeCreationFordtShoppingCartCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtShoppingCartCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtShoppingCartCopy.ColumnName = "DateTimeLastModification";
                    dtShoppingCartCopy.Columns.Add(dtColumnDateTimeLastModificationFordtShoppingCartCopy);

                    DataColumn dtColumnUserCreationIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnUserCreationIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtShoppingCartCopy.ColumnName = "UserCreationId";
                    dtShoppingCartCopy.Columns.Add(dtColumnUserCreationIdFordtShoppingCartCopy);

                    DataColumn dtColumnUserLastModificationIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtShoppingCartCopy.ColumnName = "UserLastModificationId";
                    dtShoppingCartCopy.Columns.Add(dtColumnUserLastModificationIdFordtShoppingCartCopy);

                    DataColumn dtColumnProductIdFordtShoppingCartCopy = new DataColumn();
                    dtColumnProductIdFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnProductIdFordtShoppingCartCopy.ColumnName = "ProductId";
                    dtShoppingCartCopy.Columns.Add(dtColumnProductIdFordtShoppingCartCopy);

                    DataColumn dtColumnCounterFordtShoppingCartCopy = new DataColumn();
                    dtColumnCounterFordtShoppingCartCopy.DataType = typeof(string);
                    dtColumnCounterFordtShoppingCartCopy.ColumnName = "Counter";
                    dtShoppingCartCopy.Columns.Add(dtColumnCounterFordtShoppingCartCopy);

                    
                    #endregion

                    dsShoppingCart.Tables.Add(dtShoppingCartCopy);

                    for (int i = 0; i < dsShoppingCart.Tables.Count; i++)
                    {
                        dtShoppingCartCopy = new ShoppingCartModel().Select1ByShoppingCartIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtShoppingCartCopy.Rows)
                        {
                            dsShoppingCart.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsShoppingCart.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsShoppingCart.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/ShoppingCarting/ShoppingCart/ShoppingCart_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel> { };

            if (ExportationType == "All")
            {
                lstShoppingCartModel = new ShoppingCartModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ShoppingCartModel ShoppingCartModel = new ShoppingCartModel().Select1ByShoppingCartIdToModel(Convert.ToInt32(RowChecked));
                    lstShoppingCartModel.Add(ShoppingCartModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/ShoppingCarting/ShoppingCart/ShoppingCart_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstShoppingCartModel);
            }

            return Now;
        }
        #endregion
    }
}