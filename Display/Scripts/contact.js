jQuery(document).ready(function () {
    $('.help-inline').hide();
    jQuery.validator.messages.required = "";
    jQuery("form").validate({
        rules: {
            name: "required",
            email: "required",
            subject: "required",
            message: "required"
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parent().addClass("error").find("span").show();
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parent().removeClass("error").find("span").hide();
        },
        onkeyup: false,
        submitHandler: function () {
            alert("in submit handler");
            element.parent().removeClass("error");
            alert("submit! use link below to go to the other step");
        }
    });
});