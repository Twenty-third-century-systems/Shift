$(document).ready(function () {

    $('#registration-form').validate({
        rules: {
            firstName: {
                required: true,
                maxlength: 200
            },
            middleName: {
                required: true,
                maxlength: 200
            },
            surname: {
                required: true,
                maxlength: 200
            },
            nationalId: {
                required: true,
                maxlength: 200
            },
            office: {
                required: true,
                maxlength: 200,
            },
            residentialAddress: {
                required: true,
                maxlength: 200
            },
            city: {
                required: true,
                maxlength: 200,
            },
            email: {
                required: true,
                email: true,
            },
            phoneNumber: {
                required: true,
            },
            password: {
                required: true,
                minlength: 8
            },
            confirmPassword: {
                required: true,
                minlength: 8
            },
        },
        messages: {
            firstName: {
                required: "This information is required",
                maxlength: 200
            },
            middleName: {
                required: "This information is required",
                maxlength: 200
            },
            surname: {
                required: "This information is required",
                maxlength: 200
            },
            nationalId: {
                required: "This information is required",
                maxlength: 200
            },
            office: {
                required: "This information is required",
            },
            residentialAddress: {
                required: "This information is required",
                maxlength: 200
            },
            city: {
                required: "This information is required",
                maxlength: 200,
            },
            email: {
                required: "This information is required",
                email: true,
            },
            phoneNumber: {
                required: "This information is required",
            },
            password: {
                required: "This information is required",
                minlength: 8
            },
            confirmPassword: {
                required: "This information is required",
                minlength: 8
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
});