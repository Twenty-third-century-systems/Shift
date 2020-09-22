let taskApplications = undefined;

function displayPage(){
    taskApplications.forEach((e,index)=> {
        let applicationSelect =
            '<li class="nav-item">\n' +
            '                    <a class="nav-link" href="#">\n' +
            '                        <i class="far fa-folder"></i> Application - <small>' + e.application.user + '</small>\n' +
            '                    </a>\n' +
            '                </li>';

        if (!e.application.examined){
            $(applicationSelect)
                .css('cursor', 'pointer')
                .click(function () {
                    //Reset step wizard
                    $('#smartwizard').smartWizard("reset");
                    
                    //Populate office
                    $('#physicalAddress').val(e.registeredRegOffice.physicalAddress);
                    $('#officeCity').val(e.registeredRegOffice.officeCity);
                    $('#postalAddress').val(e.registeredRegOffice.postalAddress);
                    $('#emailAddress').val(e.registeredRegOffice.emailAddress);
                    $('#telNumber').val(e.registeredRegOffice.telNumber);
                    $('#mobileNumber').val(e.registeredRegOffice.mobileNumber);
                    
                    //Populate Objects table
                    if(e.memorandumOfAssociation.objectives.length > 0){
                        e.memorandumOfAssociation.objectives.forEach((o,index)=>{
                            objectsTable.row.add([
                                o.objective
                            ]).draw(false);
                        });
                    }
                    
                    //Populate Clauses
                    $('#liabilityClause').val(e.memorandumOfAssociation.liabilityShareClauses.liabilityClause);
                    $('#shareClause').val(e.memorandumOfAssociation.liabilityShareClauses.shareClause);
                    
                    //Populate Table of articles field
                    $('#tableOfArticles').val(e.articlesOfAssociation.articleTable.tableOfArticles);
                    
                    //Populate Table of amended articles
                    if(e.articlesOfAssociation.amendedArticles > 0){
                        e.articlesOfAssociation.amendedArticles.forEach((e,index)=>{
                            articlesTable.row.add([
                                e.article
                            ]).draw(false);
                        });                        
                    }
                    
                    //populate members
                    if(e.shareHolders.length > 0){
                        e.shareHolders.forEach((e,index)=>{
                            peopleTable.row.add([
                                e.peopleCountry,
                                e.nationalId,
                                e.memberSurname,
                                e.memberName,
                                e.gender,
                                e.phyAddress,
                                e.isSecretary,
                                e.isMember,
                                e.isDirector,
                                e.ordShares,
                                e.prefShares,
                                parseInt(e.ordShares) + parseInt(e.prefShares),
                            ]).draw(false);
                        });
                    }
                })
                .appendTo('#pending');
        }
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