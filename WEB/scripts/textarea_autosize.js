$(function () {
    var txt = $('.textBox'),
    hiddenDiv = $(document.createElement('div')),
    $copyText = copyText(hiddenDiv, txt);

    txt.addClass('noscroll');
    hiddenDiv.addClass('hiddendiv');
    $('body').append(hiddenDiv);

    txt.click(function () { setTimeout('copyText', 100) });
    txt.bind('mouseout mousemove keydown keypress keyup', function (e) {
        copyText(hiddenDiv, txt);
/*        if (txt.is(":focus")) {
            $("html, body").animate({ scrollTop: txt.offset().top + txt.height() + 20 }, 600);
        }*/
    });
});

function copyText(hiddenDiv, txt) {
    hiddenDiv.css("paddind", txt.css("padding"));
    hiddenDiv.css("font", txt.css("font"));
    hiddenDiv.css("width", txt.width());

    hiddenDiv.html(txt.val().replace(/\n/g, '<br />'));

    txt.css('height', hiddenDiv.height() + 10);
}
