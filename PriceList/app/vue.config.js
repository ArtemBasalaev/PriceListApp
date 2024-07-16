const { defineConfig } = require('@vue/cli-service');
const path = require("path");

module.exports = defineConfig({
    transpileDependencies: [
        "vuetify"
    ],

    outputDir: "../wwwroot",

    devServer: {
        proxy: {
            "^/api": {
                target: "https://localhost:44352"
            }
        },
        port: 8080
    }
})
