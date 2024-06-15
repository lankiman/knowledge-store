// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const burger = document.querySelector('#burger');
const menu = document.querySelector("#menu");

const first = document.querySelector("#first");
const second = document.querySelector("#second");
//burger.addEventListener('click', () => {
//    if (menu.classList.contains('hidden')) {
//        menu.classList.remove('hidden');
//    }
//    else {
//        menu.classList.add('hidden');
//    }
//});


burger.addEventListener('click', toogle);

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