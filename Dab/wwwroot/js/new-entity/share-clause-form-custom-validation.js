$(document).ready(function () {
    $('#shareClauseForm').validate({
        rules: {
            title: {
                required: true,
            },
            totalNumberOfShares: {
                required: true,
                digits: true
            },
        },
        messages: {
            title: {
                required: 'This information in required.',
            },
            totalNumberOfShares: {
                required: 'This information in required.',
                digits: 'Only digits allowed.'
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();
            shareClauses.push(input);
            tblShareClauses.row.add([
                input.title,
                input.totalNumberOfShares,
            ]).draw(false);
            $('#shareClauseForm').trigger('reset');
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

})