$(document).ready(function () {
    jQuery.validator.addMethod("noSpace", function(value, element) {
        return value.indexOf(" ") < 0 && value != "";
    }, "No space");

    $('#allocateTasks').validate({
        rules: {
            service: {
                required: true,
            },
            examiner: {
                required: true,
            },
            numberOfApplications: {
                required: true,
                noSpace:true,
                digits:true
            },
            DateOfCompletion: {
                required: true,
            },
        },
        messages: {
            service: {
                required: 'This information is required'
            },
            examiner: {
                required: 'This information is required'
            },
            numberOfApplications: {
                required: 'This information is required',
                noSpace: 'Spaces are not allowed',
                digits: 'Only digits are required'
            },
            DateOfCompletion: {
                required: 'This information is required'
            },
        },
        submitHandler:function(form){
            $.ajax({
                type: 'Post',
                url: '/applications/allocate',
                data: $(form).serialize(),
                success: function (data) {
                    toastr.success('Your application has been submitted');
                    $('#allocateTasks').trigger('reset');
                },
                error: function (err) {
                    toastr.error('Something went wrong in submitting the task');
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