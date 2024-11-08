/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Views/**/*.cshtml", "./Views/*.cshtml", "./Views/*.cshtml.css"],
    theme: {
        extend: {
            fontFamily: {
                'primary': ['Nunito']
            },
            colors: {
                accent: "#0062DA",
                accent_lightGray: "rgb(144, 144, 144)",
                accent_lightestGray: "rgb(249, 249, 249)",
                medium_gray: "rgb(96, 96, 96)",
                dark_gray: "rgb(13, 13,13)"
            }

        }
    },
    plugins: [
        /*require('@tailwindcss/typography'),*/
        //require('@tailwindcss/forms'),
        //require('@tailwindcss/aspect-ratio'),
        //require('@tailwindcss/container-queries'),
    ],
}