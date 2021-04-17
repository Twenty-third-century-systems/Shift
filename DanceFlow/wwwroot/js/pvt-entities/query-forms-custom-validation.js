$(document).ready(function () {
    $('#industrySectorQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#industrySectorQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#officeQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#officeQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#directorsQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#directorsQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#secretaryQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#secretaryQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#objectiveQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#objectiveQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#liabilityQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#liabilityQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#shareClauseQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#shareClauseQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#membersQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#membersQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#articlesQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#articlesQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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

    $('#amendedArticlesQuery').validate({
        rules: {
            comment: {
                required: true,
            },
        },
        messages: {
            comment: {
                required: 'This information in required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/tasks/examination/query/pvt-entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    query: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Query was submitted successfully")
                    $('#amendedArticlesQuery').trigger('reset');
                },
                error: function () {
                    toastr.error("Query was submition failed")
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