using Dapper;
using MarshallStore.Areas.MarshallStore.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

namespace MarshallStore.Areas.MarshallStore.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            19 <br/> 
    /// Sub-models:      1 models <br/>
    /// Last modification: 03/08/2023 18:27:16
    /// </summary>
    [Serializable]
    public partial class PurchaseModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("PurchaseId")]
        public int PurchaseId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        public bool Active { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeCreation", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeCreation { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeLastModification", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeLastModification { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserCreationId")]
        public int UserCreationId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserLastModificationId")]
        public int UserLastModificationId { get; set; }

        [Library.ModelAttributeValidator.Decimal("FullPrice", false, 0D, 999999000.000000D)]
        public decimal FullPrice { get; set; }

        [Library.ModelAttributeValidator.String("FirstName", false, 1, 300, "")]
        public string FirstName { get; set; }

        [Library.ModelAttributeValidator.String("LastName", false, 1, 300, "")]
        public string LastName { get; set; }

        [Library.ModelAttributeValidator.String("Email", false, 1, 370, "")]
        public string Email { get; set; }

        [Library.ModelAttributeValidator.String("Phone", false, 1, 500, "")]
        public string Phone { get; set; }

        public string StreetAddress { get; set; }

        [Library.ModelAttributeValidator.String("PostCodeOrZip", false, 1, 100, "")]
        public string PostCodeOrZip { get; set; }

        [Library.ModelAttributeValidator.String("City", false, 1, 300, "")]
        public string City { get; set; }

        [Library.ModelAttributeValidator.String("Country", false, 1, 300, "")]
        public string Country { get; set; }

        [Library.ModelAttributeValidator.String("CardNumber", false, 1, 50, "")]
        public string CardNumber { get; set; }

        [Library.ModelAttributeValidator.String("CardHolder", true, 1, 400, "")]
        public string CardHolder { get; set; }

        [Library.ModelAttributeValidator.String("Expiration", false, 1, 10, "")]
        public string Expiration { get; set; }

        [Library.ModelAttributeValidator.String("CVC", false, 1, 10, "")]
        public string CVC { get; set; }
        #endregion

        #region Sub-lists
        public virtual List<PurchaseProductModel> lstPurchaseProductModel { get; set; } //Foreign Key name: PurchaseId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field PurchaseId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       19 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public PurchaseModel()
        {
            try 
            {
                PurchaseId = 0;

                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using PurchaseId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       19 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public PurchaseModel(int PurchaseId)
        {
            try
            {
                List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel>();

                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<PurchaseModel>
                    lstPurchaseModel = (List<PurchaseModel>)sqlConnection.Query<PurchaseModel>("[dbo].[MarshallStore.Purchase.Select1ByPurchaseId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstPurchaseModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[MarshallStore.Purchase.Select1ByPurchaseId] returned more than one register/row");
                }
        
                foreach (PurchaseModel purchase in lstPurchaseModel)
                {
                    this.PurchaseId = purchase.PurchaseId;
					this.Active = purchase.Active;
					this.DateTimeCreation = purchase.DateTimeCreation;
					this.DateTimeLastModification = purchase.DateTimeLastModification;
					this.UserCreationId = purchase.UserCreationId;
					this.UserLastModificationId = purchase.UserLastModificationId;
					this.FullPrice = purchase.FullPrice;
					this.FirstName = purchase.FirstName;
					this.LastName = purchase.LastName;
					this.Email = purchase.Email;
					this.Phone = purchase.Phone;
					this.StreetAddress = purchase.StreetAddress;
					this.PostCodeOrZip = purchase.PostCodeOrZip;
					this.City = purchase.City;
					this.Country = purchase.Country;
					this.CardNumber = purchase.CardNumber;
					this.CardHolder = purchase.CardHolder;
					this.Expiration = purchase.Expiration;
					this.CVC = purchase.CVC;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       19 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public PurchaseModel(int PurchaseId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, decimal FullPrice, string FirstName, string LastName, string Email, string Phone, string StreetAddress, string PostCodeOrZip, string City, string Country, string CardNumber, string CardHolder, string Expiration, string CVC)
        {
            try
            {
                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                

                this.PurchaseId = PurchaseId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.FullPrice = FullPrice;
				this.FirstName = FirstName;
				this.LastName = LastName;
				this.Email = Email;
				this.Phone = Phone;
				this.StreetAddress = StreetAddress;
				this.PostCodeOrZip = PostCodeOrZip;
				this.City = City;
				this.Country = Country;
				this.CardNumber = CardNumber;
				this.CardHolder = CardHolder;
				this.Expiration = Expiration;
				this.CVC = CVC;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), purchase, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       19 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public PurchaseModel(PurchaseModel purchase)
        {
            try
            {
                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                

                PurchaseId = purchase.PurchaseId;
				Active = purchase.Active;
				DateTimeCreation = purchase.DateTimeCreation;
				DateTimeLastModification = purchase.DateTimeLastModification;
				UserCreationId = purchase.UserCreationId;
				UserLastModificationId = purchase.UserLastModificationId;
				FullPrice = purchase.FullPrice;
				FirstName = purchase.FirstName;
				LastName = purchase.LastName;
				Email = purchase.Email;
				Phone = purchase.Phone;
				StreetAddress = purchase.StreetAddress;
				PostCodeOrZip = purchase.PostCodeOrZip;
				City = purchase.City;
				Country = purchase.Country;
				CardNumber = purchase.CardNumber;
				CardHolder = purchase.CardHolder;
				Expiration = purchase.Expiration;
				CVC = purchase.CVC;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Purchase</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.Count]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }

                int RowsCounter = Convert.ToInt32(DataTable.Rows[0][0].ToString());

                return RowsCounter;
            }
            catch (Exception ex) { throw ex; }
        }

        #region Queries to DataTable
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public DataTable Select1ByPurchaseIdToDataTable(int PurchaseId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.Select1ByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.Purchase.Select1ByPurchaseId] returned more than one register/row"); }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectAllToDataTable()
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Queries to Models
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public PurchaseModel Select1ByPurchaseIdToModel(int PurchaseId)
        {
            try
            {
                PurchaseModel PurchaseModel = new PurchaseModel();
                List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstPurchaseModel = (List<PurchaseModel>)sqlConnection.Query<PurchaseModel>("[dbo].[MarshallStore.Purchase.Select1ByPurchaseId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstPurchaseModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.Purchase.Select1ByPurchaseId] returned more than one register/row"); }

                foreach (PurchaseModel purchase in lstPurchaseModel)
                {
                    PurchaseModel.PurchaseId = purchase.PurchaseId;
					PurchaseModel.Active = purchase.Active;
					PurchaseModel.DateTimeCreation = purchase.DateTimeCreation;
					PurchaseModel.DateTimeLastModification = purchase.DateTimeLastModification;
					PurchaseModel.UserCreationId = purchase.UserCreationId;
					PurchaseModel.UserLastModificationId = purchase.UserLastModificationId;
					PurchaseModel.FullPrice = purchase.FullPrice;
					PurchaseModel.FirstName = purchase.FirstName;
					PurchaseModel.LastName = purchase.LastName;
					PurchaseModel.Email = purchase.Email;
					PurchaseModel.Phone = purchase.Phone;
					PurchaseModel.StreetAddress = purchase.StreetAddress;
					PurchaseModel.PostCodeOrZip = purchase.PostCodeOrZip;
					PurchaseModel.City = purchase.City;
					PurchaseModel.Country = purchase.Country;
					PurchaseModel.CardNumber = purchase.CardNumber;
					PurchaseModel.CardHolder = purchase.CardHolder;
					PurchaseModel.Expiration = purchase.Expiration;
					PurchaseModel.CVC = purchase.CVC;
                }

                return PurchaseModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<PurchaseModel> SelectAllToList()
        {
            try
            {
                List<PurchaseModel> lstPurchaseModel = new List<PurchaseModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstPurchaseModel = (List<PurchaseModel>)sqlConnection.Query<PurchaseModel>("[dbo].[MarshallStore.Purchase.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstPurchaseModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public purchaseSelectAllPaged SelectAllPagedToModel(purchaseSelectAllPaged purchaseSelectAllPaged)
        {
            try
            {
                purchaseSelectAllPaged.lstPurchaseModel = new List<PurchaseModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", purchaseSelectAllPaged.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", purchaseSelectAllPaged.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", purchaseSelectAllPaged.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", purchaseSelectAllPaged.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", purchaseSelectAllPaged.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", purchaseSelectAllPaged.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    purchaseSelectAllPaged.lstPurchaseModel = (List<PurchaseModel>)sqlConnection.Query<PurchaseModel>("[dbo].[MarshallStore.Purchase.SelectAllPaged]", dp, commandType: CommandType.StoredProcedure);
                    purchaseSelectAllPaged.TotalRows = dp.Get<int>("TotalRows");
                }

                purchaseSelectAllPaged.TotalPages = Library.Math.Divide(purchaseSelectAllPaged.TotalRows, purchaseSelectAllPaged.RowsPerPage, Library.Math.RoundType.RoundUp);

                //Loop through lists and sublists
                for (int i = 0; i < purchaseSelectAllPaged.lstPurchaseModel.Count; i++)
                {
                    DynamicParameters dpForPurchaseProductModel = new DynamicParameters();
                    dpForPurchaseProductModel.Add("PurchaseId", purchaseSelectAllPaged.lstPurchaseModel[i].PurchaseId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<PurchaseProductModel> lstPurchaseProductModel = new List<PurchaseProductModel>();
                        lstPurchaseProductModel = (List<PurchaseProductModel>)sqlConnection.Query<PurchaseProductModel>("[dbo].[MarshallStore.PurchaseProduct.SelectAllByPurchaseIdCustom]", dpForPurchaseProductModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var PurchaseProductModel in lstPurchaseProductModel)
                        {
                            purchaseSelectAllPaged.lstPurchaseModel[i].lstPurchaseProductModel.Add(PurchaseProductModel);
                        }
                    }
                }
                
                

                return purchaseSelectAllPaged;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Purchase table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", CVC, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Purchase table</returns>
        public int Insert(PurchaseModel purchase)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", purchase.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", purchase.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", purchase.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", purchase.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", purchase.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", purchase.FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", purchase.FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", purchase.LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", purchase.Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", purchase.Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", purchase.StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", purchase.PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", purchase.City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", purchase.Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", purchase.CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", purchase.CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", purchase.Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", purchase.CVC, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Purchase table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, decimal FullPrice, string FirstName, string LastName, string Email, string Phone, string StreetAddress, string PostCodeOrZip, string City, string Country, string CardNumber, string CardHolder, string Expiration, string CVC)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", CVC, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Purchase table</returns>
        public int UpdateByPurchaseId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", CVC, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.UpdateByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Purchase table</returns>
        public int UpdateByPurchaseId(PurchaseModel purchase)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("PurchaseId", purchase.PurchaseId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", purchase.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", purchase.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", purchase.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", purchase.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", purchase.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", purchase.FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", purchase.FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", purchase.LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", purchase.Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", purchase.Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", purchase.StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", purchase.PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", purchase.City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", purchase.Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", purchase.CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", purchase.CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", purchase.Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", purchase.CVC, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.UpdateByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Purchase table</returns>
        public int UpdateByPurchaseId(int PurchaseId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, decimal FullPrice, string FirstName, string LastName, string Email, string Phone, string StreetAddress, string PostCodeOrZip, string City, string Country, string CardNumber, string CardHolder, string Expiration, string CVC)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("FullPrice", FullPrice, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("FirstName", FirstName, DbType.String, ParameterDirection.Input);
				dp.Add("LastName", LastName, DbType.String, ParameterDirection.Input);
				dp.Add("Email", Email, DbType.String, ParameterDirection.Input);
				dp.Add("Phone", Phone, DbType.String, ParameterDirection.Input);
				dp.Add("StreetAddress", StreetAddress, DbType.String, ParameterDirection.Input);
				dp.Add("PostCodeOrZip", PostCodeOrZip, DbType.String, ParameterDirection.Input);
				dp.Add("City", City, DbType.String, ParameterDirection.Input);
				dp.Add("Country", Country, DbType.String, ParameterDirection.Input);
				dp.Add("CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
				dp.Add("CardHolder", CardHolder, DbType.String, ParameterDirection.Input);
				dp.Add("Expiration", Expiration, DbType.String, ParameterDirection.Input);
				dp.Add("CVC", CVC, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.UpdateByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        ///
        public void DeleteAll()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Purchase table</returns>
        public int DeleteByPurchaseId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.DeleteByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows affected in Purchase table</returns>
        public int DeleteByPurchaseId(int PurchaseId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("PurchaseId", PurchaseId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Purchase.DeleteByPurchaseId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// Function: Take the model stored in the given byte array to return the model. <br/>
        /// Note 1:   Similar to a decryptor function. <br/>
        /// Note 2:   The model need the [Serializable] decorator in model definition. <br/>
        /// </summary>
        public PurchaseModel ByteArrayToPurchaseModel<T>(byte[] arrPurchaseModel)
        {
            try
            {
                if (arrPurchaseModel == null)
                { return new PurchaseModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrPurchaseModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (PurchaseModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"PurchaseId: {PurchaseId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"FullPrice: {FullPrice}, " +
				$"FirstName: {FirstName}, " +
				$"LastName: {LastName}, " +
				$"Email: {Email}, " +
				$"Phone: {Phone}, " +
				$"StreetAddress: {StreetAddress}, " +
				$"PostCodeOrZip: {PostCodeOrZip}, " +
				$"City: {City}, " +
				$"Country: {Country}, " +
				$"CardNumber: {CardNumber}, " +
				$"CardHolder: {CardHolder}, " +
				$"Expiration: {Expiration}, " +
				$"CVC: {CVC}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{PurchaseId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{FullPrice}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{FirstName}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{LastName}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Email}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Phone}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{StreetAddress}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{PostCodeOrZip}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{City}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Country}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{CardNumber}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{CardHolder}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Expiration}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{CVC}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }
}