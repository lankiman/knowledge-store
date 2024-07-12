const video = document.querySelectorAll("#video");


video.forEach((v) => {
    v.addEventListener("mouseover", () => {
        if (v.puased) {
            v.play();
        }
    });
    v.addEventListener("mouseout", () => {
        if (v.played) {
            v.pause();
        }
    });
});
