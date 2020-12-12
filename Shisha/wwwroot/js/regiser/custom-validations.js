$(document).ready(function () {

    $('#registration-form').validate({
        rules: {
            names: {
                required: true,
                maxlength: 200
            },
            surname: {
                required: true,
                maxlength: 200
            },
            country: {
                required: true,
            },
            nationalId: {
                required: true,
                maxlength: 200
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
            names: {
                required: 'This information is required',
                maxlength: 'Must be at most two hundred characters',
            },
            surname: {
                required: 'This information is required',
                maxlength: 'Must be at most two hundred characters',
            },
            country: {
                required: 'This information is required'
            },
            nationalId: {
                required: 'This information is required',
                maxlength: 'Must be at most two hundred characters',
            },
            residentialAddress: {
                required: 'This information is required',
                maxlength: 'Must be at most two hundred characters',
            },
            city: {
                required: 'This information is required',
                maxlength: 'Must be at most two hundred characters',
            },
            email: {
                email: 'This is not a valid email',
            },
            phoneNumber: {
                required: 'This information is required',
                maxlength: 'Must be at least Four characters',
            },
            password: {
                required: 'This information is required',
                minlength: 'Must be at least eight characters'
            },
            confirmPassword: {
                required: 'This information is required',
                minlength: 'Must be at least eight characters'
            }
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