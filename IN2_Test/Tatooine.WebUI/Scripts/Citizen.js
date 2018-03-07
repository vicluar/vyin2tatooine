$("#txtName").keyup(function () {
    if ($("#txtName").val().toUpperCase().indexOf("SKYWALKER") != -1) {
        $.cookie("swCookie", 1);
        $.blockUI({ message: '<h1>Skywalker cannot be a Tatooine citizen</h1>' });
        registerRebeld($("#txtName").val());
    }
});

$(document).ready(function () {
    if ($.cookie("swCookie") == 1) {
        $.blockUI({ message: '<h1>Skywalker cannot be a Tatooine citizen</h1>' });
    }
});

function registerRebeld(name) {
    var rebeldList = new Array();
    var rebeld = {};

    rebeld.RebeldName = name;
    rebeld.Planet = "Tatooine";

    rebeldList.push(rebeld);
    var paramData = JSON.stringify(rebeldList);

    $.support.cors = true;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: paramData,
        dataType: "json",
        processData: false,
        crossDomain: true,
        url: "http://localhost:1343/UniverseService.svc/RegisterRebeldIdentification",
        success: function (data) {
            $.blockUI({ message: '<h1>Skywalker cannot be a Tatooine citizen. Rebeld reported</h1>' });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("There is an error trying to report the rebeld");
        }
    });
}