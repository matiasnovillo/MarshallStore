

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

//Last modification on: 03/08/2023 18:27:16

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor


//Used for file input


//LOAD EVENT
$(document).ready(function () {
    
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {
                
                //PurchaseId
                formData.append("marshallstore-purchase-purchaseid-input", $("#marshallstore-purchase-purchaseid-input").val());

                formData.append("marshallstore-purchase-fullprice-input", $("#marshallstore-purchase-fullprice-input").val());
                formData.append("marshallstore-purchase-firstname-input", $("#marshallstore-purchase-firstname-input").val());
                formData.append("marshallstore-purchase-lastname-input", $("#marshallstore-purchase-lastname-input").val());
                formData.append("marshallstore-purchase-email-input", $("#marshallstore-purchase-email-input").val());
                formData.append("marshallstore-purchase-phone-input", $("#marshallstore-purchase-phone-input").val());
                formData.append("marshallstore-purchase-streetaddress-input", $("#marshallstore-purchase-streetaddress-input").val());
                formData.append("marshallstore-purchase-postcodeorzip-input", $("#marshallstore-purchase-postcodeorzip-input").val());
                formData.append("marshallstore-purchase-city-input", $("#marshallstore-purchase-city-input").val());
                formData.append("marshallstore-purchase-country-input", $("#marshallstore-purchase-country-input").val());
                formData.append("marshallstore-purchase-cardnumber-input", $("#marshallstore-purchase-cardnumber-input").val());
                formData.append("marshallstore-purchase-cardholder-input", $("#marshallstore-purchase-cardholder-input").val());
                formData.append("marshallstore-purchase-expiration-input", $("#marshallstore-purchase-expiration-input").val());
                formData.append("marshallstore-purchase-cvc-input", $("#marshallstore-purchase-cvc-input").val());
                

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
                        window.location.replace("/PurchaseFinished");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/MarshallStore/Purchase/1/InsertOrUpdateAsync", true);
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