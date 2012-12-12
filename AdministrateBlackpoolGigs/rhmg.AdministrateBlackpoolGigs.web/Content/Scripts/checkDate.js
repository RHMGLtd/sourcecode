function checkDate() {
    if (c.valueIsEmpty($('.Date').val())) {
        return;
    }
    dateCheckModel.checkDate = c.serialiseJSONDate($('.Date').val());
    jsonData = ko.utils.stringifyJson(dateCheckModel);
    $.ajax({ type: "PUT", url: "/CheckDate",
        data: jsonData,
        contentType: "application/json",
        success: function (data) {
            $('#checkResults').empty();
            if (data.Diary.Gigs.length > 0) {
                for (var i in data.Diary.Gigs) {
                    $('#checkResults').append("<span>"+ data.Diary.Gigs[i].Summary + "</span> <a href='/" + data.Diary.Gigs[i].Id + "'>edit</a> || <a href='javascript:void(0)' onclick=my_delete('" + data.Diary.Gigs[i].Id + "')>delete</a><hr />")
                }
                return;
            }
            $('#checkResults').append('<div>none to display for this date.</div><hr />')
        },
        error: function () {
            alert("oooops something went wrong.... i'm sorry");
        }
    });
}