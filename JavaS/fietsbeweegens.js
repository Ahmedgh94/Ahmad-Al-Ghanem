// move the bike 

let breedte = window.innerWidth;
let positie = 0;
let fiets = document.getElementById("movedfiets");
let direction = 1;
let footerBreedte = document.querySelector('footer').offsetWidth;

function fietsBeweeg() {
    if (positie >= footerBreedte - fiets.offsetWidth) {
        direction = -1;
        fiets.classList.add("flipped");
    } else if (positie <= 0) {
        direction = 1;
        fiets.classList.remove("flipped");
    }
    positie += direction;
    fiets.style.left = (positie / footerBreedte) * 100 + "%";
}

setInterval(fietsBeweeg, 3);