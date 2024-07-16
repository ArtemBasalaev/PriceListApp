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

import { required, numeric, min, max, length, email, alpha, alpha_dash, confirmed, is, is_not, regex } from "vee-validate/dist/rules.umd";
import ru from "vee-validate/dist/locale/ru.json";

Vue.component("ValidationProvider", ValidationProvider);
Vue.component("ValidationObserver", ValidationObserver);

localize("ru", ru);

extend("required", {
    ...required,
    message: "поле обязательно для заполнения"
});

extend("numeric", numeric);
extend("min", min);
extend("max", max);
extend("length", length);
extend("email", email);
extend("alpha", alpha);
extend("alpha_dash", alpha_dash);
extend("confirmed", confirmed);
extend("is", is);
extend("is_not", is_not);
extend("regex", regex);

extend("confirmedBy", {
    params: ["target"],
    validate(value, { target }) {
        return value === target;
    },
    message: "{_field_} не совпадает с {target}"
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
