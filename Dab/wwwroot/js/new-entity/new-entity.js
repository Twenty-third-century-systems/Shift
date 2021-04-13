let officeSaved = false;
let memorandumSaved = false;
let articlesSaved = false;
let subscribersSaved = false;
let memoId = undefined;

let directors = []
let objects = [];
let shareClauses = [];
let amendedArticles = [];
let shareHolders = [];
let memberEntities = [];
let shares = 0;


// $('#amendedArticlesDisplay').hide();

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

    // $('#shares').on("input", function () {
    //     shares = $('#shares').val();
    // });
    $('#submitDirectors').click(function () {
        if (directors.length > 1) {
            $.ajax({
                type: 'Post',
                url: '/entity/directors',
                data: {
                    applicationId: $('#applicationId').val(),
                    directors: directors
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
            toastr.warning("A minimum of two directors is required.")
        }
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
                    toastr.success("Objects saved successfully.");
                },
                error: function () {
                    toastr.error("Something went wrong in Objectives. Refresh page and try again.");
                },
            });
        } else {
            toastr.error("You haven't added objects yet.")
        }
    });

    $('#submitShareClauses').click(function () {
        if (shareClauses.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/share/clauses',
                data: {
                    applicationId: $('#applicationId').val(),
                    clauses: shareClauses
                },
                success: function (data) {
                    memorandumSaved = true;
                    toastr.success("Share clauses saved successfully.");
                    console.log(shareClauses)
                    shareClauses.forEach(shareClause => $('#shareClass').append('<option value=' + shareClause.title + '>' + shareClause.title + '</option>'));
                },
                error: function () {
                    toastr.error("Something went wrong in share clauses. Refresh page and try again.");
                },
            });
        } else {
            toastr.error("You haven't added objects yet.")
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
        if (shareHolders.length > 0) {
            $.ajax({
                type: 'Post',
                url: '/entity/shareHolders',
                data: {
                    applicationId: $('#applicationId').val(),
                    ShareHolders: shareHolders
                },
                success: function (data) {
                    toastr.success("Share holders saved.");
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
});

