$(function() {

    $.ajax({
        type: "GET",
        url: "Bidder/GetActiveAuctionDetails",
        success: function(response) {
            if ("closed" in response) {
                $(".card-group").html(
                    '<h1 style="color:black;width:100%;text-align:center">Auction closed.</h1>' +
                    '<h3 style="width:100%;text-align:center">Please wait for results.</h3>');
            } else {
                console.log(response);
                const cardGroup = $(".card-group");
                $(cardGroup).html("");
                let iterator = 0;
                let rowNum = 0;
                for (let objItem of response.items) {
                    let objEventDetails;
                    for (let objEvent of response.events) {
                        if (objEvent.itemId === objItem.id) {
                            objEventDetails = objEvent;
                        }
                    }
                    if (iterator === 0) {
                        rowNum++;
                        $(cardGroup).append(`<div class="row row-cols-5" id="row_${rowNum}"></div>`);
                    }

                    $(`#row_${rowNum}`).append(
                        `<div class="card border" id="card_${iterator}" data-id="${objEventDetails.id}">
                                        <img src="${objItem.image}" class="card-img-top" alt="Item picture: ${objItem
                        .image}">
                                        <div class="card-body">
                                            <h4 class="card-title" style="font-weight:bold;">${objItem.name}</h5>
                                            <h6 style="color:#00A8E8;font-weight:bold;margin:0;">Current price:<span id="currentPrice_${
                        iterator}"> ${
                        objEventDetails.currentPrice}</span>€</h6>
                                            <h7 style="color:#294D4A;">Start price: ${objItem.startPrice}</h7>
                                            <form class="w-100 input-group mt-2">
                                                <a class="btn btn-primary my-1">Details</a>
                                            </form>
                                            <form class="w-100 input-group">
                                                <a class="btn btn-primary form-control">Bid</a>
                                                <div class="input-group-append">
                                                    <span class="input-group-text">${objEventDetails
                        .minPriceIncrementAmount}€</span>
                                                </div>
                                            </form>
                                        </div>
                                     </div>`);
                    iterator === 4 ? iterator = 0 : iterator++;
                }

                const detailsBtns = ("div.card a:contains(Details)");
                for (let i = 0; i < response.events.length; i++) {
                    const id = $("div#card_" + i).attr("data-id");
                    $($(detailsBtns)[i]).attr("href", "/Bidder/Details?id=" + id);
                }

                const minimumBidBtns = ("div.card a:contains(Bid)");
                for (let i = 0; i < response.events.length; i++) {
                    $($(minimumBidBtns)[i]).on("click",
                        function () {
                            $.ajax({
                                type: "POST",
                                url: "/Bidder/Bid",
                                data: {
                                    auctionId: response.events[i].auctionId,
                                    eventId: response.events[i].id,
                                    bidAmount: response.events[i].currentPrice + response.events[i].minPriceIncrementAmount,
                                    username: $("#dropdownProfile").text().trim()
                                },
                                success: function (response) {
                                    if (response.priceChanged) {
                                        $("#CurrentPrice").text(`Current price: ${response.currentPrice}€`);
                                        $("#CurrentPrice").css({ "background-color": "rgba(255,255,0,.7)" });
                                        setMinBidInput();
                                        $("#CurrentPrice").animate({ backgroundColor: "rgba(255,255,0,0)" }, 1500);
                                        $("#infoCurrentPrice").css("padding-left", ".5em")
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
                }
            }

        }
    });

    const refresh = setInterval(function() {
            $.ajax({
                type: "GET",
                url: "Bidder/GetActiveAuctionDetails",
                success: function(response) {
                    if ("closed" in response) {
                        $(".card-group").html(
                            '<h1 style="color:black;width:100%;text-align:center">Auction closed.</h1>' +
                            '<h3 style="width:100%;text-align:center">Please wait for results.</h3>');
                    } else {
                        let i = 0;
                        for (let obj of response.events) {
                            $(`#currentPrice_${i}`).text(`${obj.currentPrice}`);
                            i++;
                        }
                    }
                }
            });
        },
        3000);
})