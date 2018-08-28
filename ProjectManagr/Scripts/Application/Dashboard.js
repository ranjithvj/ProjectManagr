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
        var asterisk = '';
        var elipsis = '';

        if (data[i].Name.length > 30)
        {
            elipsis = '...';
        }
        if (data[i].IsProgressBeyondToday)
        {
            //To Show Asterisk near the Projects whose End dates have crossed current date!
            asterisk = '<div style = "color:red; float : right;">*</div>';
        }

        var projectName = '<div>' + data[i].Name.slice(0, 30) + elipsis + asterisk + '</div>';
        
        groups.add({
            id: i,
            content: projectName,
            title: data[i].Name,
        });

        var color = data[i].EntityStatusColor;
        items.add({
            id: i + 5000,
            group: i,
            start : new Date(data[i].SiteEngagementStartString),
            end: new Date(data[i].SiteEngagementEndString),
            content: data[i].EntityStatusName,
            title: "<div class='chartTooltip'><div>Application : " + data[i].ApplicationName + "</div>"
            + "<div>Project : " + data[i].Name + "</div>"
            + "<div>PM/ADL : " + data[i].PmName + "</div>"
            + "<div>Site ITM : " + data[i].SiteItmName + "</div>"
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
        start: filterStart,
        end: filterEnd,
        margin: {
            item: 10, // minimal margin between items
            axis: 5   // minimal margin between items and the axis
        },
        orientation: 'top',
        showCurrentTime: false,
        timeAxis: { scale: 'month', step: 1 },
        groupOrder: 'id'
    };

    // create a Timeline
    var container = document.getElementById('dashboard');
    timeline = new vis.Timeline(container, items, groups, options);

}
