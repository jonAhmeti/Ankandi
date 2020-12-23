$(function () {

    //Create Section____________________________________________//
    function setDateTimeVal(result, date, time) {
        result.val(date.val() + "T" + time.val());
    }

    const createStartDate = $("#createStartDate");
    const createStartTime = $("#createStartTime");
    const createStartDateTime = $("#createStartDateTime");
    const createEndDate = $("#createEndDate");
    const createEndTime = $("#createEndTime");
    const createEndDateTime = $("#createEndDateTime");

    //set start DateTime value on Date and Time change
    createStartDate.change(function () {
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
    });
    createStartTime.change(function () {
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
    });
    //set end DateTime value on Date and Time change
    createEndDate.change(function () {
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);
    });
    createEndTime.change(function () {
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);
    });

    //function to set date and time to today, parameter given on button click
    function setInitialCreateValues(today) {
        //set initial start date value
        createStartDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate());
        //set initial end date value
        createEndDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate() + 1));
        //set initial start time value
        createStartTime.val(
            (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
            ":" +
            (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));
        //set initial end time value
        createEndTime.val(
            (today.getHours() < 10 ? `0${today.getHours()}` : today.getHours()) +
            ":" +
            (today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes()));
        //set initial start DateTime value
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
        //set initial end DateTime value
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);

    }

    //set onclick for create button
    $(".create-modal-btn").on("click",
        function () {
            const today = new Date();
            console.log("inside btnClick");
            setInitialCreateValues(today);
            $("#createModal").modal("show");
        });




    //Edit Section____________________________________________//


    //Delete Section____________________________________________//
});