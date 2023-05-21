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
    
            if (res.res === true) {
                if(res.viewName === "Profile" )
                    $('#main').html(res.partialView);
                //$('#main').click();

                $('#successAlertButton').click();
            } else if (res.res === "Delete") {
                $('#verticalycentered').modal('hide');
                location.reload();
            } else if(res.res === false) {
                if(res.viewName === "_ProfileEdit"){
                    $('#profile-edit').html(res.partialView);
                    $('#profile-edit-button').click();
                }
                else if(res.viewName === "_ChangePassword"){
                    $('#profile-change-password').html(res.partialView);
                    $('#profile-change-password-button').click();
                }
                

                $('#failAlertButton').click();
             
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
            if (res === true)
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
            $('#largeModal .modal-title').html(title);
            $('#largeModal .modal-body').html(res.partialView);

            $('#modalTriggerButton').click();


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

function ChooseTutor(id,name,phone){
    $('#largeModal').modal('hide');
    
    $('#largeModal .modal-body').html("");
    $(document.body).removeClass('modal-open');
    $('.modal-backdrop').remove();
    $('#tutorId').attr("value",id);
    $('#tutorInfor').attr("value",name + " - " +phone);
    
}

