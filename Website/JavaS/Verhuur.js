     // Verhuur pagina 
     const rentBtn = document.getElementById("rentbtn");
     const bikeOptions = document.querySelectorAll(".bike-option");

     rentBtn.addEventListener('click', () => {
         const selectedOption = document.querySelector(".bike-option:checked");
         if (selectedOption) {
             const bikeDetails = selectedOption.dataset.details;
             alert(`You have rented the following bike: ${bikeDetails}`);
         } else {
             alert('Please select a bike before renting.');
         }
     });