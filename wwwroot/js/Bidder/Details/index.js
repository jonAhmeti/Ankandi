$(function () {
    //get current culture/lang
    const culture = $("#culture").children("option:selected").val();

    const urlArray = document.URL.split("?id="); // Get current url
    const eventId = urlArray[urlArray.length - 1];  // Get the last part of the array (-1)

    const bidInput = $("#BidInput");
    const bidBtn = $("#bid");

    function btnInputValidation(event) {
        //remove all earlier events so they don't get called twice
        $(bidInput).off();
        //bidInput onChange.. disable/enable button
        $(bidInput).on("change paste keyup blur",
            function () {
                if ($(this).val() < event.currentPrice + event.minPriceIncrementAmount) {
                    $(bidBtn).attr("disabled", true);
                    $(this).css({ "color": "red"});
                } else {
                    $(bidBtn).removeAttr("disabled");
                    $(this).removeAttr("style");
                }
            });
    }

    function validateInput(event) {
        if ($(bidInput).val() < event.currentPrice + event.minPriceIncrementAmount) {
            $(bidBtn).attr("disabled", true);
            $(bidInput).css({ "color": "red" });
        } else {
            $(bidBtn).removeAttr("disabled");
            $(bidInput).removeAttr("style");
        }
        $("#infoIncrement").text(event.currentPrice + event.minPriceIncrementAmount);
    }

    let refresh;
    function getEventDetails() {
        $.ajax({
            type: "GET",
            url: "/Bidder/GetEventDetails",
            data: { id: eventId },
            success: function (response) {

                //set validation onFirstLoad
                btnInputValidation(response);

                //every 3 seconds get updated info
                refresh = setInterval(function() {
                    $.ajax({
                        type: "GET",
                        url: "/Bidder/GetEventDetails",
                        data: {
                            id: response.id,
                            price: response.currentPrice
                        },
                        success: function (response) {
                            btnInputValidation(response);
                            $("#CurrentPrice").text(response.currentPrice);
                            if (response.priceChanged) {
                                $("#updatedCurrentPrice").css({ "opacity": 1, "color": "#ffc107" });
                                $("#updatedCurrentPrice").animate({ opacity: 0 }, 1500);
                            } else {
                                $("#updatedCurrentPrice").css({ "opacity": 1, "color": "#17a2b8" });
                                $("#updatedCurrentPrice").animate({ opacity: 0 }, 1500);
                            }
                            validateInput(response);
                        }
                    });
                }, 3000);


                $("#bid").on("click",
                    function () {
                        $.ajax({
                            type: "POST",
                            url: "/Bidder/Bid",
                            data: {
                                auctionId: response.auctionId,
                                eventId: response.id,
                                bidAmount: $("#BidInput").val(),
                                username: $("#dropdownProfile").text().trim()
                            },
                            success: function (response) {
                                if (response.priceChanged) {
                                    $("#CurrentPrice").text(response.currentPrice);
                                    $("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                                    $("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                                    $("#infoCurrentPrice").css("padding-left", ".5em")
                                    culture === "sq"
                                        ? $("#infoCurrentPrice")
                                        .html(
                                            '<i class="fas fa-exclamation-triangle"></i> Çmimi ka ndryshuar para se të përditësohet!<br />')
                                        : $("#infoCurrentPrice").html(
                                            '<i class="fas fa-exclamation-triangle"></i> Someone else placed a bid before the price refreshed!<br />');
                                } else {
                                    $("#CurrentPrice").text(response.currentPrice);
                                    $("#CurrentPrice").css({ "background-color": "rgba(0,0,255,.7)" });
                                    $("#CurrentPrice").animate({ backgroundColor: "rgba(0,255,0,0)" }, 1500);
                                    $("#infoCurrentPrice").removeAttr("style").html("");
                                }
                                validateInput(response);
                            }
                        });
                    });

                $("#withdraw").on("click",
                    function () {
                        $.ajax({
                            type: "POST",
                            url: "/Bidder/Withdraw",
                            data: {
                                auctionId: response.auctionId,
                                eventId: response.id
                            },
                            success: function (response) {
                                if (response !== null) {
                                    $("#CurrentPrice").text(response.currentPrice);
                                    $("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                                    $("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                                }
                                validateInput(response);
                            }
                        });
                    });
            }
        });
    }

    getEventDetails();
});