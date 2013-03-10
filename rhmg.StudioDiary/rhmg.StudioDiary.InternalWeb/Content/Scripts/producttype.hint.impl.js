$(document).ready(function () {
    $("#typeSelector").change(function () {
        displayHint(this);
    });
    var displayHint = function (selection) {
        var hint = $('#typeInformation');
        if ($(selection).val() === "1") {
            hint.html('Will allow you to pick from any of the selected rooms in the product');
        } else {
            hint.html('Will book out all rooms listed in this product');
        }
    };
    displayHint("#typeSelector");
});
