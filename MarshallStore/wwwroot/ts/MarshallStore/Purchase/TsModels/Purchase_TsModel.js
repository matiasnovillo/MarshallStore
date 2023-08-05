"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PurchaseModel = void 0;
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
//19 fields | Sub-models: 1 models  | Last modification on: 03/08/2023 18:27:16 | Stack: 9
var PurchaseModel = /** @class */ (function () {
    function PurchaseModel() {
    }
    //Queries
    PurchaseModel.Select1ByPurchaseId = function (PurchaseId) {
        var URL = "/api/MarshallStore/Purchase/1/Select1ByPurchaseIdToJSON/" + PurchaseId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PurchaseModel.SelectAll = function () {
        var URL = "/api/MarshallStore/Purchase/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PurchaseModel.SelectAllPaged = function (purchaseSelectAllPaged) {
        var URL = "/api/MarshallStore/Purchase/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: purchaseSelectAllPaged.QueryString,
            ActualPageNumber: purchaseSelectAllPaged.ActualPageNumber,
            RowsPerPage: purchaseSelectAllPaged.RowsPerPage,
            SorterColumn: purchaseSelectAllPaged.SorterColumn,
            SortToggler: purchaseSelectAllPaged.SortToggler,
            RowCount: purchaseSelectAllPaged.TotalRows,
            TotalPages: purchaseSelectAllPaged.TotalPages,
            lstPurchaseModel: purchaseSelectAllPaged.lstPurchaseModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    PurchaseModel.DeleteByPurchaseId = function (PurchaseId) {
        var URL = "/api/MarshallStore/Purchase/1/DeleteByPurchaseId/" + PurchaseId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    PurchaseModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/MarshallStore/Purchase/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PurchaseModel.CopyByPurchaseId = function (PurchaseId) {
        var URL = "/api/MarshallStore/Purchase/1/CopyByPurchaseId/" + PurchaseId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PurchaseModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Purchaseing/Purchase/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return PurchaseModel;
}());
exports.PurchaseModel = PurchaseModel;
//# sourceMappingURL=Purchase_TsModel.js.map