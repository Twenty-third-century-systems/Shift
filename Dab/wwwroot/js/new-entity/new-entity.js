let officeSaved = false;
let memorandumSaved = false;
let articlesSaved = false;
let subscribersSaved = false;
let memoId = undefined;

let objects = [];
let amendedArticles = [];
let memberPeople = [];
let memberEntities = [];


$('#amendedArticlesDisplay').hide();

$(document).ready(function () {

    function calculateTotalShares() {
        let ordShares = 0;
        let prefShares = 0;

        if ($('#ordShares').valid()) {
            if ($('#ordShares').val() !== '')
                ordShares = parseInt($('#ordShares').val());
        }

        if ($('#prefShares').valid()) {
            if ($('#prefShares').val() !== '')
                prefShares = parseInt($('#prefShares').val());
        }

        $('#totShares').val(ordShares + prefShares);
    }


    $("#ordShares").on("input", function () {
        calculateTotalShares();
    });

    $("#prefShares").on("input", function () {
        calculateTotalShares();
    });

    $('#submitObjects').click(function () {
        if (objects.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/objects',
                data: {
                    applicationId: $('#applicationId').val(),
                    pvtEntityId: $('#pvtEntityId').val(),
                    objects: objects
                },
                success: function (data) {
                    memorandumSaved = true;
                    toastr.success("Memorandum saved.");
                },
                error: function () {
                    toastr.error("Something went wrong in saving office details. Refresh page and resubmit.");
                },
            });
        } else {
            toastr.warning("You haven't added objects yet.")
        }
    });

    $('#submitAmendedArticles').click(function () {
        if (amendedArticles.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/amends',
                data: {
                    applicationId: $('#applicationId').val(),
                    pvtEntityId: $('#pvtEntityId').val(),
                    amendedArticles: amendedArticles
                },
                success: function (data) {
                    articlesSaved = true;
                    toastr.success("Amended articles saved saved.");
                },
                error: function () {
                    toastr.error("Something went wrong in saving articles. Refresh page and resubmit.");
                },
            });
        } else {
            toastr.warning("You haven't added any amended article yet.")
        }
    });

    $('#submitShareHolders').click(function () {
        if (memberPeople.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/people',
                data: {
                    applicationId: $('#applicationId').val(),
                    pvtEntityId: $('#pvtEntityId').val(),
                    people: memberPeople
                },
                success: function (data) {
                    toastr.success("Members saved.");
                },
                error: function () {
                    toastr.error("Something went wrong in saving articles. Refresh page and resubmit.");
                },
            });
        } else {
            toastr.warning("You haven't added any amended article yet.")
        }
    });

    $('#submitShareholdingEntities').click(function () {
        if (memberEntities.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/entity',
                data: {
                    applicationId: $('#applicationId').val(),
                    pvtEntityId: $('#pvtEntityId').val(),
                    Entities: memberEntities
                },
                success: function (data) {
                    toastr.success("Member entities saved.");
                },
                error: function () {
                    toastr.error("Something went wrong in saving articles. Refresh page and resubmit.");
                },
            });
        } else {
            toastr.warning("You haven't added any amended article yet.")
        }
    });

    $('#tableOfArticles').change(function () {
        if ($('#tableOfArticles').val() === 'other') {
            $('#amendedArticlesDisplay').show();
        } else {
            $('#amendedArticlesDisplay').hide();
        }
    });
})
;

