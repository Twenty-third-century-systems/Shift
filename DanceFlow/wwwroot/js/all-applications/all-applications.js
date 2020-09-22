function displayNameSearchApplications(data, refresh) {

    // Updating badges
    $('#nameSearchCount').text(data.nameSearchApplications.length);
    $('#pvtEntityCount').text(data.pvtEntityApplications.length);

    $('#applicationsContainer').empty(); // The actual applications container

    if (refresh !== undefined) {
        $('#service').val("name search");
        data.nameSearchApplications.forEach(function (e) {
            let applicationElement =
                '<div class="media text-muted pt-3 system-control" id="' + e.id + '">\n' +
                '                            <svg class="bd-placeholder-img mr-2 rounded" width="32" height="32" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: 32x32">\n' +
                '                                <title>Placeholder</title><rect width="100%" height="100%" fill="#6f42c1"/><text x="50%" y="50%" fill="#6f42c1" dy=".3em">32x32</text>\n' +
                '                            </svg>\n' +
                '                            <p class="media-body pb-3 mb-0 small lh-125 border-bottom border-gray">\n' +
                '                                <strong class="d-block text-gray-dark">' + e.service + '<span class="float-right">' + e.submissionDate + '</span></strong>\n' +
                '                                <i>' + e.user + '</i>\n' +
                '                            </p>\n' +
                '                        </div>';
            $('#applicationsContainer').append(applicationElement);
            $('#' + e.id + '').css('cursor', 'pointer').click(function () {
                alert('Element clickable')
            });
        });
    } else {
        // Setting up click listeners
        $('#nameSearchSelector').click(function () {
            if (data.nameSearchApplications.length > 0) {
                $('#allocateTasks').show();   // The form
                $('#onDisplay').text('Name search Application');  // Application container header text    
                $('#service').val("name search");
                $('#applicationsContainer').empty();
                data.nameSearchApplications.forEach(function (e) {
                    let applicationElement =
                        '<div class="media text-muted pt-3 system-control" id="' + e.id + '">\n' +
                        '                            <svg class="bd-placeholder-img mr-2 rounded" width="32" height="32" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: 32x32">\n' +
                        '                                <title>Placeholder</title><rect width="100%" height="100%" fill="#6f42c1"/><text x="50%" y="50%" fill="#6f42c1" dy=".3em">32x32</text>\n' +
                        '                            </svg>\n' +
                        '                            <p class="media-body pb-3 mb-0 small lh-125 border-bottom border-gray">\n' +
                        '                                <strong class="d-block text-gray-dark">' + e.service + '<span class="float-right">' + e.submissionDate + '</span></strong>\n' +
                        '                                <i>' + e.user + '</i>\n' +
                        '                            </p>\n' +
                        '                        </div>';
                    $('#applicationsContainer').append(applicationElement);
                    $('#' + e.id + '').css('cursor', 'pointer').click(function () {
                        alert('Element clickable')
                    });
                });
                $('.col-md-6 .card').show();
            } else {
                $('#onDisplay').text('Name search Application');
                $('#applicationsContainer').empty().append('<p>Nothing to show</p>');
                $('#service').val("");
                $('#allocateTasks').hide();
            }
        });
    }
}

function refreshNameSearchApplicationDisplay(refresh) {
    $.ajax({
        type: 'Get',
        url: '/applications/all',
        success: function (data) {
            displayNameSearchApplications(data, refresh);
        }
    });
}

$(document).ready(function () {
    //Care taking
    $('.nav-item').css('cursor', 'pointer');

    $('#applicationsContainer').empty();
    $('.col-md-6 .card').hide();
    $('#allocateTasks').hide();

    $.ajax({
        type: 'Get',
        url: '/applications/all',
        success: function (data) {
            displayNameSearchApplications(data);

            $('#pvtEntitySelector').click(function () {
                if (data.pvtEntityApplications.length > 0) {
                    $('#allocateTasks').show();
                    $('#onDisplay').text('Pvt Entity applications');
                    $('#applicationsContainer').empty();
                    data.pvtEntityApplications.forEach(function (e) {
                        let applicationElement =
                            '<div class="media text-muted pt-3 system-control" id="' + e.id + '">\n' +
                            '                            <svg class="bd-placeholder-img mr-2 rounded" width="32" height="32" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: 32x32">\n' +
                            '                                <title>Placeholder</title><rect width="100%" height="100%" fill="#6f42c1"/><text x="50%" y="50%" fill="#6f42c1" dy=".3em">32x32</text>\n' +
                            '                            </svg>\n' +
                            '                            <p class="media-body pb-3 mb-0 small lh-125 border-bottom border-gray">\n' +
                            '                                <strong class="d-block text-gray-dark">' + e.service + '<span class="float-right">' + e.submissionDate + '</span></strong>\n' +
                            '                                <i>' + e.user + '</i>\n' +
                            '                            </p>\n' +
                            '                        </div>';
                        $('#applicationsContainer').append(applicationElement);
                        $('#' + e.id + '').css('cursor', 'pointer').click(function () {
                            alert('Element clickable')
                        });
                        $('#service').val("Pvt Entity applications")
                    });
                    $('.col-md-6 .card').show();

                } else {
                    $('#onDisplay').text('Pvt limited entity entity');
                    $('#applicationsContainer').empty().append('<p>Nothing to show</p>');
                    $('#service').val("");
                    $('#allocateTasks').hide();
                }
            });
            console.log(data);
        },
        error: function (err) {
            toastr.error('Something went wrong loading request');
        },
    });
});