let tblObjects = undefined;
let tblShareholders = undefined;
let tblEntities = undefined;
let tblArticles = undefined;
let tblDirectors = undefined;
let tblShareClauses = undefined;

$(document).ready(function () {

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

