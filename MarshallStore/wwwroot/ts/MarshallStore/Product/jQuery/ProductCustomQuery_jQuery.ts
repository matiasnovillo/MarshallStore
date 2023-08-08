//Import libraries to use
import { ProductModel } from "../../Product/TsModels/Product_TsModel";
import { productSelectAllPaged } from "../DTOs/productSelectAllPaged";
import * as $ from "jquery";
import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import "bootstrap-notify";

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
let LastTopDistance: number = 0;
let QueryString: string = "";
let ActualPageNumber: number = 1;
let RowsPerPage: number = 50;
let SorterColumn: string | undefined = "";
let SortToggler: boolean = false;
let TotalPages: number = 0;
let TotalRows: number = 0;
let ViewToggler: string = "List";
let ScrollDownNSearchFlag: boolean = false;

class ProductQuery {
    static SelectAllPagedToHTML(request_productSelectAllPaged: productSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");

        var ListContent: string = ``;

        ProductModel.SelectAllPaged(request_productSelectAllPaged).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_productQuery = newrow.response as productSelectAllPaged;

                        //Set to default values if they are null
                        QueryString = response_productQuery.QueryString ?? "";
                        ActualPageNumber = response_productQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_productQuery.RowsPerPage ?? 0;
                        SorterColumn = response_productQuery.SorterColumn ?? "";
                        SortToggler = response_productQuery.SortToggler ?? false;
                        TotalRows = response_productQuery.TotalRows ?? 0;
                        TotalPages = response_productQuery.TotalPages ?? 0;

                        //Query string
                        $("#marshallstore-product-query-string").attr("placeholder", `Search... (${TotalRows} products)`);
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#marshallstore-product-search-more-button-in-list").html("");
                        }
                        else {
                            //Scroll arrow for list view
                            $("#marshallstore-product-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }

                        //Read data book
                        response_productQuery?.lstProductModel?.forEach(row => {

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
                            ListContent += `
<div class="card ml-4" style="width: 20rem;">
    <img class="card-img-top" src="${row.Image1}" alt="Card image cap">
    <div class="card-body">
        <h3 class="card-title">${row.Model}</h3>
        <h5 class="card-title">${row.Producer}</h5>
        <p class="card-text">${row.Description}</p>
        <p class="card-text text-danger"><strong>${row.Price}</strong></p>
        <a href="Product/${row.ProductId}" class="btn btn-outline-primary btn-sm">View details</a>
    </div>
</div>`;
                        })

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
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Check button inside list view
                    $(".marshallstore-product-checkbox-list").on("click", function (e) {
                        //Toggler
                        if ($(this).hasClass("list-row-checked")) {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-circle text-white"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                        }
                        else {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-check"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-unchecked").addClass("list-row-checked");
                        }
                    });

                    //Delete button in table and list
                    $("div.dropdown-menu button.marshallstore-product-table-delete-button, div.dropdown-menu button.marshallstore-product-list-delete-button").on("click", function (e) {
                        let ProductId = $(this).val();
                        ProductModel.DeleteByProductId(ProductId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row deleted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.marshallstore-product-table-copy-button, div.dropdown-menu button.marshallstore-product-list-copy-button").on("click", function (e) {
                        let ProductId = $(this).val();
                        ProductModel.CopyByProductId(ProductId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row copied successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });
                },
                error: err => {
                    //ERROR
                    // @ts-ignore
                    $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    console.log(err);
                }
            });
    }
}

function ValidateAndSearch() {

    var _productSelectAllPaged: productSelectAllPaged = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
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
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});


//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#marshallstore-product-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#marshallstore-product-search-more-button-in-list").offset()?.top ?? 0) + ($("#marshallstore-product-search-more-button-in-list").outerHeight() ?? 0);

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