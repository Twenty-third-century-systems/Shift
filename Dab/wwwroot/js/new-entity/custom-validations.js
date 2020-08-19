$(document).ready(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            alert( "Form successful submitted!" );
        }
    });

    $('#officeForm').validate({
        rules: {
            physicalAddress: {
                required: true,
                minlength: 5,
            },
            postalAddress: {
                required: true,
                minlength: 5
            },
            country: {
                required: true,
                minlength: 5
            },
            city: {
                required: true,
                minlength: 5
            },
            telNumber: {
                required: true,
                minlength: 5
            },
            mobileNumber: {
                required: true,
                minlength: 5
            },
            emailAddress: {
                required: true,
                email: true
            },
        },
        messages: {
            physicalAddress: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            postalAddress: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            country: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            city: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            telNumber: {
                required: 'This information is required',
                // tel:'This is not a valid telephone number',
                minlength: 'Must be at least Five characters'
            },
            mobileNumber: {
                required: 'This information is required',
                // tel:'This is not a valid mobile number',
                minlength: 'Must be at least Five characters'
            },
            emailAddress: {
                required: 'This information is required',
                email: 'This is not a valid email'
            },
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    
    $().validate()
});