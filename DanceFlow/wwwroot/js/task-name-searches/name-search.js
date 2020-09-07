﻿let tasksApplications, nameOnExamination, i, a = undefined;

function displayPage(refresh) {
    $(':input[type="text"]').val('');
    $('#name-section').find('span').remove();
    tasksApplications.forEach((e, index) => {
        let applicationSelect =
            '<li class="nav-item">\n' +
            '                    <a class="nav-link" href="#">\n' +
            '                        <i class="far fa-folder"></i> Application - <small>' + e.application.user + '</small>\n' +
            '                    </a>\n' +
            '                </li>';

        if (!e.application.examined) {
            if (refresh === undefined)
                $(applicationSelect)
                    .css('cursor', 'pointer')
                    .click(function () {
                        a = index;
                        console.log(a)
                        $('#reason').val(e.nameSearch.reasonForSearch.toUpperCase());
                        $('#type').val(e.nameSearch.typeOfEntity.toUpperCase());
                        $('#designation').val(e.nameSearch.designation.toUpperCase());
                        $('#justification').val(e.nameSearch.justification.toUpperCase());

                        e.nameSearch.names.forEach((x, index) => {
                            $('#name' + (index + 1)).val(x.value.toUpperCase());
                            $('#name' + (index + 1) + '-toggle').click(function () {
                                setupNameExaminationDialogue(x.value.toUpperCase());
                                nameOnExamination = x;
                                i = index;
                            });
                        });
                        updateStatuses(e.nameSearch.names);
                    })
                    .appendTo('#pending');
            else
                updateStatuses(e.nameSearch.names);
        } else {
            $('#completed').append(applicationSelect);
        }
    });
}

function setupNameExaminationDialogue(name) {
    $('#name-on-display').text(name);
}

function updateStatuses(names) {
    let container;
    $('#name-section').find('span').remove();
    names.forEach((e, index) => {
        container = $('#name' + (index + 1) + '-group');
        if (e.status === "blacklisted") {
            $('<span class="text-dark"><small>Blacklisted</small></span>').appendTo(container);
        } else if (e.status === "rejected") {
            $('<span class="text-danger"><small>Rejected</small></span>').appendTo(container);
        } else if (e.status === "not considered") {
            $('<span class="text-warning"><small>Not considered</small></span>').appendTo(container);
        } else if (e.status !== "pending") {
            $('<span class="text-success"><small>Reserved</small></span>').appendTo(container);
        }
    });
}

function setUpModalButtons() {
    $('#btn-blacklist').click(function () {
        nameOnExamination.status = "blacklisted";
        console.log(nameOnExamination);
        console.log(i + ": index");
        SendNameForExamination();
    });
    $('#btn-reject').click(function () {
        nameOnExamination.status = "rejected";
        console.log(nameOnExamination);
        console.log(i + ": index");
        SendNameForExamination();
    });
    $('#btn-approve').click(function () {
        nameOnExamination.status = "reserved";
        console.log(nameOnExamination);
        console.log(i + ": index");
        SendNameForExamination();
    });
    $('#btn-done').click(function () {
        finishExamination();
    });
}

function getAndDisplayTaskData(refresh) {
    $.ajax({
        type: 'Get',
        url: +$('#task-id').val() + '/applications',
        success: function (data) {
            console.log(data);
            tasksApplications = data;
            displayPage(refresh);
            if (refresh === undefined)
                setUpModalButtons();
        },
        error: function (err) {
            toastr.error('Something went wrong loading request');
        },
    });
}

$(document).ready(function () {
    $('#pending').empty();
    $('#completed').empty();
    getAndDisplayTaskData();
});