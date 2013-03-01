$(document).ready(function () {
    $("#datepicker").datepicker({
        buttonImage: '/Content/images/calendar.gif',
        buttonImageOnly: true,
        buttonText: "Change date of booking",
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        onSelect: function (date, instance) {
            window.location = "http://local.rhmgdiary/rehearsal/" + date + "/newbooking";
        },
        dateFormat: 'dd/mm/yy'
     });
});
