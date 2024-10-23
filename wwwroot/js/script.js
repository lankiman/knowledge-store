const profileBurger = document.querySelector("#profileBurger");
const profilePicBurger = document.querySelector("#profilePicBurger");
const mobileBurger = document.querySelector("#mobileBurger");
const mobileBurgerOpen = document.querySelector("#sidebar-burger-open");
const mobileBurgerClose = document.querySelector("#sidebar-burger-close");

function genericToggle(element) {
    if (element.classList.contains("hidden")) {
        element.classList.remove("hidden");
        element.classList.add("flex");
    }

    else {
        element.classList.remove("flex");
        element.classList.add("hidden");
    }
}

profilePicBurger.addEventListener("click", () => { genericToggle(profileBurger) });

mobileBurgerOpen.addEventListener("click", () => {
    mobileBurger.classList.toggle("-translate-x-full");
    mobileBurger.classList.toggle('translate-x-0');
});

mobileBurgerClose.addEventListener("click", () => {
    mobileBurger.classList.toggle("-translate-x-full");
    mobileBurger.classList.toggle('translate-x-0');
});