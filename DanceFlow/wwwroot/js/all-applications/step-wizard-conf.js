function markApplicationAsExamined(applicationId) {
    finishExamination(applicationId)
}

// Toolbar extra buttons
var btnDone = $('<button id="btn-done" ></button>').text('Done')
    .addClass('btn btn-info')
    .on('click', function () {
        let applicationId = $('#applicationId').val();
        if (applicationId > 0)
            $.ajax({
                type: 'Post',
                url: '/applications/approval/' + applicationId + '/approve',
                success: function (data) {
                    console.log(data);
                    applicationsPendingApproval = data;
                    displayPage();
                },
                error: function () {
                    toastr.error("Could not approve application. Please try again.")
                },
            });
        else
            toastr.error("No application is selected");
    });


// Step show event
$("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
    if (stepPosition === 'first') {
        btnDone.addClass('disabled');
    } else if (stepPosition === 'last') {
        btnDone.removeClass('disabled');
    } else {
        btnDone.addClass('disabled');
    }
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
        toolbarPosition: 'bottom', // both bottom
        toolbarButtonPosition: 'center', // left, right, center
        toolbarExtraButtons: [btnDone]
    },
    keyboardSettings: {
        keyNavigation: false
    }
});