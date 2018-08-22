
function NotifyError(msg) {
    $.notify(
    {
        icon: 'glyphicon glyphicon-thumbs-down',
        title: '<strong> Error : </strong>',
        message: msg
    },
    {
        type: 'danger',
        placement: {
            from: "top",
            align: "right"
        },
        offset: {
            x: 25,
            y: 50,
        },
        template: '<div data-notify="container" role="alert" class="col-xs-10 col-sm-3 col-md-3 alert alert-{0}" style=" max-width: 400px;">\
                <button type="button" class="close" data-notify="dismiss">\
                    <span aria-hidden="true">×</span>\
                    <span class="sr-only">Close</span>\
                </button>\
                <span data-notify="icon"></span>\
                <span data-notify="title">{1}</span>\
                <span data-notify="message">{2}</span>\
            </div>'
    });
}

function NotifySuccess(msg) {
    $.notify(
    {
        icon: 'glyphicon glyphicon-thumbs-up',
        title: '<strong> Success : </strong>',
        message: msg
    },
    {
        type: 'success',
        placement: {
            from: "top",
            align: "right"
        },
        offset: {
            x: 25,
            y: 50
        },
        template: '<div data-notify="container" role="alert" class="col-xs-10 col-sm-3 col-md-3 alert alert-{0}" style=" max-width: 600px;">\
                <button type="button" class="close" data-notify="dismiss">\
                    <span aria-hidden="true">×</span>\
                    <span class="sr-only">Close</span>\
                </button>\
                <span data-notify="icon"></span>\
                <span data-notify="title">{1}</span>\
                <span data-notify="message">{2}</span>\
            </div>'
    });
}


function NotifyInfo(msg) {
    $.notify(
    {
        icon: 'glyphicon glyphicon-info-sign',
        title: '<strong> Info : </strong>',
        message: msg
    },
    {
        placement: {
            from: "top",
            align: "right"
        },
        offset: {
            x: 25,
            y: 50
        },
        template: '<div data-notify="container" role="alert" class="col-xs-10 col-sm-3 col-md-3 alert alert-{0}" style=" max-width: 600px;">\
                <button type="button" class="close" data-notify="dismiss">\
                    <span aria-hidden="true">×</span>\
                    <span class="sr-only">Close</span>\
                </button>\
                <span data-notify="icon"></span>\
                <span data-notify="title">{1}</span>\
                <span data-notify="message">{2}</span>\
            </div>'
    });
}