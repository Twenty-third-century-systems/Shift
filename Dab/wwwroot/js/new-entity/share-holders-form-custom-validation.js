$.validator.addMethod("minAge", function (value, element, min) {
    let today = new Date();
    let birthDate = new Date(value);
    let age = today.getFullYear() - birthDate.getFullYear();

    if (age > min + 1) {
        return true;
    }

    let m = today.getMonth() - birthDate.getMonth();

    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }

    return age >= min;
}, "You are not old enough!");

$(document).ready(function () {
    $('#shareHoldersForm').validate({
        rules: {
            nomineeCountryCode: {
                required: true,
            },
            nomineeSurname: {
                required: true,
            },
            nomineeNames: {
                required: true,
            },
            nomineeGender: {
                required: true,
            },
            nomineeDateOfBirth: {
                required: true,
                minAge: 18
            },
            nomineeNationalIdentification: {
                required: true,
            },
            nomineePhysicalAddress: {
                required: true,
            },
            nomineeMobileNumber: {
                required: true,
            },
            nomineeEmailAddress: {
                required: true,
                email: true,
            },
            nomineeDateOfAppointment: {
                required: true,
            },
            nomineeOccupation:{
                required: true,
            },


            shareClass: {
                required: true,
            },
            amountSubscribed: {
                required: true,
                digits: true
            },
        },
        messages: {
            nomineeCountryCode: {
                required: "This information is required.",
            },
            nomineeSurname: {
                required: "This information is required.",
            },
            nomineeNames: {
                required: "This information is required.",
            },
            nomineeGender: {
                required: "This information is required.",
            },
            nomineeDateOfBirth: {
                required: "This information is required.",
                minAge: "Nominee or share holder must be 18 years old or above. Those under 18 should nominate a representative."
            },
            nomineeNationalIdentification: {
                required: "This information is required.",
            },
            nomineePhysicalAddress: {
                required: "This information is required.",
            },
            nomineeMobileNumber: {
                required: "This information is required.",
            },
            nomineeEmailAddress: {
                required: "This information is required.",
                email: "This is not a valid email.",
            },
            nomineeDateOfAppointment: {
                required: "This information is required.",
            },
            nomineeOccupation: {
                required: "This information is required.",
            },


            shareClass: {
                required: "This information is required.",
            },
            amountSubscribed: {
                required: "This information is required.",
                digits: "Only digits are allowed.",
            },
        },
        submitHandler: function (form) {
            let input = $(form).serializeToJSON();

            console.log(shareHolders);
            console.log(shares);

            if (input.isDirector == null) {
                input.isDirector = false;
            }
            if (input.isSecretary == null) {
                input.isSecretary = false;
            }
            if (input.isMember == null) {
                input.isMember = false;
            }

            //Handle changes number of shares and those allocated before populating the database

            // if (shares - (input.ordShares + input.prefShares) >= 0) {
            shareHolders.push(input);
            tblShareholders.row.add([
                input.nomineeCountryCode,
                input.nomineeSurname,
                input.nomineeNames,
                input.nomineeGender,
                input.nomineeDateOfBirth,
                input.nomineeNationalIdentification,
                input.nomineePhysicalAddress,
                input.nomineeMobileNumber,
                input.nomineeEmailAddress,
                input.nomineeDateOfAppointment,
                input.nomineeOccupation,
            ]).draw(false);
            $('#shareHoldersForm').trigger('reset');
            // } else {
            //     toastr.error('The total number of allocated shares ' +
            //         'is now greater than the number of ' +
            //         'shares specified in the share capital. ' +
            //         '' + shares + ' are left for allocation');
            // }

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