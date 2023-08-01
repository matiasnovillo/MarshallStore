import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { productSelectAllPaged } from "../DTOs/productSelectAllPaged";
import { PurchaseProductModel } from "../../PurchaseProduct/TsModels/PurchaseProduct_TsModel";import { ShoppingCartModel } from "../../ShoppingCart/TsModels/ShoppingCart_TsModel";import { RateModel } from "../../Rate/TsModels/Rate_TsModel";import { CommentModel } from "../../Comment/TsModels/Comment_TsModel";

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

//14 fields | Sub-models: 4 models  | Last modification on: 31/07/2023 14:32:16 | Stack: 9

export class ProductModel {

    //Fields
    ProductId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ProductCategoryId?: number;
	Producer?: string | string[] | number | undefined;
	Model?: string | string[] | number | undefined;
	Price?: number;
	Description?: string | string[] | number | undefined;
	Image1?: string | string[] | number | undefined;
	Image2?: string | string[] | number | undefined;
	Image3?: string | string[] | number | undefined;
    lstPurchaseProductModel?: PurchaseProductModel[] | undefined;
    lstShoppingCartModel?: ShoppingCartModel[] | undefined;
    lstRateModel?: RateModel[] | undefined;
    lstCommentModel?: CommentModel[] | undefined;
    

    //Queries
    static Select1ByProductId(ProductId: number) {
        let URL = "/api/MarshallStore/Product/1/Select1ByProductIdToJSON/" + ProductId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/Product/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(productSelectAllPaged: productSelectAllPaged) {
        let URL = "/api/MarshallStore/Product/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: productSelectAllPaged.QueryString,
            ActualPageNumber: productSelectAllPaged.ActualPageNumber,
            RowsPerPage: productSelectAllPaged.RowsPerPage,
            SorterColumn: productSelectAllPaged.SorterColumn,
            SortToggler: productSelectAllPaged.SortToggler,
            RowCount: productSelectAllPaged.TotalRows,
            TotalPages: productSelectAllPaged.TotalPages,
            lstProductModel: productSelectAllPaged.lstProductModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByProductId(ProductId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/Product/1/DeleteByProductId/" + ProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/Product/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByProductId(ProductId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/Product/1/CopyByProductId/" + ProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Producting/Product/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}