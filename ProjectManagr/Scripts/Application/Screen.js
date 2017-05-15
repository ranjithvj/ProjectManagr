$(document).ready(function () {
    $("#screenTable").DataTable({
        "ajax": "Screen/GetAllScreens",

        "columns": [
            { "data": "Name" },
            { "data": "TechLead" },
            {
                "data": "EstCompletion",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "EstRelease",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            //{ "data": "EstRevisedRelease" },
            //{ "data": "ActualRelease" },
            { "data": "EstHours" },
            { "data": "ActualHours" },
            { "data": "Comments" },
            //{ "data": "RevisionReason" },
            //{ "data": "ResourceCount" },
            //{ "data": "SprintsRef" },
            //{ "data": "TotalManDays" },
            //{ "data": "ManDaysPerResource" }
        ]
    })
});