let registeredNamesTable = undefined;

$(document).ready(function () {
    registeredNamesTable = $('#tblRegNames').DataTable({
        ajax: {
            url: '/entity/names/reg',
            dataSrc: ''
        },
        columns: [
            {
                data: 'nameId',
                visible: false
            },
            {data: 'ref'},
            {data: 'name'},
            {data: 'dateSubmitted'},
            {data: 'dateExp'},
        ]
    });

    $('#tblRegNames tbody').on('click', 'tr', function () {
        var data = registeredNamesTable.row(this).data();
        window.location.href = '/entity/' + data.nameId + '/new'
        // alert('You clicked on ' + data.name + '\'s row');
    });
})