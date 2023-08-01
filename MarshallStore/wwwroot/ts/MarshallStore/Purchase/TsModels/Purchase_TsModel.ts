import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { purchaseSelectAllPaged } from "../DTOs/purchaseSelectAllPaged";
import { PurchaseProductModel } from "../../PurchaseProduct/TsModels/PurchaseProduct_TsModel";

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

//8 fields | Sub-models: 1 models  | Last modification on: 31/07/2023 14:25:18 | Stack: 9

export class PurchaseModel {

    //Fields
    PurchaseId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Address?: string | string[] | number | undefined;
	FullPrice?: number;
    lstPurchaseProductModel?: PurchaseProductModel[] | undefined;
    

    //Queries
    static Select1ByPurchaseId(PurchaseId: number) {
        let URL = "/api/MarshallStore/Purchase/1/Select1ByPurchaseIdToJSON/" + PurchaseId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/Purchase/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(purchaseSelectAllPaged: purchaseSelectAllPaged) {
        let URL = "/api/MarshallStore/Purchase/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: purchaseSelectAllPaged.QueryString,
            ActualPageNumber: purchaseSelectAllPaged.ActualPageNumber,
            RowsPerPage: purchaseSelectAllPaged.RowsPerPage,
            SorterColumn: purchaseSelectAllPaged.SorterColumn,
            SortToggler: purchaseSelectAllPaged.SortToggler,
            RowCount: purchaseSelectAllPaged.TotalRows,
            TotalPages: purchaseSelectAllPaged.TotalPages,
            lstPurchaseModel: purchaseSelectAllPaged.lstPurchaseModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByPurchaseId(PurchaseId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/Purchase/1/DeleteByPurchaseId/" + PurchaseId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/Purchase/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByPurchaseId(PurchaseId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/Purchase/1/CopyByPurchaseId/" + PurchaseId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Purchaseing/Purchase/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}