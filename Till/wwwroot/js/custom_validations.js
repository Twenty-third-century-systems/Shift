﻿$(document).ready(function () {

    $('#paymentForm').validate({
        rules: {
            email: {
                required: true,
                email: true,
            },
            mode: {
                required: true,
            },
            pNumber: {
                required: true,
                minlength: 3
            },
            amount: {
                required: true,
            },
        },
        messages: {
            email: {
                required: 'This information is required.',
                email: 'This is not a valid email.'
            },
            mode: {
                required: 'This information is required.',
            },
            pNumber: {
                required: 'This information is required.',
                minlength: 'Must be at least 3 characters long'
            },
            amount: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/api/Payments/Topup',
                data: {
                    paymentData: $(form).serializeToJSON()
                },
                success: function (data) {
                    console.log(data);
                    alert(data);
                },
                error: function (err) {
                    alert(err.toString());
                },
            });
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