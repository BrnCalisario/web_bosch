
let section = [$("#home"), $("#sobre-mim"), $("contato")]


$(window).scroll(function() {
    var homeBtn = $("#home-btn")
    var aboutBtn = $("#about-btn")
    var contatoBtn = $("#contato-btn")
    
    var actualPos = $(window).scrollTop()

    homeBtn.css("color", "black")
    aboutBtn.css("color", "black")
    contatoBtn.css("color", "black")

    console.log(actualPos)

    if (actualPos < 20) {
        homeBtn.css("color", "#62B6B7")
        $("header").css("visibility", "visible")
        $("header").css("opacity" , "1")
        return
    }

    if (actualPos >= $("#home").position().top  && actualPos < $("#sobre-mim").position().top) {
        homeBtn.css("color", "#62B6B7")
        $("header").css("visibility", "hidden")
        $("header").css("opacity" , "0")
        return
    }


    if (actualPos >= $("#sobre-mim").position().top && actualPos < $("#contato").position().top) {
        aboutBtn.css("color", "#62B6B7")
        console.log(actualPos  + ' ' + ($("#contato").position().top))
    } else if (actualPos >= $("#contato").position().top) {
        contatoBtn.css("color", "#62B6B7")
    }    

    $("header").css("visibility", "visible")
    $("header").css("opacity" , "1")
    

    
    // console.log($("#sobre-mim").position().top)
})


$(document).on('click', 'a[href^="#"]', function (event) {
    event.preventDefault();

    $('html, body').animate({
        scrollTop: $($.attr(this, 'href')).offset().top
    }, 500);
});