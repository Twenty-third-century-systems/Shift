
$(document).ready(function () {
    // $.validator.setDefaults({
    //     submitHandler: function () {
    //         alert( "Form successful submitted!" );
    //     }
    // });

    $('#officeForm').validate({
        rules: {
            physicalAddress: {
                required: true,
                minlength: 5,
            },
            postalAddress: {
                required: true,
                minlength: 5
            },
            officeCity: {
                required: true,
                minlength: 5
            },
            telNumber: {
                required: true,
                minlength: 5
            },
            mobileNumber: {
                required: true,
                minlength: 5
            },
            emailAddress: {
                required: true,
                email: true
            },
        },
        messages: {
            physicalAddress: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            postalAddress: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            country: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            city: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            telNumber: {
                required: 'This information is required',
                // tel:'This is not a valid telephone number',
                minlength: 'Must be at least Five characters'
            },
            mobileNumber: {
                required: 'This information is required',
                // tel:'This is not a valid mobile number',
                minlength: 'Must be at least Five characters'
            },
            emailAddress: {
                required: 'This information is required',
                email: 'This is not a valid email'
            },
        },
        submitHandler: function(form){
            alert("Office");
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
    
    $('#clausesForm').validate({
        rules: {
            liabilityClause: {
                required: true,
                minlength: 5,
            },
            shareClause: {
                required: true,
                minlength: 5
            },
        },
        messages: {
            liabilityClause: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            shareClause: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
        },
        submitHandler: function(form){
            alert("Clauses");
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
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
        },
        submitHandler: function(form){
            alert("Objective");
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
                required: 'This information is required',
            },
        },
        submitHandler: function(form){
            alert("Articles");
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
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
        },
        submitHandler: function(form){
            alert("Articles 2");
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

    $('#peopleForm').validate({
        rules: {
            peopleCountry: {
                required: true,
                minlength: 5,
            },
            nationalId: {
                required: true,
                minlength: 5,
            },
            memberSurname: {
                required: true,
                minlength: 5,
            },
            memberName: {
                required: true,
                minlength: 5,
            },
            gender: {
                required: true,
                minlength: 5,
            },
            phyAddress: {
                required: true,
                minlength: 5,
            },
            ordShares: {
                required: true,
                minlength: 5,
            },
            prefShares: {
                required: true,
                minlength: 5,
            },
            totShares: {
                required: true,
                minlength: 5,
            },
        },
        messages: {
            peopleCountry: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            nationalid: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            memberSurname: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            memberName: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            gender: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            phyAddress: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            isSecretary: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            isMember: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            isDirector: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            ordShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            prefShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
            totShares: {
                required: 'This information is required',
                minlength: 'Must be at least Five characters'
            },
        },
        submitHandler: function(form){
            alert("People");
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
        submitHandler: function(form){
            alert("Entity form");
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