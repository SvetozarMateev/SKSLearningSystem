$("#completed").click(() => {
  
    $('.pending').hide();
    $('.started').hide();
    $('.completed').show();
    $('.overdue').hide();
});
$("#pending").click(() => {
    $('.pending').show();
    $('.started').hide();
    $('.completed').hide();
    $('.overdue').hide();
});
$("#started").click(() => {
    $('.pending').hide();
    $('.started').show();
    $('.completed').hide();
    $('.overdue').hide();
});

$("#overdue").click(() => {
    $('.pending').hide();
    $('.started').hide();
    $('.completed').hide();
    $('.overdue').show();
});