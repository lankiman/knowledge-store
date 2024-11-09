//General Helper Functions
const sizeConverter = (size) => {
    let units = ["bytes", "kb", "mb", "gb"];
    let unitIndex = 0;

    while (size >= 1024 && unitIndex < units.length - 1) {
        size /= 1024;
        unitIndex++;
    }
    return `${size.toFixed(2)} ${units[unitIndex]}`;
};


function updateState(element, removeStyle, addStyle) {
    if (element && removeStyle) {
        element.classList.remove(removeStyle)
    }
    if (element && addStyle) {
        element.classList.add(addStyle)
    }
}

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

const layoutHandler = (function () {
    const sidebarMenuButton = document.querySelector('[data-sidebar-menu-icon]');
    const mobileSearchButton = document.querySelector('[data-mobile-search-icon]');
    const mobileSearchBar = document.querySelector('[data-mobile-search-bar]');
    const sidebarMenu = document.querySelector("[data-sidebar-menu]")

    return {
        init: function () {
            if (sidebarMenuButton && sidebarMenu) {
                sidebarMenuButton.addEventListener("click", () => customToggleElement(sidebarMenu, "sidebar-open"));
            }
            if (mobileSearchButton && mobileSearchBar) {
                mobileSearchButton.addEventListener("click", () => toggleElement(mobileSearchBar));
            }  
        }
    }
})()
layoutHandler.init()