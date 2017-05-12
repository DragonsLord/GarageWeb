$(document).ready(function () {
    $(".message-info").delay(3000).fadeOut("slow");

    $(".file-label").hover(
        function () {
            if ($("#preload").attr('src') != '#') {
                $("#preload").show("slow");
            }
        },
        function () {
            $("#preload").hide("slow");
        });

    var carousel_menu = $("#owl-demo-menu");
    var carousel_news = $("#owl-demo-news");

    carousel_menu.slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        arrows: false,
    });

    carousel_news.slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        autoplaySpeed: 2000,
        arrows: false,
    });

    $(".order a").click(function (e) {
        e.preventDefault();
    })
    $(".menu-slider .right-arrear").click(function (event) {
        event.preventDefault();
        carousel_menu.slick('slickNext');
    });
    $(".menu-slider .left-arrear").click(function (event) {
        event.preventDefault();
        carousel_menu.slick('slickPrev');
    });

    $(".news-slider .right-arrear").click(function (event) {
        event.preventDefault();
        carousel_news.slick('slickNext');
    });
    $(".news-slider .left-arrear").click(function (event) {
        event.preventDefault();
        carousel_news.slick('slickPrev');
    });
    
    $("#file").on("change", function (e) {
        var fileName = '';
        fileName = e.target.value.split('\\').pop();
        $(this).parent().find(".file-label span").text(fileName);
    });

    $("#file").change(function () {
        readURL(this);
    });
});

function addToBusket(id) {
    $.post("/Basket/AddToBasket", { id: id }).then(function () {
        $.ajax({ method: "POST", url: "/Basket/GetCount" })
            .done(function (data) {
                $(".basket .count").text(data.Count);
                $(".basket .summ, .whole-price").text(data.Price);})
            .fail(function () {})
            .always(function () {});
    });
};
function changeWorth(element, state) {

    var diff = state == 0 ? 1 : -1;

    var count = parseInt($(element).parent().find(".count").text()) + diff;
    if (count >= 0) {
        var price = parseInt($(element).parent().parent().find(".worth").text());

        $(element).parent().parent().find(".dish-price").text(count * price);

        $(element).parent().find(".count").text(count);
    }
}
function addOneToBusket(id, link) {
    addToBusket(id);
    changeWorth(link,0);
};

function RemoveOneFromBasket(id,link) {
    $.post("/Basket/RemoveOneFromBasket", { id: id }).then(function () {
        $.ajax({ method: "POST", url: "/Basket/GetCount" })
            .done(function (data) {
                $(".basket .count").text(data.Count);
                $(".basket .summ, .whole-price").text(data.Price);
            })
            .fail(function () { })
            .always(function () { });
    });

    changeWorth(link,1);
};

function RemoveFromBasket(id,link) {
    $.post("/Basket/RemoveFromBasket", { id: id }).then(function () {
        $.ajax({ method: "POST", url: "/Basket/GetCount" })
            .done(function (data) {
                $(".basket .count").text(data.Count);
                $(".basket .summ, .whole-price").text(data.Price);
            })
            .fail(function () { })
            .always(function () { });
    });
    $(link).parent().parent().remove();
};

function clearBasket(link) {
    $.post("/Basket/ClearBasket").then(function () {
        $.ajax({ method: "POST", url: "/Basket/GetCount" })
            .done(function (data) {
                $(".basket .count").text(data.Count);
                $(".basket .summ, .whole-price").text(data.Price);
            })
            .fail(function () { })
            .always(function () { });
    });
    $(link).parents(".order-list").remove();
};


function displayFancybox(param1, param2) {

    $("#recall h2").text(param1);

    $("input[name='id']").val(param2);

    $("a.menu-remove").fancybox();

};

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#preload').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

