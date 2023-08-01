"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PurchaseProductModel = void 0;
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
//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:25:25 | Stack: 9
var PurchaseProductModel = /** @class */ (function () {
    function PurchaseProductModel() {
    }
    //Queries
    PurchaseProductModel.Select1ByPurchaseProductId = function (PurchaseProductId) {
        var URL = "/api/MarshallStore/PurchaseProduct/1/Select1ByPurchaseProductIdToJSON/" + PurchaseProductId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PurchaseProductModel.SelectAll = function () {
        var URL = "/api/MarshallStore/PurchaseProduct/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PurchaseProductModel.SelectAllPaged = function (purchaseproductSelectAllPaged) {
        var URL = "/api/MarshallStore/PurchaseProduct/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: purchaseproductSelectAllPaged.QueryString,
            ActualPageNumber: purchaseproductSelectAllPaged.ActualPageNumber,
            RowsPerPage: purchaseproductSelectAllPaged.RowsPerPage,
            SorterColumn: purchaseproductSelectAllPaged.SorterColumn,
            SortToggler: purchaseproductSelectAllPaged.SortToggler,
            RowCount: purchaseproductSelectAllPaged.TotalRows,
            TotalPages: purchaseproductSelectAllPaged.TotalPages,
            lstPurchaseProductModel: purchaseproductSelectAllPaged.lstPurchaseProductModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    PurchaseProductModel.DeleteByPurchaseProductId = function (PurchaseProductId) {
        var URL = "/api/MarshallStore/PurchaseProduct/1/DeleteByPurchaseProductId/" + PurchaseProductId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    PurchaseProductModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/MarshallStore/PurchaseProduct/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PurchaseProductModel.CopyByPurchaseProductId = function (PurchaseProductId) {
        var URL = "/api/MarshallStore/PurchaseProduct/1/CopyByPurchaseProductId/" + PurchaseProductId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PurchaseProductModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/PurchaseProducting/PurchaseProduct/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return PurchaseProductModel;
}());
exports.PurchaseProductModel = PurchaseProductModel;
//# sourceMappingURL=PurchaseProduct_TsModel.js.map