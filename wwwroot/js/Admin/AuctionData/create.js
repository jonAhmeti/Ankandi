$(function () {
    //Create Section____________________________________________//
    const today = new Date();
    function setDateTimeVal(result, date, time) {
        result.val(date.val() + "T" + time.val());
    }
    //set start date value
    var createStartDate = $("#createStartDate");
    createStartDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate());
    //set start time value
    var createStartTime = $("#createStartTime");
    createStartTime.val(
        (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
        ":" +
        (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));
    //set StartDateTime value for form submit
    var createStartDateTime = $("#createStartDateTime");
    setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
    createStartDate.change(function() {
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
    });
    createStartTime.change(function() {
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
    });
    //set end date value
    var createEndDate = $("#createEndDate");
    createEndDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate() + 1));
    //set end time value
    var createEndTime = $("#createEndTime");
    createEndTime.val(
        (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
        ":" +
        (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));
    //set EndDateTime value for form submit
    var createEndDateTime = $("#createEndDateTime");
    setDateTimeVal(createEndDateTime, createEndDate, createEndTime);
    createEndDate.change(function() {
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);
    });
    createEndTime.change(function() {
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);
    });


    //Edit Section____________________________________________//
    function editModalShow(id) {
        $.ajax({
            type: "GET",
            url: "Auction/Edit",
            data: { id: id },
            success: function(response) {
                $("#editModal .modal-body").html(response);
                $("#editModal").modal("show");
            }
        });
    }

    const editBtns = $("a.edit-modal-btn");

    for (let i = 0; i < editBtns.length; i++) {
        const id = $(editBtns[i]).attr("data-id");
        $(editBtns[i]).on(
            "click",
            function() {
                editModalShow(id);
            });
    };
});