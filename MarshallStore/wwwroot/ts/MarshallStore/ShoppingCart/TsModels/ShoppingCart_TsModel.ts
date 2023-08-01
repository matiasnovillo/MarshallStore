import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { shoppingcartSelectAllPaged } from "../DTOs/shoppingcartSelectAllPaged";


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

//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:45:20 | Stack: 9

export class ShoppingCartModel {

    //Fields
    ShoppingCartId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ProductId?: number;
	Counter?: number;
    

    //Queries
    static Select1ByShoppingCartId(ShoppingCartId: number) {
        let URL = "/api/MarshallStore/ShoppingCart/1/Select1ByShoppingCartIdToJSON/" + ShoppingCartId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/ShoppingCart/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(shoppingcartSelectAllPaged: shoppingcartSelectAllPaged) {
        let URL = "/api/MarshallStore/ShoppingCart/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: shoppingcartSelectAllPaged.QueryString,
            ActualPageNumber: shoppingcartSelectAllPaged.ActualPageNumber,
            RowsPerPage: shoppingcartSelectAllPaged.RowsPerPage,
            SorterColumn: shoppingcartSelectAllPaged.SorterColumn,
            SortToggler: shoppingcartSelectAllPaged.SortToggler,
            RowCount: shoppingcartSelectAllPaged.TotalRows,
            TotalPages: shoppingcartSelectAllPaged.TotalPages,
            lstShoppingCartModel: shoppingcartSelectAllPaged.lstShoppingCartModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByShoppingCartId(ShoppingCartId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/ShoppingCart/1/DeleteByShoppingCartId/" + ShoppingCartId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/ShoppingCart/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByShoppingCartId(ShoppingCartId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/ShoppingCart/1/CopyByShoppingCartId/" + ShoppingCartId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/ShoppingCarting/ShoppingCart/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}