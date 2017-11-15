function showInfo(msg) {
    $("#infoBox").text(msg);
    $(".alertContainer").show();
    setTimeout(function () {
        $(".alertContainer").fadeOut();
    }, 1500);
}