"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ShoppingCart_TsModel_1 = require("../../ShoppingCart/TsModels/ShoppingCart_TsModel");
var numeral = require('numeral');
//Delete button in table and list
$(".marshallstore-shoppingcart-list-delete-button").on("click", function (e) {
    var ShoppingCartId = $(this).next().val();
    ShoppingCart_TsModel_1.ShoppingCartModel.DeleteByShoppingCartId(ShoppingCartId).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            window.location.replace("/Cart");
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//# sourceMappingURL=Cart.js.map