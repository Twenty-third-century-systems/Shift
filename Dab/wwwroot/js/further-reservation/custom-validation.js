$(document).ready(function () {

    $('#reservation').validate({
        rules: {
            reference: {
                required: true
            }
        },
        messages: {
            reference: {
                required: "This information is required."
            }
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/name-search/reserve',
                data: $(form).serializeToJSON(),
                success: function (data) {
                    toastr.success("The name has been further reserved.");
                    $('#reservation').trigger('reset');
                },
                error: function (err) {
                    toastr.error("Something went wrong reserving the name. Try again later.");
                    console.log($(form).serializeToJSON());
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