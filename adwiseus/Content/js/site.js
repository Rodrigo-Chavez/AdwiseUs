// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
ScrollReveal().reveal('.banner-index', {
    duration: 3000,
    origin: 'right',
    distance: '-100px'
});
ScrollReveal().reveal('.wsp-icon', {
    duration: 1000,
    origin: 'right',
    distance: '-100px',
    scale: 0.5,
    delay: 1000
});
ScrollReveal().reveal('.card-index', {
    duration: 3000,
    origin: 'top',
    distance: '-100px'
});

function toggleActive() {
    this.classList.toggle("active");
}

const arrowSvg = document.querySelector(".svg-arrow");
arrowSvg.addEventListener("click", toggleActive);
