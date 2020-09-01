$('#tblContains').DataTable({
    responsive: true,
    scrollX: true,
    autoWidth: true,
    lengthMenu: [
        [4, 10, 25, 50, -1],
        ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
    ],
});

$('#tblNamesStartWith').DataTable({
    responsive: true,
    scrollX: true,
    autoWidth: true,
    lengthMenu: [
        [4, 10, 25, 50, -1],
        ['4 rows', '10 rows', '25 rows', '50 rows', 'Show all']
    ],
});

// var n = document.createElement('script');
// n.setAttribute('language', 'JavaScript');
// n.setAttribute('src', 'https://debug.datatables.net/debug.js');
// document.body.appendChild(n);