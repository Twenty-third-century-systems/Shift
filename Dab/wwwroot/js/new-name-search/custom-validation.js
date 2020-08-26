let readyForSubmission = false;

$(document).ready(function () {
    jQuery.validator.addMethod("noSpace", function(value, element) {
        return value.indexOf(" ") < 0 && value != "";
    }, "No space");

    $('#name-search-form').validate({
        rules: {
            reason: {
                required: true,
            },
            type: {
                required: true,
            },
            designation: {
                required: true,
            },
            office: {
                required: true,
            },
            justification: {
                required: true,
                noSpace:true
            },
            name1: {
                required: true,
                minlength: 4,
                noSpace:true
            },
            name2: {
                required: true,
                minlength: 4,
                noSpace:true
            },
            name3: {
                minlength: 4,
                noSpace:true
            },
            name4: {
                minlength: 4,
                noSpace:true
            },
            name5: {
                minlength: 4,
                noSpace:true
            },
        },
        messages: {
            reason: {
                required: 'This information is required',
            },
            type: {
                required: 'This information is required'
            },
            designation: {
                required: 'This information is required'
            },
            office: {
                required: 'This information is required'
            },
            justification: {
                required: 'This information is required',
                noSpace:'Spaces are not allowed'
            },
            name1: {
                required: 'This information is required',
                minlength: 'Must be at least Four characters',
                noSpace:'Spaces are not allowed'
            },
            name2: {
                required: 'This information is required',
                minlength: 'Must be at least Four characters',
                noSpace:'Spaces are not allowed'
            },
            name3: {
                minlength: 'Must be at least Four characters',
            },
            name4: {
                minlength: 'Must be at least Four characters',
            },
            name5: {
                minlength: 'Must be at least Four characters',
            },
        },
        submitHandler:function(form){           
            if(readyForSubmission){
                $.ajax({
                    type: 'Post',
                    url: '/name-search/submission',
                    data: $(form).serialize(),
                    success: function (data) {
                        toastr.success('Your application has been submitted');
                        $('#name-search-form').trigger('reset');
                    },
                    error: function (err) {
                        toastr.error('Something went wrong in submitting the application');
                    },
                });
            }else {
                $('#reason').text($("[name='reason'] option:selected").text());
                $('#type').text($("[name='type'] option:selected").text());
                $('#designation').text($("[name='designation'] option:selected").text());
                $('#sorting').text($("[name='office'] option:selected").text());
                $('#justification').text($("[name='justification']").val());
                $('#name1').text($("[name='name1']").val());
                $('#name2').text($("[name='name2']").val());
                $('#name3').text($("[name='name3']").val());
                $('#name4').text($("[name='name4']").val());
                $('#name5').text($("[name='name5']").val());
                $('#modal-name-search-confirm').modal('show');
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

    $().validate()
});