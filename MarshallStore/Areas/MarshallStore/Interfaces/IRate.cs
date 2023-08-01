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

//Last modification on: 31/07/2023 14:25:35

namespace MarshallStore.Areas.MarshallStore.Interfaces
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Interface. <br/>
    /// Function:          This interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 31/07/2023 14:25:35
    /// </summary>
    public partial interface IRate
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="RateId"></param>
        /// <returns></returns>
        RateModel Select1ByRateIdToModel(int RateId);

        List<RateModel> SelectAllToList();

        rateSelectAllPaged SelectAllPagedToModel(rateSelectAllPaged rateSelectAllPaged);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="Rate"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Rate table</returns>
        int Insert(RateModel Rate);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="Rate"></param>
        /// <returns>The number of rows updated in Rate table</returns>
        int UpdateByRateId(RateModel Rate);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="RateId"></param>
        /// <returns>The number of rows deleted in Rate table</returns>
        int DeleteByRateId(int RateId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByRateId(int RateId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}