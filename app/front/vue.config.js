module.exports = {
  outputDir: "../wwwroot/app",
  filenameHashing: false,
  chainWebpack: config => {
    config.module
      .rule("images")
      .use("url-loader")
      .loader("url-loader")
      .tap(options => Object.assign(options, { limit: 500000 }));
  },
  "transpileDependencies": [
    "vuetify"
  ]
}