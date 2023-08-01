

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

//Last modification on: 31/07/2023 14:32:16

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor


//Used for file input
let marshallstoreproductimage1input;
let marshallstoreproductimage1boolfileadded;
$("#marshallstore-product-image1-input").on("change", function (e) {
    marshallstoreproductimage1input = $(this).get(0).files;
    marshallstoreproductimage1boolfileadded = true;
    formData.append("marshallstore-product-image1-input", marshallstoreproductimage1input[0], marshallstoreproductimage1input[0].name);
});

let marshallstoreproductimage2input;
let marshallstoreproductimage2boolfileadded;
$("#marshallstore-product-image2-input").on("change", function (e) {
    marshallstoreproductimage2input = $(this).get(0).files;
    marshallstoreproductimage2boolfileadded = true;
    formData.append("marshallstore-product-image2-input", marshallstoreproductimage2input[0], marshallstoreproductimage2input[0].name);
});

let marshallstoreproductimage3input;
let marshallstoreproductimage3boolfileadded;
$("#marshallstore-product-image3-input").on("change", function (e) {
    marshallstoreproductimage3input = $(this).get(0).files;
    marshallstoreproductimage3boolfileadded = true;
    formData.append("marshallstore-product-image3-input", marshallstoreproductimage3input[0], marshallstoreproductimage3input[0].name);
});



//LOAD EVENT
$(document).ready(function () {
    $("#marshallstore-product-productcategoryid-select").on("change", function (e) {
        $("#marshallstore-product-productcategoryid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#marshallstore-product-productcategoryid-select option:selected").text()}
            </a>
            <input type="hidden" id="marshallstore-product-productcategoryid-input" value="${$("#marshallstore-product-productcategoryid-select option:selected").val()}"/>
        </li>`);
    });
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {
                
                //ProductId
                formData.append("marshallstore-product-productid-input", $("#marshallstore-product-productid-input").val());

                formData.append("marshallstore-product-productcategoryid-input", $("#marshallstore-product-productcategoryid-input").val());
                formData.append("marshallstore-product-producer-input", $("#marshallstore-product-producer-input").val());
                formData.append("marshallstore-product-model-input", $("#marshallstore-product-model-input").val());
                formData.append("marshallstore-product-price-input", $("#marshallstore-product-price-input").val());
                formData.append("marshallstore-product-description-input", $("#marshallstore-product-description-input").val());
                if (!marshallstoreproductimage1boolfileadded) {
                    formData.append("marshallstore-product-image1-input", $("#marshallstore-product-image1-readonly").val());
                }
                if (!marshallstoreproductimage2boolfileadded) {
                    formData.append("marshallstore-product-image2-input", $("#marshallstore-product-image2-readonly").val());
                }
                if (!marshallstoreproductimage3boolfileadded) {
                    formData.append("marshallstore-product-image3-input", $("#marshallstore-product-image3-readonly").val());
                }
                

                //Setup request
                var xmlHttpRequest = new XMLHttpRequest();
                //Set event listeners
                xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
                    //SAVING
                    $.notify({ message: "Saving data. Please, wait" }, { type: "info", placement: { from: "bottom", align: "center" } });
                });
                xmlHttpRequest.onload = function () {
                    if (xmlHttpRequest.status >= 400) {
                        //ERROR
                        console.log(xmlHttpRequest);
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while saving the data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    }
                    else {
                        //SUCCESS
                        window.location.replace("/MarshallStore/ProductQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/MarshallStore/Product/1/InsertOrUpdateAsync", true);
                //Send request
                xmlHttpRequest.send(formData);
            }
            else {
                $.notify({ message: "Please, complete all fields." }, { type: "warning", placement: { from: "bottom", align: "center" } });
            }


            form.classList.add("was-validated");
        }, false);
    });
});