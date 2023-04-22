// fietsen pagina 

        // fietsen array
        var bikes = [
            {
                id: 1,
                image: "fietsen/Pelikaan_Carry_On_Lady.png",
                name: " Pelikaan Carry On Lady",
                price: "€769,00",
                type: "elektrisch",
                suitableFor: "zakelijk en prive",
                gender: "dames",
                category: "transport",
                brand: "Pelikaan",
                condition: "nieuw",
                color: "zwart",
                size: "28 Inch 53 cm 3V V-Brakes",
                // shipping: "€9,99"
            },
            {
                id: 2,
                image: "fietsen/Stella_Allegra_voorwielmotor.png",
                name: "Allegra voorwielmotor",
                price: "€999,00",
                type: "elektrisch",
                suitableFor: "zakelijk en prive",
                gender: "dames",
                category: "stads",
                brand: "Pelikaan",
                condition: "nieuw",
                color: "zwart",
                size: "S (Lichaamslengte 1,62m - 1,73m)",
                // shipping: "€12,49"
            },
            {
                id: 3,
                image: "fietsen/Gazelle_Orange_C7.png",
                name: " Gazelle Orange C7+ HMB 2020",
                price: "€2199,00",
                type: "elektrisch",
                suitableFor: "zakelijk en prive",
                gender: "heren",
                category: "stads",
                brand: "Gazelle",
                condition: "nieuw",
                color: "Blauw",
                size: "53cm(1.66cm-1.77cm)",
                // shipping: "€8,99"
            },
            {
                id: 4,
                image: "fietsen/Gazelle_CityGo_C7.png",
                name: "Gazelle CityGo C7",
                price: "€679,00",
                type: "niet elektrisch",
                suitableFor: "zakelijk en prive",
                gender: "heren",
                category: "stads",
                brand: "Gazelle",
                condition: "nieuw",
                color: "meerdere",
                size: "28 Inch 53 cm 3V V-Brakes",
                // shipping: "€15"
            }
        ];

        // functie om fietsdetails te tonen
        function showBikeDetails(id) {
            var bike = bikes.find(b => b.id === id);
            if (bike) {
                document.getElementById("bike-image").src = bike.image;
                document.getElementById("bike-name").innerHTML = "Naam: " + bike.name;
                document.getElementById("bike-price").innerHTML = "Prijs: " + bike.price;
                document.getElementById("bike-type").innerHTML = "Type: " + bike.type;
                document.getElementById("bike-suitableFor").innerHTML = "Geschiktvoor: " + bike.suitableFor;


                
                document.getElementById("bike-gender").innerHTML = "Geslacht: " + bike.gender;
                document.getElementById("bike-category").innerHTML = "category: " + bike.category;


                document.getElementById("bike-brand").innerHTML = "Merk: " + bike.brand;
                document.getElementById("bike-condition").innerHTML = "Staat: " + bike.condition;
                document.getElementById("bike-color").innerHTML = "Kleur: " + bike.color;
                document.getElementById("bike-size").innerHTML = "size: " + bike.size;
                // document.getElementById("bike-shipping").innerHTML = "shipping: " + bike.shipping;

                document.getElementById("bike-details-add-to-cart").onclick = function () {
                    addToCart(bike);
                }
                document.getElementById("bike-details").style.display = "block";
            }
        }

        // functie om fietsdetails te verbergen
        function hideBikeDetails() {
            document.getElementById("bike-details").style.display = "none";
        }

        // functie om fiets toe te voegen aan winkelwagen
        function addToCart(bike) {
            // voeg fiets toe aan winkelwagen
            alert(bike.name + " is toegevoegd aan de winkelwagen!");
            hideBikeDetails();
        }


        