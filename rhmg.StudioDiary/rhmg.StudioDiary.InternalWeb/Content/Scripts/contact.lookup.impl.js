$(document).ready(function () {
    $("#phoneNumberLookup").click(function () {
        var value = $("#phoneNumberInput").val();
        if (value) {
            $.ajax({
                type: "POST",
                url: "/lookup/contact/from/phoneNumber",
                data: { PhoneNumber: value }
            }).done(function (contact) {
                if (contact) {
                    $('#contactIdInput').val(contact.Id);
                    $('#mainContactNameInput').val(contact.MainContactName);
                    $('#bandNameInput').val(contact.Name);
                    $('#emailAddressInput').val(contact.EmailAddress);
                    $('#outstandingOwings').html(contact.CurrentOwings);
                    $('#contactInformationPanel').show();
                    $('#contactInformationPanel').effect("highlight", {}, 3000);
                    return;
                }
                $("#phoneNumberLookup").html("new customer... please fill in below");
            });
        }
    });
});
