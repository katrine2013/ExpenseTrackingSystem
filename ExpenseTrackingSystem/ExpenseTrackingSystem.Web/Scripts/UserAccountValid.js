$(document).ready(function () {
    if ($("div.alert-success").length > 0) {
        $("#Login").attr("disabled", "disabled");
        $("#Login").attr("disabled", "disabled");
        $("#ConfirmPassword").parent().parents("div.control-group").detach();
        $("#Password").parent().parents("div.control-group").detach();
        $("button.btn-large").addClass("disabled");
    }

    $("#registrationForm").validate({
        rules:
                {
                    Login:
                        {
                            required: true,
                            minlength: 4
                        },
                    Password:
                        {
                            required: true,
                            minlength: 6
                        },
                    ConfirmPassword:
                        {
                            required: true,
                            equalTo: "#Password"
                        }
                },
        messages: {
            Login:
                    {
                        required: "Your account name",
                        minlength: "Create account name longer than 4 chars."
                    },
            Password:
                    {
                        required: "Fill in password",
                        minlength: "Password should be longer, than 6 chars!"
                    },
            ConfirmPassword:
                    {
                        required: "Repeat your password.",
                        equalTo: "The value of password do not match."
                    }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
            element.parent().parents("div.control-group").addClass("error");
            error.addClass("help-inline");
        },
        success: function (element) {
            element.parent().parents("div.control-group").removeClass("error");

        }
    });

});