let tasksApplications, OnExamination, i, a = undefined;
let ind = -1;

function displayPage(refresh) {
    $(':input[type="text"]').val('');
    $('#name-section').find('span').remove();
    tasksApplications.forEach((e, index) => {
        let applicationSelect =
            '<li class="nav-item">\n' +
            '                    <a class="nav-link" href="#">\n' +
            '                        <i class="far fa-folder"></i> Application \n' +
            '                    </a>\n' +
            '                </li>';

        console.log(refresh);
        if (!e.examined) {
            if (refresh === undefined)
                $(applicationSelect)
                    .css('cursor', 'pointer')
                    .click(function () {
                        a = index;
                        console.log(a)
                        $('#reason').val(e.nameSearch.reasonForSearch.toUpperCase());
                        $('#type').val(e.nameSearch.service.toUpperCase());
                        $('#designation').val(e.nameSearch.designation.toUpperCase());
                        $('#justification').val(e.nameSearch.justification.toUpperCase());
                        $('#object').val(e.nameSearch.mainObject.toUpperCase());
                        
                        e.nameSearch.names.forEach((x, index) => {
                            $('#name' + (index + 1)).val(x.value.toUpperCase());
                            $('#name' + (index + 1) + '-toggle').click(function () {
                                setupNameExaminationDialogue(x.value.toUpperCase());
                                nameOnExamination = x;
                                nameUnderExamination = x.value;
                                // tblNamesThatContain.clear().draw();
                                // tblNamesThatStartWith.clear().draw();
                                // tblNamesThatContain.ajax.reload();
                                // tblNamesThatStartWith.ajax.reload();
                                initializeTables(x.id);
                                i = index;
                            });
                        });
                        updateStatuses(e.nameSearch.names);
                    })
                    .appendTo('#pending');
            else {
                let application = tasksApplications[a];
                $('#reason').val(application.nameSearch.reasonForSearch.toUpperCase());
                $('#type').val(application.nameSearch.service.toUpperCase());
                $('#designation').val(application.nameSearch.designation.toUpperCase());
                $('#justification').val(application.nameSearch.justification.toUpperCase());
                $('#object').val(application.nameSearch.mainObject.toUpperCase());

                application.nameSearch.names.forEach((x, index) => {
                    $('#name' + (index + 1)).val(x.value.toUpperCase());
                    $('#name' + (index + 1) + '-toggle').click(function () {
                        setupNameExaminationDialogue(x.value.toUpperCase());
                        nameOnExamination = x;
                        i = index;
                    });
                });
                updateStatuses(application.nameSearch.names);
            }
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
        if (e.status === "Blacklisted") {
            $('<span class="text-dark"><small>Blacklisted</small></span>').appendTo(container);
        } else if (e.status === "Rejected") {
            $('<span class="text-danger"><small>Rejected</small></span>').appendTo(container);
        } else if (e.status === "NotConsidered") {
            $('<span class="text-warning"><small>Not considered</small></span>').appendTo(container);
        } else if (e.status !== "Pending") {
            $('<span class="text-success"><small>Reserved</small></span>').appendTo(container);
        }
    });
}

function setUpModalButtons() {
    $('#btn-blacklist').click(function () {
        nameOnExamination.status = 4;
        console.log(nameOnExamination);
        console.log(i + ": index");
        SendNameForExamination();
    });
    $('#btn-reject').click(function () {
        nameOnExamination.status = 3;
        console.log(nameOnExamination);
        console.log(i + ": index");
        SendNameForExamination();
    });
    $('#btn-approve').click(function () {
        nameOnExamination.status = 1;
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