"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Import libraries to use
var Comment_TsModel_1 = require("../TsModels/Comment_TsModel");
var $ = require("jquery");
require("bootstrap-notify");
var CommentQuery = /** @class */ (function () {
    function CommentQuery() {
    }
    CommentQuery.SelectAllByProductIdToHTML = function (ProductId) {
        //Used for list view
        $(window).off("scroll");
        var Content = "";
        Comment_TsModel_1.CommentModel.SelectAllByProductId(ProductId).subscribe({
            next: function (newrow) {
                //Only works when there is data available
                if (newrow.status != 204) {
                    var response_CommentQuery = newrow.response;
                    Content += "".concat(response_CommentQuery.map(function (row2) {
                        return "<div class=\"media media-comment\">\n                    <img alt=\"Image placeholder\" class=\"media-comment-avatar rounded-circle\" src=\"/img/CMSCore/User.png\">\n                    <div class=\"media-body\">\n                      <div class=\"media-comment-text\">\n                        <p class=\"text-sm lh-160\">".concat(row2.Text, "</p>\n                      </div>\n                    </div>\n                  </div>");
                    }).join(""));
                    Content += "<div class=\"media align-items-center mt-2\">\n                  <div class=\"media-body\">\n                    <form>\n                        <div class=\"row\">\n                            <div class=\"col text-right\">\n                                <textarea class=\"form-control mt-4\"\n                                    placeholder=\"Write your comment\"\n                                    rows=\"3\"\n                                    resize=\"none\"\n                                    maxlength=\"8000\">\n                                </textarea>\n                                <button class=\"btn btn-sm mt-2 mr-0 btn-primary btn-post-comment\" type=\"button\">Post comment</button>\n                            </div>\n                        </div>\n                    </form>\n                  </div>\n                </div>";
                    $("#marshallstore-comment-list").html(Content);
                }
                else {
                    //Show error message
                }
            },
            complete: function () {
                //Post comment button
                $(".btn-post-comment").on("click", function (e) {
                    var _a;
                    if ($(this).prev().val() == "") {
                        // @ts-ignore
                        $.notify({ icon: "fas fa-info", message: "Write a comment" }, { type: "info", placement: { from: "bottom", align: "center" } });
                        return;
                    }
                    var formData = new FormData();
                    var ProductId = $("#marshallstore-shoppingcart-productid-input").val();
                    if (ProductId === undefined) {
                        return;
                    }
                    // @ts-ignore
                    formData.append("ProductId", ProductId);
                    var Text = (_a = $(this).prev().val()) === null || _a === void 0 ? void 0 : _a.toString();
                    if (Text === undefined) {
                        Text = "";
                    }
                    formData.append("Text", Text);
                    //Setup request
                    var xmlHttpRequest = new XMLHttpRequest();
                    xmlHttpRequest.onload = function () {
                        if (xmlHttpRequest.status >= 400) {
                            // @ts-ignore
                            $.notify({ icon: "fas fa-info", message: "You have to login first" }, { type: "info", placement: { from: "bottom", align: "center" } });
                        }
                        else {
                            if (xmlHttpRequest.response == "You have to login first") {
                                // @ts-ignore
                                $.notify({ icon: "fas fa-info", message: "You have to login first" }, { type: "info", placement: { from: "bottom", align: "center" } });
                            }
                            else {
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Comment posted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                                ValidateAndSearch();
                            }
                        }
                    };
                    //Open connection
                    xmlHttpRequest.open("POST", "/api/MarshallStore/Comment/1/Insert", true);
                    //Send request
                    xmlHttpRequest.send(formData);
                });
            },
            error: function (err) {
            }
        });
    };
    return CommentQuery;
}());
function ValidateAndSearch() {
    // @ts-ignore
    var ProductId = $("#marshallstore-shoppingcart-productid-input").val();
    CommentQuery.SelectAllByProductIdToHTML(ProductId);
}
//LOAD EVENT
$(document).ready(function () {
    ValidateAndSearch();
});
//# sourceMappingURL=CommentQueryCustom_jQuery.js.map