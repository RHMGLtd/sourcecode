$(document).ready(function () {
    $("#AddContact").click(function () {
        $('#OtherContacts').append('<div>Other Contact :: <input id="OtherContact" type="text" name="OtherContacts" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
    $("#AddLink").click(function () {
        $('#OtherLinks').append('<div>Other Web Link :: <input id="OtherLink" type="text" name="OtherLinks" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
});
function remove(o) {
    $(o).parent().remove();
};