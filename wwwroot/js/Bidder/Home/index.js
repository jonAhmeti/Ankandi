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

                $(`#row_${rowNum}`).append(`<div class="card border">
                                        <img src="${objItem.image}" class="card-img-top" alt="Item picture: ${objItem
                                            .image}">
                                        <div class="card-body">
                                            <h4 class="card-title" style="font-weight:bold;">${objItem.name}</h5>
                                            <h6 style="color:#00A8E8;font-weight:bold;margin:0;">Current price: ${objEventDetails.currentPrice}€</h6>
                                            <h7 style="color:#294D4A;">Start price: ${objItem.startPrice}</h7>
                                            <form class="w-100 input-group mt-2">
                                                <a href="#" class="btn btn-primary my-1">Details</a>
                                            </form>
                                            <form class="w-100 input-group">
                                                <a href="#" class="btn btn-primary form-control">Bid by</a>
                                                <div class="input-group-append">
                                                    <span class="input-group-text">${objEventDetails.minPriceIncrementAmount}€</span>
                                                </div>
                                            </form>
                                        </div>
                                     </div>`);
                iterator === 4 ? iterator = 0 : iterator++;
            }
        }

    });
})