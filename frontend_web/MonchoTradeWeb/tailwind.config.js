/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        black: '#000000',
      },
      borderRadius: {
        'md': '4px',
      }

  },
},
  variants:{
    extend: {}, 
  },
  plugins: [],
}

