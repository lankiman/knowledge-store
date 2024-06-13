/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Views/**/*.cshtml", "./Views/*.cshtml"],
    theme: {
        extend: {
            fontFamily: {
                'primary': ['Nunito']
            }
        },
    },
    plugins: [
        //        require('@tailwindcss/forms'),
        //        require('@tailwindcss/typography')
    ],
}