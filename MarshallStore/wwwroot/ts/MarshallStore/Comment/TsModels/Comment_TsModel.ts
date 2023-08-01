import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { commentSelectAllPaged } from "../DTOs/commentSelectAllPaged";


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

//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:24:26 | Stack: 9

export class CommentModel {

    //Fields
    CommentId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ProductId?: number;
	Text?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByCommentId(CommentId: number) {
        let URL = "/api/MarshallStore/Comment/1/Select1ByCommentIdToJSON/" + CommentId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/MarshallStore/Comment/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(commentSelectAllPaged: commentSelectAllPaged) {
        let URL = "/api/MarshallStore/Comment/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: commentSelectAllPaged.QueryString,
            ActualPageNumber: commentSelectAllPaged.ActualPageNumber,
            RowsPerPage: commentSelectAllPaged.RowsPerPage,
            SorterColumn: commentSelectAllPaged.SorterColumn,
            SortToggler: commentSelectAllPaged.SortToggler,
            RowCount: commentSelectAllPaged.TotalRows,
            TotalPages: commentSelectAllPaged.TotalPages,
            lstCommentModel: commentSelectAllPaged.lstCommentModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByCommentId(CommentId: number | string | string[] | undefined) {
        let URL = "/api/MarshallStore/Comment/1/DeleteByCommentId/" + CommentId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/MarshallStore/Comment/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByCommentId(CommentId: string | number | string[] | undefined) {
        let URL = "/api/MarshallStore/Comment/1/CopyByCommentId/" + CommentId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Commenting/Comment/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}