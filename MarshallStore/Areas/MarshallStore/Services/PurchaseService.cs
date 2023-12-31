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

//Last modification on: 03/08/2023 18:27:16

namespace MarshallStore.Areas.MarshallStore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 03/08/2023 18:27:16
    /// </summary>
    public partial class PurchaseService : IPurchase
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public PurchaseService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public PurchaseModel Select1ByPurchaseIdToModel(int PurchaseId)
        {
            return new PurchaseModel().Select1ByPurchaseIdToModel(PurchaseId);
        }

        public List<PurchaseModel> SelectAllToList()
        {
            return new PurchaseModel().SelectAllToList();
        }

        public purchaseSelectAllPaged SelectAllPagedToModel(purchaseSelectAllPaged purchaseSelectAllPaged)
        {
            return new PurchaseModel().SelectAllPagedToModel(purchaseSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(PurchaseModel PurchaseModel)
        {
            int NewPurchaseId = new PurchaseModel().Insert(PurchaseModel);

            ShoppingCartModel ShoppingCartModel = new ShoppingCartModel();

            //Insert ProductId and PurchaseId in PurchaseProductModel
            List<ShoppingCartModel> lstShoppingCartModel = ShoppingCartModel.SelectAllByUserCreationIdToList(PurchaseModel.UserCreationId);
            foreach (ShoppingCartModel shoppingCartModel in lstShoppingCartModel)
            {
                PurchaseProductModel PurchaseProductModel = new PurchaseProductModel()
                {
                    Active = true,
                    DateTimeCreation = DateTime.Now,
                    DateTimeLastModification = DateTime.Now,
                    PurchaseId = NewPurchaseId,
                    UserCreationId = PurchaseModel.UserCreationId,
                    UserLastModificationId = PurchaseModel.UserCreationId,
                    ProductId = shoppingCartModel.ProductId
                };

                PurchaseProductModel.Insert();
            }

            //Delete ShoppingCart by UserCreationId to show empty cart once the user made the purchase
            ShoppingCartModel.DeleteByUserCreationId(PurchaseModel.UserCreationId);

            return NewPurchaseId;
        }

        public int UpdateByPurchaseId(PurchaseModel PurchaseModel)
        {
            return new PurchaseModel().UpdateByPurchaseId(PurchaseModel);
        }

        public int DeleteByPurchaseId(int PurchaseId)
        {
            return new PurchaseModel().DeleteByPurchaseId(PurchaseId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                PurchaseModel PurchaseModel = new PurchaseModel();
                PurchaseModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PurchaseModel PurchaseModel = new PurchaseModel().Select1ByPurchaseIdToModel(Convert.ToInt32(RowsChecked[i]));
                    PurchaseModel.DeleteByPurchaseId(PurchaseModel.PurchaseId);
                }
            }
        }

        public int CopyByPurchaseId(int PurchaseId)
        {
            PurchaseModel PurchaseModel = new PurchaseModel().Select1ByPurchaseIdToModel(PurchaseId);
            int NewEnteredId = new PurchaseModel().Insert(PurchaseModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel> { };
                lstPurchaseModel = new PurchaseModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstPurchaseModel.Count];

                for (int i = 0; i < lstPurchaseModel.Count; i++)
                {
                    NewEnteredIds[i] = lstPurchaseModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PurchaseModel PurchaseModel = new PurchaseModel().Select1ByPurchaseIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = PurchaseModel.Insert();
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
            List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel> { };

            if (ExportationType == "All")
            {
                lstPurchaseModel = new PurchaseModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PurchaseModel PurchaseModel = new PurchaseModel().Select1ByPurchaseIdToModel(Convert.ToInt32(RowChecked));
                    lstPurchaseModel.Add(PurchaseModel);
                }
            }

            foreach (PurchaseModel row in lstPurchaseModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Purchase</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PurchaseId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FullPrice&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FirstName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">LastName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Email&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Phone&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">StreetAddress&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PostCodeOrZip&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">City&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Country&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">CardNumber&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">CardHolder&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Expiration&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">CVC&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/MarshallStore/Purchase/Purchase_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtPurchase = new DataTable();
                dtPurchase.TableName = "Purchase";

                //We define another DataTable dtPurchaseCopy to avoid issue related to DateTime conversion
                DataTable dtPurchaseCopy = new DataTable();
                dtPurchaseCopy.TableName = "Purchase";

                #region Define columns for dtPurchaseCopy
                DataColumn dtColumnPurchaseIdFordtPurchaseCopy = new DataColumn();
                    dtColumnPurchaseIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPurchaseIdFordtPurchaseCopy.ColumnName = "PurchaseId";
                    dtPurchaseCopy.Columns.Add(dtColumnPurchaseIdFordtPurchaseCopy);

                    DataColumn dtColumnActiveFordtPurchaseCopy = new DataColumn();
                    dtColumnActiveFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnActiveFordtPurchaseCopy.ColumnName = "Active";
                    dtPurchaseCopy.Columns.Add(dtColumnActiveFordtPurchaseCopy);

                    DataColumn dtColumnDateTimeCreationFordtPurchaseCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPurchaseCopy.ColumnName = "DateTimeCreation";
                    dtPurchaseCopy.Columns.Add(dtColumnDateTimeCreationFordtPurchaseCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPurchaseCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPurchaseCopy.ColumnName = "DateTimeLastModification";
                    dtPurchaseCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPurchaseCopy);

                    DataColumn dtColumnUserCreationIdFordtPurchaseCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPurchaseCopy.ColumnName = "UserCreationId";
                    dtPurchaseCopy.Columns.Add(dtColumnUserCreationIdFordtPurchaseCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPurchaseCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPurchaseCopy.ColumnName = "UserLastModificationId";
                    dtPurchaseCopy.Columns.Add(dtColumnUserLastModificationIdFordtPurchaseCopy);

                    DataColumn dtColumnFullPriceFordtPurchaseCopy = new DataColumn();
                    dtColumnFullPriceFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnFullPriceFordtPurchaseCopy.ColumnName = "FullPrice";
                    dtPurchaseCopy.Columns.Add(dtColumnFullPriceFordtPurchaseCopy);

                    DataColumn dtColumnFirstNameFordtPurchaseCopy = new DataColumn();
                    dtColumnFirstNameFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnFirstNameFordtPurchaseCopy.ColumnName = "FirstName";
                    dtPurchaseCopy.Columns.Add(dtColumnFirstNameFordtPurchaseCopy);

                    DataColumn dtColumnLastNameFordtPurchaseCopy = new DataColumn();
                    dtColumnLastNameFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnLastNameFordtPurchaseCopy.ColumnName = "LastName";
                    dtPurchaseCopy.Columns.Add(dtColumnLastNameFordtPurchaseCopy);

                    DataColumn dtColumnEmailFordtPurchaseCopy = new DataColumn();
                    dtColumnEmailFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnEmailFordtPurchaseCopy.ColumnName = "Email";
                    dtPurchaseCopy.Columns.Add(dtColumnEmailFordtPurchaseCopy);

                    DataColumn dtColumnPhoneFordtPurchaseCopy = new DataColumn();
                    dtColumnPhoneFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPhoneFordtPurchaseCopy.ColumnName = "Phone";
                    dtPurchaseCopy.Columns.Add(dtColumnPhoneFordtPurchaseCopy);

                    DataColumn dtColumnStreetAddressFordtPurchaseCopy = new DataColumn();
                    dtColumnStreetAddressFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnStreetAddressFordtPurchaseCopy.ColumnName = "StreetAddress";
                    dtPurchaseCopy.Columns.Add(dtColumnStreetAddressFordtPurchaseCopy);

                    DataColumn dtColumnPostCodeOrZipFordtPurchaseCopy = new DataColumn();
                    dtColumnPostCodeOrZipFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPostCodeOrZipFordtPurchaseCopy.ColumnName = "PostCodeOrZip";
                    dtPurchaseCopy.Columns.Add(dtColumnPostCodeOrZipFordtPurchaseCopy);

                    DataColumn dtColumnCityFordtPurchaseCopy = new DataColumn();
                    dtColumnCityFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCityFordtPurchaseCopy.ColumnName = "City";
                    dtPurchaseCopy.Columns.Add(dtColumnCityFordtPurchaseCopy);

                    DataColumn dtColumnCountryFordtPurchaseCopy = new DataColumn();
                    dtColumnCountryFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCountryFordtPurchaseCopy.ColumnName = "Country";
                    dtPurchaseCopy.Columns.Add(dtColumnCountryFordtPurchaseCopy);

                    DataColumn dtColumnCardNumberFordtPurchaseCopy = new DataColumn();
                    dtColumnCardNumberFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCardNumberFordtPurchaseCopy.ColumnName = "CardNumber";
                    dtPurchaseCopy.Columns.Add(dtColumnCardNumberFordtPurchaseCopy);

                    DataColumn dtColumnCardHolderFordtPurchaseCopy = new DataColumn();
                    dtColumnCardHolderFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCardHolderFordtPurchaseCopy.ColumnName = "CardHolder";
                    dtPurchaseCopy.Columns.Add(dtColumnCardHolderFordtPurchaseCopy);

                    DataColumn dtColumnExpirationFordtPurchaseCopy = new DataColumn();
                    dtColumnExpirationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnExpirationFordtPurchaseCopy.ColumnName = "Expiration";
                    dtPurchaseCopy.Columns.Add(dtColumnExpirationFordtPurchaseCopy);

                    DataColumn dtColumnCVCFordtPurchaseCopy = new DataColumn();
                    dtColumnCVCFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCVCFordtPurchaseCopy.ColumnName = "CVC";
                    dtPurchaseCopy.Columns.Add(dtColumnCVCFordtPurchaseCopy);

                    
                #endregion

                dtPurchase = new PurchaseModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtPurchase.Rows)
                {
                    dtPurchaseCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtPurchaseCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Purchaseing/Purchase/Purchase_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsPurchase = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtPurchaseCopy to avoid issue related to DateTime conversion
                    DataTable dtPurchaseCopy = new DataTable();
                    dtPurchaseCopy.TableName = "Purchase";

                    #region Define columns for dtPurchaseCopy
                    DataColumn dtColumnPurchaseIdFordtPurchaseCopy = new DataColumn();
                    dtColumnPurchaseIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPurchaseIdFordtPurchaseCopy.ColumnName = "PurchaseId";
                    dtPurchaseCopy.Columns.Add(dtColumnPurchaseIdFordtPurchaseCopy);

                    DataColumn dtColumnActiveFordtPurchaseCopy = new DataColumn();
                    dtColumnActiveFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnActiveFordtPurchaseCopy.ColumnName = "Active";
                    dtPurchaseCopy.Columns.Add(dtColumnActiveFordtPurchaseCopy);

                    DataColumn dtColumnDateTimeCreationFordtPurchaseCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPurchaseCopy.ColumnName = "DateTimeCreation";
                    dtPurchaseCopy.Columns.Add(dtColumnDateTimeCreationFordtPurchaseCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPurchaseCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPurchaseCopy.ColumnName = "DateTimeLastModification";
                    dtPurchaseCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPurchaseCopy);

                    DataColumn dtColumnUserCreationIdFordtPurchaseCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPurchaseCopy.ColumnName = "UserCreationId";
                    dtPurchaseCopy.Columns.Add(dtColumnUserCreationIdFordtPurchaseCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPurchaseCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPurchaseCopy.ColumnName = "UserLastModificationId";
                    dtPurchaseCopy.Columns.Add(dtColumnUserLastModificationIdFordtPurchaseCopy);

                    DataColumn dtColumnFullPriceFordtPurchaseCopy = new DataColumn();
                    dtColumnFullPriceFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnFullPriceFordtPurchaseCopy.ColumnName = "FullPrice";
                    dtPurchaseCopy.Columns.Add(dtColumnFullPriceFordtPurchaseCopy);

                    DataColumn dtColumnFirstNameFordtPurchaseCopy = new DataColumn();
                    dtColumnFirstNameFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnFirstNameFordtPurchaseCopy.ColumnName = "FirstName";
                    dtPurchaseCopy.Columns.Add(dtColumnFirstNameFordtPurchaseCopy);

                    DataColumn dtColumnLastNameFordtPurchaseCopy = new DataColumn();
                    dtColumnLastNameFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnLastNameFordtPurchaseCopy.ColumnName = "LastName";
                    dtPurchaseCopy.Columns.Add(dtColumnLastNameFordtPurchaseCopy);

                    DataColumn dtColumnEmailFordtPurchaseCopy = new DataColumn();
                    dtColumnEmailFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnEmailFordtPurchaseCopy.ColumnName = "Email";
                    dtPurchaseCopy.Columns.Add(dtColumnEmailFordtPurchaseCopy);

                    DataColumn dtColumnPhoneFordtPurchaseCopy = new DataColumn();
                    dtColumnPhoneFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPhoneFordtPurchaseCopy.ColumnName = "Phone";
                    dtPurchaseCopy.Columns.Add(dtColumnPhoneFordtPurchaseCopy);

                    DataColumn dtColumnStreetAddressFordtPurchaseCopy = new DataColumn();
                    dtColumnStreetAddressFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnStreetAddressFordtPurchaseCopy.ColumnName = "StreetAddress";
                    dtPurchaseCopy.Columns.Add(dtColumnStreetAddressFordtPurchaseCopy);

                    DataColumn dtColumnPostCodeOrZipFordtPurchaseCopy = new DataColumn();
                    dtColumnPostCodeOrZipFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnPostCodeOrZipFordtPurchaseCopy.ColumnName = "PostCodeOrZip";
                    dtPurchaseCopy.Columns.Add(dtColumnPostCodeOrZipFordtPurchaseCopy);

                    DataColumn dtColumnCityFordtPurchaseCopy = new DataColumn();
                    dtColumnCityFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCityFordtPurchaseCopy.ColumnName = "City";
                    dtPurchaseCopy.Columns.Add(dtColumnCityFordtPurchaseCopy);

                    DataColumn dtColumnCountryFordtPurchaseCopy = new DataColumn();
                    dtColumnCountryFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCountryFordtPurchaseCopy.ColumnName = "Country";
                    dtPurchaseCopy.Columns.Add(dtColumnCountryFordtPurchaseCopy);

                    DataColumn dtColumnCardNumberFordtPurchaseCopy = new DataColumn();
                    dtColumnCardNumberFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCardNumberFordtPurchaseCopy.ColumnName = "CardNumber";
                    dtPurchaseCopy.Columns.Add(dtColumnCardNumberFordtPurchaseCopy);

                    DataColumn dtColumnCardHolderFordtPurchaseCopy = new DataColumn();
                    dtColumnCardHolderFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCardHolderFordtPurchaseCopy.ColumnName = "CardHolder";
                    dtPurchaseCopy.Columns.Add(dtColumnCardHolderFordtPurchaseCopy);

                    DataColumn dtColumnExpirationFordtPurchaseCopy = new DataColumn();
                    dtColumnExpirationFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnExpirationFordtPurchaseCopy.ColumnName = "Expiration";
                    dtPurchaseCopy.Columns.Add(dtColumnExpirationFordtPurchaseCopy);

                    DataColumn dtColumnCVCFordtPurchaseCopy = new DataColumn();
                    dtColumnCVCFordtPurchaseCopy.DataType = typeof(string);
                    dtColumnCVCFordtPurchaseCopy.ColumnName = "CVC";
                    dtPurchaseCopy.Columns.Add(dtColumnCVCFordtPurchaseCopy);

                    
                    #endregion

                    dsPurchase.Tables.Add(dtPurchaseCopy);

                    for (int i = 0; i < dsPurchase.Tables.Count; i++)
                    {
                        dtPurchaseCopy = new PurchaseModel().Select1ByPurchaseIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtPurchaseCopy.Rows)
                        {
                            dsPurchase.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsPurchase.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsPurchase.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Purchaseing/Purchase/Purchase_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel> { };

            if (ExportationType == "All")
            {
                lstPurchaseModel = new PurchaseModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PurchaseModel PurchaseModel = new PurchaseModel().Select1ByPurchaseIdToModel(Convert.ToInt32(RowChecked));
                    lstPurchaseModel.Add(PurchaseModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Purchaseing/Purchase/Purchase_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstPurchaseModel);
            }

            return Now;
        }
        #endregion
    }
}