$(document).ready(function () {
    $('#phoneNumber').intlTelInput({
        preferredCountries: [ "zw", "za" ],
        utilsScript:"/lib/intl-tel-input-master/build/js/utils.js"
    });
});