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

    $.ajax({ method: "POST", url: "/Busket/GetCount" })
        .done(function (data) {
            $(".count").text(data);
        })
        .fail(function () {
        })
        .always(function () {
        });


    $("#file").on("change", function (e) {
        var fileName = '';
        fileName = e.target.value.split('\\').pop();
        $(this).parent().find(".file-label span").text(fileName);
    });
});
function addToBusket(id) {
       $.post("/Busket/AddToBucket", { id: id });
        console.log(id);
    
    $.ajax({ method: "POST", url: "/Busket/GetCount" })
        .done(function (data) {
            $(".count").text(data);
        })
        .fail(function () {
        })
        .always(function () {
    });
}
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
$("#file").change(function () {
    readURL(this);
});

