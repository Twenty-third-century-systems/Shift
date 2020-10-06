let connection = undefined;

function finishExamination(applicationId) {
    connection
        .invoke("Finish", parseInt(applicationId))
        .catch(function (err) {
            alert("Failed to invoke finish");
            return console.error(err.toString());
        });
}

$(document).ready(function () {
    connection = new signalR.HubConnectionBuilder().withUrl("/pvt/ex").build();

    connection
        .start()
        .then(function () {
            toastr.success("Connection established");
        })
        .catch(function (err) {
            toastr.error("Something wen't wrong. You wont be able to examine.");
        });

    connection
        .on("ReceivePvtExaminationUpdate", function (name) {
            $('#pending').empty();
            $('#completed').empty();
            getAndDisplayData();
        });
});