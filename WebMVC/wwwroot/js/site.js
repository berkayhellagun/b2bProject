function info(message) {
    var isRtl = $('html').attr('data-textdirection') === 'rtl';
    toastr['info'](message, 'Info!', {
        closeButton: false,
        tapToDismiss: true,
        positionClass: 'toast-top-right',
        rtl: isRtl,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut",
    });
}
function error(message) {
    var isRtl = $('html').attr('data-textdirection') === 'rtl';
    toastr['error'](message, 'Error!', {
        closeButton: false,
        tapToDismiss: true,
        positionClass: 'toast-top-right',
        rtl: isRtl,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    });
}
function success(message) {
    var isRtl = $('html').attr('data-textdirection') === 'rtl';
    toastr['success'](message, 'Progress Bar', {
        closeButton: false,
        tapToDismiss: true,
        progressBar: true,
        positionClass: 'toast-top-right',
        rtl: isRtl,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    });
}
function warning(message) {
    var isRtl = $('html').attr('data-textdirection') === 'rtl';
    toastr['warning'](message, 'Warning!', {
        closeButton: false,
        tapToDismiss: true,
        positionClass: 'toast-top-right',
        rtl: isRtl,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    });
}


function Delete(id, url, redirecturl) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-outline-danger ms-1'
        },
        buttonsStyling: false
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: url + '?Id=' + id,
                type: 'POST',
                dataType: 'json',
                success: function (data) {

                }
            });
        }
        if (result.value) {
            window.location.href = redirecturl;
        }
    });
}

function UserRoleDelete(roleid, userid, posturl, redirecturl) {
    console.log(posturl);
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-outline-danger ms-1'
        },
        buttonsStyling: false
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: posturl + '?userId=' + userid + '&roleId=' + roleid,
                type: 'POST',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        window.location.href = redirecturl;
                    }
                    else {
                        info("User role not deleted!");
                    }
                }
            });
        }
    });
}
//$(function () {
//    //$(".exportchartiamge").click(function () {
//    //    console.log("test");
//    //    let Canv_Widget = $(this).attr("data-id");
//    //    html2canvas($("#" + Canv_Widget), {
//    //        onrendered: function (canvas) {
//    //            saveAs(canvas.toDataURL(), "Export.png");
//    //        }
//    //    });
//    //});
//});
//function DownloadImage(uri, filename) {
//    console.log("DownloadImage");
//    var link = document.createElement("a");
//    if (typeof link.download === "string") {
//        link.href = uri;
//        link.download = filename;
//        document.body.appendChild(link);
//        link.click();
//        document.body.removeChild(link);
//    } else {
//        window.open(uri);
//    }
//}
