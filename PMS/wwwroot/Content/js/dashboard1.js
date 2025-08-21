

// Dashboard 1 Morris-chart



Morris.Area({
        element: 'morris-area-chart2',
        data: [{
            period: '2010',
            iMac: 0,
            iPhone: 0,
            
        }, {
            period: '2011',
            iMac: 130,
            iPhone: 100,
            
        }, {
            period: '2012',
            iMac: 30,
            iPhone: 60,
            
        }, {
            period: '2013',
            iMac: 30,
            iPhone: 200,
            
        }, {
            period: '2014',
            iMac: 200,
            iPhone: 150,
            
        }, {
            period: '2015',
            iMac: 105,
            iPhone: 90,
            
        },
         {
            period: '2016',
            iMac: 250,
            iPhone: 150,
           
        }],
        xkey: 'period',
        ykeys: ['iMac', 'iPhone'],
        labels: ['iMac', 'iPhone'],
        pointSize: 0,
        fillOpacity: 0.4,
        pointStrokeColors:['#b4becb', '#01c0c8'],
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        lineWidth: 0,
        smooth: true,
        hideHover: 'auto',
        lineColors: ['#b4becb', '#01c0c8'],
        resize: true
        
    });

 // Morris donut chart
        
    Morris.Donut({
        element: 'morris-donut-chart',
        data: [{
            label: "Adv",
            value: 8500,

        }, {
            label: "Tredshow",
            value: 3630,
        }, {
            label: "Web",
            value: 4870
        }],
        resize: true,
        colors:['#ea6c41', '#01c0c8', '#4F5467']
    });
 
$("#sparkline8").sparkline([2,4,4,6,8,5,6,4,8,6,6,2 ], {
            type: 'line',
            width: '100%',
            height: '130',
            lineColor: '#00a65a',
            fillColor: 'rgba(0, 194, 146, 0.2)',
            maxSpotColor: '#00a65a',
            highlightLineColor: 'rgba(0, 0, 0, 0.2)',
            highlightSpotColor: '#00a65a'
        });
        $("#sparkline9").sparkline([0,2,8,6,8,5,6,4,8,6,6,2 ], {
            type: 'line',
            width: '100%',
            height: '130',
            lineColor: '#177ec1',
            fillColor: 'rgba(3, 169, 243, 0.2)',
            minSpotColor:'#177ec1',
            maxSpotColor: '#177ec1',
            highlightLineColor: 'rgba(0, 0, 0, 0.2)',
            highlightSpotColor: '#177ec1'
        });
        $("#sparkline10").sparkline([2,4,4,6,8,5,6,4,8,6,6,2], {
            type: 'line',
            width: '100%',
            height: '130',
            lineColor: '#ea6c41',
            fillColor: 'rgba(251, 150, 120, 0.2)',
            maxSpotColor: '#ea6c41',
            highlightLineColor: 'rgba(0, 0, 0, 0.2)',
            highlightSpotColor: '#ea6c41'
        });
