$(document).ready(function () {
    $("#roomSelector").change(function () {
        $.ajax({
            url: '/lookup/rates/for/' + $(this).val(),
            cache: false,
            dataType: 'json'
        }).done(function (rates) {
            var select = $('#rateSelector');
            select.children().remove();
            $.each(rates, function () {
                select.append($('<option />').val(this.Id).text(this.Description));
            });
        });
    });
});
