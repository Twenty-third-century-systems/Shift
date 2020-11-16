let nameUnderExamination = ' ';
let tblNamesThatContain;
let tblNamesThatStartWith;

function initializeTables(nameId) {
    if(tblNamesThatContain !== undefined){
        tblNamesThatContain.destroy();
    }

    if(tblNamesThatStartWith !== undefined){
        tblNamesThatStartWith.destroy();
    }
    
    tblNamesThatContain = $('#tblContains').DataTable({
        ajax: {
            url: '/tasks/examination/' + nameUnderExamination + '/' + nameId + '/contain/',
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                visible: false
            },
            {data: 'name'},
            {data: 'dateSubmitted'},
            {data: 'typeOfBusiness'},
            {data: 'status'},
        ]
    });

    tblNamesThatStartWith = undefined;
    tblNamesThatStartWith = $('#tblNamesStartWith').DataTable({
        ajax: {
            url: '/tasks/examination/' + nameUnderExamination + '/' + nameId + '/starts',
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                visible: false
            },
            {data: 'name'},
            {data: 'dateSubmitted'},
            {data: 'typeOfBusiness'},
            {data: 'status'},
        ]
    });
}

// $(document).ready(function () {
//     initializeTables();
// });


// var n = document.createElement('script');
// n.setAttribute('language', 'JavaScript');
// n.setAttribute('src', 'https://debug.datatables.net/debug.js');
// document.body.appendChild(n);