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
            $('#successAlertButton').click();

        },
        error: function (err) {
            console.log(err);
            alert(err);
        }
    })
    return false;

}
