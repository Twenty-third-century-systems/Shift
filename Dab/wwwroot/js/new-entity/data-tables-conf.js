let tblObjects = undefined;
let tblShareholders = undefined;
let tblEntities = undefined;
let tblArticles = undefined;
let tblDirectors = undefined;
let tblShareClauses = undefined;

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

        tblObjects = $('#tblObjects').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblShareholders = $('#tblShareholders').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblEntities = $('#tblEntities').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblArticles = $('#tblArticles').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblDirectors = $('#tblDirectors').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblShareClauses = $('#shareClauses').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });
    }
);

