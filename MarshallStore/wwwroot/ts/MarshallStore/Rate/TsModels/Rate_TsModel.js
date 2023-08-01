"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RateModel = void 0;
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
//8 fields | Sub-models: 0 models  | Last modification on: 31/07/2023 14:25:36 | Stack: 9
var RateModel = /** @class */ (function () {
    function RateModel() {
    }
    //Queries
    RateModel.Select1ByRateId = function (RateId) {
        var URL = "/api/MarshallStore/Rate/1/Select1ByRateIdToJSON/" + RateId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RateModel.SelectAll = function () {
        var URL = "/api/MarshallStore/Rate/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RateModel.SelectAllPaged = function (rateSelectAllPaged) {
        var URL = "/api/MarshallStore/Rate/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: rateSelectAllPaged.QueryString,
            ActualPageNumber: rateSelectAllPaged.ActualPageNumber,
            RowsPerPage: rateSelectAllPaged.RowsPerPage,
            SorterColumn: rateSelectAllPaged.SorterColumn,
            SortToggler: rateSelectAllPaged.SortToggler,
            RowCount: rateSelectAllPaged.TotalRows,
            TotalPages: rateSelectAllPaged.TotalPages,
            lstRateModel: rateSelectAllPaged.lstRateModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    RateModel.DeleteByRateId = function (RateId) {
        var URL = "/api/MarshallStore/Rate/1/DeleteByRateId/" + RateId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RateModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/MarshallStore/Rate/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RateModel.CopyByRateId = function (RateId) {
        var URL = "/api/MarshallStore/Rate/1/CopyByRateId/" + RateId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RateModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Rateing/Rate/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RateModel;
}());
exports.RateModel = RateModel;
//# sourceMappingURL=Rate_TsModel.js.map