import type { Config } from 'tailwindcss'

export default <Partial<Config>>{
  // content: [
  //   '*.{js,ts,jsx,tsx,mdx,vue}',
  // ],
  content: [
    './components/**/*.{vue,js,ts}',
    './layouts/**/*.vue',
    './pages/**/*.vue',
    './features/**/*.vue',
    './widgets/**/*.vue',
    './entities/**/*.vue',
    './shared/**/*.vue',
    './app.vue',
    './plugins/**/*.{js,ts}',
    './src/**/*.{vue,js,ts}',
    './nuxt.config.{js,ts}'
  ],
  theme: {
    extend: {
      aspectRatio: {
        auto: 'auto',
        square: '1 / 1',
        video: '16 / 9'
      },
      colors: {
        green: {
          50: '#f8f9ec',
          100: '#eef2d5',
          200: '#dee6b0',
          300: '#c6d581',
          400: '#aec15a',
          500: '#8fa43b',
          600: '#718136',
          700: '#50673b',
          800: '#465123',
          900: '#3c4621',
        },
        gray: {
          50: '#f7f7f7',
          100: '#f0f0f0',
          200: '#e3e3e3',
          300: '#d1d1d1',
          400: '#bfbfbf',
          500: '#aaaaaa',
          600: '#969696',
          700: '#818181',
          800: '#6a6a6a',
          900: '#585858',
        },
      },
    },
  },
  plugins: [],
}