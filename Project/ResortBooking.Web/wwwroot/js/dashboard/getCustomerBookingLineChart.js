

$(document).ready(function () {
    loadCustomerBookingLineChart();
});

function loadCustomerBookingLineChart() {
    $.ajax({
        url: "/Dashboard/GetMemberAndBookingLineChart",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
           
            loadLineChart("customerAndBookingsLineChart", data);
        }
    });
}

function loadLineChart(id, data) {
    var chartColors = getChartArray(id);

    options = {
        colors: chartColors,
        series: data.series,

        chart: {
            type: 'line',
            height: 270,

        },
        stroke: {
            curve: "smooth",
            width: 2
        },
        markers: {
            size: 3,
            strokeWidth: 0,
            hover: {
                size:7
            }
        },
        xaxis: {
            categories: data.categories,
            labels: {
                style: {
                    colors: '#fff'
                }
            }
        },
        yaxis: {
            labels: {
                style: {
                    colors: '#fff'
                }
            }
        },
        legend: {
            position: 'bottom',
            horizontalAlign: 'center',
            labels: {
                colors: '#fff',
            }
        },
        tooltip: {
            theme: 'dark'
        }
    };
   

    var chart = new ApexCharts(document.querySelector("#" + id), options);
    chart.render();
}

function getChartArray(id) {
    if (document.getElementById(id) != null) {
        var colors = document.getElementById(id).getAttribute("data-colors");
        if (colors) {
            colors = JSON.parse(colors);
            return colors.map(function (value) {
                var newValue = value.replace(" ", "");
                if (newValue.indexOf(",") === -1) {
                    var color = getComputedStyle(document.documentElement).getPropertyValue(newValue);
                    if (color) return color;
                    else return newValue;
                }
            });
        }
    }
}
