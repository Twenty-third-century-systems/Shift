let tblObjects = undefined;
let tblPeople = undefined;
let tblEntities = undefined;
let tblArticles = undefined;
let tblDirectors = undefined;

$(document).ready(function () {

        tblObjects = $('#tblObjects').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        tblPeople = $('#tblPeople').DataTable({
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
        
        tblDirectors =$('#tblDirectors').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });
    }
);

