$(function () {
    //get current culture/lang
    const culture = $("#culture").children("option:selected").val();

    function setDateTimeVal(result, date, time) {
        result.val(date.val() + "T" + time.val());
    }

    //Create Section____________________________________________//

    //get the create date, time and hidden DateTime inputs
    const createSoldDate = $("#createSoldDate");
    const createSoldTime = $("#createSoldTime");
    const createSoldDateTime = $("#createSoldDateTime");
    //set onclick for create button
    $(".create-modal-btn").on("click",
        function () {
            $("#createModal").modal("show");
        });
    //set onchange values for soldDate and soldTime
    createSoldDate.change(function() {
        setDateTimeVal(createSoldDateTime, createSoldDate, createSoldTime);
    });
    createSoldTime.change(function() {
        setDateTimeVal(createSoldDateTime, createSoldDate, createSoldTime);
    });


    //Edit Section____________________________________________//

    let editSoldDate;
    let editSoldTime;
    let editSoldDateTime;
    //get anchor tags with class edit-modal-btn
    const editBtns = $(".edit-modal-btn");

    //ajax call method for partial edit modal form
    function editModalShow(id) {
        $.ajax({
            type: "GET",
            url: "Item/Edit",
            data: { id: id },
            success: function (response) {
                $("#editModal .modal-body").html(response);

                //get the sold date and time inputs
                editSoldDate = $("#editSoldDate");
                editSoldTime = $("#editSoldTime");
                editSoldDateTime = $("#editSoldDateTime");
                //set onchange methods for Date and Time inputs
                editSoldDate.change(function() {
                    setDateTimeVal(editSoldDateTime, editSoldDate, editSoldTime);
                });
                editSoldTime.change(function () {
                    setDateTimeVal(editSoldDateTime, editSoldDate, editSoldTime);
                });

                //editSoldDate.val(editSoldDateTime.val().getFullYear() + "-" + (editSoldDateTime.val().getMonth() + 1) + "-" + editSoldDateTime.val().getDate());
                //editSoldTime.val(editSoldDateTime.val().getTime());
                $("#editModal").modal("show");
            }
        });
    }

    //set onclick methods for items in editBtns
    for (let i = 0; i < editBtns.length; i++) {
        const id = $(editBtns[i]).attr("data-id");
        $(editBtns[i]).on(
            "click",
            function () {
                editModalShow(id);
            });
    };


    //Delete Section____________________________________________//

    //get anchor tags with class delete-btn
    const deleteBtns = $(".delete-btn");
    //set onclick methods for items in editBtns
    for (let i = 0; i < deleteBtns.length; i++) {
        const id = $(deleteBtns[i]).attr("data-id");
        $(deleteBtns[i]).on(
            "click",
            function () {
                if ($(deleteBtns[i]).css("background-color") === "rgba(255, 50, 50, 0.2)") {
                    $.ajax({
                        type: "DELETE",
                        url: "Item/Delete",
                        data: { id: id },
                        success: function (result) {
                            window.location.replace(result.redirect);
                            window.location.reload();
                        }
                    });
                } else {
                    const anchor = ($(deleteBtns[i]).children())[0];
                    $(deleteBtns[i]).css({
                        "background-color": "rgba(255, 50, 50, 0.2)",
                    });

                    let timer = 5;
                    const countdown = setInterval(function () {
                        if (timer === 0) {
                            culture === "sq" ? $(anchor).text("Fshij") : $(anchor).text("Delete");
                                $(deleteBtns[i]).removeAttr("style");
                                clearInterval(countdown);
                            } else {
                                $(anchor).text(timer);
                                timer--;
                            }
                        },
                        1000);
                }
            });
    };

});