
$(".dot").click(function () {
    let color = $(this).css("background-color");
    let rgb = color.slice(4, color.length - 1).split(",");
    rgb = rgb.map(function (val) {
        return parseInt(val, 10).toString(16);
    });
    color = "#" + rgb.join("");
    $(".color-val").val(color);
});

$(document).ready(function () {
    function isContains(str, compstr,param) {

        str = str[0].innerText.toLowerCase()
        compstr = compstr.toLowerCase();
        let find = str.indexOf(compstr);
        if (find == -1) {
            return false;
        }
       
        return true;
    }
    $("#search-box").keyup(function () {
       let seachText = $("#search-box").val();
        $(".content").each(function () {
            if (isContains($(this).children(), seachText)) {
                $(this).show();
              
            } else {
                 $(this).hide();
            }
        });

    })
});

