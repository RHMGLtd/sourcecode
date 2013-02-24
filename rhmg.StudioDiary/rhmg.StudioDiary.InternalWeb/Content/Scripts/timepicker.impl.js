$(document).ready(function () {
    $('#startTimePicker').timepicker({
        defaultTime: '',
        hours: { starts: 10, ends: 23 },
        minutes: { interval: 15 },
        rows: 3,
        onHourShow: tpStartOnHourShowCallback,
        onMinuteShow: tpStartOnMinuteShowCallback
    });
    $('#endTimePicker').timepicker({
        defaultTime: '',
        hours: { starts: 10, ends: 23 },
        minutes: { interval: 15 },
        rows: 3,
        onHourShow: tpEndOnHourShowCallback,
        onMinuteShow: tpEndOnMinuteShowCallback
    });
});

function tpStartOnHourShowCallback(hour) {
    var tpEndHour = $('#endTimePicker').timepicker('getHour');
    // all valid if no end time selected
    if ($('#endTimePicker').val() == '') { return true; }
    // Check if proposed hour is prior or equal to selected end time hour
    if (hour <= tpEndHour) { return true; }
    // if hour did not match, it can not be selected
    return false;
}
function tpStartOnMinuteShowCallback(hour, minute) {
    var tpEndHour = $('#endTimePicker').timepicker('getHour');
    var tpEndMinute = $('#endTimePicker').timepicker('getMinute');
    // all valid if no end time selected
    if ($('#endTimePicker').val() == '') { return true; }
    // Check if proposed hour is prior to selected end time hour
    if (hour < tpEndHour) { return true; }
    // Check if proposed hour is equal to selected end time hour and minutes is prior
    if ((hour == tpEndHour) && (minute < tpEndMinute)) { return true; }
    // if minute did not match, it can not be selected
    return false;
}

function tpEndOnHourShowCallback(hour) {
    var tpStartHour = $('#startTimePicker').timepicker('getHour');
    // all valid if no start time selected
    if ($('#startTimePicker').val() == '') { return true; }
    // Check if proposed hour is after or equal to selected start time hour
    if (hour >= tpStartHour) { return true; }
    // if hour did not match, it can not be selected
    return false;
}
function tpEndOnMinuteShowCallback(hour, minute) {
    var tpStartHour = $('#startTimePicker').timepicker('getHour');
    var tpStartMinute = $('#startTimePicker').timepicker('getMinute');
    // all valid if no start time selected
    if ($('#startTimePicker').val() == '') { return true; }
    // Check if proposed hour is after selected start time hour
    if (hour > tpStartHour) { return true; }
    // Check if proposed hour is equal to selected start time hour and minutes is after
    if ((hour == tpStartHour) && (minute > tpStartMinute)) { return true; }
    // if minute did not match, it can not be selected
    return false;
}