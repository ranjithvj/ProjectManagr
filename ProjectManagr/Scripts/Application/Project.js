$(document).ready(function () {
    //Open up the Add Project partial View
    $("#btnCreateProject").on("click", function () {
        var url = $(this).data("url");

        $.get(url, function (data) {
            $('#createProjectContainer').html(data);
            $('#createProjectSiteModal').modal('show');
        });
    });
});