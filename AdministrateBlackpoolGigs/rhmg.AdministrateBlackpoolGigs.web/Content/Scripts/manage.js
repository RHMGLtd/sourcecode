$(document).ready(function () {
    $("#AddBand").click(function () {
        var date = new Date()
        var ticks = date.getTime()
        $('#Bands').append('<div>Band Name :: <input id="BandName' + ticks.toString() + '" type="text" name="BandNames" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div> :: <span>we don&apos;t need to put start times on for each band</span>');
        autoBobbins($('#BandName' + ticks.toString()), "BandNames");
        return false;
    });

    autoBobbins($('#Venue'), "Venues");
    autoBobbins($('#BandName'), "BandNames");
});
function remove(o) {
    $(o).parent().remove();
};
function autoBobbins(e, which) {
    e.autocomplete({
        source: function (req, add) {
            $.getJSON("/lookup/" + which + "?Term=" + e.val().replace(" ", "%20"), function (data) {
                var suggestions = [];
                $.each(data, function (i, val) {
                    suggestions.push({
                        value: val
                    });
                });
                add(suggestions);
            });
        },
        minLength: 2,
        select: function (event, ui) {
            e.val(ui.item.value);
        }
    });
};
function my_delete(id) {
    if (confirm('are you sure you want to delete this gig?')) {
        if (confirm("i don't believe you!!! you're sure?")) {
            $.ajax({ type: "DELETE", url: "/" + id,
                success: function () {
                    alert("done");
                    window.location = window.location;
                },
                error: function () {
                    alert("oooops something went wrong.... i'm sorry");
                }
            });
        }
    }
}