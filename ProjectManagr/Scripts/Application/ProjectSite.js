$(document).ready(function () {
    $("#screenTable").DataTable({
        "info": "false",
        "bLengthChange": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": $('#getUrl').val(),
            "type": "POST"
        },
        "columns": [
            { "title": "Id", "data": "Id" },
            { "title": "Entity Status", "data": "EntityStatusName" },
            { "title": "Project ID Number", "data": "Code" },
            { "title": "Project Name", "data": "Name" },
            { "title": "PM / ADL / Planner", "data": "PmName" },
            { "title": "Sub Portfolio Name", "data": "SubPortfolioName" },
            { "title": "Site Name", "data": "SiteName" },
            { "title": "Site ITM", "data": "SiteItm" },
            { "title": "Site ITM Feedback", "data": "SiteItmFeedbackName" },
            {
                "title": "Site Engagement Start Date",
                "data": "SiteEngagementStart",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "title": "Site Engagement End Date",
                "data": "SiteEngagementEnd",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            { "title": "Created By", "data": "CreatedBy" },
            {
                "title": "Created On",
                "data": "CreatedDate",
                "render": function (data) {
                    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
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
        "columnDefs": [
           {
               "targets": [0],
               "visible": false,
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
        'data-url="' + createUrl + '" id="btnCreateProjectSite">' +
    '<span class="glyphicon glyphicon-plus" aria-hidden="true"></span> ' +
    'Add Project Site</button>');

    //Open up the Add partial View
    $("#btnCreateProjectSite").on("click", function () {
        var url = $(this).data("url");

        $.get(url, function (data) {
            $('#createProjectSiteContainer').html(data);
            $('#createProjectSiteModal').modal('show');
            InitializeProjectEvents();
        });
    });

});

$(document).on('hidden.bs.modal', function (event) {
    if ($('.modal:visible').length) {
        $('body').addClass('modal-open');
    }
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
        $('#createProjectSiteContainer').html(data);
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
        $('#createProjectSiteContainer').html(data);
        return;
    }
    $('#createProjectSiteModal').modal('hide');
    $('#createProjectSiteContainer').html("");
    var table = $("#screenTable").DataTable();
    table.ajax.reload();
}

function OnSuccessfulProjectSiteDelete(data) {
    if (data != "success") {
        $('#deleteProjectSiteContainer').html(data);
        return;
    }
    $('#deleteProjectSiteModal').modal('hide');
    $('#deleteProjectSiteContainer').html("");
    var table = $("#screenTable").DataTable();
    table.ajax.reload();
}


function InitializeProjectEvents() {
    //event for the Add Project button
    //Open up the Add Project partial View
    $("#btnCreateProject").on("click", function () {
        var url = $(this).data("url");

        $.get(url, function (data) {
            $('#createProjectContainer').html(data);
            $('#createProjectModal').modal('show');
        });
    });
}

function OnSuccessfulProjectAddition(data) {
    if (data.status != "success") {
        $('#createProjectContainer').html(data);
        return;
    }

    //Hide modal
    $('#createProjectModal').modal('hide');
    $('#createProjectContainer').html("");

    //Set value in Project Site screen
    if (data.project != null) {
        PopulateProjectFields(data)

        //Add value to Project name dropdown
        $("#ProjectId option[value = '']").remove()

        var option = new Option(data.project.Name, 0);
        $('#ProjectId').append($(option));

        $("#ProjectId").val("0");
    }
}

function ProjectDropdownchange() {
    var url = $("#projectChangeUrl").val();
    var projectId = $("#ProjectId option:selected").val();

    if (projectId === "") {
        ClearProjectFields();
    }
    else {
        $.get(url, { id: projectId }, function (data) {
            PopulateProjectFields(data)
        });
    }
}

function PopulateProjectFields(data) {

    $('#ProjectId').val(data.project.Id);
    $('#Code').val(data.project.Code);
    $('#Name').val(data.project.Name);
    $('#Description').val(data.project.Description);
    $('#PmName').val(data.project.PmName);
    $('#ApplicationName').val(data.project.ApplicationName);
    $('#SubPortfolioId').val(data.project.SubPortfolioId);
    $('#SubPortfolioIdRef').val(data.project.SubPortfolioId);
}

function ClearProjectFields() {
    $('#ProjectId').val("");
    $('#Code').val("");
    $('#Name').val("");
    $('#Description').val("");
    $('#PmName').val("");
    $('#ApplicationName').val("");
    $('#SubPortfolioId').val("");
    $('#SubPortfolioIdRef').val("");
}

function ParseDate(data) {
    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
    var month = date.getMonth() + 1;
    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
}