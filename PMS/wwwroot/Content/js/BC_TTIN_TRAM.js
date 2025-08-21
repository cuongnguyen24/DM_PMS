$(document).ready(function () {
    $.ajax({  
        type: "POST",  
        url: "/BC_TTIN_TRAM/GetData",
        contentType: "application/json; charset=utf-8",  
        dataType: "json",  
        success: function (chData) {  
            var aData = chData;  
            var aLabels = aData[0];
            var aDatasets1 = aData[1];

            var ctx2 = document.getElementById("chart2").getContext("2d");
            var data2 = {
                labels: aLabels,
                datasets: [
                    {
                        label: "My First dataset",
                        fillColor: "rgb(255, 102, 0)",
                        strokeColor: "rgb(255, 102, 0)",
                        highlightFill: "rgb(255, 128, 0)",
                        highlightStroke: "rgb(255, 128, 0)",
                        data: aDatasets1
                    }
                ]
            };

            var chart2 = new Chart(ctx2).Bar(data2, {
                scaleBeginAtZero: true,
                scaleShowGridLines: true,
                scaleGridLineColor: "rgba(0,0,0,.005)",
                scaleGridLineWidth: 0,
                scaleShowHorizontalLines: true,
                scaleShowVerticalLines: true,
                barShowStroke: true,
                barStrokeWidth: 0,
                tooltipCornerRadius: 2,
                barDatasetSpacing: 3,
                legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                responsive: true
            });
        }
    })

    
});