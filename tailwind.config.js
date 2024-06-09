/** @type {import('tailwindcss').Config} */
module.exports = {
    purge: ["./Views/**/*.cshtml", "./Views/*.cshtml"],
    theme: {
        extend: {},
    },
    plugins: [
        //        require('@tailwindcss/forms'),
        //        require('@tailwindcss/typography')
    ],
}