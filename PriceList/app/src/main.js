import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";

Vue.config.productionTip = false;

import { ValidationProvider, ValidationObserver, localize, extend, configure } from "vee-validate";

configure({
    useConstraintAttrs: false
});

import { required, numeric } from "vee-validate/dist/rules.umd";
import ru from "vee-validate/dist/locale/ru.json";

Vue.component("ValidationProvider", ValidationProvider);
Vue.component("ValidationObserver", ValidationObserver);

localize("ru", ru);

extend("required", {
    ...required,
    message: "поле обязательно для заполнения"
});

extend("numeric", {
    ...numeric,
    message: "значение должно быть числом"
});

import axios from "axios";
axios.defaults.headers.post["Content-Type"] = "application/json";

axios.interceptors.response.use(function (response) {
    return response.data;
}, function (e) {
    store.commit("toast/error", e);
    return Promise.reject(e);
});

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount("#app")
