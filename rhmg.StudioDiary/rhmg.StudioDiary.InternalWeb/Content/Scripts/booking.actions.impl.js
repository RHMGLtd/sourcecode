$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        show: {
            effect: "fade",
            duration: 500
        },
        hide: {
            effect: "fade",
            duration: 500
        },
        resizable: false
    });
    $(".booked").click(function (e) {
        e.preventDefault();
        $("#dialog").dialog("option", "title", $(this).attr('title'));
        $("#dialog").dialog("option", "position", {
            my: "left",
            at: "right",
            of: event,
            offset: "20 40"
        });
        $("#arriveBooking").attr("bookingId", $(this).attr('bookingId'));

        $("#cancelBooking").attr("bookingId", $(this).attr('bookingId'));

        $("#noShowBooking").attr("bookingId", $(this).attr('bookingId'));

        $("#editBooking").attr("bookingId", $(this).attr('bookingId'));

        $("#dialog").dialog("open");
    });
    $("#editBooking").click(function (e) {
        e.preventDefault();
        window.location = '/' + $(this).attr('bookingId');
    });
});
