

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
    function isContains(str, compstr, param) {
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

$(".change-color-dot").click(function () {
    let colorVal = $(this).css("background-color");
    let blocks = $(".music-content");
    $(".to-change-color-dot").css("background-color", colorVal);
    $(".to-change-color-dot").text("");

    blocks.each(function (elem) {
        let colBlock = this.querySelector(".color-musick-block");
        if (colBlock.style.backgroundColor != colorVal) {
            this.style.display = "none";
        } else {
            this.style.display = "block";
        }
    })
});