$(document).ready(function () {
    $("#roomSelector").change(function () {
        var val = $(this).val();
        var select = $('#rateSelector');
        if (!val || val === '') {
            select.children().remove();
            select.append($('<option />').val('').text('... select a room to get available rates'));
            return;
        }
        //
        $.ajax({
            url: '/lookup/rates/for/' + val,
            cache: false,
            dataType: 'json'
        }).done(function (rates) {
            select.children().remove();
            $.each(rates, function () {
                select.append($('<option />').val(this.Id).text(this.Description));
            });
        });
    });
});
