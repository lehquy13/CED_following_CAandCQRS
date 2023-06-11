// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    //$("#loaderbody").addClass('d-none');

    $(document).bind('ajaxStart', function () {
        //$("#loaderbody").removeClass('d-none');
        //alert("i 2 was called");
        $('#preloder').css('display', 'block');


       
    }).bind('ajaxStop', function () {

        $('#preloder').css('display', 'none');


    });
});
/*------------------
        Preloader
    --------------------*/
$(window).on('load', function () {
    //$(".loader").fadeOut();
    $("#preloder").delay(200).fadeOut("slow");
    
    /*------------------
        Product filter
    --------------------*/
    $('.filter__controls li').on('click', function () {
        $('.filter__controls li').removeClass('active');
        $(this).addClass('active');
    });
    if ($('.property__gallery').length > 0) {
        var containerEl = document.querySelector('.property__gallery');
        var mixer = mixitup(containerEl);
    }
});
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
            //alert(err);
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
            //alert(err);
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
    
    if(id === 'file'){
        var filesTemp = $('#file')[0].files;
        for (var i = 0; i < filesTemp.length; i++) {
            console.log(filesTemp[i]);
            formData.append('formFiles', filesTemp[i]);
        }
        
       
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                $("#filesInputValues").html('');
                if (res.res === true) {
                    for (const value of res.images) {
                        $("#filesInputValues")
                            .append('<img src='+value+' style="max-width: 120px;" alt="Profile" id="tempFiles">\n' +
                                '<input id='+value+' name="TutorVerificationInfoDtos.Image" hidden type="text"/>');
                    }

                


                }
                console.log(res);

            },
            error: function (err) {
                console.log(err);
                //alert(err);
            }
        })
    }
    else{
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
                //alert(err);
            }
        })
    }
   
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
function AddMajorSubject(id,name,des){

    $('#tutorMajorCard .list-group').append(`<input name="SubjectId" value="${id}" hidden="hidden"/>\n` +
        `    <a class="list-group-item list-group-item-action" href="/Subject/Detail?id=${id}" >\n` +
        `        <div class="d-flex w-100 justify-content-between">\n` +
        `            <h5 class="mb-1">`+name+`</h5>\n` +
        `        </div>\n` +
        `        <p class="mb-1">`+des+`</p>\n` +
        `    </a>`);


}

