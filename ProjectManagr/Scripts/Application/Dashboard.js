var timeline;

$(document).ready(function () {
    $('#StartDate').datepicker();
    $('#EndDate').datepicker();
});

function ValidateDashboardFilter() {
    var filterStart = $('#StartDate').val();
    var filterEnd = $('#EndDate').val();
    var selectedSite = $('#SelectedSite').val();

    if (selectedSite) {
        if (filterStart && filterEnd && new Date(filterStart) > new Date(filterEnd)) {
            NotifyError(" 'Start date' is greater than 'End date'");
            return false;
        }

        return true;
    }
    else {
        NotifyError('Please enter the required fields');
        return false;
    }
}

function OnChartFilterApplied(response) {

    var items = response.data;

    //Remove existing chart!
    if (timeline != null && timeline.body != null) {
        timeline.destroy();
    }

    if (items) {
        if (items.ChartData) {
            //If the given filters have no data, do not render the empty chart
            if (items.ChartData.length > 0) {
                LoadCharts(items.ChartData, items.MinDate, items.MaxDate);

                timeline.redraw();
            } else {
                NotifyInfo("No data available for given criteria")
            }
        }
    }

}


function ParseDate(data) {
    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
    var month = date.getMonth() + 1;
    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
}

function LoadCharts(data, minDate, maxDate) {
    var groups = new vis.DataSet();
    var items = new vis.DataSet();
    var numberOfGroups = data.length;
    var filterStart = $('#StartDate').val();
    var filterEnd = $('#EndDate').val();

    if (!filterStart) {
        filterStart = new Date(minDate);
    }
    if (!filterEnd) {
        filterEnd = new Date(maxDate);
    }

    for (var i = 0; i < numberOfGroups ; i++) {
        groups.add({
            id: i,
            content: data[i].Name.slice(0, 30) + '...',
            title: data[i].Name
            //style: "width : 10%; overflow: hidden;text-overflow:ellipsis;"
        });

        var color = data[i].EntityStatusColor;
        items.add({
            id: i + 1000,
            group: i,
            //start: ParseDate(data[i].SiteEngagementStart),
            //end: ParseDate(data[i].SiteEngagementEnd),
            start : new Date(data[i].SiteEngagementStartString),
            end: new Date(data[i].SiteEngagementEndString),
            content: data[i].EntityStatusName,
            title: "<div class='chartTooltip'><div>Application : " + data[i].ApplicationName + "</div>"
            + "<div>Project : " + data[i].Name + "</div>"
            + "<div>PM/ADL : " + data[i].PmName + "</div>"
            + "<div>Site ITM : " + data[i].SiteItm + "</div>"
            + "<div>Site ITM Feedback : " + data[i].SiteItmFeedbackName + "</div></div>"
            ,
            style: "background-color : " + data[i].ColorCode,
        });
    }


    // specify options
    var options = {
        stack: true,
        horizontalScroll: false,
        zoomable: false,
        moveable: false,
        //maxHeight: 400,
        start: filterStart,
        end: filterEnd,
        //editable: true,
        margin: {
            item: 10, // minimal margin between items
            axis: 5   // minimal margin between items and the axis
        },
        orientation: 'top',
        showCurrentTime: false,
        timeAxis: { scale: 'month', step: 1 }
    };

    // create a Timeline
    var container = document.getElementById('dashboard');
    timeline = new vis.Timeline(container, items, groups, options);

}
