$(function () {


    //Create Section____________________________________________//
    function setDateTimeVal(result, date, time) {
        result.val(date.val() + "T" + time.val());
    }

    var createStartDate = $("#createStartDate");
    var createStartTime = $("#createStartTime");
    var createStartDateTime = $("#createStartDateTime");
    var createEndDate = $("#createEndDate");
    var createEndTime = $("#createEndTime");
    var createEndDateTime = $("#createEndDateTime");

    function setInitialCreateValues(today) {
        //set initial start date value
        createStartDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate());
        //set initial end date value
        createEndDate.val(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate() + 1));
        //set initial start DateTime value
        setDateTimeVal(createStartDateTime, createStartDate, createStartTime);
        //set initial end DateTime value
        setDateTimeVal(createEndDateTime, createEndDate, createEndTime);

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
    }

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


    $("a.create-modal-btn").on("click",
        function () {
            const today = new Date();
            console.log("inside btnClick");
            setInitialCreateValues(today);
            $("#createModal").modal('show');
        });





    //Edit Section____________________________________________//

    var editStartDate;
    var editStartTime;
    var editStartDateTime;
    var editEndDate;
    var editEndTime;
    var editEndDateTime;

    //get html objects
    function initEditObjects() {
        editStartDate = $("#editStartDate"); //start date input
        editStartTime = $("#editStartTime"); //start time input
        editStartDateTime = $("#editStartDateTime"); //hidden input
        editEndDate = $("#editEndDate"); //end date input
        editEndTime = $("#editEndTime"); //end time input
        editEndDateTime = $("#editEndDateTime"); //hidden input
    }

    //set onchange methods
    function setEditOnChangeMethods() {
        editStartDate.change(function() {
            setDateTimeVal(editStartDateTime, editStartDate, editStartTime);
        });
        editStartTime.change(function() {
            setDateTimeVal(editStartDateTime, editStartDate, editStartTime);
        });
        editEndDate.change(function() {
            setDateTimeVal(editEndDateTime, editEndDate, editEndTime);
        });
        editEndTime.change(function() {
            setDateTimeVal(editEndDateTime, editEndDate, editEndTime);
        });
    }


    //ajax call method for partial edit modal form
    function editModalShow(id) {
        $.ajax({
            type: "GET",
            url: "Auction/Edit",
            data: { id: id },
            success: function(response) {
                $("#editModal .modal-body").html(response);

                //get edit objects
                initEditObjects();

                //set hidden inputs initial value
                setDateTimeVal(editStartDateTime, editStartDate, editStartTime);
                setDateTimeVal(editEndDateTime, editEndDate, editEndTime);

                setEditOnChangeMethods();

                $("#editModal").modal("show");
            }
        });
    }

    //get anchor tags with class edit-modal-btn
    const editBtns = $("a.edit-modal-btn");
    //set onclick methods for items in editBtns
    for (let i = 0; i < editBtns.length; i++) {
        const id = $(editBtns[i]).attr("data-id");
        $(editBtns[i]).on(
            "click",
            function() {
                editModalShow(id);
            });
    };


});