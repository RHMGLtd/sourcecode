$(document).ready(function () {
    $("#AddPhoneNumber").click(function () {
        $('#PhoneNumbers').append('<div>Secondary Phone Number :: <input id="SecondaryPhoneNumbers" type="text" name="SecondaryPhoneNumbers" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
    $("#AddAddressLine").click(function () {
        $('#Address').append('<div>Address Line :: <input id="AddressLines" type="text" name="AddressLines" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
});
function remove(o) {
    $(o).parent().remove();
};

function my_delete(id) {
    if (confirm('are you sure you want to delete this metadata?')) {
        if (confirm("i don't believe you!!! you're sure?")) {
            $.ajax({ type: "DELETE", url: "/metadata/" + id,
                success: function () {
                    alert("done");
                    window.location = "/venues";
                },
                error: function () {
                    alert("oooops something went wrong.... i'm sorry");
                }
            });
        }
    }
}