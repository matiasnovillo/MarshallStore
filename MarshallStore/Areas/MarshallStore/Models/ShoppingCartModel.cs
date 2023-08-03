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
    /// Fields:            8 <br/> 
    /// Sub-models:      0 models <br/>
    /// Last modification: 31/07/2023 14:45:20
    /// </summary>
    [Serializable]
    public partial class ShoppingCartModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("ShoppingCartId")]
        public int ShoppingCartId { get; set; }

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

        [Library.ModelAttributeValidator.Key("ProductId")]
        public int ProductId { get; set; }

        [Library.ModelAttributeValidator.Int("Counter", false, 0, 9999)]
        public int Counter { get; set; }

        public string Producer { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Image1 { get; set; }
        #endregion

        #region Sub-lists
        
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field ShoppingCartId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ShoppingCartModel()
        {
            try 
            {
                ShoppingCartId = 0;

                //Initialize sub-lists
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using ShoppingCartId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ShoppingCartModel(int ShoppingCartId)
        {
            try
            {
                List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel>();

                //Initialize sub-lists
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<ShoppingCartModel>
                    lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstShoppingCartModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId] returned more than one register/row");
                }
        
                foreach (ShoppingCartModel shoppingcart in lstShoppingCartModel)
                {
                    this.ShoppingCartId = shoppingcart.ShoppingCartId;
					this.Active = shoppingcart.Active;
					this.DateTimeCreation = shoppingcart.DateTimeCreation;
					this.DateTimeLastModification = shoppingcart.DateTimeLastModification;
					this.UserCreationId = shoppingcart.UserCreationId;
					this.UserLastModificationId = shoppingcart.UserLastModificationId;
					this.ProductId = shoppingcart.ProductId;
					this.Counter = shoppingcart.Counter;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ShoppingCartModel(int ShoppingCartId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductId, int Counter)
        {
            try
            {
                //Initialize sub-lists
                

                this.ShoppingCartId = ShoppingCartId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.ProductId = ProductId;
				this.Counter = Counter;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), shoppingcart, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ShoppingCartModel(ShoppingCartModel shoppingcart)
        {
            try
            {
                //Initialize sub-lists
                

                ShoppingCartId = shoppingcart.ShoppingCartId;
				Active = shoppingcart.Active;
				DateTimeCreation = shoppingcart.DateTimeCreation;
				DateTimeLastModification = shoppingcart.DateTimeLastModification;
				UserCreationId = shoppingcart.UserCreationId;
				UserLastModificationId = shoppingcart.UserLastModificationId;
				ProductId = shoppingcart.ProductId;
				Counter = shoppingcart.Counter;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside ShoppingCart</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.Count]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }

                int RowsCounter = Convert.ToInt32(DataTable.Rows[0][0].ToString());

                return RowsCounter;
            }
            catch (Exception ex) { throw ex; }
        }

        public int CountByUserCreationId(int UserCreationId)
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.CountByUserCreationId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByShoppingCartIdToDataTable(int ShoppingCartId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public ShoppingCartModel Select1ByShoppingCartIdToModel(int ShoppingCartId)
        {
            try
            {
                ShoppingCartModel ShoppingCartModel = new ShoppingCartModel();
                List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstShoppingCartModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.ShoppingCart.Select1ByShoppingCartId] returned more than one register/row"); }

                foreach (ShoppingCartModel shoppingcart in lstShoppingCartModel)
                {
                    ShoppingCartModel.ShoppingCartId = shoppingcart.ShoppingCartId;
					ShoppingCartModel.Active = shoppingcart.Active;
					ShoppingCartModel.DateTimeCreation = shoppingcart.DateTimeCreation;
					ShoppingCartModel.DateTimeLastModification = shoppingcart.DateTimeLastModification;
					ShoppingCartModel.UserCreationId = shoppingcart.UserCreationId;
					ShoppingCartModel.UserLastModificationId = shoppingcart.UserLastModificationId;
					ShoppingCartModel.ProductId = shoppingcart.ProductId;
					ShoppingCartModel.Counter = shoppingcart.Counter;
                }

                return ShoppingCartModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<ShoppingCartModel> SelectAllToList()
        {
            try
            {
                List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstShoppingCartModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<ShoppingCartModel> SelectAllByUserCreationIdToList(int UserCreationId)
        {
            try
            {
                List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.SelectAllByUserCreationId]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstShoppingCartModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public shoppingcartSelectAllPaged SelectAllPagedToModel(shoppingcartSelectAllPaged shoppingcartSelectAllPaged)
        {
            try
            {
                shoppingcartSelectAllPaged.lstShoppingCartModel = new List<ShoppingCartModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", shoppingcartSelectAllPaged.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", shoppingcartSelectAllPaged.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", shoppingcartSelectAllPaged.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", shoppingcartSelectAllPaged.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", shoppingcartSelectAllPaged.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", shoppingcartSelectAllPaged.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    shoppingcartSelectAllPaged.lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.SelectAllPaged]", dp, commandType: CommandType.StoredProcedure);
                    shoppingcartSelectAllPaged.TotalRows = dp.Get<int>("TotalRows");
                }

                shoppingcartSelectAllPaged.TotalPages = Library.Math.Divide(shoppingcartSelectAllPaged.TotalRows, shoppingcartSelectAllPaged.RowsPerPage, Library.Math.RoundType.RoundUp);

                

                return shoppingcartSelectAllPaged;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in ShoppingCart table</returns>
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
				dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in ShoppingCart table</returns>
        public int Insert(ShoppingCartModel shoppingcart)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", shoppingcart.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", shoppingcart.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", shoppingcart.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", shoppingcart.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", shoppingcart.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductId", shoppingcart.ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", shoppingcart.Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in ShoppingCart table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductId, int Counter)
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
				dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in ShoppingCart table</returns>
        public int UpdateByShoppingCartId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.UpdateByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in ShoppingCart table</returns>
        public int UpdateByShoppingCartId(ShoppingCartModel shoppingcart)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ShoppingCartId", shoppingcart.ShoppingCartId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", shoppingcart.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", shoppingcart.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", shoppingcart.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", shoppingcart.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", shoppingcart.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductId", shoppingcart.ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", shoppingcart.Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.UpdateByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in ShoppingCart table</returns>
        public int UpdateByShoppingCartId(int ShoppingCartId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductId, int Counter)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Counter", Counter, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.UpdateByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in ShoppingCart table</returns>
        public int DeleteByShoppingCartId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.DeleteByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in ShoppingCart table</returns>
        public int DeleteByShoppingCartId(int ShoppingCartId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ShoppingCartId", ShoppingCartId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.ShoppingCart.DeleteByShoppingCartId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public ShoppingCartModel ByteArrayToShoppingCartModel<T>(byte[] arrShoppingCartModel)
        {
            try
            {
                if (arrShoppingCartModel == null)
                { return new ShoppingCartModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrShoppingCartModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (ShoppingCartModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"ShoppingCartId: {ShoppingCartId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"ProductId: {ProductId}, " +
				$"Counter: {Counter}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ShoppingCartId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ProductId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Counter}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }
}