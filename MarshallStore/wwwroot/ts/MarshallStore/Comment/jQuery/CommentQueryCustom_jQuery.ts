//Import libraries to use
import { CommentModel } from "../TsModels/Comment_TsModel";
import * as $ from "jquery";
import { format } from "timeago.js";
import "bootstrap-notify";

class CommentQuery {
    static SelectAllByProductIdToHTML(ProductId: number) {
        //Used for list view
        $(window).off("scroll");

        var Content: string = ``;

        CommentModel.SelectAllByProductId(ProductId).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {
                        const response_CommentQuery = newrow.response as CommentModel[];

                        Content += `${response_CommentQuery.map(row2 => {
                            return `<div class="media media-comment">
                    <img alt="Image placeholder" class="media-comment-avatar rounded-circle" src="/img/CMSCore/User.png">
                    <div class="media-body">
                      <div class="media-comment-text">
                        <p class="text-sm lh-160">${row2.Text}</p>
                      </div>
                    </div>
                  </div>`}).join("")}`;

                        Content += `<div class="media align-items-center mt-2">
                  <div class="media-body">
                    <form>
                        <div class="row">
                            <div class="col text-right">
                                <textarea class="form-control mt-4"
                                    placeholder="Write your comment"
                                    rows="3"
                                    resize="none"
                                    maxlength="8000">
                                </textarea>
                                <button class="btn btn-sm mt-2 mr-0 btn-primary btn-post-comment" type="button">Post comment</button>
                            </div>
                        </div>
                    </form>
                  </div>
                </div>`;

                        $("#marshallstore-comment-list").html(Content);
                    }
                    else {
                        //Show error message
                    }
                },
                complete: () => {

                    //Post comment button
                    $(".btn-post-comment").on("click", function (e) {

                        if ($(this).prev().val() == "") {
                            // @ts-ignore
                            $.notify({ icon: "fas fa-info", message: "Write a comment" }, { type: "info", placement: { from: "bottom", align: "center" } });
                            return;
                        }

                        let formData = new FormData();

                        let ProductId = $("#marshallstore-shoppingcart-productid-input").val();
                        if (ProductId === undefined) {
                            return;
                        }
                        // @ts-ignore
                        formData.append("ProductId", ProductId);

                        let Text = $(this).prev().val()?.toString();
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
                error: err => {
                }
            });
    }
}

function ValidateAndSearch() {

    // @ts-ignore
    let ProductId: number = $("#marshallstore-shoppingcart-productid-input").val();

    CommentQuery.SelectAllByProductIdToHTML(ProductId);
}

//LOAD EVENT
$(document).ready(function () {
    ValidateAndSearch();
});