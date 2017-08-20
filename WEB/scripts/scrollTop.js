$(document).ready(function () {
    var rememberOffset = 0;

    $(window).scroll(function () {
        if (window.pageYOffset ? window.pageYOffset : document.body.scrollTop > $(".head").height() || rememberOffset > 0) {
            $('#scrollTopBckgr').fadeIn();
            if (rememberOffset > 0 && rememberOffset > window.pageYOffset ? window.pageYOffset : document.body.scrollTop) {
                $('#scrollTop').html("<i class='fa fa-angle-down' aria-hidden='true'></i>Вниз");
            }
            else if (rememberOffset == 0 || rememberOffset < window.pageYOffset ? window.pageYOffset : document.body.scrollTop) {
                $('#scrollTop').html("<i class='fa fa-angle-up' aria-hidden='true'></i>Вверх");
                rememberOffset = 0;
            }
        } else {
            $('#scrollTopBckgr').fadeOut();
        }
    });

    $('#scrollTopBckgr').click(function () {
        if (rememberOffset == 0) {
            $("html, body").animate({ scrollTop: $('.central_sidebar .SiteMap_Path').offset().top - $(".logo").height() }, 600);
            rememberOffset = window.pageYOffset ? window.pageYOffset : document.body.scrollTop;
        }
        else {
            $("html, body").animate({ scrollTop: rememberOffset }, 600);
            rememberOffset = 0;
        }
        return false;
    });

    $('#scrollTop').click(function () {
        if (rememberOffset == 0) {
            $("html, body").animate({ scrollTop: $('.central_sidebar .SiteMap_Path').offset().top - $(".logo").height() }, 600);
            rememberOffset = window.pageYOffset ? window.pageYOffset : document.body.scrollTop;
        }
        else {
            $("html, body").animate({ scrollTop: rememberOffset }, 600);
            rememberOffset = 0;
        }
        return false;
    });

});
