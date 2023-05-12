
function ChartReload(url) {

    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (res) {
            var obj1 = JSON.parse(res.chartWeekData);
            var obj2 = JSON.parse(res.datesWeekData);
            console.log(obj1);
            console.log(obj2);
            console.log(res.datesWeekData);

            const chart = window.reportsChartStore;

            chart.updateOptions({
                series:obj1,
                xaxis: obj2
            })

        }
    })
}
function PieChartReload(url) {

    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (res) {
            var obj1 = JSON.parse(res.pieWeekData1);
            var obj2 = JSON.parse(res.pieWeekData2);
            
            console.log(obj1);
            console.log(obj2);

            const chart = window.pieChartStore;

            chart.updateOptions({
                series:obj1,
                labels: obj2
            })

        }
    })
}