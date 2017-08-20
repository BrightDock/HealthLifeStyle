//sticky sidebar
var p = 0;
var b = null;

$(document).ready(function () {
    p = $(".logo").height() + 20;
});

$(window).resize({
    function() {
        stickyInit();
    }
});

function pageLoad(sender, args) { 
    p = $(".logo").height() + 20;
    b = null;
    stickyInit();
}

function stickyInit() {
    if ($(window).width() > 991) {
        $(".post_img_wrapper").stick_in_parent({
            parent: $(".post"),
            offset_top: p
        });

        $(".sticky").stick_in_parent({
            parent: $(".central_sidebar"),
            offset_top: p
        });
    }
    else {
        $("#post_img_wrapper").trigger("sticky_kit:detach");
        $("#sticky").trigger("sticky_kit:detach");
        $("#post_img_wrapper").trigger("sticky_kit:recalc");
        $("#sticky").trigger("sticky_kit:recalc");
    }
}