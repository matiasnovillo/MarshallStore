using MarshallStore.Areas.Examples.DTOs;
using MarshallStore.Areas.Examples.Models;
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

//Last modification on: 21/02/2023 18:16:41

namespace MarshallStore.Areas.Examples.Interfaces
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Interface. <br/>
    /// Function:          This interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 21/02/2023 18:16:41
    /// </summary>
    public partial interface IExample
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="ExampleId"></param>
        /// <returns></returns>
        ExampleModel Select1ByExampleIdToModel(int ExampleId);

        List<ExampleModel> SelectAllToList();

        exampleSelectAllPaged SelectAllPagedToModel(exampleSelectAllPaged exampleSelectAllPaged);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="Example"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Example table</returns>
        int Insert(ExampleModel Example);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="Example"></param>
        /// <returns>The number of rows updated in Example table</returns>
        int UpdateByExampleId(ExampleModel Example);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="ExampleId"></param>
        /// <returns>The number of rows deleted in Example table</returns>
        int DeleteByExampleId(int ExampleId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByExampleId(int ExampleId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}