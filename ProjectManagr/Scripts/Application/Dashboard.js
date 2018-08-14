$(document).ready(function () {
    //Initialize the Dashboard 
    $(document).ready(function () {

        //Get the data using AJAX
        $.get("Home/Get", function (data, status) {

            var groups = new vis.DataSet();
            var items = new vis.DataSet();
            var numberOfGroups = data.length;

            for (var i = 0; i < numberOfGroups ; i++) {
                groups.add({
                    id: i,
                    content: data[i].ProjectName
                });

                var color = data[i].EntityStatusColor;
                items.add({
                    id: i + 1000,
                    group: i,
                    start: ParseDate(data[i].SiteEngagementStart),
                    end: ParseDate(data[i].SiteEngagementEnd),
                    content: data[i].EntityStatusName,
                    //style: "background-color : " + color,
                });
            }


            // specify options
            var options = {
                stack: true,
                horizontalScroll: false,
                zoomKey: 'ctrlKey',
                //maxHeight: 400,
                //start: new Date(),
                //end: new Date(1000 * 60 * 60 * 24 + (new Date()).valueOf()),
                //editable: true,
                margin: {
                    item: 10, // minimal margin between items
                    axis: 5   // minimal margin between items and the axis
                },
                orientation: 'top',
                timeAxis: { scale: 'month', step: 1 }
            };

            // create a Timeline
            var container = document.getElementById('dashboard');
            timeline = new vis.Timeline(container, items, groups, options);
        });

    });

    //$('#dashBoardSearchBtn').on('click', function (evt) {
    //    evt.preventDefault();
    //    evt.stopPropagation();

    //    var $chartDiv = $('#dashBoardDiv'),
    //        url = $(this).data('url');

    //    $.get(url, function (data) {
    //        $chartDiv.replaceWith(data);
    //    });
    //});
});



function ParseDate(data) {
    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
    var month = date.getMonth() + 1;
    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
}