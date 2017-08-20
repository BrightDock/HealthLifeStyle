$(document).ready(function () {
    $(document).keypress(function (e) {
        var searchField = $("#Central_block_central_col_search_box");
        if (e.which == 13 && searchField.val() != "Что найти?" && searchField.val() != "" && $("#search_box").is(":focus")) {
            __doPostBack('ctl00$search', '');
        }
    });
});

$(document).ready(function () {
    var sButton = $("Central_block_central_col_search_button");
    sButton.click(function () {
        window.location = "/Search/" + document.getElementById("Central_block_central_col_search_box").value;
    });
});

$(document).ready(function () {
    $("#search").click(function () {
        search_visibility();
    })

function search_visibility(){
    if (document.getElementById("search_box").value == "Что найти?" || document.getElementById("search_box").value.length == 0) {
        if (document.getElementById("search_panel").style.width != "240px") {
            $("#search_panel")
                .animate({ width: "240px", opacity: "1" }, {
                    queue: false,
                    duration: 100
                });
            document.getElementById("search_panel").style.border = "1px solid #CCC7C7";
            document.getElementById("search_panel").style.zIndex = "1";
            document.getElementById("search_box").value = "Что найти?";
            document.getElementById("search_box").style.fontSize = "medium";
            document.getElementById("search_box").style.opacity = "0.5"; 
            document.getElementById("search").getElementsByTagName("i")[0].style.color = "#8b8bef";
        }
        else {
            $(".search_panel")
                .animate({ width: "0px", opacity: "0" }, {
                    queue: false,
                    duration: 100
                });
            document.getElementById("search_panel").style.border = "1px solid transparent";
            document.getElementById("search_panel").style.zIndex = "0";
            document.getElementById("search").getElementsByTagName("i")[0].style.color = "";
        }
    }
    else {
//        document.getElementById("search").click();
        window.location = "/Search/" + document.getElementById("search_box").value;
    }
    $('#search_panel').offset({ left: $('#search').offset().left - 240, top: ($('#search').offset().top) });
}

document.getElementById("search_box").addEventListener("focusin", search_box_visible);
document.getElementById("search_box").addEventListener("focusout", search_box_visible);
    
function search_box_visible() {
    if (document.getElementById("search_box").value == "Что найти?") {
        document.getElementById("search_box").value = "";
        document.getElementById("search_box").style.opacity = "1";
    }
    else if (document.getElementById("search_box").value.length == 0) {

        document.getElementById("search_box").value = "Что найти?";
        document.getElementById("search_box").style.opacity = "0.5";
        search_visibility();
    }
}

});

$(document).ready(function () {
    var searchField = $("#Central_block_central_col_search_box");

    if (searchField.length) {
        searchField.click(function () {
            showHideSField();
        });
        searchField.focusout(function () {
            showHideSField();
        });
    }

    function showHideSField() {
        if (searchField.val() == "Что найти?") {
            searchField.val("");
        }
        else if (searchField.val().length == 0) {
            searchField.val("Что найти?");
        }
    }
});