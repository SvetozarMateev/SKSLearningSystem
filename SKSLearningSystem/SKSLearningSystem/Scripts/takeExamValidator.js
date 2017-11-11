$("#exam").submit(function () {
    var questionsCount = $(".question").length;
    var checkedCount = $(".question input:checked").length;
   

   if (questionsCount < checkedCount) {
        alert("Please check max one checkbox per question");
    } else if (questionsCount > checkedCount) {
        alert("Please check at least one checkbox per question");
    }
});