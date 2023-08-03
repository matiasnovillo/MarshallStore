"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Import libraries to use
var Product_TsModel_1 = require("../../Product/TsModels/Product_TsModel");
var $ = require("jquery");
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
//Last modification on: 01/08/2023 19:39:01
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
var ProductQuery = /** @class */ (function () {
    function ProductQuery() {
    }
    ProductQuery.SelectAllPagedToHTML = function (request_productSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");
        var ListContent = "";
        Product_TsModel_1.ProductModel.SelectAllPaged(request_productSelectAllPaged).subscribe({
            next: function (newrow) {
                var _a, _b, _c, _d, _e, _f, _g, _h;
                //Only works when there is data available
                if (newrow.status != 204) {
                    var response_productQuery = newrow.response;
                    //Set to default values if they are null
                    QueryString = (_a = response_productQuery.QueryString) !== null && _a !== void 0 ? _a : "";
                    ActualPageNumber = (_b = response_productQuery.ActualPageNumber) !== null && _b !== void 0 ? _b : 0;
                    RowsPerPage = (_c = response_productQuery.RowsPerPage) !== null && _c !== void 0 ? _c : 0;
                    SorterColumn = (_d = response_productQuery.SorterColumn) !== null && _d !== void 0 ? _d : "";
                    SortToggler = (_e = response_productQuery.SortToggler) !== null && _e !== void 0 ? _e : false;
                    TotalRows = (_f = response_productQuery.TotalRows) !== null && _f !== void 0 ? _f : 0;
                    TotalPages = (_g = response_productQuery.TotalPages) !== null && _g !== void 0 ? _g : 0;
                    //Query string
                    $("#marshallstore-product-query-string").attr("placeholder", "Search... (".concat(TotalRows, " products)"));
                    //If we are at the final of book disable next and last buttons in pagination
                    if (ActualPageNumber === TotalPages) {
                        $("#marshallstore-product-search-more-button-in-list").html("");
                    }
                    else {
                        //Scroll arrow for list view
                        $("#marshallstore-product-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                    }
                    //Read data book
                    (_h = response_productQuery === null || response_productQuery === void 0 ? void 0 : response_productQuery.lstProductModel) === null || _h === void 0 ? void 0 : _h.forEach(function (row) {
                        //                            ListContent += `<div class="row mx-2">
                        //    <div class="col-sm">
                        //        <div class="card bg-gradient-primary mb-2">
                        //            <div class="card-body">
                        //                <div class="row">
                        //                    <div class="col text-truncate">
                        //                        <span class="text-white text-light mb-4">
                        //                           ProductId <i class="fas fa-key"></i> ${row.ProductId}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Active <i class="fas fa-toggle-on"></i> ${row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>"}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           DateTimeCreation <i class="fas fa-calendar"></i> ${row.DateTimeCreation}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           DateTimeLastModification <i class="fas fa-calendar"></i> ${row.DateTimeLastModification}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           UserCreationId <i class="fas fa-key"></i> ${row.UserCreationId}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           UserLastModificationId <i class="fas fa-key"></i> ${row.UserLastModificationId}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           ProductCategoryId <i class="fas fa-key"></i> ${row.ProductCategoryId}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Producer <i class="fas fa-font"></i> ${row.Producer}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Model <i class="fas fa-font"></i> ${row.Model}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Price <i class="fas fa-divide"></i> ${row.Price}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Description <i class="fas fa-font"></i> ${row.Description}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Image1 <i class="fas fa-file"></i> ${row.Image1}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Image2 <i class="fas fa-file"></i> ${row.Image2}
                        //                        </span>
                        //                        <br/>
                        //                        <span class="text-white mb-4">
                        //                           Image3 <i class="fas fa-file"></i> ${row.Image3}
                        //                        </span>
                        //                        <br/>
                        //                    </div>
                        //                    <div class="col-auto">
                        //                    </div>
                        //                </div>
                        //            </div>
                        //        </div>
                        //    </div>
                        //</div>`;
                        ListContent += "\n<div class=\"card ml-4\" style=\"width: 20rem;\">\n    <img class=\"card-img-top\" src=\"".concat(row.Image1, "\" alt=\"Card image cap\">\n    <div class=\"card-body\">\n        <h3 class=\"card-title\">").concat(row.Model, "</h3>\n        <h5 class=\"card-title\">").concat(row.Producer, "</h5>\n        <p class=\"card-text\">").concat(row.Description, "</p>\n        <p class=\"card-text text-danger\"><strong>").concat(row.Price, "</strong></p>\n        <a href=\"Product/").concat(row.ProductId, "\" class=\"btn btn-outline-primary btn-sm\">View details</a>\n    </div>\n</div>");
                    });
                    //Used for list view
                    if (ScrollDownNSearchFlag) {
                        $("#marshallstore-product-body-list").append(ListContent);
                        ScrollDownNSearchFlag = false;
                    }
                    else {
                        //Clear list view
                        $("#marshallstore-product-body-list").html("");
                        $("#marshallstore-product-body-list").html(ListContent);
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
                //Check button inside list view
                $(".marshallstore-product-checkbox-list").on("click", function (e) {
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
                //Delete button in table and list
                $("div.dropdown-menu button.marshallstore-product-table-delete-button, div.dropdown-menu button.marshallstore-product-list-delete-button").on("click", function (e) {
                    var ProductId = $(this).val();
                    Product_TsModel_1.ProductModel.DeleteByProductId(ProductId).subscribe({
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
                $("div.dropdown-menu button.marshallstore-product-table-copy-button, div.dropdown-menu button.marshallstore-product-list-copy-button").on("click", function (e) {
                    var ProductId = $(this).val();
                    Product_TsModel_1.ProductModel.CopyByProductId(ProductId).subscribe({
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
    return ProductQuery;
}());
function ValidateAndSearch() {
    var _productSelectAllPaged = {
        QueryString: QueryString,
        ActualPageNumber: ActualPageNumber,
        RowsPerPage: RowsPerPage,
        SorterColumn: SorterColumn,
        SortToggler: SortToggler,
        TotalRows: TotalRows,
        TotalPages: TotalPages
    };
    ProductQuery.SelectAllPagedToHTML(_productSelectAllPaged);
}
//LOAD EVENT
if ($("#marshallstore-product-title-page").html().includes("Marshall Store")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "ProductId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#marshallstore-product-lnk-first-page-lg, #marshallstore-product-lnk-first-page").attr("disabled", "disabled");
    $("#marshallstore-product-lnk-previous-page-lg, #marshallstore-product-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#marshallstore-product-export-message").html("");
    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#marshallstore-product-search-button")).on("click", function () {
    ValidateAndSearch();
});
//Query string
$("#marshallstore-product-query-string").on("change keyup input", function (e) {
    var _a, _b;
    //If undefined, set QueryString to "" value
    QueryString = (_b = ((_a = $(this).val()) === null || _a === void 0 ? void 0 : _a.toString())) !== null && _b !== void 0 ? _b : "";
    ValidateAndSearch();
});
//Used to list view
function ScrollDownNSearch() {
    var _a, _b, _c, _d, _e, _f, _g, _h;
    var WindowsTopDistance = (_a = $(window).scrollTop()) !== null && _a !== void 0 ? _a : 0;
    var WindowsBottomDistance = ((_b = $(window).scrollTop()) !== null && _b !== void 0 ? _b : 0) + ((_c = $(window).innerHeight()) !== null && _c !== void 0 ? _c : 0);
    var CardsFooterTopPosition = (_e = (_d = $("#marshallstore-product-search-more-button-in-list").offset()) === null || _d === void 0 ? void 0 : _d.top) !== null && _e !== void 0 ? _e : 0;
    var CardsFooterBottomPosition = ((_g = (_f = $("#marshallstore-product-search-more-button-in-list").offset()) === null || _f === void 0 ? void 0 : _f.top) !== null && _g !== void 0 ? _g : 0) + ((_h = $("#marshallstore-product-search-more-button-in-list").outerHeight()) !== null && _h !== void 0 ? _h : 0);
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
//# sourceMappingURL=ProductQuery_jQuery.js.map