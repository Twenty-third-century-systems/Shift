$(document).ready(function () {
    jQuery.validator.addMethod("noSpace", function (value, element) {
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
                noSpace: true,
                digits: true
            },
            dateOfCompletion: {
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
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/applications/allocate',
                data: $(form).serialize(),
                success: function (data) {
                    toastr.success('Task has been allocated');
                    if ($("[name='serviceId']").val() === 0)
                        $('#nameSearchCount').text(data)
                    if ($("[name='serviceId']").val() === 1)
                        $('#pvtEntityCount').text(data)
                    console.log(data)
                    $('#allocateTasks').trigger('reset');
                    // $('#allocateTasks').trigger('reset');
                    // refreshNameSearchApplicationDisplay(1);
                },
                error: function (err) {
                    toastr.error('Something went wrong in allocating the task: ' + err);
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