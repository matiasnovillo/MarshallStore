import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { purchaseproductSelectAllPaged } from "../DTOs/purchaseproductSelectAllPaged";


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

//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:25:25 | Stack: 9

export class PurchaseProductModel {

    //Fields
    PurchaseProductId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	PurchaseId?: number;
	ProductId?: number;
    

    //Queries
    static Select1ByPurchaseProductId(PurchaseProductId: number) {
        let URL = "/api/MarshallStore/PurchaseProduct/1/Select1ByPurchaseProductIdToJSON/" + PurchaseProductId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/PurchaseProduct/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(purchaseproductSelectAllPaged: purchaseproductSelectAllPaged) {
        let URL = "/api/MarshallStore/PurchaseProduct/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: purchaseproductSelectAllPaged.QueryString,
            ActualPageNumber: purchaseproductSelectAllPaged.ActualPageNumber,
            RowsPerPage: purchaseproductSelectAllPaged.RowsPerPage,
            SorterColumn: purchaseproductSelectAllPaged.SorterColumn,
            SortToggler: purchaseproductSelectAllPaged.SortToggler,
            RowCount: purchaseproductSelectAllPaged.TotalRows,
            TotalPages: purchaseproductSelectAllPaged.TotalPages,
            lstPurchaseProductModel: purchaseproductSelectAllPaged.lstPurchaseProductModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByPurchaseProductId(PurchaseProductId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/PurchaseProduct/1/DeleteByPurchaseProductId/" + PurchaseProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/PurchaseProduct/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByPurchaseProductId(PurchaseProductId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/PurchaseProduct/1/CopyByPurchaseProductId/" + PurchaseProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/PurchaseProducting/PurchaseProduct/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}