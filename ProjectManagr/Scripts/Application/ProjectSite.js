var grid;

$(document).ready(function () {

    //Add search box across all columns of the grid
    AddSearchBoxAndSelectAllCheckbox()

    grid = $("#screenTable").DataTable({
        "scrollX": true,
        "fixedHeader": true,
        "orderCellsTop": true,
        "ajax": {
            "url": $('#getUrl').val(),
            "type": "POST"
        },
        "columns": [
            {
                "title": "", "data": "IsSelected",
                "render": function (data, type, full) {
                    return ''
                }
            },
            { "title": "No.", "data": null },
            { "title": "Entity Status", "data": "EntityStatusName" },
            { "title": "Project ID", "data": "Code" },
            { "title": "Project Name", "data": "Name" },
            { "title": "PM/ADL /Planner", "data": "PmName", "width": "5%" },
            { "title": "Sub Portfolio", "data": "SubPortfolioName" },
            { "title": "Site", "data": "SiteName" },
            { "title": "Site ITM", "data": "SiteItmName" },
            { "title": "Site ITM Feedback", "data": "SiteItmFeedbackName" },
            { "title": "Eng. Start", "data": "SiteEngagementStartString" },
            { "title": "Eng. End", "data": "SiteEngagementEndString" },
            { "title": "Created By", "data": "CreatedBy" },
            { "title": "Created On", "data": "CreatedDateString" },
            {
                "title": "Actions",
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editUrl = $('#editUrl').val() + '?id=' + data;
                    var detailsUrl = $('#detailsUrl').val() + '?id=' + data;
                    return '<a href=' + detailsUrl + ' class="detailsProject" ><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span></a> | <a href=' + editUrl + ' class="editProject"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a>';
                }
            },
            { "title": "Id", "data": "Id" },

        ],
        "columnDefs": [
            {
                "orderable": false,
                "className": 'select-checkbox',
                "targets": 0,
                "width": "2%",
            },
            {
                "orderable": false,
                "searchable": false,
                "targets": 1,
                "width": "2%",
                "visible": false
            },
            {
                "targets": [2],
                "width": "6%",
                "render": $.fn.dataTable.render.ellipsis(10, true)
            }
           ,
           {
               "targets": [3],
               "width": "5%",
               "render": $.fn.dataTable.render.ellipsis(10, true)
           },
            {
                "targets": [4],
                "width": "10%",
                "render": $.fn.dataTable.render.ellipsis(15, true)
            }
           ,
           {
               "targets": [5],
               "width": "15%",
               "render": $.fn.dataTable.render.ellipsis(15, true)
           },
           {
               "targets": [6],
               "width": "5%",
               "render": $.fn.dataTable.render.ellipsis(10, true)
           }
           ,
           {
               "targets": [7],
               "width": "5%",
               "render": $.fn.dataTable.render.ellipsis(10, true)
           },
            {
                "targets": [8],
                "width": "5%",
                "render": $.fn.dataTable.render.ellipsis(12, true)
            },
            {
                "targets": [9],
                "width": "5%",
                "render": $.fn.dataTable.render.ellipsis(10, true)
            }
           ,
           {
               "targets": [10],
               "width": "5%",
               "render": $.fn.dataTable.render.ellipsis(11, true)
           },
            {
                "targets": [11],
                "width": "5%",
                "render": $.fn.dataTable.render.ellipsis(11, true)
            },
            {
                "targets": [12],
                "width": "5%",
                "render": $.fn.dataTable.render.ellipsis(10, true)
            },
            {
                "targets": [15],
                "visible": false,
            },
            {
                "targets": [14],
                "width": "5%",
                "render": $.fn.dataTable.render.ellipsis(11, true)
            },
        ],
        "select": {
            "style": 'multi',
            "selector": 'td:first-child'
        },
        "order": [[4, 'asc']],
        //For aligning the components
        "dom": '<"toolbar">rt<"row" <"col-md-6"i><"col-md-2 rowlength"><"col-md-4"p>>', //Add 'l' after rowlength" if you want the row length dropdwown back
        //To remove Search Label
        "oLanguage": {
            "sSearch": ""
        },
        "pageLength": 100,
        scrollY: "58vh",
    });

    //Add new button
    var createUrl = $('#createUrl').val();
    var deleteUrl = $('#deleteRequestUrl').val();
    $("div.toolbar").append('<br>')

    $("div.toolbar").html('<div class="row"><div class="col-md-1"><button type="button" class="btn btn-default btn-md" data-toggle="modal" ' +
        'data-url="' + createUrl + '" id="btnCreateProjectSite">' +
    '<span class="glyphicon glyphicon-plus" aria-hidden="true"></span> ' +
    'Add New</button></div>' +

    '<div class="col-md-1"><button type="button" class="btn btn-default btn-md" ' +
        'data-url="' + deleteUrl + '" id="btnDeleteSelected">' +
    '<span class="glyphicon glyphicon-trash" aria-hidden="true"></span> ' +
    'Delete Selected </button></div></div>'
     );

    $("div.toolbar").append('<br>')

    //Open up the Add partial View
    $("#btnCreateProjectSite").on("click", function () {
        var url = $(this).data("url");

        $.get(url, function (data) {
            $('#createProjectSiteContainer').html(data);
            $('#createProjectSiteModal').modal('show');

        });
    });

    //Open up the Delete 
    $("#btnDeleteSelected").on("click", function () {
        var url = $(this).data("url");
        var rows = grid.rows({ selected: true });
        var result = grid.cells(rows.nodes(), 15).data().toArray();

        if (result.length == 0) {
            NotifyError("Please select atleast 1 record to delete");
            return;
        }

        $.get(url, { count: result.length }, function (data) {
            $('#deleteProjectSiteContainer').html(data);
            $('#deleteProjectSiteModal').modal('show');

            var postUrl = $('#deleteUrl').val();

            $("#btnConfirmDelete").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: postUrl,
                    traditional: true,
                    data: { ids: result },
                }).done(function (data) {
                    OnSuccessfulProjectSiteDelete(result);
                });;
            });
        });
    });



});

//For having nested popups
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


        //Handle Country/Site dependant dropdown functionality
        CountryDropdownchange();
    });
});

function AddSearchBoxAndSelectAllCheckbox() {

    // Setup - add a text input to each footer cell
    $('#screenTable thead tr').clone(true).appendTo('#screenTable thead');
    $('#screenTable thead tr:eq(1) th').each(function (i) {
        if (i == 0) {
            //Add Select all checkbox
            $(this).html('<input type="checkbox" id = "selectAllCheckbox" onclick="SelectAllCheckboxChanged()">');
            return true;
        }
        if (i === 14 || i == 1) {
            //Select and Actions columns dont need the Search textbox
            return true;
        }

        //Add Search box across other columns
        $(this).addClass("searchHeader");
        var title = $(this).text();
        $(this).html('<input type="text" class="searchbox" placeholder="Search ' + title + '" />');

        $('input', this).on('keyup change', function () {
            if (grid.column(i).search() !== this.value) {
                grid
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });
}

function SelectAllCheckboxChanged() {
    $("#selectAllCheckbox").change(function () {
        if (this.checked) {
            grid.rows({ filter: 'applied' }).select();
        } else {
            grid.rows().deselect();
        }
    });
}

function OnSuccessfulProjectSiteUpdate(data, isEdit) {
    if (data.status != "success") {
        $('#createProjectSiteContainer').html(data);

        //Handle Country/Site dependant dropdown functionality
        CountryDropdownchange();

        NotifyError("Please check the validation errors.");
        return;
    }

    $('#createProjectSiteModal').modal('hide');
    $('#createProjectSiteContainer').html("");

    grid.ajax.reload();

    if (isEdit == true) {
        NotifySuccess('Details updated');
    } else {
        NotifySuccess('Details added');
    }
}

function OnSuccessfulProjectSiteDelete(ids) {

    $('#deleteProjectSiteModal').modal('hide');
    $('#deleteProjectSiteContainer').html("");
    $("#selectAllCheckbox").prop("checked", false);

    grid.ajax.reload();
    NotifySuccess(ids.length + ' records deleted');
}

function OpenCreateProjectPopup() {
    var url = $('#btnCreateProject').attr('data-url');

    $.get(url, function (data) {
        $('#createProjectContainer').html(data);
        $('#createProjectModal').modal('show');
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

        var option = new Option(data.project.Name, data.project.Id);
        $('#ProjectId').append($(option));

        $("#ProjectId").val(data.project.Id);
    }
}

function ProjectDropdownchange() {
    var url = $("#projectChangeUrl").val();
    var projectId = $("#ProjectId option:selected").val();

    if (projectId === "") {
        ClearProjectFields();
    }
    else {
        $.get(url, {
            id: projectId
        }, function (data) {
            PopulateProjectFields(data)
        });
    }
}

function CountryDropdownchange() {

    var siteId = parseInt($("#SiteId option:selected").val());
    var countryId = parseInt($("#CountryId option:selected").val());

    //Empty the site dropdown
    $("#SiteId").empty();

    //Return if "Select One" option is selected
    if (isNaN(countryId)) {
        return;
    }

    //Add filtered site values to the site dropdown
    var countrySiteMap = JSON.parse($("#CountrySiteMap").val());
    var siteList = JSON.parse($("#SiteList").val());
    var siteIds = countrySiteMap[countryId];

    var filteredSiteList = siteList.filter(function (site) {
        return siteIds.includes(parseInt(site.Value))
    })

    for (i = 0; i < filteredSiteList.length; i++) {
        $('#SiteId').append($('<option>', { value: filteredSiteList[i].Value, text: filteredSiteList[i].Text }));
    }

    //Make the site dropdown editable
    $("#SiteId").prop("disabled", false);

    $("#SiteId").val(filteredSiteList[0].Value);
}

function SiteDropdownchange() {

}

function PopulateProjectFields(data) {

    $('#ProjectId').val(data.project.Id);
    $('#Code').val(data.project.Code);
    $('#Name').val(data.project.Name);
    $('#Description').val(data.project.Description);
    $('#PmId').val(data.project.PmId);
    $('#PmIdRef').val(data.project.PmId);
    $('#ApplicationName').val(data.project.ApplicationName);
    $('#SubPortfolioId').val(data.project.SubPortfolioId);
    $('#SubPortfolioIdRef').val(data.project.SubPortfolioId);
}

function ClearProjectFields() {
    $('#ProjectId').val("");
    $('#Code').val("");
    $('#Name').val("");
    $('#Description').val("");
    $('#PmId').val("");
    $('#PmIdRef').val("");
    $('#ApplicationName').val("");
    $('#SubPortfolioId').val("");
    $('#SubPortfolioIdRef').val("");
}

function ParseDate(data) {
    var date = new Date(parseInt(data.replace(/\/Date\((-?\d+)\)\//gi, "$1")));
    var month = date.getMonth() + 1;
    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
}

