

$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    loadDataTable(status);
});


function loadDataTable(status) {
    
        "ajax":{
            "url": '/Booking/GetAll=status=' + status,
            "type": "GET",
            "datatype":"json"

        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "5%" },
            { data: 'phone', "width": "5%" },
            { data: 'email', "width": "5%" },
            { data: 'status', "width": "5%" },
            { data: 'checkInDate', "width": "5%" },
            { data: 'nights', "width": "5%" },
            { data: 'totalCost', "width": "5%" }
        ]
    
}
