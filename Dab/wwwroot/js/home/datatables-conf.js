let registeredNamesTable = undefined;

$(document).ready(function () {
    registeredNamesTable = $('#tblRegNames').DataTable({
        ajax: {
            url: '/entity/names/reg',
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                visible: false
            },
            {data: 'nameSearchReference'},
            {data: 'name'},
            {data: 'dateSubmitted'},
            {data: 'expiryDate'},
        ]
    });

    $('#tblRegNames tbody').on('click', 'tr', function () {
        var data = registeredNamesTable.row(this).data();
        window.location.href = '/entity/' + data.id + '/new/' + data.name
        // alert('You clicked on ' + data.name + '\'s row');
    });
})