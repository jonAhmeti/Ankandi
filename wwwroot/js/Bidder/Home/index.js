$(function() {

    $.ajax({
        type: "GET",
        url: "Bidder/GetActiveAuctionDetails",
        success: function(response) {
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

                $(`#row_${rowNum}`).append(`<div class="card border" id="card_${iterator}" data-id="${objEventDetails.id}">
                                        <img src="${objItem.image}" class="card-img-top" alt="Item picture: ${objItem
                                            .image}">
                                        <div class="card-body">
                                            <h4 class="card-title" style="font-weight:bold;">${objItem.name}</h5>
                                            <h6 style="color:#00A8E8;font-weight:bold;margin:0;">Current price: ${objEventDetails.currentPrice}€</h6>
                                            <h7 style="color:#294D4A;">Start price: ${objItem.startPrice}</h7>
                                            <form class="w-100 input-group mt-2">
                                                <a class="btn btn-primary my-1">Details</a>
                                            </form>
                                            <form class="w-100 input-group">
                                                <a class="btn btn-primary form-control">Bid</a>
                                                <div class="input-group-append">
                                                    <span class="input-group-text">${objEventDetails.minPriceIncrementAmount}€</span>
                                                </div>
                                            </form>
                                        </div>
                                     </div>`);
                iterator === 4 ? iterator = 0 : iterator++;
            }

            const detailsBtns = ("div.card a:contains(Details)");
            console.log($(detailsBtns));
            for (let i = 0; i < response.events.length; i++) {
                const id = $("div#card_" + i).attr("data-id");
                console.log($($(detailsBtns)[i]));
                $($(detailsBtns)[i]).attr("href", "/Bidder/Details?id=" + id);
            }
        }
    });
})