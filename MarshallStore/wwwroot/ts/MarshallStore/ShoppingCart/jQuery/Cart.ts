import { ShoppingCartModel } from "../../ShoppingCart/TsModels/ShoppingCart_TsModel";
var numeral = require('numeral');

//Delete button in table and list
$(".marshallstore-shoppingcart-list-delete-button").on("click", function (e) {
    let ShoppingCartId = $(this).next().val();
    ShoppingCartModel.DeleteByShoppingCartId(ShoppingCartId).subscribe({
        next: newrow => {
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            window.location.replace("/Cart");
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});