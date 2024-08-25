/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        satoshi: ["Satoshi", "sans-serif"],
        inter: ["Inter", "sans-serif"],
      },
      backgroundImage: {
        arrow: "url(/src/assets/sep.svg)",
      },
    },
  },
  plugins: [require("daisyui")],
};
