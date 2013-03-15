$(function () {
    $("#contextMenu").dialog({
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
});

$(document).ready(function () {
    $(".booked").click(function (e) {
        e.preventDefault();
        $("#contextMenu").dialog("option", "title", $(this).attr('title'));
        $("#contextMenu").dialog("option", "position", {
            my: "left",
            at: "right",
            of: event,
            offset: "20 40"
        });
        $("#checkInBooking").attr("bookingId", $(this).attr('bookingId'));
        $("#cancelBooking").attr("bookingId", $(this).attr('bookingId'));
        $("#noShowBooking").attr("bookingId", $(this).attr('bookingId'));
        $("#editBooking").attr("bookingId", $(this).attr('bookingId'));
        $('#bookingIdInput').val($(this).attr('bookingId'));
        $('#menuItems').show();
        $('#cancellationDialogue').hide();
        $("#contextMenu").dialog("open");
    });
    $("#editBooking").click(function (e) {
        e.preventDefault();
        window.location = '/' + $(this).attr('bookingId');
    });
    $("#cancelBooking").click(function (e) {
        e.preventDefault();
        $('#menuItems').hide();
        $('#cancellationDialogue').show();
    });
    $('#cancelButton').click(function (e) {
        e.preventDefault();
        var cancellationType = $('#cancellationTypeInput').val();
        var reason = $('#reasonInput').val();
        var bookingId = $('#bookingIdInput').val();
        if (!reason) {
            alert('please provide a reason');
            return;
        }
        if (confirm('Are you sure you want to Cancel this booking?')) {
            $.ajax({
                url: '/' + bookingId + '/cancel',
                type: 'POST',
                data: { CancellationType: cancellationType, Reason: reason },
                cache: false
            }).done(function (success) {
                if (success) {
                    alert('Booking cancelled!');
                    window.location.reload(true);
                    return;
                }
                alert('Some sort of error occurred');
            });
        }
    });
    $("#noShowBooking").click(function (e) {
        e.preventDefault();
        if (confirm('Are you sure you want to No Show this booking?')) {
            $.ajax({
                url: '/' + $(this).attr('bookingId') + '/noshow',
                cache: false
            }).done(function (success) {
                if (success) {
                    alert('Booking marked as No Show!');
                    window.location.reload(true);
                    return;
                }
                alert('Some sort of error occurred');
            });
        }
    });
    $("#checkInBooking").click(function (e) {
        e.preventDefault();
        if (confirm('Are you sure you want to Check In this booking?')) {
            $.ajax({
                url: '/' + $(this).attr('bookingId') + '/CheckIn',
                cache: false
            }).done(function (success) {
                if (success) {
                    alert('Booking marked as checked in');
                    window.location.reload(true);
                    return;
                }
                alert('Some sort of error occurred');
            });
        }
    });
});