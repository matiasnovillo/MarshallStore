import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { rateSelectAllPaged } from "../DTOs/rateSelectAllPaged";


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

//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:25:36 | Stack: 9

export class RateModel {

    //Fields
    RateId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ProductId?: number;
	Score?: number;
    

    //Queries
    static Select1ByRateId(RateId: number) {
        let URL = "/api/MarshallStore/Rate/1/Select1ByRateIdToJSON/" + RateId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/Rate/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(rateSelectAllPaged: rateSelectAllPaged) {
        let URL = "/api/MarshallStore/Rate/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: rateSelectAllPaged.QueryString,
            ActualPageNumber: rateSelectAllPaged.ActualPageNumber,
            RowsPerPage: rateSelectAllPaged.RowsPerPage,
            SorterColumn: rateSelectAllPaged.SorterColumn,
            SortToggler: rateSelectAllPaged.SortToggler,
            RowCount: rateSelectAllPaged.TotalRows,
            TotalPages: rateSelectAllPaged.TotalPages,
            lstRateModel: rateSelectAllPaged.lstRateModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRateId(RateId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/Rate/1/DeleteByRateId/" + RateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/Rate/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRateId(RateId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/Rate/1/CopyByRateId/" + RateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Rateing/Rate/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}