// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function callPostActionWithForm(formInput) {

    var formData = new FormData(formInput);

    $.ajax({
        type: "POST",
        url: formInput.action,
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            //$('#successAlert').removeAttr('hidden');
            //$('#successAlert').show();
            if (res.res === true) {
                if (res.viewName === "Profile") {
                    $('#main').html(res.partialView);
                    $('#main').click();
                    $('#successAlertButton').click();

                }
                else {
                    $('#successAlertButton').click();
                }
            }
                
            else if (res.res === "deleted") {
                $('#verticalycentered').modal('hide');
                location.reload();
            }
               


        },
        error: function (err) {
            console.log(err);
            alert(err);
        }
    })
    return false;

}
function ChangePassword(formInput) {

    var formData = new FormData(formInput);

    $.ajax({
        type: "POST",
        url: formInput.action,
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            if(res === true)
                $('#successUpdatePasswordAlert').click();

        },
        error: function (err) {
            console.log(err);
            alert(err);
        }
    })
    return false;

}

function OpenGetDialog(url, title) {

    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (res) {
            //$("#form-modal .modal-body").html(res);
            //$("#form-modal .modal-title").html(title);
            //$("#form-modal").modal('show');
            //debugger;
            console.log(res);
            $('#largeModal .modal-title').html(title);
            $('#largeModal .modal-body').html(res.html);

            $('#modalTriggerButton').click();
            //alert("<3");

            //$.notify("Access granted", "success", { position: "right" });

        }
    })
}

function OpenConfirmDialog(url, title) {
    $('#verticalycentered .modal-title').html(title);

    $('#confirmDialogForm').attr('action', url);
    $('#verticalycentered').modal('show')
   
}
function LoadImage(url, id) {
 

    var formData = new FormData();
    formData.append('formFile', $('#formFile')[0].files[0]);
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {

            if (res.res === true) {
                $('#' + id).attr("src", res.image);
                $('#image').attr("value", res.image);
                
                
            }
            console.log(res);

        },
        error: function (err) {
            console.log(err);
            alert(err);
        }
    })

    return false;
 
   
}
