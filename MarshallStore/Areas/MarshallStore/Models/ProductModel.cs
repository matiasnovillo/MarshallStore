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
    /// Fields:            14 <br/> 
    /// Sub-models:      4 models <br/>
    /// Last modification: 31/07/2023 14:32:16
    /// </summary>
    [Serializable]
    public partial class ProductModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("ProductId")]
        public int ProductId { get; set; }

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

        [Library.ModelAttributeValidator.Key("ProductCategoryId")]
        public int ProductCategoryId { get; set; }

        [Library.ModelAttributeValidator.String("Producer", false, 1, 300, "")]
        public string Producer { get; set; }

        [Library.ModelAttributeValidator.String("Model", false, 1, 300, "")]
        public string Model { get; set; }

        [Library.ModelAttributeValidator.Decimal("Price", false, 0D, 999999000.000000D)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Library.ModelAttributeValidator.String("Image1", false, 1, 8000, "")]
        public string Image1 { get; set; }

        [Library.ModelAttributeValidator.String("Image2", false, 1, 8000, "")]
        public string Image2 { get; set; }

        [Library.ModelAttributeValidator.String("Image3", false, 1, 8000, "")]
        public string Image3 { get; set; }
        #endregion

        #region Sub-lists
        public virtual List<PurchaseProductModel> lstPurchaseProductModel { get; set; } //Foreign Key name: ProductId 
		public virtual List<ShoppingCartModel> lstShoppingCartModel { get; set; } //Foreign Key name: ProductId 
		public virtual List<RateModel> lstRateModel { get; set; } //Foreign Key name: ProductId 
		public virtual List<CommentModel> lstCommentModel { get; set; } //Foreign Key name: ProductId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field ProductId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       14 <br/> 
        /// Dependencies: 4 models depend on this model <br/>
        /// </summary>
        public ProductModel()
        {
            try 
            {
                ProductId = 0;

                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                lstShoppingCartModel = new List<ShoppingCartModel>();
                lstRateModel = new List<RateModel>();
                lstCommentModel = new List<CommentModel>();
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using ProductId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       14 <br/> 
        /// Dependencies: 4 models depend on this model <br/>
        /// </summary>
        public ProductModel(int ProductId)
        {
            try
            {
                List<ProductModel> lstProductModel = new List<ProductModel>();

                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                lstShoppingCartModel = new List<ShoppingCartModel>();
                lstRateModel = new List<RateModel>();
                lstCommentModel = new List<CommentModel>();
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<ProductModel>
                    lstProductModel = (List<ProductModel>)sqlConnection.Query<ProductModel>("[dbo].[MarshallStore.Product.Select1ByProductId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstProductModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[MarshallStore.Product.Select1ByProductId] returned more than one register/row");
                }
        
                foreach (ProductModel product in lstProductModel)
                {
                    this.ProductId = product.ProductId;
					this.Active = product.Active;
					this.DateTimeCreation = product.DateTimeCreation;
					this.DateTimeLastModification = product.DateTimeLastModification;
					this.UserCreationId = product.UserCreationId;
					this.UserLastModificationId = product.UserLastModificationId;
					this.ProductCategoryId = product.ProductCategoryId;
					this.Producer = product.Producer;
					this.Model = product.Model;
					this.Price = product.Price;
					this.Description = product.Description;
					this.Image1 = product.Image1;
					this.Image2 = product.Image2;
					this.Image3 = product.Image3;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       14 <br/> 
        /// Dependencies: 4 models depend on this model <br/>
        /// </summary>
        public ProductModel(int ProductId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductCategoryId, string Producer, string Model, decimal Price, string Description, string Image1, string Image2, string Image3)
        {
            try
            {
                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                lstShoppingCartModel = new List<ShoppingCartModel>();
                lstRateModel = new List<RateModel>();
                lstCommentModel = new List<CommentModel>();
                

                this.ProductId = ProductId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.ProductCategoryId = ProductCategoryId;
				this.Producer = Producer;
				this.Model = Model;
				this.Price = Price;
				this.Description = Description;
				this.Image1 = Image1;
				this.Image2 = Image2;
				this.Image3 = Image3;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), product, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       14 <br/> 
        /// Dependencies: 4 models depend on this model <br/>
        /// </summary>
        public ProductModel(ProductModel product)
        {
            try
            {
                //Initialize sub-lists
                lstPurchaseProductModel = new List<PurchaseProductModel>();
                lstShoppingCartModel = new List<ShoppingCartModel>();
                lstRateModel = new List<RateModel>();
                lstCommentModel = new List<CommentModel>();
                

                ProductId = product.ProductId;
				Active = product.Active;
				DateTimeCreation = product.DateTimeCreation;
				DateTimeLastModification = product.DateTimeLastModification;
				UserCreationId = product.UserCreationId;
				UserLastModificationId = product.UserLastModificationId;
				ProductCategoryId = product.ProductCategoryId;
				Producer = product.Producer;
				Model = product.Model;
				Price = product.Price;
				Description = product.Description;
				Image1 = product.Image1;
				Image2 = product.Image2;
				Image3 = product.Image3;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Product</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByProductIdToDataTable(int ProductId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.Select1ByProductId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.Product.Select1ByProductId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public ProductModel Select1ByProductIdToModel(int ProductId)
        {
            try
            {
                ProductModel ProductModel = new ProductModel();
                List<ProductModel> lstProductModel = new List<ProductModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstProductModel = (List<ProductModel>)sqlConnection.Query<ProductModel>("[dbo].[MarshallStore.Product.Select1ByProductId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstProductModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[MarshallStore.Product.Select1ByProductId] returned more than one register/row"); }

                foreach (ProductModel product in lstProductModel)
                {
                    ProductModel.ProductId = product.ProductId;
					ProductModel.Active = product.Active;
					ProductModel.DateTimeCreation = product.DateTimeCreation;
					ProductModel.DateTimeLastModification = product.DateTimeLastModification;
					ProductModel.UserCreationId = product.UserCreationId;
					ProductModel.UserLastModificationId = product.UserLastModificationId;
					ProductModel.ProductCategoryId = product.ProductCategoryId;
					ProductModel.Producer = product.Producer;
					ProductModel.Model = product.Model;
					ProductModel.Price = product.Price;
					ProductModel.Description = product.Description;
					ProductModel.Image1 = product.Image1;
					ProductModel.Image2 = product.Image2;
					ProductModel.Image3 = product.Image3;
                }

                return ProductModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<ProductModel> SelectAllToList()
        {
            try
            {
                List<ProductModel> lstProductModel = new List<ProductModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstProductModel = (List<ProductModel>)sqlConnection.Query<ProductModel>("[dbo].[MarshallStore.Product.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstProductModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public productSelectAllPaged SelectAllPagedToModel(productSelectAllPaged productSelectAllPaged)
        {
            try
            {
                productSelectAllPaged.lstProductModel = new List<ProductModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", productSelectAllPaged.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", productSelectAllPaged.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", productSelectAllPaged.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", productSelectAllPaged.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", productSelectAllPaged.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", productSelectAllPaged.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    productSelectAllPaged.lstProductModel = (List<ProductModel>)sqlConnection.Query<ProductModel>("[dbo].[MarshallStore.Product.SelectAllPaged]", dp, commandType: CommandType.StoredProcedure);
                    productSelectAllPaged.TotalRows = dp.Get<int>("TotalRows");
                }

                productSelectAllPaged.TotalPages = Library.Math.Divide(productSelectAllPaged.TotalRows, productSelectAllPaged.RowsPerPage, Library.Math.RoundType.RoundUp);

                //Loop through lists and sublists
                for (int i = 0; i < productSelectAllPaged.lstProductModel.Count; i++)
                {
                    DynamicParameters dpForPurchaseProductModel = new DynamicParameters();
                    dpForPurchaseProductModel.Add("ProductId", productSelectAllPaged.lstProductModel[i].ProductId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<PurchaseProductModel> lstPurchaseProductModel = new List<PurchaseProductModel>();
                        lstPurchaseProductModel = (List<PurchaseProductModel>)sqlConnection.Query<PurchaseProductModel>("[dbo].[MarshallStore.PurchaseProduct.SelectAllByProductIdCustom]", dpForPurchaseProductModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var PurchaseProductModel in lstPurchaseProductModel)
                        {
                            productSelectAllPaged.lstProductModel[i].lstPurchaseProductModel.Add(PurchaseProductModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < productSelectAllPaged.lstProductModel.Count; i++)
                {
                    DynamicParameters dpForShoppingCartModel = new DynamicParameters();
                    dpForShoppingCartModel.Add("ProductId", productSelectAllPaged.lstProductModel[i].ProductId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<ShoppingCartModel> lstShoppingCartModel = new List<ShoppingCartModel>();
                        lstShoppingCartModel = (List<ShoppingCartModel>)sqlConnection.Query<ShoppingCartModel>("[dbo].[MarshallStore.ShoppingCart.SelectAllByProductIdCustom]", dpForShoppingCartModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var ShoppingCartModel in lstShoppingCartModel)
                        {
                            productSelectAllPaged.lstProductModel[i].lstShoppingCartModel.Add(ShoppingCartModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < productSelectAllPaged.lstProductModel.Count; i++)
                {
                    DynamicParameters dpForRateModel = new DynamicParameters();
                    dpForRateModel.Add("ProductId", productSelectAllPaged.lstProductModel[i].ProductId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RateModel> lstRateModel = new List<RateModel>();
                        lstRateModel = (List<RateModel>)sqlConnection.Query<RateModel>("[dbo].[MarshallStore.Rate.SelectAllByProductIdCustom]", dpForRateModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RateModel in lstRateModel)
                        {
                            productSelectAllPaged.lstProductModel[i].lstRateModel.Add(RateModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < productSelectAllPaged.lstProductModel.Count; i++)
                {
                    DynamicParameters dpForCommentModel = new DynamicParameters();
                    dpForCommentModel.Add("ProductId", productSelectAllPaged.lstProductModel[i].ProductId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<CommentModel> lstCommentModel = new List<CommentModel>();
                        lstCommentModel = (List<CommentModel>)sqlConnection.Query<CommentModel>("[dbo].[MarshallStore.Comment.SelectAllByProductIdCustom]", dpForCommentModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var CommentModel in lstCommentModel)
                        {
                            productSelectAllPaged.lstProductModel[i].lstCommentModel.Add(CommentModel);
                        }
                    }
                }
                
                

                return productSelectAllPaged;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Product table</returns>
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
				dp.Add("ProductCategoryId", ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", Image3, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Product table</returns>
        public int Insert(ProductModel product)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", product.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", product.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", product.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", product.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", product.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductCategoryId", product.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", product.Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", product.Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", product.Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", product.Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", product.Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", product.Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", product.Image3, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Product table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductCategoryId, string Producer, string Model, decimal Price, string Description, string Image1, string Image2, string Image3)
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
				dp.Add("ProductCategoryId", ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", Image3, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Product table</returns>
        public int UpdateByProductId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductCategoryId", ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", Image3, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.UpdateByProductId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Product table</returns>
        public int UpdateByProductId(ProductModel product)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProductId", product.ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", product.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", product.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", product.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", product.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", product.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductCategoryId", product.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", product.Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", product.Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", product.Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", product.Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", product.Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", product.Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", product.Image3, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.UpdateByProductId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Product table</returns>
        public int UpdateByProductId(int ProductId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ProductCategoryId, string Producer, string Model, decimal Price, string Description, string Image1, string Image2, string Image3)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("ProductCategoryId", ProductCategoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Producer", Producer, DbType.String, ParameterDirection.Input);
				dp.Add("Model", Model, DbType.String, ParameterDirection.Input);
				dp.Add("Price", Price, DbType.Decimal, ParameterDirection.Input, precision: 24, scale: 6);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
				dp.Add("Image1", Image1, DbType.String, ParameterDirection.Input);
				dp.Add("Image2", Image2, DbType.String, ParameterDirection.Input);
				dp.Add("Image3", Image3, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.UpdateByProductId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Product table</returns>
        public int DeleteByProductId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.DeleteByProductId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in Product table</returns>
        public int DeleteByProductId(int ProductId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ProductId", ProductId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[MarshallStore.Product.DeleteByProductId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public ProductModel ByteArrayToProductModel<T>(byte[] arrProductModel)
        {
            try
            {
                if (arrProductModel == null)
                { return new ProductModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrProductModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (ProductModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"ProductId: {ProductId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"ProductCategoryId: {ProductCategoryId}, " +
				$"Producer: {Producer}, " +
				$"Model: {Model}, " +
				$"Price: {Price}, " +
				$"Description: {Description}, " +
				$"Image1: {Image1}, " +
				$"Image2: {Image2}, " +
				$"Image3: {Image3}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ProductId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ProductCategoryId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Producer}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Model}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Price}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Description}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Image1}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Image2}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Image3}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }
}