/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Pages/**/*.{html,js,razor,cshtml}", "./Components/**/*.{html,js,razor,cshtml}", "./MainLayout.razor"],
  theme: {
    extend: {
        lineHeight: {
            '100': '100%'
        },
        colors: {
            'white': "#FFFFFF",
            'TdaGrey': "#333333",
            'TdaCyan': "#74C7D3",
            'TdaYellow': "#FECB2E",
            'TdaDarkBlue': "#00384D"
        },
        spacing: {
            'magicky-padding': "1rem!important",
            '90p': "90%"
        },
        fontFamily: {
            lalezar: ["Lalezar", "system-ui"],
            opensans: ["Open Sans", "sans-serif"]
        },
        backgroundImage: {
            'index-hero': "url('../images/backgrounds/index.jpg')",
            "index-filler": "url('../images/backgrounds/filler.jpg')"
        }
    },
  },
  plugins: [],
}
