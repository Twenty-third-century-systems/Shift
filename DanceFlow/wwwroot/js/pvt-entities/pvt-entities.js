let taskApplications = undefined;
let index = undefined;


function displayPage() {
    taskApplications.forEach((e, index) => {
        let applicationSelect = populateApplicationSelect(e);
        
        // check if application was examined
        if (e.application.status !== "Examined") { 
            $(applicationSelect).css('cursor', 'pointer').click(function () {
                // Activate btn-done
                $('#btn-done').removeClass('disabled');

                // Reset step wizard
                $('#smartwizard').smartWizard("reset");

                // ApplicationId
                $('#applicationId').val(e.application.applicationId);

                // Industry 
                $('#industrySector').val(e.industrySector);

                // Office 
                $('#effectiveFrom').val(e.office.effectiveFrom);
                $('#physicalAddress').val(e.office.physicalAddress);
                $('#officeCity').val(e.office.cityTown);
                $('#postalAddress').val(e.office.postalAddress);
                $('#emailAddress').val(e.office.emailAddress);
                $('#telNumber').val(e.office.telephoneNumber);
                $('#mobileNumber').val(e.office.mobileNumber);

                // Directors
                if (e.directors.length > 0) {
                    e.directors.forEach((d, index) => {
                        directorsTable.row.add([
                            d.fullName,
                            d.country,
                            d.gender,
                            d.dateOfBirth,
                            d.nationalIdentification,
                            d.physicalAddress,
                            d.mobileNumber,
                            d.emailAddress,
                            d.dateOfAppointment,
                        ]).draw(false);
                    });
                }

                // Secretary
                $('#secretaryCountry').val(e.secretary.country);
                $('#secretaryFullName').val(e.secretary.fullName);
                $('#secretaryGender').val(e.secretary.gender);
                $('#secretaryDOB').val(e.secretary.dateOfBirth);
                $('#secretaryNationalId').val(e.secretary.nationalIdentification);
                $('#secretaryPhysicalAddress').val(e.secretary.physicalAddress);
                $('#secretaryMobile').val(e.secretary.mobileNumber);
                $('#secretaryEmail').val(e.secretary.emailAddress);
                $('#secretaryDOA').val(e.secretary.dateOfAppointment);

                // Objects table
                if (e.memorandumOfAssociation.memorandumObjects.length > 0) {
                    e.memorandumOfAssociation.memorandumObjects.forEach((o, index) => {
                        objectivesTable.row.add([
                            o.value
                        ]).draw(false);
                    });
                }

                // Liability
                $('#liabilityClause').val(e.memorandumOfAssociation.liabilityClause);

                // Share clause
                if (e.memorandumOfAssociation.shareClauses.length > 0) {
                    e.memorandumOfAssociation.shareClauses.forEach((s, index) => {
                        shareClauseTable.row.add([
                            s.value
                        ]).draw(false);
                    });
                }

                // Members
                if (e.members.length > 0) {
                    e.members.forEach((m, index) => {
                        memberTable.row.add([
                            m.fullName,
                            m.country,
                            m.gender,
                            m.dateOfBirth,
                            m.nationalIdentification,
                            m.physicalAddress,
                            m.mobileNumber,
                            m.emailAddress,
                            m.dateOfTakeUp,
                            m.occupation
                        ]).draw(false);
                    });
                }


                // Articles of association
                $('#tableOfArticles').val(e.articlesOfAssociation.tableOfArticles);

                // Amended articles
                if (e.articlesOfAssociation.amendedArticles > 0) {
                    e.articlesOfAssociation.amendedArticles.forEach((a, index) => {
                        articlesTable.row.add([
                            e.value
                        ]).draw(false);
                    });
                }
            }).appendTo('#pending');
        } else {
            $(applicationSelect).css('cursor', 'pointer').click(function () {
                // Activate btn-done
                $('#btn-done').removeClass('disabled');

                // Reset step wizard
                $('#smartwizard').smartWizard("reset");

                // ApplicationId
                $('#applicationId').val(e.application.applicationId);

                // Industry 
                $('#industrySector').val(e.industrySector);

                // Office 
                $('#effectiveFrom').val(e.office.effectiveFrom);
                $('#physicalAddress').val(e.office.physicalAddress);
                $('#officeCity').val(e.office.cityTown);
                $('#postalAddress').val(e.office.postalAddress);
                $('#emailAddress').val(e.office.emailAddress);
                $('#telNumber').val(e.office.telephoneNumber);
                $('#mobileNumber').val(e.office.mobileNumber);

                // Directors
                if (e.directors.length > 0) {
                    e.directors.forEach((d, index) => {
                        directorsTable.row.add([
                            d.fullName,
                            d.country,
                            d.gender,
                            d.dateOfBirth,
                            d.nationalIdentification,
                            d.physicalAddress,
                            d.mobileNumber,
                            d.emailAddress,
                            d.dateOfAppointment,
                        ]).draw(false);
                    });
                }

                // Secretary
                $('#secretaryCountry').val(e.secretary.country);
                $('#secretaryFullName').val(e.secretary.fullName);
                $('#secretaryGender').val(e.secretary.gender);
                $('#secretaryDOB').val(e.secretary.dateOfBirth);
                $('#secretaryNationalId').val(e.secretary.nationalIdentification);
                $('#secretaryPhysicalAddress').val(e.secretary.physicalAddress);
                $('#secretaryMobile').val(e.secretary.mobileNumber);
                $('#secretaryEmail').val(e.secretary.emailAddress);
                $('#secretaryDOA').val(e.secretary.dateOfAppointment);

                // Objects table
                if (e.memorandumOfAssociation.memorandumObjects.length > 0) {
                    e.memorandumOfAssociation.memorandumObjects.forEach((o, index) => {
                        objectivesTable.row.add([
                            o.value
                        ]).draw(false);
                    });
                }

                // Liability
                $('#liabilityClause').val(e.memorandumOfAssociation.liabilityClause);

                // Share clause
                if (e.memorandumOfAssociation.shareClauses.length > 0) {
                    e.memorandumOfAssociation.shareClauses.forEach((s, index) => {
                        shareClauseTable.row.add([
                            s.value
                        ]).draw(false);
                    });
                }

                // Members
                if (e.members.length > 0) {
                    e.members.forEach((m, index) => {
                        memberTable.row.add([
                            m.fullName,
                            m.country,
                            m.gender,
                            m.dateOfBirth,
                            m.nationalIdentification,
                            m.physicalAddress,
                            m.mobileNumber,
                            m.emailAddress,
                            m.dateOfTakeUp,
                            m.occupation
                        ]).draw(false);
                    });
                }


                // Articles of association
                $('#tableOfArticles').val(e.articlesOfAssociation.tableOfArticles);

                // Amended articles
                if (e.articlesOfAssociation.amendedArticles > 0) {
                    e.articlesOfAssociation.amendedArticles.forEach((a, index) => {
                        articlesTable.row.add([
                            e.value
                        ]).draw(false);
                    });
                }
            }).appendTo('#pending');
        }

        $(applicationSelect).css('cursor', 'pointer').click(function () {
                // Activate btn-done
                $('#btn-done').removeClass('disabled');

                // Reset step wizard
                $('#smartwizard').smartWizard("reset");

                // ApplicationId
                $('#applicationId').val(e.application.applicationId);

                // Industry 
                $('#industrySector').val(e.industrySector);

                // Office 
                $('#effectiveFrom').val(e.office.effectiveFrom);
                $('#physicalAddress').val(e.office.physicalAddress);
                $('#officeCity').val(e.office.cityTown);
                $('#postalAddress').val(e.office.postalAddress);
                $('#emailAddress').val(e.office.emailAddress);
                $('#telNumber').val(e.office.telephoneNumber);
                $('#mobileNumber').val(e.office.mobileNumber);

                // Directors
                if (e.directors.length > 0) {
                    e.directors.forEach((d, index) => {
                        directorsTable.row.add([
                            d.fullName,
                            d.country,
                            d.gender,
                            d.dateOfBirth,
                            d.nationalIdentification,
                            d.physicalAddress,
                            d.mobileNumber,
                            d.emailAddress,
                            d.dateOfAppointment,
                        ]).draw(false);
                    });
                }

                // Secretary
                $('#secretaryCountry').val(e.secretary.country);
                $('#secretaryFullName').val(e.secretary.fullName);
                $('#secretaryGender').val(e.secretary.gender);
                $('#secretaryDOB').val(e.secretary.dateOfBirth);
                $('#secretaryNationalId').val(e.secretary.nationalIdentification);
                $('#secretaryPhysicalAddress').val(e.secretary.physicalAddress);
                $('#secretaryMobile').val(e.secretary.mobileNumber);
                $('#secretaryEmail').val(e.secretary.emailAddress);
                $('#secretaryDOA').val(e.secretary.dateOfAppointment);

                // Objects table
                if (e.memorandumOfAssociation.memorandumObjects.length > 0) {
                    e.memorandumOfAssociation.memorandumObjects.forEach((o, index) => {
                        objectivesTable.row.add([
                            o.value
                        ]).draw(false);
                    });
                }

                // Liability
                $('#liabilityClause').val(e.memorandumOfAssociation.liabilityClause);

                // Share clause
                if (e.memorandumOfAssociation.shareClauses.length > 0) {
                    e.memorandumOfAssociation.shareClauses.forEach((s, index) => {
                        shareClauseTable.row.add([
                            s.value
                        ]).draw(false);
                    });
                }

                // Members
                if (e.members.length > 0) {
                    e.members.forEach((m, index) => {
                        memberTable.row.add([
                            m.fullName,
                            m.country,
                            m.gender,
                            m.dateOfBirth,
                            m.nationalIdentification,
                            m.physicalAddress,
                            m.mobileNumber,
                            m.emailAddress,
                            m.dateOfTakeUp,
                            m.occupation
                        ]).draw(false);
                    });
                }


                // Articles of association
                $('#tableOfArticles').val(e.articlesOfAssociation.tableOfArticles);

                // Amended articles
                if (e.articlesOfAssociation.amendedArticles > 0) {
                    e.articlesOfAssociation.amendedArticles.forEach((a, index) => {
                        articlesTable.row.add([
                            e.value
                        ]).draw(false);
                    });
                }
            });
    });
}

function populateApplicationSelect(e) {
    return '<li class="nav-item">\n' +
        '                    <a class="nav-link" href="#">\n' +
        '                        <i class="far fa-folder"></i> Application - <small></small>\n' +
        '                    </a>\n' +
        '                </li>';
}

function getAndDisplayData() {
    $.ajax({
        type: 'Get',
        url: $('#task-id').val() + '/applications',
        success: function (data) {
            console.log(data);
            taskApplications = data;
            displayPage();
        },
        error: function () {
            alert('Error');
        },
    });
}

$(document).ready(function () {
    $.ajax({
        type: 'Get',
        url: $('#task-id').val() + '/applications',
        success: function (data) {
            console.log(data);
            taskApplications = data;
            displayPage();
        },
        error: function () {
            alert('Error');
        },
    });
});