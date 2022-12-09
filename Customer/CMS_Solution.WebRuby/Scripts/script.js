$(document).ready(function () {
    //search attribute
    $(".filter_class").click(function (e) {
        var jThis = $(this);
        let inputCurrent = $(this).find("input[type='checkbox']");
        if (inputCurrent.hasClass("selected_filter")) {
            inputCurrent.removeClass('selected_filter');
            inputCurrent.prop("checked", false);
        } else {
            inputCurrent.addClass('selected_filter');
            inputCurrent.prop("checked", true);
        }
        if (!$("input[type='checkbox']").hasClass("selected_filter")) {
            e.preventDefault();
            window.location.href = $(".index_col_products").data("urlfau");
            return true;
        }

        var myurl = "";
        var bt_url = urlAjaxAttributes + "?id=" + $(".index_col_products").data("id") + "&groupid=" + $(".index_col_products").data("groupid");
        var queries = [];
        $(".filter_class").each(function () {
            let tempInput = $(this).find("input[type='checkbox']");
            if (tempInput.prop("checked")) {
                var param = $(this).data("param");
                if (queries.length === 0 || !queries.find(x => x.param === param)) {
                    queries.push({
                        param: param,
                        index: 0
                    });
                }
                var query = queries.find(x => x.param === param);
                myurl = myurl + "&" + param + "[" + query.index + "]=" + $(this).attr("data-href");
                query.index++;
            }
        });
        $("#ajax_loader").show();
        console.log('.filter_class');
        //$("body,html").animate({
        //    scrollTop: 200
        //}, 1000);
        // bt_url = bt_url + "&" + myurl.substring(1) + "&orderBy=" + $(".filterprice").val();
        bt_url = bt_url + "&" + myurl.substring(1);
        //$(".index_col_products").data("url", bt_url).data("order", $(".filterprice").val());
        $(".index_col_products").data("url", bt_url);
        console.log(bt_url)
        $.ajax({
            type: "POST",
            url: bt_url,
            traditional: true,
            success: function (rel) {
                $("#ajax_loader").hide();
                $(".index_col_products").html(rel);
                //window.location.hash = myurl.substring(1) + "&orderby=" + $(".filterprice").val();
                history.replaceState({}, null, window.location.origin + window.location.pathname + "?" + myurl.substring(1));;
                jThis.prop("checked", true);
                clickPagerSearch();
            },
            error: function () {
                $("#ajax_loader").hide();
            }
        });
        return false;
    });
    $('.btn_SearchProduct').click(function (e) {
        var jThis = $(this);
        let fullURL = jThis.data("url") + "?tukhoa=" + $("#tukhoa").val();
        window.location.href = fullURL;
    });
    $('.input_SearchProduct').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();

            var jThis = $('.btn_SearchProduct');
            let fullURL = jThis.data("url") + "?tukhoa=" + $("#tukhoa").val();
            window.location.href = fullURL;
            return false;
        }
    });
    countOrder("orderCart");
    $(".li-scroll").on('click', 'a', function (event) {
        event.preventDefault();
        $(".li-scroll").removeClass('active');
        $(this).parent().addClass('active');
        var o = $($(this).attr("href")).offset();
        var sT = o.top - $(".fixed-header").outerHeight(true) - 10;
        window.scrollTo(0, sT);
    });
    $('.index_col_products').each(function () {
        const urlfau = $(this).data('urlfau');
        const url = $(this).data('url');
        $(this).find('.filter-search-pager .page-link').each(function () {
            if ($(this).attr('href'))
                $(this).attr('href', $(this).attr('href').replace(urlfau + "?", url + "&"));
        });
        clickPagerSearch();
    });
    $('.reply-btn').click(function () {
        $(this).parent().parent().next().toggle(40);
        $('.reply-form textarea').focus();
    });
});

function clickPagerSearch() {
    console.log('clickPagerSearch');
    $(".filter-search-pager a").unbind("click").click(function (e) {
        $("#ajax_loader").show();
        //$("body,html").animate({
        //    scrollTop: 200
        //}, 1000);
        var bt_url = $(this).attr("href");

        $.ajax({
            type: "POST",
            url: bt_url,
            success: function (rel) {
                $("#ajax_loader").hide();
                const urlfau = $(".index_col_products").data('urlfau');
                const url = urlAjaxAttributes + "?id=" + $(".index_col_products").data("id") + "&groupid=" + $(".index_col_products").data("groupid");
                history.replaceState({}, null, window.location.origin + bt_url.replace(url + "&", urlfau + "?"));
                $(".index_col_products").html(rel);
                //window.location.hash = bt_url;
                clickPagerSearch();
            },
            error: function () {
                $("#ajax_loader").hide();
            }
        });
        e.preventDefault();
        return true;
    });
}
function countOrder(cookieName) {
    let cookieKey = accessCookie(cookieName);
    //if (!!cookieKey) {
    if (cookieKey !== null && cookieKey !== "") {
        let cookieCount = Number(accessCookie(cookieKey));
        $('.count-product-order').text(!isNaN(cookieCount) ? cookieCount : 0);
    }
}
function accessCookie(cookieName) {
    var name = cookieName + "=";
    var allCookieArray = document.cookie.split(';');
    for (var i = 0; i < allCookieArray.length; i++) {
        var temp = allCookieArray[i].trim();
        if (temp.indexOf(name) === 0)
            return temp.substring(name.length, temp.length);
    }
    return "";
}

var wd = screen.width;
if (wd >= 1025) {
    $(window).scroll(function () {
        //if ($(window).scrollTop() >= 105) {
        //    $('header').addClass('fixed-header');
        //}
        //else {
        //    $('header').removeClass('fixed-header');
        //}
    });
    $('.box-service').owlCarousel({
        autoplay: true,
        nav: false,
        loop: true,
        dots: true,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2, margin: 10,
            },
            768: {
                items: 3, margin: 10,
            },
            1120: {
                items: 3, margin: 10,
            },
            1400: {
                items: 3
            }
        }
    });

    $('.box-package').owlCarousel({
        autoplay: true,
        nav: false,
        loop: true,
        items: 5,
        responsive: {
            0: {
                items: 2,
                margin: 10
            },
            480: {
                items: 2,
                margin: 10
            },
            768: {
                items: 3,
                margin: 20
            },
            1120: {
                items: 3,
                margin: 20
            },
            1400: {
                items: 3,
                margin: 20
            }
        }
    });
}
if (wd <= 768) {
    $('.setup .container-fluid .row').addClass('box-setup');
    $('.box-setup').addClass('owl-carousel');
    $('.setup .container-fluid').find('.col-md-4').removeClass();
    $('.setup .container-fluid').find('.row').removeClass('row');


    $(window).scroll(function () {
        if ($(window).scrollTop() >= 65) {
            $('header').addClass('fixed-header');
        }
        else {
            $('header').removeClass('fixed-header');
        }
    });


}
loadEvent();
function loadEvent() {
    $('.box-product').each(function (index) {
        let box = $(this);
        const count = box.data('count');
        let responsive = {
            0: {
                items: 2,
                margin: 10
            },
            480: {
                items: 3,
                margin: 10
            },
            768: {
                items: 3,
                margin: 10
            },
            1120: {
                items: 4,
                margin: 20
            },
            1400: {
                items: 4,
                margin: 20
            }
        }
        if (count === 3) {
            responsive["1120"].items = 3;
            responsive["1400"].items = 3;
        }
        else if (count === 2) {
            responsive["480"].items = 2;
            responsive["768"].items = 2;
            responsive["1120"].items = 2;
            responsive["1400"].items = 2;
        }
        else if (count === 1) {
            responsive["0"].items = 1;
            responsive["480"].items = 1;
            responsive["768"].items = 1;
            responsive["1120"].items = 1;
            responsive["1400"].items = 1;
        }
        box.owlCarousel({
            autoplay: true,
            nav: false,
            margin: 20,
            loop: true,
            responsive: responsive
        });
    });
    $('.box-user').owlCarousel({
        autoplay: true,
        nav: false,
        margin: 20,
        loop: true,
        items: 2,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2
            },
            768: {
                items: 2
            },
            1120: {
                items: 2
            },
            1400: {
                items: 2
            }
        }
    });
    $('.box-setup').owlCarousel({
        autoplay: true,
        nav: false,
        loop: true,
        items: 2,
        responsive: {
            0: {
                items: 2,
                margin: 10
            },
            480: {
                items: 2,
                margin: 10
            },
            768: {
                items: 3,
                margin: 20
            }
        }
    });
    $('.box-partner').owlCarousel({
        autoplay: true,
        nav: false,
        margin: 20,
        loop: true,
        items: 5,
        responsive: {
            0: {
                items: 2
            },
            480: {
                items: 3
            },
            768: {
                items: 4
            },
            1120: {
                items: 5
            },
            1400: {
                items: 5
            }
        }
    });
    $('.slider').nivoSlider({
        controlNav: false,
        directionNav: false
    });
    $('.account-login').click(function () {
        $('.account-control').toggle(100);
    });

    $('label.mg-0').click(function () {
        $('label.mg-0').removeClass('active');
        $(this).addClass('active');

        // $('.content-method').find('.active').removeClass('active').sibling().addClass('active');
    });

    jQuery('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter('.quantity input');
    jQuery('.quantity').each(function () {
        var spinner = jQuery(this),
            input = spinner.find('input[type="number"]'),
            btnUp = spinner.find('.quantity-up'),
            btnDown = spinner.find('.quantity-down'),
            min = input.attr('min'),
            max = input.attr('max');
        
        btnUp.click(function () {
            var newVal = 0;
            var oldValue = parseFloat(input.val());
            if (oldValue >= max) {
                //var newVal = oldValue;
                newVal = oldValue;
            } else {
                //var newVal = oldValue + 1;
                newVal = oldValue + 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

        btnDown.click(function () {
            var newVal = 0;
            var oldValue = parseFloat(input.val());
            if (oldValue <= min) {
                //var newVal = oldValue;
                newVal = oldValue;
            } else {
                //var newVal = oldValue - 1;
                newVal = oldValue - 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

    });
    $('.box-office').owlCarousel({
        autoplay: true,
        nav: false,
        margin: 20,
        loop: true,
        items: 4,
        responsive: {
            0: { items: 2 },
            480: { items: 2 },
            768: { items: 3 },
            1120: { items: 4 },
            1400: { items: 4 }
        }
    });
}


