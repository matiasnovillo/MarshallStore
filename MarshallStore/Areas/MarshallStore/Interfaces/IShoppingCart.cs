using MarshallStore.Areas.MarshallStore.DTOs;
using MarshallStore.Areas.MarshallStore.Models;
using MarshallStore.Library;
using System;
using System.Collections.Generic;

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

namespace MarshallStore.Areas.MarshallStore.Interfaces
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Interface. <br/>
    /// Function:          This interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 31/07/2023 14:45:20
    /// </summary>
    public partial interface IShoppingCart
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="ShoppingCartId"></param>
        /// <returns></returns>
        ShoppingCartModel Select1ByShoppingCartIdToModel(int ShoppingCartId);

        List<ShoppingCartModel> SelectAllToList();

        shoppingcartSelectAllPaged SelectAllPagedToModel(shoppingcartSelectAllPaged shoppingcartSelectAllPaged);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="ShoppingCart"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in ShoppingCart table</returns>
        int Insert(ShoppingCartModel ShoppingCart);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="ShoppingCart"></param>
        /// <returns>The number of rows updated in ShoppingCart table</returns>
        int UpdateByShoppingCartId(ShoppingCartModel ShoppingCart);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="ShoppingCartId"></param>
        /// <returns>The number of rows deleted in ShoppingCart table</returns>
        int DeleteByShoppingCartId(int ShoppingCartId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByShoppingCartId(int ShoppingCartId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}