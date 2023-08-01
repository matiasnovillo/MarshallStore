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

//Last modification on: 31/07/2023 14:24:26

namespace MarshallStore.Areas.MarshallStore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 31/07/2023 14:24:26
    /// </summary>
    public partial class CommentService : IComment
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public CommentService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public CommentModel Select1ByCommentIdToModel(int CommentId)
        {
            return new CommentModel().Select1ByCommentIdToModel(CommentId);
        }

        public List<CommentModel> SelectAllToList()
        {
            return new CommentModel().SelectAllToList();
        }

        public commentSelectAllPaged SelectAllPagedToModel(commentSelectAllPaged commentSelectAllPaged)
        {
            return new CommentModel().SelectAllPagedToModel(commentSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(CommentModel CommentModel)
        {
            return new CommentModel().Insert(CommentModel);
        }

        public int UpdateByCommentId(CommentModel CommentModel)
        {
            return new CommentModel().UpdateByCommentId(CommentModel);
        }

        public int DeleteByCommentId(int CommentId)
        {
            return new CommentModel().DeleteByCommentId(CommentId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                CommentModel CommentModel = new CommentModel();
                CommentModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    CommentModel CommentModel = new CommentModel().Select1ByCommentIdToModel(Convert.ToInt32(RowsChecked[i]));
                    CommentModel.DeleteByCommentId(CommentModel.CommentId);
                }
            }
        }

        public int CopyByCommentId(int CommentId)
        {
            CommentModel CommentModel = new CommentModel().Select1ByCommentIdToModel(CommentId);
            int NewEnteredId = new CommentModel().Insert(CommentModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<CommentModel> lstCommentModel = new List<CommentModel> { };
                lstCommentModel = new CommentModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstCommentModel.Count];

                for (int i = 0; i < lstCommentModel.Count; i++)
                {
                    NewEnteredIds[i] = lstCommentModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    CommentModel CommentModel = new CommentModel().Select1ByCommentIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = CommentModel.Insert();
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
            List<CommentModel> lstCommentModel = new List<CommentModel> { };

            if (ExportationType == "All")
            {
                lstCommentModel = new CommentModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    CommentModel CommentModel = new CommentModel().Select1ByCommentIdToModel(Convert.ToInt32(RowChecked));
                    lstCommentModel.Add(CommentModel);
                }
            }

            foreach (CommentModel row in lstCommentModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Comment</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">CommentId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Text&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/MarshallStore/Comment/Comment_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtComment = new DataTable();
                dtComment.TableName = "Comment";

                //We define another DataTable dtCommentCopy to avoid issue related to DateTime conversion
                DataTable dtCommentCopy = new DataTable();
                dtCommentCopy.TableName = "Comment";

                #region Define columns for dtCommentCopy
                DataColumn dtColumnCommentIdFordtCommentCopy = new DataColumn();
                    dtColumnCommentIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnCommentIdFordtCommentCopy.ColumnName = "CommentId";
                    dtCommentCopy.Columns.Add(dtColumnCommentIdFordtCommentCopy);

                    DataColumn dtColumnActiveFordtCommentCopy = new DataColumn();
                    dtColumnActiveFordtCommentCopy.DataType = typeof(string);
                    dtColumnActiveFordtCommentCopy.ColumnName = "Active";
                    dtCommentCopy.Columns.Add(dtColumnActiveFordtCommentCopy);

                    DataColumn dtColumnDateTimeCreationFordtCommentCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtCommentCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtCommentCopy.ColumnName = "DateTimeCreation";
                    dtCommentCopy.Columns.Add(dtColumnDateTimeCreationFordtCommentCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtCommentCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtCommentCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtCommentCopy.ColumnName = "DateTimeLastModification";
                    dtCommentCopy.Columns.Add(dtColumnDateTimeLastModificationFordtCommentCopy);

                    DataColumn dtColumnUserCreationIdFordtCommentCopy = new DataColumn();
                    dtColumnUserCreationIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtCommentCopy.ColumnName = "UserCreationId";
                    dtCommentCopy.Columns.Add(dtColumnUserCreationIdFordtCommentCopy);

                    DataColumn dtColumnUserLastModificationIdFordtCommentCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtCommentCopy.ColumnName = "UserLastModificationId";
                    dtCommentCopy.Columns.Add(dtColumnUserLastModificationIdFordtCommentCopy);

                    DataColumn dtColumnProductIdFordtCommentCopy = new DataColumn();
                    dtColumnProductIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnProductIdFordtCommentCopy.ColumnName = "ProductId";
                    dtCommentCopy.Columns.Add(dtColumnProductIdFordtCommentCopy);

                    DataColumn dtColumnTextFordtCommentCopy = new DataColumn();
                    dtColumnTextFordtCommentCopy.DataType = typeof(string);
                    dtColumnTextFordtCommentCopy.ColumnName = "Text";
                    dtCommentCopy.Columns.Add(dtColumnTextFordtCommentCopy);

                    
                #endregion

                dtComment = new CommentModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtComment.Rows)
                {
                    dtCommentCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtCommentCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Commenting/Comment/Comment_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsComment = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtCommentCopy to avoid issue related to DateTime conversion
                    DataTable dtCommentCopy = new DataTable();
                    dtCommentCopy.TableName = "Comment";

                    #region Define columns for dtCommentCopy
                    DataColumn dtColumnCommentIdFordtCommentCopy = new DataColumn();
                    dtColumnCommentIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnCommentIdFordtCommentCopy.ColumnName = "CommentId";
                    dtCommentCopy.Columns.Add(dtColumnCommentIdFordtCommentCopy);

                    DataColumn dtColumnActiveFordtCommentCopy = new DataColumn();
                    dtColumnActiveFordtCommentCopy.DataType = typeof(string);
                    dtColumnActiveFordtCommentCopy.ColumnName = "Active";
                    dtCommentCopy.Columns.Add(dtColumnActiveFordtCommentCopy);

                    DataColumn dtColumnDateTimeCreationFordtCommentCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtCommentCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtCommentCopy.ColumnName = "DateTimeCreation";
                    dtCommentCopy.Columns.Add(dtColumnDateTimeCreationFordtCommentCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtCommentCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtCommentCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtCommentCopy.ColumnName = "DateTimeLastModification";
                    dtCommentCopy.Columns.Add(dtColumnDateTimeLastModificationFordtCommentCopy);

                    DataColumn dtColumnUserCreationIdFordtCommentCopy = new DataColumn();
                    dtColumnUserCreationIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtCommentCopy.ColumnName = "UserCreationId";
                    dtCommentCopy.Columns.Add(dtColumnUserCreationIdFordtCommentCopy);

                    DataColumn dtColumnUserLastModificationIdFordtCommentCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtCommentCopy.ColumnName = "UserLastModificationId";
                    dtCommentCopy.Columns.Add(dtColumnUserLastModificationIdFordtCommentCopy);

                    DataColumn dtColumnProductIdFordtCommentCopy = new DataColumn();
                    dtColumnProductIdFordtCommentCopy.DataType = typeof(string);
                    dtColumnProductIdFordtCommentCopy.ColumnName = "ProductId";
                    dtCommentCopy.Columns.Add(dtColumnProductIdFordtCommentCopy);

                    DataColumn dtColumnTextFordtCommentCopy = new DataColumn();
                    dtColumnTextFordtCommentCopy.DataType = typeof(string);
                    dtColumnTextFordtCommentCopy.ColumnName = "Text";
                    dtCommentCopy.Columns.Add(dtColumnTextFordtCommentCopy);

                    
                    #endregion

                    dsComment.Tables.Add(dtCommentCopy);

                    for (int i = 0; i < dsComment.Tables.Count; i++)
                    {
                        dtCommentCopy = new CommentModel().Select1ByCommentIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtCommentCopy.Rows)
                        {
                            dsComment.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsComment.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsComment.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Commenting/Comment/Comment_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<CommentModel> lstCommentModel = new List<CommentModel> { };

            if (ExportationType == "All")
            {
                lstCommentModel = new CommentModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    CommentModel CommentModel = new CommentModel().Select1ByCommentIdToModel(Convert.ToInt32(RowChecked));
                    lstCommentModel.Add(CommentModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Commenting/Comment/Comment_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstCommentModel);
            }

            return Now;
        }
        #endregion
    }
}