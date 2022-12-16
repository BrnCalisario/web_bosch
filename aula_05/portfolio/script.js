let articles = document.getElementsByTagName("article")
let anchors = document.getElementsByClassName("btn")

function activeAnchor(a) {
    for(let i = 0; i < anchors.length; i++) {
        anchors[i].style.color = "black"
    }
    a.style.color = "#62B6B7"
}

function comeco() {
    for (let i = 0; i < anchors.length; i++) {
        anchors[i].onclick = () => {
            activeAnchor(anchors[i])
        }
    }

    window.onscroll = () => {
        for(let i = 0; i < articles.length; i++) {
            let top = window.scrollY
            let offset = articles[i].offsetTop
            let height = articles[i].offsetHeight
            let id = articles[i].getAttribute('id')

            if(top >= offset && top < offset + height) {
                const target = document.querySelector(`[href='#${id}']`)
                activeAnchor(target)
            }
        }

        if($(window).scrollTop() > 100 && $(window).scrollTop() < 800) {
            $("header").css("visibility", "hidden")
            $("header").css("opacity" , "0")
        } else {
            $("header").css("visibility", "visible")
            $("header").css("opacity" , "1")
        }
    }
}


$(document).on('click', 'a[href^="#"]', function (event) {
    event.preventDefault();
    $('html, body').animate({
        scrollTop: $($.attr(this, 'href')).offset().top
    }, 500);

    
});