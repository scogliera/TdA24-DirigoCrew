/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Pages/**/*.{html,js,razor,cshtml}", "./Components/**/*.{html,js,razor,cshtml}"],
  theme: {
    colors: {
        'white': '#ffffff',
        'TDAgrey': '#333333',
        'TDAcyan': '#74C7D3',
        'TDAyellow': '#FECB2E',
    },
    extend: {},
  },
  plugins: [],
}

