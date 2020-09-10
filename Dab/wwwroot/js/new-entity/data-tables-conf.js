let tblObjects = undefined;
let tblPeople = undefined;
let tblEntities = undefined;
let tblArticles = undefined;

$(document).ready(function () {

        tblObjects = $('#tblObjects').DataTable({
            responsive: true, 
            lengthMenu: [
                [4, 10, 25, 50, -1],
                ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
            ],
        });

        tblPeople = $('#tblPeople').DataTable({
            responsive: true, 
            // scrollX: true,
            lengthMenu: [
                [4, 10, 25, 50, -1],
                ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
            ],
        });

        tblEntities = $('#tblEntities').DataTable({
            responsive: true, 
            // scrollX: true,
            lengthMenu: [
                [4, 10, 25, 50, -1],
                ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
            ],
        });

        tblArticles = $('#tblArticles').DataTable({
            responsive: true, 
            // scrollX: true,
            lengthMenu: [
                [4, 10, 25, 50, -1],
                ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
            ],
        });
    }
);

