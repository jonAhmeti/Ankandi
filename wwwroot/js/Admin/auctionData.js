$(function() {
    var today = new Date();

    function setDateTimeVal(result, date, time) {
        result.val(date.val() + "T" + time.val());
    }

    //set start date value
    var startDate = $("#startDate");
    startDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate());

    //set start time value
    var startTime = $("#startTime");
    startTime.val(
        (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
        ":" +
        (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));

    //set StartDateTime value for form submit
    var startDateTime = $("#startDateTime");
    setDateTimeVal(startDateTime, startDate, startTime);

    startDate.change(function() {
        setDateTimeVal(startDateTime, startDate, startTime);
    });

    startTime.change(function() {
        setDateTimeVal(startDateTime, startDate, startTime);
    });

    //set end date value
    var endDate = $("#endDate");
    endDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate() + 1));

    //set end time value
    var endTime = $("#endTime");
    endTime.val(
        (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
        ":" +
        (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));

    //set EndDateTime value for form submit
    var endDateTime = $("#endDateTime");
    setDateTimeVal(endDateTime, endDate, endTime);

    endDate.change(function() {
        setDateTimeVal(endDateTime, endDate, endTime);
    });

    endTime.change(function() {
        setDateTimeVal(endDateTime, endDate, endTime);
    });

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

    var editBtns = $("a.edit-modal-btn");

    for (var i = 0; i < editBtns.length; i++) {
        let id = $(editBtns[i]).attr("data-id");
        $(editBtns[i]).on(
            "click",
            function() {
                editModalShow(id);
            });
    };
});