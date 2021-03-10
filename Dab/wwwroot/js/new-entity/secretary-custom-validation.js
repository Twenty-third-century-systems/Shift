$.validator.addMethod("minAge", function(value, element, min) {
    let today = new Date();
    let birthDate = new Date(value);
    let age = today.getFullYear() - birthDate.getFullYear();

    if (age > min+1) {
        return true;
    }

    let m = today.getMonth() - birthDate.getMonth();

    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }

    return age >= min;
}, "You are not old enough!");

$(document).ready(function () {
    $('#secretaryForm').validate({
        rules: {
            countryCode: {
                required: true,
            },
            surname: {
                required: true,
            },
            names: {
                required: true,
            },
            gender: {
                required: true,
            },
            dateOfBirth: {
                required: true,
                minAge: 18
            },
            nationalIdentification: {
                required: true,
            },
            physicalAddress: {
                required: true,
            },
            mobileNumber: {
                required: true,
            },
            emailAddress: {
                required: true,
            },
            dateOfAppointment: {
                required: true,
            },
        },
        messages: {
            countryCode: {
                required: 'This information is required.',
            },
            surname: {
                required: 'This information is required.',
            },
            names: {
                required: 'This information is required.',
            },
            gender: {
                required: 'This information is required.',
            },
            dateOfBirth: {
                required: 'This information is required.',
                minAge: "Must be at least 18 years old!"
            },
            nationalIdentification: {
                required: 'This information is required.',
            },
            physicalAddress: {
                required: 'This information is required.',
            },
            mobileNumber: {
                required: 'This information is required.',
            },
            emailAddress: {
                required: 'This information is required.',
            },
            dateOfAppointment: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            $.ajax({
                type: 'Post',
                url: '/entity/secretary',
                data: {
                    applicationId: $('#applicationId').val(),
                    secretary: $(form).serializeToJSON()
                },
                success: function () {
                    toastr.success("Secretary saved.");
                },
                error: function (err) {
                    toastr.error("Something went wrong in saving secretary details. Refresh page and resubmit.")
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
})