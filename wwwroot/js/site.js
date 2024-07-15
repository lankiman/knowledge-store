// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const burger = document.querySelector('#burger');
const menu = document.querySelector("#menu");

const first = document.querySelector("#first");
const second = document.querySelector("#second");

const passwordInput = document.querySelector(".password__input");

const passwordVisible = document.querySelector(".password__input--visible");

const passwordNotVisible = document.querySelector(".password__input--hidden");

const confirmPasswordInput = document.querySelector(".confirm__password__input");

const confirmPasswordVisible = document.querySelector(".confirm__password__input--visible");

const confirmPasswordNotVisible = document.querySelector(".confirm__password__input--hidden");

const loadingOverlay = document.querySelector(".loading__overlay");

const loadingTrigger = document.querySelector(".loading__trigger");


if (loadingOverlay) {
    if (loadingTrigger) {
        loadingTrigger.addEventListener("click", () => {
            loadingOverlay.classList.remove("hidden");
        });
    }
}

if (passwordInput) {
    if (passwordNotVisible) {
        passwordNotVisible.addEventListener("click", () => {
            passwordInput.setAttribute("type", "text");
            passwordNotVisible.classList.add("hidden");
            passwordVisible.classList.remove("hidden");
        });
    }
    if (passwordVisible) {
        passwordVisible.addEventListener("click", () => {
            passwordInput.setAttribute("type", "password");
            passwordVisible.classList.add("hidden");
            passwordNotVisible.classList.remove("hidden");
        });
    }
}

if (confirmPasswordInput) {
    if (passwordNotVisible) {
        confirmPasswordNotVisible.addEventListener("click", () => {
            confirmPasswordInput.setAttribute("type", "text");
            confirmPasswordNotVisible.classList.add("hidden");
            confirmPasswordVisible.classList.remove("hidden");
        });
    }
    if (passwordVisible) {
        confirmPasswordVisible.addEventListener("click", () => {
            confirmPasswordInput.setAttribute("type", "password");
            confirmPasswordVisible.classList.add("hidden");
            confirmPasswordNotVisible.classList.remove("hidden");
        });
    }
}

if (burger) {
    burger.addEventListener('click', toogle);
}

function toogle() {
    if (menu.classList.contains('hidden')) {
        menu.classList.remove('hidden');
        first.classList.add('hidden');
        second.classList.remove('hidden');
    }
    else {
        menu.classList.add('hidden');
        first.classList.remove('hidden');
        second.classList.add('hidden');
    }
}