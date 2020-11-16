$(document).ready(function () {
    $.ajax({
        type: 'Get',
        url: '/entity/' + $('#applicationId').val() + '/reload',
        success: function (data) {
            toastr.success("Worked");
            console.log(data);
            //Populating saved office details
            if (data.office != null) {
                $('#physicalAddress').val(data.office.physicalAddress);
                $('#officeCity').val(data.office.officeCity);
                $('#postalAddress').val(data.office.postalAddress);
                $('#emailAddress').val(data.office.emailAddress);
                $('#telNumber').val(data.office.telNumber);
                $('#mobileNumber').val(data.office.mobileNumber);
            }
            //Populating saved liability clause
            if (data.liabilityClause != null) {
                $('#liabilityClause').val(data.liabilityClause);
            }
            //Populating saved share clause
            if (data.shareClause != null) {
                $('#shareClause').val(data.shareClause);
            }
            //Populating objectives
            if (data.objectives != null) {
                data.objectives.forEach((x,index)=>{
                    objects.push(x);
                })
                console.log(objects);
                tblObjects.destroy();
                tblObjects = $('#tblObjects').DataTable({
                    responsive: true,
                    scrollX: true,
                    data: objects,
                    select: true,
                    lengthMenu: [
                        [4, 10],
                        ['4 rows', '10 rows']
                    ],
                    columns: [
                        {data: 'objective'},
                    ],
                });
            }
            //Populating saved table of articles
            if (data.tableOfArticles != null) {
                if (data.tableOfArticles === 'Table A')
                    $('#tableOfArticles').val('table A');
                else if (data.tableOfArticles === 'Table B')
                    $('#tableOfArticles').val('table B');
                else
                    $('#tableOfArticles').val('other');
            }
            //Populating saved members
            if (data.members != null) {
                tblPeople.destroy();
                tblPeople = $('#tblPeople').DataTable({
                    responsive: true,
                    scrollX: true,
                    data: data.members,
                    lengthMenu: [
                        [4, 10],
                        ['4 rows', '10 rows']
                    ],
                    columns: [
                        {data: 'peopleCountry'},
                        {data: 'nationalId'},
                        {data: 'memberSurname'},
                        {data: 'memberName'},
                        {data: ''},
                        {data: 'phyAddress'},
                        {data: 'isSecretary'},
                        {data: 'isMember'},
                        {data: 'isDirector'},
                        {data: 'ordShares'},
                        {data: 'prefShares'},
                        {data: ''},
                    ],
                });
            }

        },
        error: function (error) {
            toastr.error(error.toString());
        },
    });
});