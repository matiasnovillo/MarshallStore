"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Import libraries to use
var VisitorCounter_TsModel_1 = require("../../VisitorCounter/TsModels/VisitorCounter_TsModel");
var $ = require("jquery");
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
require("bootstrap-notify");
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
//Stack: 10
//Last modification on: 22/02/2023 7:45:50
//Set default values
var LastTopDistance = 0;
var QueryString = "";
var ActualPageNumber = 1;
var RowsPerPage = 50;
var SorterColumn = "";
var SortToggler = false;
var TotalPages = 0;
var TotalRows = 0;
var ViewToggler = "List";
var ScrollDownNSearchFlag = false;
var VisitorCounterQuery = /** @class */ (function () {
    function VisitorCounterQuery() {
    }
    VisitorCounterQuery.SelectAllPagedToHTML = function (request_visitorcounterSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");
        //Load some part of table
        var TableContent = "<thead class=\"thead-light\">\n    <tr>\n        <th scope=\"col\">\n            <div>\n                <input id=\"visitorcounter-table-check-all\" type=\"checkbox\">\n            </div>\n        </th>\n        <th scope=\"col\">\n            <button value=\"VisitorCounterId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                VisitorCounterId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Active\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Active\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"DateTimeCreation\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                DateTimeCreation\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"DateTimeLastModification\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                DateTimeLastModification\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"UserCreationId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                UserCreationId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"UserLastModificationId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                UserLastModificationId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"DateTime\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                DateTime\n            </button>\n        </th>\n        \n        <th scope=\"col\"></th>\n    </tr>\n</thead>\n<tbody>";
        var ListContent = "";
        VisitorCounter_TsModel_1.VisitorCounterModel.SelectAllPaged(request_visitorcounterSelectAllPaged).subscribe({
            next: function (newrow) {
                var _a, _b, _c, _d, _e, _f, _g, _h, _j;
                //Only works when there is data available
                if (newrow.status != 204) {
                    var response_visitorcounterQuery = newrow.response;
                    //Set to default values if they are null
                    QueryString = (_a = response_visitorcounterQuery.QueryString) !== null && _a !== void 0 ? _a : "";
                    ActualPageNumber = (_b = response_visitorcounterQuery.ActualPageNumber) !== null && _b !== void 0 ? _b : 0;
                    RowsPerPage = (_c = response_visitorcounterQuery.RowsPerPage) !== null && _c !== void 0 ? _c : 0;
                    SorterColumn = (_d = response_visitorcounterQuery.SorterColumn) !== null && _d !== void 0 ? _d : "";
                    SortToggler = (_e = response_visitorcounterQuery.SortToggler) !== null && _e !== void 0 ? _e : false;
                    TotalRows = (_f = response_visitorcounterQuery.TotalRows) !== null && _f !== void 0 ? _f : 0;
                    TotalPages = (_g = response_visitorcounterQuery.TotalPages) !== null && _g !== void 0 ? _g : 0;
                    //Query string
                    $("#basiccore-visitorcounter-query-string").attr("placeholder", "Search... (".concat(TotalRows, " records)"));
                    //Total pages of pagination
                    $("#basiccore-visitorcounter-total-pages-lg, #basiccore-visitorcounter-total-pages").html(TotalPages.toString());
                    //Actual page number of pagination
                    $("#basiccore-visitorcounter-actual-page-number-lg, #basiccore-visitorcounter-actual-page-number").html(ActualPageNumber.toString());
                    //If we are at the final of book disable next and last buttons in pagination
                    if (ActualPageNumber === TotalPages) {
                        $("#basiccore-visitorcounter-lnk-next-page-lg, #basiccore-visitorcounter-lnk-next-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-lnk-last-page-lg, #basiccore-visitorcounter-lnk-last-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-search-more-button-in-list").html("");
                    }
                    else {
                        $("#basiccore-visitorcounter-lnk-next-page-lg, #basiccore-visitorcounter-lnk-next-page").removeAttr("disabled");
                        $("#basiccore-visitorcounter-lnk-last-page-lg, #basiccore-visitorcounter-lnk-last-page").removeAttr("disabled");
                        //Scroll arrow for list view
                        $("#basiccore-visitorcounter-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                    }
                    //If we are at the begining of the book disable previous and first buttons in pagination
                    if (ActualPageNumber === 1) {
                        $("#basiccore-visitorcounter-lnk-previous-page-lg, #basiccore-visitorcounter-lnk-previous-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-lnk-first-page-lg, #basiccore-visitorcounter-lnk-first-page").attr("disabled", "disabled");
                    }
                    else {
                        $("#basiccore-visitorcounter-lnk-previous-page-lg, #basiccore-visitorcounter-lnk-previous-page").removeAttr("disabled");
                        $("#basiccore-visitorcounter-lnk-first-page-lg, #basiccore-visitorcounter-lnk-first-page").removeAttr("disabled");
                    }
                    //If book is empty set to default pagination values
                    if (((_h = response_visitorcounterQuery === null || response_visitorcounterQuery === void 0 ? void 0 : response_visitorcounterQuery.lstVisitorCounterModel) === null || _h === void 0 ? void 0 : _h.length) === 0) {
                        $("#basiccore-visitorcounter-lnk-previous-page-lg, #basiccore-visitorcounter-lnk-previous-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-lnk-first-page-lg, #basiccore-visitorcounter-lnk-first-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-lnk-next-page-lg, #basiccore-visitorcounter-lnk-next-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-lnk-last-page-lg, #basiccore-visitorcounter-lnk-last-page").attr("disabled", "disabled");
                        $("#basiccore-visitorcounter-total-pages-lg, #basiccore-visitorcounter-total-pages").html("1");
                        $("#basiccore-visitorcounter-actual-page-number-lg, #basiccore-visitorcounter-actual-page-number").html("1");
                    }
                    //Read data book
                    (_j = response_visitorcounterQuery === null || response_visitorcounterQuery === void 0 ? void 0 : response_visitorcounterQuery.lstVisitorCounterModel) === null || _j === void 0 ? void 0 : _j.forEach(function (row) {
                        TableContent += "<tr>\n    <!-- Checkbox -->\n    <td>\n        <div>\n            <input class=\"visitorcounter-table-checkbox-for-row\" value=\"".concat(row.VisitorCounterId, "\" type=\"checkbox\">\n        </div>\n    </td>\n    <!-- Data -->\n    <td class=\"text-left text-light\">\n        <i class=\"fas fa-key\"></i> ").concat(row.VisitorCounterId, "\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-toggle-on\"></i> ").concat(row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>", "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-calendar\"></i> ").concat(row.DateTimeCreation, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-calendar\"></i> ").concat(row.DateTimeLastModification, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> ").concat(row.UserCreationId, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> ").concat(row.UserLastModificationId, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-calendar\"></i> ").concat(row.DateTime, "\n        </strong>\n    </td>\n    \n    <!-- Actions -->\n    <td class=\"text-right\">\n        <a class=\"btn btn-icon-only text-primary\" href=\"/BasicCore/VisitorCounterNonQueryPage?VisitorCounterId=").concat(row.VisitorCounterId, "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"Edit\">\n            <i class=\"fas fa-edit\"></i>\n        </a>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-icon-only text-danger\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-trash\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button class=\"dropdown-item text-danger basiccore-visitorcounter-table-delete-button\" value=\"").concat(row.VisitorCounterId, "\" type=\"button\">\n                    <i class=\"fas fa-exclamation-triangle\"></i> Yes, delete\n                </button>\n            </div>\n        </div>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-sm btn-icon-only text-primary\" href=\"#\" type=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-ellipsis-v\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button type=\"button\" class=\"dropdown-item basiccore-visitorcounter-table-copy-button\" value=\"").concat(row.VisitorCounterId, "\">\n                    <i class=\"fas fa-copy text-primary\"></i>&nbsp;Copy\n                </button>\n            </div>\n        </div>\n    </td>\n</tr>");
                        ListContent += "<div class=\"row mx-2\">\n    <div class=\"col-sm\">\n        <div class=\"card bg-gradient-primary mb-2\">\n            <div class=\"card-body\">\n                <div class=\"row\">\n                    <div class=\"col text-truncate\">\n                        <span class=\"text-white text-light mb-4\">\n                           VisitorCounterId <i class=\"fas fa-key\"></i> ".concat(row.VisitorCounterId, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Active <i class=\"fas fa-toggle-on\"></i> ").concat(row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>", "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           DateTimeCreation <i class=\"fas fa-calendar\"></i> ").concat(row.DateTimeCreation, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           DateTimeLastModification <i class=\"fas fa-calendar\"></i> ").concat(row.DateTimeLastModification, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           UserCreationId <i class=\"fas fa-key\"></i> ").concat(row.UserCreationId, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           UserLastModificationId <i class=\"fas fa-key\"></i> ").concat(row.UserLastModificationId, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           DateTime <i class=\"fas fa-calendar\"></i> ").concat(row.DateTime, "\n                        </span>\n                        <br/>\n                        \n                    </div>\n                    <div class=\"col-auto\">\n                    </div>\n                </div>\n                <!-- Actions -->\n                <div class=\"row\">\n                    <div class=\"col\">\n                        <div class=\"justify-content-end text-right mt-2\">\n                            <div class=\"mb-2\">\n                                <a class=\"basiccore-visitorcounter-checkbox-list list-row-unchecked icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"Check\">\n                                    <i class=\"fas fa-circle text-white\"></i>\n                                </a>\n                                <input type=\"hidden\" value=\"").concat(row.VisitorCounterId, "\"/>\n                            </div>\n                            <a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"/BasicCore/VisitorCounterNonQueryPage?VisitorCounterId=").concat(row.VisitorCounterId, "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"edit\">\n                                <i class=\"fas fa-edit text-primary\"></i>\n                            </a>\n                            <div class=\"dropup\">\n                                <a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                                    <i class=\"fas fa-ellipsis-v\"></i>\n                                </a>\n                                <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                                    <button value=\"").concat(row.VisitorCounterId, "\" class=\"dropdown-item text-primary basiccore-visitorcounter-list-copy-button\" type=\"button\">\n                                        <i class=\"fas fa-copy\"></i>&nbsp;Copy\n                                    </button>\n                                    <button value=\"").concat(row.VisitorCounterId, "\" class=\"dropdown-item text-danger basiccore-visitorcounter-list-delete-button\" type=\"button\">\n                                        <i class=\"fas fa-trash\"></i>&nbsp;Delete\n                                    </button>\n                                </div>\n                            </div>\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>");
                    });
                    //If view table is activated, clear table view, if not, clear list view
                    if (ViewToggler === "Table") {
                        $("#basiccore-visitorcounter-body-and-head-table").html("");
                        $("#basiccore-visitorcounter-body-and-head-table").html(TableContent);
                    }
                    else {
                        //Used for list view
                        if (ScrollDownNSearchFlag) {
                            $("#basiccore-visitorcounter-body-list").append(ListContent);
                            ScrollDownNSearchFlag = false;
                        }
                        else {
                            //Clear list view
                            $("#basiccore-visitorcounter-body-list").html("");
                            $("#basiccore-visitorcounter-body-list").html(ListContent);
                        }
                    }
                }
                else {
                    //ERROR
                    // @ts-ignore
                    $.notify({ icon: "fas fa-exclamation-triangle", message: "No registers found" }, { type: "warning", placement: { from: "bottom", align: "center" } });
                }
            },
            complete: function () {
                //Execute ScrollDownNSearch function when the user scroll the page
                $(window).on("scroll", ScrollDownNSearch);
                //Add final content to TableContent
                TableContent += "</tbody>\n                                </table>";
                //Check button inside list view
                $(".basiccore-visitorcounter-checkbox-list").on("click", function (e) {
                    //Toggler
                    if ($(this).hasClass("list-row-checked")) {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-circle text-white\"></i>\n                                                        </a>");
                        $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                    }
                    else {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-check\"></i>\n                                                        </a>");
                        $(this).removeClass("list-row-unchecked").addClass("list-row-checked");
                    }
                });
                //Check all button inside table
                $("#visitorcounter-table-check-all").on("click", function (e) {
                    //Toggler
                    if ($("tr td div input.visitorcounter-table-checkbox-for-row").is(":checked")) {
                        $("tr td div input.visitorcounter-table-checkbox-for-row").removeAttr("checked");
                    }
                    else {
                        $("tr td div input.visitorcounter-table-checkbox-for-row").attr("checked", "checked");
                    }
                });
                //Buttons inside head of table
                $("tr th button").one("click", function (e) {
                    //Toggler
                    if (SorterColumn == $(this).attr("value")) {
                        SorterColumn = "";
                        SortToggler = true;
                    }
                    else {
                        SorterColumn = $(this).attr("value");
                        SortToggler = false;
                    }
                    ValidateAndSearch();
                });
                //Delete button in table and list
                $("div.dropdown-menu button.basiccore-visitorcounter-table-delete-button, div.dropdown-menu button.basiccore-visitorcounter-list-delete-button").on("click", function (e) {
                    var VisitorCounterId = $(this).val();
                    VisitorCounter_TsModel_1.VisitorCounterModel.DeleteByVisitorCounterId(VisitorCounterId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            //SUCCESS
                            // @ts-ignore
                            $.notify({ icon: "fas fa-check", message: "Row deleted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                            ValidateAndSearch();
                        },
                        error: function (err) {
                            //ERROR
                            // @ts-ignore
                            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                            console.log(err);
                        }
                    });
                });
                //Copy button in table and list
                $("div.dropdown-menu button.basiccore-visitorcounter-table-copy-button, div.dropdown-menu button.basiccore-visitorcounter-list-copy-button").on("click", function (e) {
                    var VisitorCounterId = $(this).val();
                    VisitorCounter_TsModel_1.VisitorCounterModel.CopyByVisitorCounterId(VisitorCounterId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            //SUCCESS
                            // @ts-ignore
                            $.notify({ icon: "fas fa-check", message: "Row copied successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                            ValidateAndSearch();
                        },
                        error: function (err) {
                            //ERROR
                            // @ts-ignore
                            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                            console.log(err);
                        }
                    });
                });
            },
            error: function (err) {
                //ERROR
                // @ts-ignore
                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                console.log(err);
            }
        });
    };
    return VisitorCounterQuery;
}());
function ValidateAndSearch() {
    var _visitorcounterSelectAllPaged = {
        QueryString: QueryString,
        ActualPageNumber: ActualPageNumber,
        RowsPerPage: RowsPerPage,
        SorterColumn: SorterColumn,
        SortToggler: SortToggler,
        TotalRows: TotalRows,
        TotalPages: TotalPages
    };
    VisitorCounterQuery.SelectAllPagedToHTML(_visitorcounterSelectAllPaged);
}
//LOAD EVENT
if ($("#basiccore-visitorcounter-title-page").html().includes("Query visitorcounter")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "VisitorCounterId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#basiccore-visitorcounter-lnk-first-page-lg, #basiccore-visitorcounter-lnk-first-page").attr("disabled", "disabled");
    $("#basiccore-visitorcounter-lnk-previous-page-lg, #basiccore-visitorcounter-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#basiccore-visitorcounter-export-message").html("");
    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#basiccore-visitorcounter-search-button")).on("click", function () {
    ValidateAndSearch();
});
//Query string
$("#basiccore-visitorcounter-query-string").on("change keyup input", function (e) {
    var _a, _b;
    //If undefined, set QueryString to "" value
    QueryString = (_b = ((_a = $(this).val()) === null || _a === void 0 ? void 0 : _a.toString())) !== null && _b !== void 0 ? _b : "";
    ValidateAndSearch();
});
//First page link in pagination
$("#basiccore-visitorcounter-lnk-first-page-lg, #basiccore-visitorcounter-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});
//Previous page link in pagination
$("#basiccore-visitorcounter-lnk-previous-page-lg, #basiccore-visitorcounter-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});
//Next page link in pagination
$("#basiccore-visitorcounter-lnk-next-page-lg, #basiccore-visitorcounter-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});
//Last page link in pagination
$("#basiccore-visitorcounter-lnk-last-page-lg, #basiccore-visitorcounter-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});
//Table view button
$("#basiccore-visitorcounter-table-view-button").on("click", function (e) {
    $("#basiccore-visitorcounter-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#basiccore-visitorcounter-body-and-head-table").html("");
    ValidateAndSearch();
});
//List view button
$("#basiccore-visitorcounter-list-view-button").on("click", function (e) {
    $("#basiccore-visitorcounter-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#basiccore-visitorcounter-body-list").html("");
    ValidateAndSearch();
});
//Used to list view
function ScrollDownNSearch() {
    var _a, _b, _c, _d, _e, _f, _g, _h;
    var WindowsTopDistance = (_a = $(window).scrollTop()) !== null && _a !== void 0 ? _a : 0;
    var WindowsBottomDistance = ((_b = $(window).scrollTop()) !== null && _b !== void 0 ? _b : 0) + ((_c = $(window).innerHeight()) !== null && _c !== void 0 ? _c : 0);
    var CardsFooterTopPosition = (_e = (_d = $("#basiccore-visitorcounter-search-more-button-in-list").offset()) === null || _d === void 0 ? void 0 : _d.top) !== null && _e !== void 0 ? _e : 0;
    var CardsFooterBottomPosition = ((_g = (_f = $("#basiccore-visitorcounter-search-more-button-in-list").offset()) === null || _f === void 0 ? void 0 : _f.top) !== null && _g !== void 0 ? _g : 0) + ((_h = $("#basiccore-visitorcounter-search-more-button-in-list").outerHeight()) !== null && _h !== void 0 ? _h : 0);
    if (WindowsTopDistance > LastTopDistance) {
        //Scroll down
        if ((WindowsBottomDistance > CardsFooterTopPosition) && (WindowsTopDistance < CardsFooterBottomPosition)) {
            //Search More button visible
            if (ActualPageNumber !== TotalPages) {
                ScrollDownNSearchFlag = true;
                ActualPageNumber += 1;
                ValidateAndSearch();
            }
        }
        else { /*Card footer not visible*/ }
    }
    else { /*Scroll up*/ }
    LastTopDistance = WindowsTopDistance;
}
//Used to list view
$(window).on("scroll", ScrollDownNSearch);
//Export as PDF button
$("#basiccore-visitorcounter-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#basiccore-visitorcounter-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_1 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.visitorcounter-table-checkbox-for-row:checked").each(function () {
                CheckedRows_1.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_1.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/BasicCore/VisitorCounter/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#basiccore-visitorcounter-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for PDF file
            $("#basiccore-visitorcounter-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/PDFFiles/BasicCore/VisitorCounter/VisitorCounter_".concat(DateTimeNow.AjaxForString, ".pdf\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-pdf\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//Export as Excel button
$("#basiccore-visitorcounter-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#basiccore-visitorcounter-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_2 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.visitorcounter-table-checkbox-for-row:checked").each(function () {
                CheckedRows_2.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_2.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/BasicCore/VisitorCounter/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#basiccore-visitorcounter-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for Excel file
            $("#basiccore-visitorcounter-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/ExcelFiles/BasicCore/VisitorCounter/VisitorCounter_".concat(DateTimeNow.AjaxForString, ".xlsx\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-excel\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//Export as CSV button
$("#basiccore-visitorcounter-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#basiccore-visitorcounter-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_3 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.visitorcounter-table-checkbox-for-row:checked").each(function () {
                CheckedRows_3.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_3.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/BasicCore/VisitorCounter/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#basiccore-visitorcounter-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for CSV file
            $("#basiccore-visitorcounter-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/CSVFiles/BasicCore/VisitorCounter/VisitorCounter_".concat(DateTimeNow.AjaxForString, ".csv\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-csv\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//Export close button in modal
$("#basiccore-visitorcounter-export-close-button").on("click", function (e) {
    $("#basiccore-visitorcounter-export-message").html("");
});
//Massive action Copy
$("#basiccore-visitorcounter-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    var CopyType = "";
    var Body = {};
    if ($("#basiccore-visitorcounter-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        var CheckedRows_4 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.visitorcounter-table-checkbox-for-row:checked").each(function () {
                CheckedRows_4.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_4.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_4.toString()
        };
    }
    VisitorCounter_TsModel_1.VisitorCounterModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed copy" }, { type: "success", placement: { from: "bottom", align: "center" } });
            ValidateAndSearch();
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//Massive action Delete
$("#basiccore-visitorcounter-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    var DeleteType = "";
    var Body = {};
    if ($("#basiccore-visitorcounter-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        var CheckedRows_5 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.visitorcounter-table-checkbox-for-row:checked").each(function () {
                CheckedRows_5.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_5.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_5.toString()
        };
    }
    VisitorCounter_TsModel_1.VisitorCounterModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed deletion" }, { type: "success", placement: { from: "bottom", align: "center" } });
            ValidateAndSearch();
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//# sourceMappingURL=VisitorCounterQuery_jQuery.js.map