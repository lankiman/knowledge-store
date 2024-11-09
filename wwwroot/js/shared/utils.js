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