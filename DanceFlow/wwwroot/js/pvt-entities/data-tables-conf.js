let directorsTable = undefined;
let memberTable = undefined;
let articlesTable = undefined;
let objectivesTable = undefined;
let shareClauseTable = undefined;

$(document).ready(function () {

        directorsTable = $('#tblDirectors').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });


        objectivesTable = $('#tblObjectives').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        shareClauseTable = $('#tblShareClass').DataTable({
            responsive: true,
            scrollX: true,
            lengthMenu: [
                [4, 10],
                ['4 rows', '10 rows']
            ],
        });

        memberTable = $('#tblMembers').DataTable({
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

