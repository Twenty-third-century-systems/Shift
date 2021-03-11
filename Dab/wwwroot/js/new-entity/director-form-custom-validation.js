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
    $('#directorForm').validate({
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
                email: true,
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
                email: 'This is not a valid email.'
            },
            dateOfAppointment: {
                required: 'This information is required.',
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();
            directors.push(input);
            tblDirectors.row.add([
                input.countryCode,
                input.surname,
                input.names,
                input.gender,
                input.dateOfBirth,
                input.nationalIdentification,
                input.physicalAddress,
                input.mobileNumber,
                input.emailAddress,
                input.dateOfAppointment
            ]).draw(false);
            $('#directorForm').trigger('reset');
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