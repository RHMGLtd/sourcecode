$(document).ready(function () {
    $("#weekpicker").datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        buttonImage: '/Content/images/calendar.gif',
        buttonImageOnly: true,
        buttonText: "Change date of booking",
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        onSelect: function (date, instance) {
            window.location = "http://local.rhmgdiary/rehearsal/" + date + "/newbooking";
        },
        beforeShow: function () {
            $(".ui-state-default").on("mouseenter", function () {
                Debug.write("jobby");
            });
        },
        dateFormat: 'dd/mm/yy'
    });
});


//$(document).ready(function () {
//    //var startDate;
//    //var endDate;

//    //var selectCurrentWeek = function () {
//    //    window.setTimeout(function () {
//    //        $('#weekpicker').find('.ui-datepicker-current-day a').addClass('ui-state-active');
//    //    }, 1);
//    //};
//    $("#weekpicker").datepicker({
//        buttonImage: '/Content/images/calendar.gif',
//        buttonImageOnly: true,
//        showOtherMonths: true,
//        selectOtherMonths: true,
//        onSelect: function (dateText, inst) {
//            //var date = $(this).datepicker('getDate');
//            //startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
//            //endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
//            //var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
//            //$('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
//            //$('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));
//            alert(dateText);
//            //selectCurrentWeek();
//        },
//        beforeShowDay: function (date) {
//            var cssClass = '';
//            if (date >= startDate && date <= endDate)
//                cssClass = 'ui-datepicker-current-day';
//            return [true, cssClass];
//        },
//        onChangeMonthYear: function (year, month, inst) {
//            selectCurrentWeek();
//        }
//    });
//});