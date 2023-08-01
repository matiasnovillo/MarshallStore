using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using MarshallStore.Areas.Examples.Models;
using MarshallStore.Areas.Examples.DTOs;
using MarshallStore.Areas.Examples.Interfaces;
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

//Last modification on: 21/02/2023 18:16:41

namespace MarshallStore.Areas.Examples.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 21/02/2023 18:16:41
    /// </summary>
    public partial class ExampleService : IExample
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ExampleService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ExampleModel Select1ByExampleIdToModel(int ExampleId)
        {
            return new ExampleModel().Select1ByExampleIdToModel(ExampleId);
        }

        public List<ExampleModel> SelectAllToList()
        {
            return new ExampleModel().SelectAllToList();
        }

        public exampleSelectAllPaged SelectAllPagedToModel(exampleSelectAllPaged exampleSelectAllPaged)
        {
            return new ExampleModel().SelectAllPagedToModel(exampleSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ExampleModel ExampleModel)
        {
            return new ExampleModel().Insert(ExampleModel);
        }

        public int UpdateByExampleId(ExampleModel ExampleModel)
        {
            return new ExampleModel().UpdateByExampleId(ExampleModel);
        }

        public int DeleteByExampleId(int ExampleId)
        {
            return new ExampleModel().DeleteByExampleId(ExampleId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ExampleModel ExampleModel = new ExampleModel();
                ExampleModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ExampleModel ExampleModel = new ExampleModel().Select1ByExampleIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ExampleModel.DeleteByExampleId(ExampleModel.ExampleId);
                }
            }
        }

        public int CopyByExampleId(int ExampleId)
        {
            ExampleModel ExampleModel = new ExampleModel().Select1ByExampleIdToModel(ExampleId);
            int NewEnteredId = new ExampleModel().Insert(ExampleModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ExampleModel> lstExampleModel = new List<ExampleModel> { };
                lstExampleModel = new ExampleModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstExampleModel.Count];

                for (int i = 0; i < lstExampleModel.Count; i++)
                {
                    NewEnteredIds[i] = lstExampleModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ExampleModel ExampleModel = new ExampleModel().Select1ByExampleIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ExampleModel.Insert();
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
            List<ExampleModel> lstExampleModel = new List<ExampleModel> { };

            if (ExportationType == "All")
            {
                lstExampleModel = new ExampleModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ExampleModel ExampleModel = new ExampleModel().Select1ByExampleIdToModel(Convert.ToInt32(RowChecked));
                    lstExampleModel.Add(ExampleModel);
                }
            }

            foreach (ExampleModel row in lstExampleModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Example</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ExampleId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Boolean&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTime&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Decimal&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Integer&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextBasic&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextEmail&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextFile&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextPassword&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextPhoneNumber&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextTag&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextTextArea&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextTextEditor&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextURL&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ForeignKeyDropDown&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ForeignKeyOption&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextHexColour&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Time&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Examples/Example/Example_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtExample = new DataTable();
                dtExample.TableName = "Example";

                //We define another DataTable dtExampleCopy to avoid issue related to DateTime conversion
                DataTable dtExampleCopy = new DataTable();
                dtExampleCopy.TableName = "Example";

                #region Define columns for dtExampleCopy
                DataColumn dtColumnExampleIdFordtExampleCopy = new DataColumn();
                    dtColumnExampleIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnExampleIdFordtExampleCopy.ColumnName = "ExampleId";
                    dtExampleCopy.Columns.Add(dtColumnExampleIdFordtExampleCopy);

                    DataColumn dtColumnActiveFordtExampleCopy = new DataColumn();
                    dtColumnActiveFordtExampleCopy.DataType = typeof(string);
                    dtColumnActiveFordtExampleCopy.ColumnName = "Active";
                    dtExampleCopy.Columns.Add(dtColumnActiveFordtExampleCopy);

                    DataColumn dtColumnDateTimeCreationFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtExampleCopy.ColumnName = "DateTimeCreation";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeCreationFordtExampleCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtExampleCopy.ColumnName = "DateTimeLastModification";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeLastModificationFordtExampleCopy);

                    DataColumn dtColumnUserCreationIdFordtExampleCopy = new DataColumn();
                    dtColumnUserCreationIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtExampleCopy.ColumnName = "UserCreationId";
                    dtExampleCopy.Columns.Add(dtColumnUserCreationIdFordtExampleCopy);

                    DataColumn dtColumnUserLastModificationIdFordtExampleCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtExampleCopy.ColumnName = "UserLastModificationId";
                    dtExampleCopy.Columns.Add(dtColumnUserLastModificationIdFordtExampleCopy);

                    DataColumn dtColumnBooleanFordtExampleCopy = new DataColumn();
                    dtColumnBooleanFordtExampleCopy.DataType = typeof(string);
                    dtColumnBooleanFordtExampleCopy.ColumnName = "Boolean";
                    dtExampleCopy.Columns.Add(dtColumnBooleanFordtExampleCopy);

                    DataColumn dtColumnDateTimeFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeFordtExampleCopy.ColumnName = "DateTime";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeFordtExampleCopy);

                    DataColumn dtColumnDecimalFordtExampleCopy = new DataColumn();
                    dtColumnDecimalFordtExampleCopy.DataType = typeof(string);
                    dtColumnDecimalFordtExampleCopy.ColumnName = "Decimal";
                    dtExampleCopy.Columns.Add(dtColumnDecimalFordtExampleCopy);

                    DataColumn dtColumnIntegerFordtExampleCopy = new DataColumn();
                    dtColumnIntegerFordtExampleCopy.DataType = typeof(string);
                    dtColumnIntegerFordtExampleCopy.ColumnName = "Integer";
                    dtExampleCopy.Columns.Add(dtColumnIntegerFordtExampleCopy);

                    DataColumn dtColumnTextBasicFordtExampleCopy = new DataColumn();
                    dtColumnTextBasicFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextBasicFordtExampleCopy.ColumnName = "TextBasic";
                    dtExampleCopy.Columns.Add(dtColumnTextBasicFordtExampleCopy);

                    DataColumn dtColumnTextEmailFordtExampleCopy = new DataColumn();
                    dtColumnTextEmailFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextEmailFordtExampleCopy.ColumnName = "TextEmail";
                    dtExampleCopy.Columns.Add(dtColumnTextEmailFordtExampleCopy);

                    DataColumn dtColumnTextFileFordtExampleCopy = new DataColumn();
                    dtColumnTextFileFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextFileFordtExampleCopy.ColumnName = "TextFile";
                    dtExampleCopy.Columns.Add(dtColumnTextFileFordtExampleCopy);

                    DataColumn dtColumnTextPasswordFordtExampleCopy = new DataColumn();
                    dtColumnTextPasswordFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextPasswordFordtExampleCopy.ColumnName = "TextPassword";
                    dtExampleCopy.Columns.Add(dtColumnTextPasswordFordtExampleCopy);

                    DataColumn dtColumnTextPhoneNumberFordtExampleCopy = new DataColumn();
                    dtColumnTextPhoneNumberFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextPhoneNumberFordtExampleCopy.ColumnName = "TextPhoneNumber";
                    dtExampleCopy.Columns.Add(dtColumnTextPhoneNumberFordtExampleCopy);

                    DataColumn dtColumnTextTagFordtExampleCopy = new DataColumn();
                    dtColumnTextTagFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTagFordtExampleCopy.ColumnName = "TextTag";
                    dtExampleCopy.Columns.Add(dtColumnTextTagFordtExampleCopy);

                    DataColumn dtColumnTextTextAreaFordtExampleCopy = new DataColumn();
                    dtColumnTextTextAreaFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTextAreaFordtExampleCopy.ColumnName = "TextTextArea";
                    dtExampleCopy.Columns.Add(dtColumnTextTextAreaFordtExampleCopy);

                    DataColumn dtColumnTextTextEditorFordtExampleCopy = new DataColumn();
                    dtColumnTextTextEditorFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTextEditorFordtExampleCopy.ColumnName = "TextTextEditor";
                    dtExampleCopy.Columns.Add(dtColumnTextTextEditorFordtExampleCopy);

                    DataColumn dtColumnTextURLFordtExampleCopy = new DataColumn();
                    dtColumnTextURLFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextURLFordtExampleCopy.ColumnName = "TextURL";
                    dtExampleCopy.Columns.Add(dtColumnTextURLFordtExampleCopy);

                    DataColumn dtColumnForeignKeyDropDownFordtExampleCopy = new DataColumn();
                    dtColumnForeignKeyDropDownFordtExampleCopy.DataType = typeof(string);
                    dtColumnForeignKeyDropDownFordtExampleCopy.ColumnName = "ForeignKeyDropDown";
                    dtExampleCopy.Columns.Add(dtColumnForeignKeyDropDownFordtExampleCopy);

                    DataColumn dtColumnForeignKeyOptionFordtExampleCopy = new DataColumn();
                    dtColumnForeignKeyOptionFordtExampleCopy.DataType = typeof(string);
                    dtColumnForeignKeyOptionFordtExampleCopy.ColumnName = "ForeignKeyOption";
                    dtExampleCopy.Columns.Add(dtColumnForeignKeyOptionFordtExampleCopy);

                    DataColumn dtColumnTextHexColourFordtExampleCopy = new DataColumn();
                    dtColumnTextHexColourFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextHexColourFordtExampleCopy.ColumnName = "TextHexColour";
                    dtExampleCopy.Columns.Add(dtColumnTextHexColourFordtExampleCopy);

                    DataColumn dtColumnTimeFordtExampleCopy = new DataColumn();
                    dtColumnTimeFordtExampleCopy.DataType = typeof(string);
                    dtColumnTimeFordtExampleCopy.ColumnName = "Time";
                    dtExampleCopy.Columns.Add(dtColumnTimeFordtExampleCopy);

                    
                #endregion

                dtExample = new ExampleModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtExample.Rows)
                {
                    dtExampleCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtExampleCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Exampleing/Example/Example_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsExample = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtExampleCopy to avoid issue related to DateTime conversion
                    DataTable dtExampleCopy = new DataTable();
                    dtExampleCopy.TableName = "Example";

                    #region Define columns for dtExampleCopy
                    DataColumn dtColumnExampleIdFordtExampleCopy = new DataColumn();
                    dtColumnExampleIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnExampleIdFordtExampleCopy.ColumnName = "ExampleId";
                    dtExampleCopy.Columns.Add(dtColumnExampleIdFordtExampleCopy);

                    DataColumn dtColumnActiveFordtExampleCopy = new DataColumn();
                    dtColumnActiveFordtExampleCopy.DataType = typeof(string);
                    dtColumnActiveFordtExampleCopy.ColumnName = "Active";
                    dtExampleCopy.Columns.Add(dtColumnActiveFordtExampleCopy);

                    DataColumn dtColumnDateTimeCreationFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtExampleCopy.ColumnName = "DateTimeCreation";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeCreationFordtExampleCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtExampleCopy.ColumnName = "DateTimeLastModification";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeLastModificationFordtExampleCopy);

                    DataColumn dtColumnUserCreationIdFordtExampleCopy = new DataColumn();
                    dtColumnUserCreationIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtExampleCopy.ColumnName = "UserCreationId";
                    dtExampleCopy.Columns.Add(dtColumnUserCreationIdFordtExampleCopy);

                    DataColumn dtColumnUserLastModificationIdFordtExampleCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtExampleCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtExampleCopy.ColumnName = "UserLastModificationId";
                    dtExampleCopy.Columns.Add(dtColumnUserLastModificationIdFordtExampleCopy);

                    DataColumn dtColumnBooleanFordtExampleCopy = new DataColumn();
                    dtColumnBooleanFordtExampleCopy.DataType = typeof(string);
                    dtColumnBooleanFordtExampleCopy.ColumnName = "Boolean";
                    dtExampleCopy.Columns.Add(dtColumnBooleanFordtExampleCopy);

                    DataColumn dtColumnDateTimeFordtExampleCopy = new DataColumn();
                    dtColumnDateTimeFordtExampleCopy.DataType = typeof(string);
                    dtColumnDateTimeFordtExampleCopy.ColumnName = "DateTime";
                    dtExampleCopy.Columns.Add(dtColumnDateTimeFordtExampleCopy);

                    DataColumn dtColumnDecimalFordtExampleCopy = new DataColumn();
                    dtColumnDecimalFordtExampleCopy.DataType = typeof(string);
                    dtColumnDecimalFordtExampleCopy.ColumnName = "Decimal";
                    dtExampleCopy.Columns.Add(dtColumnDecimalFordtExampleCopy);

                    DataColumn dtColumnIntegerFordtExampleCopy = new DataColumn();
                    dtColumnIntegerFordtExampleCopy.DataType = typeof(string);
                    dtColumnIntegerFordtExampleCopy.ColumnName = "Integer";
                    dtExampleCopy.Columns.Add(dtColumnIntegerFordtExampleCopy);

                    DataColumn dtColumnTextBasicFordtExampleCopy = new DataColumn();
                    dtColumnTextBasicFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextBasicFordtExampleCopy.ColumnName = "TextBasic";
                    dtExampleCopy.Columns.Add(dtColumnTextBasicFordtExampleCopy);

                    DataColumn dtColumnTextEmailFordtExampleCopy = new DataColumn();
                    dtColumnTextEmailFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextEmailFordtExampleCopy.ColumnName = "TextEmail";
                    dtExampleCopy.Columns.Add(dtColumnTextEmailFordtExampleCopy);

                    DataColumn dtColumnTextFileFordtExampleCopy = new DataColumn();
                    dtColumnTextFileFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextFileFordtExampleCopy.ColumnName = "TextFile";
                    dtExampleCopy.Columns.Add(dtColumnTextFileFordtExampleCopy);

                    DataColumn dtColumnTextPasswordFordtExampleCopy = new DataColumn();
                    dtColumnTextPasswordFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextPasswordFordtExampleCopy.ColumnName = "TextPassword";
                    dtExampleCopy.Columns.Add(dtColumnTextPasswordFordtExampleCopy);

                    DataColumn dtColumnTextPhoneNumberFordtExampleCopy = new DataColumn();
                    dtColumnTextPhoneNumberFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextPhoneNumberFordtExampleCopy.ColumnName = "TextPhoneNumber";
                    dtExampleCopy.Columns.Add(dtColumnTextPhoneNumberFordtExampleCopy);

                    DataColumn dtColumnTextTagFordtExampleCopy = new DataColumn();
                    dtColumnTextTagFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTagFordtExampleCopy.ColumnName = "TextTag";
                    dtExampleCopy.Columns.Add(dtColumnTextTagFordtExampleCopy);

                    DataColumn dtColumnTextTextAreaFordtExampleCopy = new DataColumn();
                    dtColumnTextTextAreaFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTextAreaFordtExampleCopy.ColumnName = "TextTextArea";
                    dtExampleCopy.Columns.Add(dtColumnTextTextAreaFordtExampleCopy);

                    DataColumn dtColumnTextTextEditorFordtExampleCopy = new DataColumn();
                    dtColumnTextTextEditorFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextTextEditorFordtExampleCopy.ColumnName = "TextTextEditor";
                    dtExampleCopy.Columns.Add(dtColumnTextTextEditorFordtExampleCopy);

                    DataColumn dtColumnTextURLFordtExampleCopy = new DataColumn();
                    dtColumnTextURLFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextURLFordtExampleCopy.ColumnName = "TextURL";
                    dtExampleCopy.Columns.Add(dtColumnTextURLFordtExampleCopy);

                    DataColumn dtColumnForeignKeyDropDownFordtExampleCopy = new DataColumn();
                    dtColumnForeignKeyDropDownFordtExampleCopy.DataType = typeof(string);
                    dtColumnForeignKeyDropDownFordtExampleCopy.ColumnName = "ForeignKeyDropDown";
                    dtExampleCopy.Columns.Add(dtColumnForeignKeyDropDownFordtExampleCopy);

                    DataColumn dtColumnForeignKeyOptionFordtExampleCopy = new DataColumn();
                    dtColumnForeignKeyOptionFordtExampleCopy.DataType = typeof(string);
                    dtColumnForeignKeyOptionFordtExampleCopy.ColumnName = "ForeignKeyOption";
                    dtExampleCopy.Columns.Add(dtColumnForeignKeyOptionFordtExampleCopy);

                    DataColumn dtColumnTextHexColourFordtExampleCopy = new DataColumn();
                    dtColumnTextHexColourFordtExampleCopy.DataType = typeof(string);
                    dtColumnTextHexColourFordtExampleCopy.ColumnName = "TextHexColour";
                    dtExampleCopy.Columns.Add(dtColumnTextHexColourFordtExampleCopy);

                    DataColumn dtColumnTimeFordtExampleCopy = new DataColumn();
                    dtColumnTimeFordtExampleCopy.DataType = typeof(string);
                    dtColumnTimeFordtExampleCopy.ColumnName = "Time";
                    dtExampleCopy.Columns.Add(dtColumnTimeFordtExampleCopy);

                    
                    #endregion

                    dsExample.Tables.Add(dtExampleCopy);

                    for (int i = 0; i < dsExample.Tables.Count; i++)
                    {
                        dtExampleCopy = new ExampleModel().Select1ByExampleIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtExampleCopy.Rows)
                        {
                            dsExample.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsExample.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsExample.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Exampleing/Example/Example_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ExampleModel> lstExampleModel = new List<ExampleModel> { };

            if (ExportationType == "All")
            {
                lstExampleModel = new ExampleModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ExampleModel ExampleModel = new ExampleModel().Select1ByExampleIdToModel(Convert.ToInt32(RowChecked));
                    lstExampleModel.Add(ExampleModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Exampleing/Example/Example_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstExampleModel);
            }

            return Now;
        }
        #endregion
    }
}