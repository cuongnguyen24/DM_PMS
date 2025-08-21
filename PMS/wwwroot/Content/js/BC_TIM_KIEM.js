$(document).ready(function () {
    var jsData = "{ID:'" + "-1" + "'}";
    $.ajax({
        type: "POST",
        url: "/BC_TIM_KIEM/GetFindByLogSearch",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsData,
        success: function (chData) {
            var aData = chData;
            var aLabels = aData[0];
            var aDatasets1 = aData[1];

            var ctx3 = document.getElementById("rcTimKiem").getContext("2d");
            var data3 = [
               {
                   value: aData[1][0],
                   color: "#1b73d1",
                   highlight: "#247ad6",
                   label: "Tài khoản khác",
               },
               {
                   value: aData[1][1],
                   color: "#ea7648",
                   highlight: "#ef7c4f",
                   label: "Tài khoản đăng nhập"
               }
            ];

            var myPieChart = new Chart(ctx3).Pie(data3, {
                segmentShowStroke: true,
                segmentStrokeColor: "#fff",
                segmentStrokeWidth: 0,
                animationSteps: 100,
                tooltipCornerRadius: 0,
                animationEasing: "easeOutBounce",
                animateRotate: true,
                animateScale: false,
                legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                responsive: true
            });

        }
    })

    var jsData2 = "{ID:'" + "-1" + "'}";
    $.ajax({
        type: "POST",
        url: "/BC_TIM_KIEM/GetFindByCapDA",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsData2,
        success: function (chData) {
            var ctx2 = document.getElementById("rcCapDA").getContext("2d");
            var aData = chData;
            var aLabels = aData[0];
            var aTong = aData[1];
            var aCaNhan = aData[2];

            var data2 = {

                labels: aLabels,
                datasets: [
                      {
                          label: "Tài khoản đăng nhập:",
                          fillColor: "rgb(39, 182, 219)",
                          strokeColor: "rgb(39, 182, 219)",
                          highlightFill: "rgba(180,193,215,1)",
                          highlightStroke: "rgba(180,193,215,1)",
                          data: aCaNhan
                      },
                    {
                        label: "Tài khoản khác:",
                        fillColor: "rgb(211, 25, 25)",
                        strokeColor: "rgb(211, 25, 25)",
                        highlightFill: "rgba(252,201,186,1)",
                        highlightStroke: "rgba(252,201,186,1)",
                        data: aTong
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
                tooltipTemplate: "<%=value%> Files",
                legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
                responsive: true,
                options: {
                    animation: false,
                    tooltips: {
                        enabled: true,
                        mode: 'single',
                        callbacks: {
                            label: function (tooltipItem, data2) {
                                var successCount =1;
                                var failCount = 2;
                                return "Success: " + successCount + "\n Distinct: " + failCount;
                            }
                        }
                    }
                }
            });

           
        }
    })
   
    $.ajax({
        type: "POST",
        url: "/BC_TIM_KIEM/GetFindByTime",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            var ctx2 = document.getElementById("morris-area-chart");          
            Morris.Area({
                element: 'morris-area-chart',         
                data: chData,
                xkey: 'NGAY',
                ykeys: ['TONG', 'CA_NHAN'],
                labels: ['Tổng số', 'Tài khoản đăng nhập'],
                pointSize: 3,
                fillOpacity: 0,
                pointStrokeColors: ['#00bfc7', '#fdc006'],
                behaveLikeLine: true,
                gridLineColor: '#e0e0e0',
                lineWidth: 1,
                hideHover: 'auto',
                lineColors: ['#00bfc7', '#fdc006'],
                resize: true
            });
        }
    })  
});