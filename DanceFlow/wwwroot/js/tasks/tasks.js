$(document).ready(function () {
    $('#name-search-tasks').hide();
    $('#pvt-application-tasks').hide();
    $.ajax({
        type: 'Get',
        url: '/tasks/pending',
        success: function (data) {
            console.log(data);
            if (data !== undefined) {

                if (data.nameSearchTasks.length > 0) {

                    $('#name-search-tasks').show();
                    let container = $('#name-search-item-container');
                    container.empty();
                    data.nameSearchTasks.forEach(function (e) {
                        let elementItem = '<div class="media text-muted pt-3" id="' + e.id + '">\n' +
                            '                            <svg class="bd-placeholder-img mr-2 rounded" width="32" height="32" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: 32x32">\n' +
                            '                                <title>Placeholder</title><rect width="100%" height="100%" fill="#6f42c1"/><text x="50%" y="50%" fill="#6f42c1" dy=".3em">32x32</text>\n' +
                            '                            </svg>\n' +
                            '                            <p class="media-body pb-3 mb-0 small lh-125 border-bottom border-gray">\n' +
                            '                                <strong class="d-block text-gray-dark">' + e.service + '<span class="float-right">' + e.expectedDateOfCompletion + '</span></strong>\n' +
                            '                                <i>' + e.applicationCount + ' application(s)</i>\n' +
                            '                            </p>\n' +
                            '                        </div>';
                        container.append(elementItem)
                        $('#' + e.id + '').css('cursor', 'pointer').click(function () {
                            window.location.href = '/tasks/name-search/' + e.id + '';
                        });
                        $('#name-search-tasks').show();
                    });

                }

                if (data.pvtEntityTasks.length > 0) {

                    $('#pvt-application-tasks').show();
                    let container = $('#pvt-ltd-item-container');
                    container.empty();
                    data.pvtEntityTasks.forEach(function (e) {
                        let elementItem = '<div class="media text-muted pt-3" id="' + e.id + '">\n' +
                            '                            <svg class="bd-placeholder-img mr-2 rounded" width="32" height="32" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: 32x32">\n' +
                            '                                <title>Placeholder</title><rect width="100%" height="100%" fill="#6f42c1"/><text x="50%" y="50%" fill="#6f42c1" dy=".3em">32x32</text>\n' +
                            '                            </svg>\n' +
                            '                            <p class="media-body pb-3 mb-0 small lh-125 border-bottom border-gray">\n' +
                            '                                <strong class="d-block text-gray-dark">' + e.service + '<span class="float-right">' + e.expectedDateOfCompletion + '</span></strong>\n' +
                            '                                <i>' + e.applicationCount + ' application(s)</i>\n' +
                            '                            </p>\n' +
                            '                        </div>';
                        container.append(elementItem)
                        $('#' + e.id + '').css('cursor', 'pointer').click(function () {
                            window.location.href = '/tasks/pvt-entity/' + e.id + '';
                        });
                        $('#pvt-application-tasks').show();
                    });
                }
            } else {
                $('#parent-container').append('<p>You have no pending tasks at the moment. <a href="/tasks/allocated">Refresh</a> this page after some time.</p>')
            }
        },
        error: function (err) {
            toastr.error('Something went wrong loading request');
        }
    });
});