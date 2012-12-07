$(document).ready(function () {
    $("#AddContact").click(function () {
        $('#OtherContacts').append('<div>An additional contact for your band :: <input id="OtherContact" type="text" name="OtherContacts" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
    $("#AddLink").click(function () {
        $('#OtherLinks').append('<div>Another web link relevant to your band :: <input id="OtherLink" type="text" name="OtherLinks" class="input" minlength="2"/> :: <button onclick="remove(this); return false;">remove</button></div>');
        return false;
    });
    var Event = YAHOO.util.Event,
    Dom = YAHOO.util.Dom,
    lang = YAHOO.lang,
    slider,
    bg = "slider-bg", thumb = "slider-thumb"

    // The slider can move 0 pixels up
    var topConstraint = 0;

    // The slider can move 200 pixels down
    var bottomConstraint = 200;

    // Custom scale factor for converting the pixel offset into a real value
    var scaleFactor = 0.5;

    // The amount the slider moves when the value is changed with the arrow
    // keys
    var keyIncrement = 20;

    var tickSize = 20;

    Event.onDOMReady(function () {

        slider = YAHOO.widget.Slider.getHorizSlider(bg,
    thumb, topConstraint, bottomConstraint, 20);

        // Sliders with ticks can be animated without YAHOO.util.Anim
        slider.animate = true;

        slider.getRealValue = function () {
            return Math.round(this.getValue() * scaleFactor);
        }
        //slider.setValue(, false); //false here means to animate if possible

        slider.subscribe("change", function (offsetFromStart) {

            // use the scale factor to convert the pixel offset into a real
            // value
            var actualValue = slider.getRealValue();

            // Update the title attribute on the background.  This helps assistive
            // technology to communicate the state change
            Dom.get(bg).title = "slider value = " + actualValue;
            $('#CoverPercentage').val(actualValue);
            $('#OriginalPercentage').val(100 - actualValue);
        });

        slider.subscribe("slideStart", function () {
            YAHOO.log("slideStart fired", "warn");
        });

        slider.subscribe("slideEnd", function () {
            YAHOO.log("slideEnd fired", "warn");
        });
        slider.setValue(100);
    });
});
function remove(o) {
    $(o).parent().remove();
};