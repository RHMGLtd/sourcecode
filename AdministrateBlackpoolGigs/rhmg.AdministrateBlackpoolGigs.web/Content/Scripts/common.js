var c = {};

(function ($) {

    c.deserialiseJSONDate = function (inDate) {
        if (c.valueIsEmpty(inDate)) {
            return "";
        }

        var saveDate = new Date(parseInt(inDate.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1')));
        var year = saveDate.getFullYear();
        var month = saveDate.getMonth() + 1 < 10 ? '0' + (saveDate.getMonth() + 1) : saveDate.getMonth() + 1;
        var day = saveDate.getDate() < 10 ? '0' + saveDate.getDate() : saveDate.getDate();

        return $.datepicker.formatDate('dd M yy', new Date(year, month - 1, day));
    }

    c.valueIsEmpty = function (val) {


        if (Date.parse(val)) {
            val = new Date(val);
        }

        var retVal = (val == null || val === "" || val === "null" || val == "__ko.bindingHandlers.options.optionValueDomData__"
            || val == "/Date(-62135596800000)/" || val == "01/01/0001 00:00:00" || val.length == 0
            || val == "Tue Jan 01 1901 00:00:00 GMT+0000 (GMT Standard Time)"
            || val == "Mon Jan 01 2001 00:00:00 GMT+0000 (GMT Standard Time)"
            || val == "Tue Jan 1 00:00:00 UTC 1901" || val == "Mon Jan 1 00:00:00 UTC 2001");
        return retVal;
    }

    c.serialiseJSONDate = function (inDate) {
        if (!inDate || inDate == "")
            inDate = "01/01/1";

        //check if a date, if so return
        if (inDate.length == null)
            return inDate;

        //if date is already serialised then ignore
        if (inDate.indexOf('Date(-') > 0)
            return inDate;

        return new Date(inDate);
    }
    $.fn.allowDatePickerClear = function () {
        this.keydown(function (e) {
            if (e.keyCode == 46 || e.keyCode == 8) {
                //Delete and backspace clear text 
                $(this).val(''); //Clear text
                $(this).datepicker("hide"); //Hide the datepicker calendar if displayed
                $(this).blur(); //aka "unfocus"
            }
            //Prevent user from manually entering in a date - have to use the datepicker box
            e.preventDefault();
        });
    };
})(jQuery);