let objectsTable = undefined;
let peopleTable = undefined;
let articlesTable = undefined;

$(document).ready(function () {

        objectsTable = $('#tblObjects').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        peopleTable = $('#tblPeople').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        articlesTable = $('#tblArticles').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });
    }
);

