"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CommentModel = void 0;
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
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
var CommentModel = /** @class */ (function () {
    function CommentModel() {
    }
    //Queries
    CommentModel.Select1ByCommentId = function (CommentId) {
        var URL = "/api/MarshallStore/Comment/1/Select1ByCommentIdToJSON/" + CommentId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    CommentModel.SelectAll = function () {
        var URL = "/api/MarshallStore/Comment/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    CommentModel.SelectAllPaged = function (commentSelectAllPaged) {
        var URL = "/api/MarshallStore/Comment/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: commentSelectAllPaged.QueryString,
            ActualPageNumber: commentSelectAllPaged.ActualPageNumber,
            RowsPerPage: commentSelectAllPaged.RowsPerPage,
            SorterColumn: commentSelectAllPaged.SorterColumn,
            SortToggler: commentSelectAllPaged.SortToggler,
            RowCount: commentSelectAllPaged.TotalRows,
            TotalPages: commentSelectAllPaged.TotalPages,
            lstCommentModel: commentSelectAllPaged.lstCommentModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    CommentModel.DeleteByCommentId = function (CommentId) {
        var URL = "/api/MarshallStore/Comment/1/DeleteByCommentId/" + CommentId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    CommentModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/MarshallStore/Comment/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    CommentModel.CopyByCommentId = function (CommentId) {
        var URL = "/api/MarshallStore/Comment/1/CopyByCommentId/" + CommentId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    CommentModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Commenting/Comment/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return CommentModel;
}());
exports.CommentModel = CommentModel;
//# sourceMappingURL=Comment_TsModel.js.map