/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Pages/**/*.{html,js,razor,cshtml}", "./Components/**/*.{html,js,razor,cshtml}"],
  theme: {
    colors: {
          'white': '#ffffff',
          'black': '#000000',
          'TDAgrey': '#333333',
          'TDAcyan': '#74C7D3',
          'TDAcyanDark': '#5DA1AA',
          'TDAblue': '#00384D',
          'TDAyellow': '#FECB2E',
          'TDAyellowDark': '#D6AA28',
    },
    extend: {},
  },
  plugins: [],
}

