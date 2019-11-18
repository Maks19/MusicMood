var values = [];
$(".dot").click(function () {
    let color = $(this).css("background-color");
    let rgb = color.slice(4, color.length - 1).split(",");
    rgb = rgb.map(function (val) {
        return parseInt(val, 10).toString(16);
    });
    color = "#" + rgb.join("");
    $("#color-val").val(color);
});


$("#add").on("click", function () {
    let value = "#" + $('#text-for-tag').val()
    var newOption = $('<option id="hash-tag" value="' + value + '">' + value + '</option>');
    $("#selector-tags").append(newOption);

});

