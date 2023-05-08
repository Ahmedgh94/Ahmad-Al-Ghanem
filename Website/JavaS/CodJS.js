var slideIndex = 1;
showDivs(slideIndex);

function plusDivs(n) {
  showDivs(slideIndex += n);
}

function currentSlide(n) {
  showDivs(slideIndex = n);
}

function autoSlide() {
  showDivs((slideIndex += 1));
}

setInterval(autoSlide, 3000);

function showDivs(n) {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var pionts = document.getElementsByClassName("dot");
  if (n > slides.length) { slideIndex = 1 }
  if (n < 1) { slideIndex = slides.length }
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";

  }
  for (i = 0; i < pionts.length; i++) {
    pionts[i].className = pionts[i].className.replace(" active", "");
  }
  slides[slideIndex - 1].style.display = "block";
  pionts[slideIndex - 1].className += " active";

}




// Get the current time in hours using the Date object
const now = new Date();
const currentHour = now.getHours();

// Check if the store is open
if (currentHour >= 9 && currentHour < 17) {
  document.getElementById("open-status").innerHTML = "Onze Winkel Is nu Open!";
  document.getElementById("open-status").style.backgroundColor = "green";
} else {
  document.getElementById("open-status").innerHTML = "Onze winkel is nu gesloten!!";
  document.getElementById("open-status").style.backgroundColor = "red";
}









