let connection = undefined;

function SendNameForExamination() {
    connection
        .invoke("UpdateName", nameOnExamination)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function finishExamination() {
    connection
        .invoke("Finish", tasksApplications[a].nameSearch.nameSearchId)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

$(document).ready(function () {
    connection = new signalR.HubConnectionBuilder().withUrl("/name/ex").build();

    connection
        .start()
        .then(function () {
            // toastr.success("Connection established");
        })
        .catch(function (err) {
            toastr.error("Something wen't wrong. You wont be able to examine.");
        });

    connection
        .on("ReceiveExaminationUpdate", function (name) {
            tasksApplications[a].nameSearch.names[i] = name
            getAndDisplayTaskData(1);
        });

    connection
        .on("ReceiveApplicationUpdate", function (nameSearchId) {
            console.log(nameSearchId);
            toastr.success("Application has been examined");
            if (tasksApplications[a].nameSearch.nameSearchId === nameSearchId) {
                tasksApplications[a].examined = true;

                $('#pending').empty();
                $('#completed').empty();
                displayPage();
            }
        });
});