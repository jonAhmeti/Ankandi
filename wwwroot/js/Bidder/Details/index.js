$(function () {

    const urlArray = document.URL.split("?id="); // Get current url
    const eventId = urlArray[urlArray.length - 1];  // Get the last part of the array (-1)

    const bidInput = $("#BidInput");
    let event;
    function setMinBidInput() {
        $.ajax({
            type: "GET",
            url: "/Bidder/GetEventDetails",
            data: { id: eventId },
            success: function (response) {
                event = response;
                $(bidInput).on("change paste keydown keyup",
                    function (e) {
                        if ($(this).val() < response.currentPrice + response.minPriceIncrementAmount) {
                            $(this).val(response.currentPrice + response.minPriceIncrementAmount);
                        }
                    });
                $(bidInput).val(response.currentPrice + response.minPriceIncrementAmount);
            }
        });
    }

    setMinBidInput();

    $("#bid").on("click",
        function() {
            $.ajax({
                type: "POST",
                url: "/Bidder/Bid",
                data: {
                    auctionId: event.auctionId,
                    eventId: event.id,
                    bidAmount: $("#BidInput").val(),
                    username: $("#dropdownProfile").text().trim()
                },
                success: function(response) {
                    if (response.priceChanged) {
                        $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                        $("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                        setMinBidInput();
                        $("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                        $("#infoCurrentPrice").css("padding-left",".5em")
                            .html(
                                '<i class="fas fa-exclamation-triangle"></i> Someone else placed a bid before the price refreshed!<br />');
                    } else {
                        $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                        $("#CurrentPrice").css({ "background-color": "rgba(0,0,255,.7)" });
                        setMinBidInput();
                        $("#CurrentPrice").animate({ backgroundColor: "rgba(0,255,0,0)" }, 1500);
                        $("#infoCurrentPrice").removeAttr("style").html("");
                    }
                }
            });
        });

    $("#withdraw").on("click",
        function() {
            $.ajax({
                type: "POST",
                url: "/Bidder/Withdraw",
                data: {
                    auctionId: event.auctionId,
                    eventId: event.id,
                    username: $("#dropdownProfile").text().trim()
                },
                success: function (response) {
                    if (response !== null) {
                        $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                        $("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                        setMinBidInput();
                        $("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                    }
                }
            });
        });

    const refresh = setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Bidder/GetEventDetails",
            data: {
                id: event.id,
                username: $("#dropdownProfile").text().trim(),
                price: event.currentPrice
            },
            success: function(response) {
                if (response.priceChanged) {
                    $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                    //$("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                    setMinBidInput();
                    //$("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                    $("#updatedCurrentPrice").css({ "opacity": 1, "color": "#ffc107" });
                    $("#updatedCurrentPrice").animate({ opacity: 0 }, 1500);
                } else {
                    $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                    setMinBidInput();
                    $("#updatedCurrentPrice").css({ "opacity": 1, "color": "#17a2b8" });
                    $("#updatedCurrentPrice").animate({ opacity: 0 }, 1500);
                }
            }
        });
    }, 3000);

});