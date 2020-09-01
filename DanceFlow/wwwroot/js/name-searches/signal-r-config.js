let connection = undefined;

$(document).ready(function () {
    connection = new signalR.HubConnectionBuilder().withUrl("/name/ex").build();

    connection
        .start()
        .then(function () {
            toastr.success("Connection established");
        })
        .catch(function (err) {
            toastr.error("Connection failed");
        });

    connection.on("ReceiveExaminationUpdate", function (name) {
        tasksApplications[a].nameSearch.names[i] = name
        reUpdateStatus();
    });
    
    connection.on("ReceiveApplicationUpdate", function (applicationId) {
        toastr.success("Application has been examined");
        if(tasksApplications[a].application.id === applicationId){
            tasksApplications[a].application.examined = true;

            $('#pending').empty();
            $('#completed').empty();
            displayPage();
        }
    });
});

function SendNameForExamination() {
    connection
        .invoke("UpdateName", nameOnExamination)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function finishExamination(){
    connection
        .invoke("Finish", tasksApplications[a].application.id)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function reUpdateStatus() {
    let container = $('#name' + (i + 1) + '-group');
    container.find('span').remove();
    if (nameOnExamination.status === "blacklisted") {
        $('<span class="text-dark"><small>Blacklisted</small></span>').appendTo(container);
    } else if (nameOnExamination.status === "rejected") {
        $('<span class="text-danger"><small>Rejected</small></span>').appendTo(container);
    } else if (nameOnExamination.status === "not considered") {
        $('<span class="text-warning"><small>Not considered</small></span>').appendTo(container);
    } else if (nameOnExamination.status !== "pending") {
        $('<span class="text-success"><small>Reserved</small></span>').appendTo(container);
    }
}
