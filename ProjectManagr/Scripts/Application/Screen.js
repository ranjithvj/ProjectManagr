var colorDictionary = {};
colorDictionary[1] = "#F5B041";
colorDictionary[2] = "#2ECC71";
colorDictionary[3] = "#273746";
colorDictionary[4] = "#1ABC9C";
colorDictionary[5] = "#F1C40F";
colorDictionary[6] = "#D35400";
colorDictionary[7] = "#99A3A4";
colorDictionary[8] = "#E74C3C"; //Future Purposes
colorDictionary[9] = "#6C3483"; //Future Purposes
colorDictionary[10] = "#D8076C";//Future Purposes

$(document).ready(function () {
    $("#screenTable").DataTable({
        "ajax": "Screen/GetAllScreens",
        "info": "false",
        "bLengthChange": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": $('#getUrl').val()
        },
        "columns": [
            { "title": "Name", "data": "Name" },
            { "title": "Tech Lead", "data": "TechLead" },
            {
                "title": "Estimated Completion",
                "data": "EstCompletion",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "title": "Estimated Release",
                "data": "EstRelease",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            { "title": "Estimated Hours", "data": "EstHours" },
            { "title": "Actual Hours", "data": "ActualHours" },
            {
                "title": "Actions",
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editUrl = $('#editUrl').val() + '?id=' + data;
                    var deleteUrl = $('#deleteUrl').val() + '?id=' + data;
                    var detailsUrl = $('#detailsUrl').val() + '?id=' + data;
                    return '<a href=' + detailsUrl + ' class="detailsProject" ><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span></a> | <a href=' + editUrl + ' class="editProject"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a>  | <a href=' + deleteUrl + ' class="deleteProject"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></a>';
                }
            }
        ],
        //For aligning the components
        "dom": 'f<"toolbar">rtip',
        //To remove Search Label
        "oLanguage": {
            "sSearch": ""
        },
        //Add Search button instead of calling server for every keystroke
        initComplete: function () {
            var input = $('.dataTables_filter input').unbind(),
                self = this.api(),
                $searchButton = $('<button type="button" class="btn btn-default btn-md" id="dataTableSearchBtn"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></button>')
                           .click(function () {
                               self.search(input.val()).draw();
                           })

            $('.dataTables_filter').append($searchButton);
        }
    });

    //Add Search placeholder
    $('.dataTables_filter input').attr('id', 'dataTablesSearchbox');
    $("#dataTablesSearchbox").attr("placeholder", "Search");

    //Add new button
    var createUrl = $('#createUrl').val();
    $("div.toolbar").html('<button type="button" class="btn btn-default btn-md" data-toggle="modal" ' +
        'data-url="' + createUrl + '" id="btnCreateProject">' +
    '<span class="glyphicon glyphicon-plus" aria-hidden="true"></span> ' +
    'Add Project </button>');

    //Open up the Add partial View
    $("#btnCreateProject").on("click", function () {
        var url = $(this).data("url");

        $.get(url, function (data) {
            $('#createProjectContainer').html(data);
            $('#createProjectSiteModal').modal('show');
        });

    });

});



//Details Section
$('#screenTable').on("click", ".detailsProject", function (event) {
    event.preventDefault();
    var url = $(this).attr("href");

    $.get(url, function (data) {
        $('#detailsProjectSiteContainer').html(data);
        $('#detailsProjectSiteModal').modal('show');
    });

});

//Edit Section
$('#screenTable').on("click", ".editProject", function (event) {
    event.preventDefault();
    var url = $(this).attr("href");

    $.get(url, function (data) {
        $('#createProjectContainer').html(data);
        $('#createProjectSiteModal').modal('show');
    });

});

//Delete Section
$('#screenTable').on("click", ".deleteProject", function (event) {
    event.preventDefault();
    var url = $(this).attr("href");

    $.get(url, function (data) {
        $('#deleteProjectSiteContainer').html(data);
        $('#deleteProjectSiteModal').modal('show');
    });

});

function OnSuccessfulProjectSiteUpdate(data) {
    if (data != "success") {
        $('#createProjectContainer').html(data);
        return;
    }
    $('#createProjectSiteModal').modal('hide');
    $('#createProjectContainer').html("");
    //assetListVM.refresh();
    //TODO : Refresh your Datatable!!!!!!!!!!!
}

function OnSuccessfulProjectSiteDelete(data) {
    if (data != "success") {
        $('#deleteProjectSiteContainer').html(data);
        return;
    }
    $('#deleteProjectSiteModal').modal('hide');
    $('#deleteProjectSiteContainer').html("");
    //assetListVM.refresh();
    //TODO : Refresh your Datatable!!!!!!!!!!!
}

function ParseDate(data) {
    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
    var month = date.getMonth() + 1;
    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
}