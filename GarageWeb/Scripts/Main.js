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
});

function displayFancybox(param1, param2) {
    // Open FancyBox
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
var input = document.getElementById("file");

input.addEventListener('change', function (e) {
    var fileName = '';
    fileName = e.target.value.split('\\').pop();
    $(".file-label span").text(fileName);
});