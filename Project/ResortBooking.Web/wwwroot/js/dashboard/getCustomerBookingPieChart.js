

$(document).ready(function () {
    loadCustomerBookingPieChart();
});

function loadCustomerBookingPieChart() {
    $.ajax({
        url: "/Dashboard/GetBookingPieChart",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
           
            loadPieChart("customerPieChart", data);
        }
    });
}

function loadPieChart(id, data) {
    var chartColors = getChartArray(id);

    options = {
        series: data.series,
        labels: data.labels,
        colors: chartColors,
        chart: {
            
            type: 'donut',
            width: 300
        },
        stroke: {
            show: false
        },
        legend: {
            position: 'bottom',
            horizontalAlign: 'center',
            labels: {
                colors: '#fff',
                useSeriesColors: true
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#" + id), options);
    chart.render();
}

