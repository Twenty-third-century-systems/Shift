$(document).ready(function () {

    $('#paymentForm').validate({
        rules: {
            email: {
                required: true,
                email: true,
            },
            walletProvider: {
                required: true,
            },
            phoneNumber: {
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
            walletProvider: {
                required: 'This information is required.',
            },
            phoneNumber: {
                required: 'This information is required.',
                minlength: 'Must be at least 3 characters long'
            },
            amount: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            $('#busy').show()
            $.ajax({
                type: 'Post',
                url: '/api/Payments/Topup',
                data: $(form).serializeToJSON(),
                success: function (data) {
                    $('#busy').hide();
                    let instructionsAlertElement = '' +
                        '<div class="alert alert-info">\n' +
                        '        <p id="Instructions">Please attend to a prompt displayed on your handset<br>' +
                        'If nothing is displayed follow these steps:<br>' + 
                        data;
                    $('#instructionContainer').append(instructionsAlertElement);
                    $('#modal-lg').modal('toggle');
                    $('#paymentForm').trigger('reset');
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