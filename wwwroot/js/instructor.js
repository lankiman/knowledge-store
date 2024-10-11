const sidebarMenuButton = document.querySelector('[data-inr_sidebar-menu-icon]');
const mobileSearchButton = document.querySelector('[data-inr-mb-search-icon]');
const mobileSearchBar = document.querySelector('[data-inr_mbl_search-bar]');
const sidebarMenu = document.querySelector("[data-inr_sidebar-menu]")


function toggleElement(element) {
    if (element.classList.contains("hidden")) {
        element.classList.remove("hidden");
    } else {
        element.classList.add("hidden");
    }
}

function customToggleElement(element, classname) {
    if (element.classList.contains(classname)) {
        element.classList.remove(classname);
    } else {
        element.classList.add(classname);
    }
}

sidebarMenuButton.addEventListener("click", () => customToggleElement(sidebarMenu, "sidebar-open"));
mobileSearchButton.addEventListener("click",()=>toggleElement(mobileSearchBar));



