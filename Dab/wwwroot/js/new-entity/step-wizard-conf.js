$(document).ready(function () {
    // Toolbar extra buttons
    var btnDone = $('<button></button>').text('Done')
        .addClass('btn btn-info')
        .on('click', function () {
            alert('Finish Clicked');
        });


    // Step show event
    $('#smartwizard').on('showStep', function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
        if (stepPosition === 'first') {
            btnDone.addClass('disabled');
        } else if (stepPosition === 'last') {
            btnDone.removeClass('disabled');
        } else {
            btnDone.addClass('disabled');
        }
    });
    
    //Step leave event
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepNumber, stepDirection, stepPosition) {        
        console.log(stepNumber);
        console.log(stepDirection);
        if (stepNumber === 0 && stepDirection-stepNumber === 1) {
            console.log(officeSaved);
            return officeSaved;
        }
        // else if (stepNumber === 1 && stepDirection === 'forward') {
        //     $('#submitOffice').trigger('click');
        //     return true;
        // } else if (stepNumber === 2 && stepDirection === 'forward') {
        //     submitMembers();
        //     return true;
        // } else if (stepNumber === 3 && stepDirection === 'forward') {
        //     submitMemo();
        //     return true;
        // }
    });


    // Smart Wizard Configuration
    $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows', // default, arrows, dots, progress
        autoAdjustHeight: false,
        transition: {
            animation: 'fade', // Effect on navigation, none/fade/slide-horizontal/slide-vertical/slide-swing
        },
        toolbarSettings: {
            toolbarPosition: 'top', // both bottom
            toolbarExtraButtons: [btnDone]
        },
        keyboardSettings: {
            keyNavigation: false
        }
    });
})