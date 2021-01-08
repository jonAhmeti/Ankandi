$(function () {


    const url_array = document.URL.split("?id="); // Get current url
    const id = url_array[url_array.length - 1];  // Get the last part of the array (-1)

    const bidInput = $("#BidInput");

    $.ajax({
        type: "GET",
        url: "/Bidder/GetEventDetails",
        data: { id: id },
        success: function(response) {
            $(bidInput).on("change paste keydown keyup",
                function(e) {
                    if ($(this).val() < response.currentPrice + response.minPriceIncrementAmount) {
                        $(this).val(response.currentPrice + response.minPriceIncrementAmount);
                    }
                });
            $(bidInput).val(response.currentPrice + response.minPriceIncrementAmount);
        }
    });
});