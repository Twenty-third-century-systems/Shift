$(document).ready(function () {

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[A-Za-z ]+$/i.test(value);
    }, "Letters only please");

    $('#officeForm').validate({
        rules: {
            effectiveFrom: {
                required: true,
            },
            industrySector: {
                required: true,
                minlength: 3,
            },
            physicalAddress: {
                required: true,
                minlength: 3,
            },
            postalAddress: {
                required: true,
                minlength: 3,
            },
            cityTown: {
                required: true,
            },
            emailAddress: {
                required: true,
                email: true,
            },
            telephoneNumber: {
                required: true,
            },
            mobileNumber: {
                required: true,
            },
        },
        messages: {
            effectiveFrom: {
                required: 'This information is required.',
            },
            industrySector: {
                required: 'This information is required.',
                minlength: 'Must be at least Three characters.'
            },
            physicalAddress: {
                required: 'This information is required.',
                minlength: 'Must be at least Three characters.'
            },
            postalAddress: {
                required: 'This information is required.',
                minlength: 'Must be at least Three characters.'
            },
            cityTown: {
                required: 'This information is required.',
            },
            emailAddress: {
                required: 'This information is required.',
                email: 'This is not a valid email.'
            },
            telephoneNumber: {
                required: 'This information is required.',
            },
            mobileNumber: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/entity/office',
                data: {
                    applicationId: $('#applicationId').val(),
                    addressInformation: $(form).serializeToJSON()
                },
                success: function () {
                    officeSaved = true;
                    toastr.success("Office details saved.");
                },
                error: function (err) {
                    toastr.error("Something went wrong in saving office details. Refresh page and resubmit.")
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
            // $(element).addClass('is-valid');
        }
    });

    $('#liabilityClauseForm').validate({
        rules: {
            liabilityClause: {
                required: true,
                minlength: 5,
                lettersonly: true,
            },
        },
        messages: {
            liabilityClause: {
                required: 'This information is required.',
                minlength: 'Must be at least Five characters.',
                lettersonly: 'Only letters of the alphabet required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/entity/liability/clause',
                data: {
                    applicationId: $('#applicationId').val(),
                    clause: $(form).serializeToJSON()
                },
                success: function (data) {
                    memoId = data;
                    toastr.success("Share and liability clause saved.");
                },
                error: function (err) {
                    toastr.error("Something went wrong in saving office details. Refresh page and resubmit.");
                }
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

    $('#objectiveForm').validate({
        rules: {
            objective: {
                required: true,
                minlength: 5,
            },
        },
        messages: {
            objective: {
                required: 'This information is required.',
                minlength: 'Must be at least Five characters.'
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();
            objects.push(input);
            console.log(objects);
            console.log(input);
            tblObjects.row.add([
                input.objective
            ]).draw(false);
            $('#objectiveForm').trigger('reset');
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

    $('#articlesForm').validate({
        rules: {
            tableOfArticles: {
                required: true,
            },
        },
        messages: {
            tableOfArticles: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/entity/table',
                data: {
                    applicationId: $('#applicationId').val(),
                    tableOfArticles: $(form).serializeToJSON()
                },
                success: function (data) {
                    toastr.success("Table of articles saved.");
                },
                error: function (err) {
                    toastr.error("Something went wrong in saving the table of articles. Refresh page and resubmit.");
                }
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

    $('#articleModalForm').validate({
        rules: {
            article: {
                required: true,
                minlength: 5,
            },
        },
        messages: {
            article: {
                required: 'This information is required.',
                minlength: 'Must be at least Five characters.'
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();
            amendedArticles.push(input);
            console.log(amendedArticles);
            tblArticles.row.add([
                input.article
            ]).draw(false);
            $('#articleModalForm').trigger('reset');
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

    $('#entityForm').validate({
        rules: {
            entityCountry: {
                required: true,
                minlength: 5,
            },
            entityRef: {
                required: true,
                minlength: 5,
            },
            entityName: {
                required: true,
                minlength: 5,
            },
            entityOrdShares: {
                required: true,
                minlength: 5,
            },
            entityPrefShares: {
                required: true,
                minlength: 5,
            },
            entityTotShares: {
                required: true,
                minlength: 5,
            },
        },
        messages: {
            entityCountry: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            entityRef: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            entityName: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            entityOrdShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            entityPrefShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            entityTotShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();
            memberEntities.push(input);
            console.log(memberEntities);
            tblEntities.row.add([
                input.entityCountry,
                input.entityRef,
                input.entityName,
                input.entityOrdShares,
                input.entityPrefShares,
                input.entityTotShares,
            ]).draw(false);
            $('#entityForm').trigger('reset');
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