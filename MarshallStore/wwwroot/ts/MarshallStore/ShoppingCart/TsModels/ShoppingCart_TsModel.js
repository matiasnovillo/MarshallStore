"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ShoppingCartModel = void 0;
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
//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:45:20 | Stack: 9
var ShoppingCartModel = /** @class */ (function () {
    function ShoppingCartModel() {
    }
    //Queries
    ShoppingCartModel.Select1ByShoppingCartId = function (ShoppingCartId) {
        var URL = "/api/MarshallStore/ShoppingCart/1/Select1ByShoppingCartIdToJSON/" + ShoppingCartId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ShoppingCartModel.SelectAll = function () {
        var URL = "/api/MarshallStore/ShoppingCart/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ShoppingCartModel.SelectAllPaged = function (shoppingcartSelectAllPaged) {
        var URL = "/api/MarshallStore/ShoppingCart/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: shoppingcartSelectAllPaged.QueryString,
            ActualPageNumber: shoppingcartSelectAllPaged.ActualPageNumber,
            RowsPerPage: shoppingcartSelectAllPaged.RowsPerPage,
            SorterColumn: shoppingcartSelectAllPaged.SorterColumn,
            SortToggler: shoppingcartSelectAllPaged.SortToggler,
            RowCount: shoppingcartSelectAllPaged.TotalRows,
            TotalPages: shoppingcartSelectAllPaged.TotalPages,
            lstShoppingCartModel: shoppingcartSelectAllPaged.lstShoppingCartModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    ShoppingCartModel.DeleteByShoppingCartId = function (ShoppingCartId) {
        var URL = "/api/MarshallStore/ShoppingCart/1/DeleteByShoppingCartId/" + ShoppingCartId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ShoppingCartModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/MarshallStore/ShoppingCart/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ShoppingCartModel.CopyByShoppingCartId = function (ShoppingCartId) {
        var URL = "/api/MarshallStore/ShoppingCart/1/CopyByShoppingCartId/" + ShoppingCartId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ShoppingCartModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/ShoppingCarting/ShoppingCart/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ShoppingCartModel;
}());
exports.ShoppingCartModel = ShoppingCartModel;
//# sourceMappingURL=ShoppingCart_TsModel.js.map