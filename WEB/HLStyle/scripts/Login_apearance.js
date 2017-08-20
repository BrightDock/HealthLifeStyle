/*$(document).ready(function () {
    $("#loginBut").click(function () {
        $('#login_panel').offset({ left: ($(window).width() - $('#login_panel').outerWidth(true) - ($(window).width() - ($('#loginBut').offset().left + $('#loginBut').outerWidth(true)))), top: ($('#login_panel').offset().top) });
        document.getElementById("loginBut").disabled = true;
        if (document.getElementById("login_panel").style.top != ($('#loginBut').outerHeight(true) - 1) + 'px') {
            $("#login_panel")
                .animate({ top: ($('#loginBut').outerHeight(true) - 1), padding: "15px 10px", opacity: "1" }, {
                    queue: false,
                    duration: 100
                });
            document.getElementById("login_panel").style.border = "1px solid #CCC7C7";
            document.getElementById("login_panel").style.zIndex = "1";

            document.getElementById("loginBut").style.backgroundColor = "#f3f3f3";
        }
        else {
            $(".login")
                .animate({ top: ($('#loginBut').outerHeight(true) - 40), padding: "10px 10px", opacity: "0" }, {
                    queue: false,
                    duration: 100,
                    specialEasing: {
                        margin: "slow"
                    }
                });
            document.getElementById("login_panel").style.border = "1px solid transparent";
            document.getElementById("login_panel").style.zIndex = "-1";
            document.getElementById("loginBut").style.backgroundColor = "transparent";

            if ($("#Login") == undefined) {
                if (document.getElementById("Login").value.length > 0)
                    document.getElementById("Login").value = "";
                if (document.getElementById("Password").value.length > 0)
                    document.getElementById("Password").value = "";
            }
        }
        document.getElementById("loginBut").disabled = false;
    });
});
*/
$(document).ready(function () {
    $("#loginBut").click(function () {
        $("#login_panel").toggleClass("l_panel-open");
        if($(window).width() <= 500)
        if ($("#login_panel").hasClass("l_panel-open")) {
            $(".content").css("filter", "blur(5px)");
        }
        else {
            $(".content").css("filter", "none");
        }
    });
});