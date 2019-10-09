$(document).ready(function () {
    //Tooltips
    var tip;
    $(".tip_trigger").hover(function () {
        console.log("hovered");
        tip = $(this).find('.tip');
        console.log($(this).find('.tip').find('span').html())
        if ($(this).find('.tip').find('span').html() != '') {
            $(this).find('.tip').show(); //Show tooltip
        }
    }, function () {
        $(this).find('.tip').hide(); //Hide tooltip 
    });

    ////Required fields
    $('input').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            $(this).css("border-color", "#DA9BA2")

        }
        if ($(this).val() != '') {

            $(this).addClass("validated");
        }
    });

    $('input').blur(function () {

        if ($(this).val() != '') {

            $(this).addClass("validated");
        }
        else {

            $(this).css("border-color", "#DA9BA2")
        }
    });
});