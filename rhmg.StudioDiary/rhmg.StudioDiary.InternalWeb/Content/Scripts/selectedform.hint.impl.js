$(document).ready(function () {
    $("#formSelector").change(function () {
        displayHint(this);
    });
    var displayHint = function (selection) {
        var hint = $('#formInformation');
        if ($(selection).val() === "1") {
            hint.html('The standard booking form, used normally for rehearsals or recordings');
        } else if ($(selection).val() === "2") {
            hint.html('The extended booking form, used for experiences and parties');
        } else {
            hint.html('The abbreviated booking form, used for studio closures and other non-standard events');
        }
    };
    displayHint("#formSelector");
});
