const profileBurger = document.querySelector("#profileBurger");
const profilePicBurger = document.querySelector("#profilePicBurger");


console.log(profileBurger, profilePicBurger);
function genericToggle(element) {    
        if (element.classList.contains("hidden")) {
            element.classList.remove("hidden");
            element.classList.add("flex");
        }

        else {
            element.classList.remove("flex");
            element.classList.add("hidden");
        }
    console.log("fucntion called");
}

profilePicBurger.addEventListener("click", () => { genericToggle(profileBurger) });



