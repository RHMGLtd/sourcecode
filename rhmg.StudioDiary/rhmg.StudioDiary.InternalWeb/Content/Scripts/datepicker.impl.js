$(document).ready(function () {
    $("#datepicker").datepicker({
        buttonImage: '/Content/images/calendar.gif',
        buttonImageOnly: true,
        buttonText: "Change date of booking",
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        onSelect: function (dateText, instance) {
            var day, month, year;
            var date = new Date(dateText);
            day = date.getDate();
            var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
               "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            month = months[date.getMonth()];
            year = date.getFullYear();
            $('#monthTitle').html(day + ' ' + month + ' ' + year);
            $('#dateInput').val(day + ' ' + month + ' ' + year);

            $.ajax({
                type: "GET",
                url: "/lookup/occupancy/for/day/" + day + '/' + month + '/' + year
            }).done(function (html) {
                if (html) {
                    $('#currentBookingsForDay').html(html);
                }
            });
        }
    });
});
