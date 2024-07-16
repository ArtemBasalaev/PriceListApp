import Vue from "vue";
import VueRouter from "vue-router";
import PriceListsView from "../views/PriceListsView.vue";
import PriceListView from "../views/PriceListView.vue";
import AddPriceListView from "../views/AddPriceListView.vue";

Vue.use(VueRouter)

const routes = [
    {
        path: "/",
        name: "priceListsView",
        component: PriceListsView,
        meta: {
            pageTitle: "Прайс листы"
        }
    },
    {
        path: "/PriceList/:id",
        name: "priceListView",
        component: PriceListView,
        props: true,
        meta: {
            pageTitle: "Прайс лист"
        }
    },
    {
        path: "/AddPriceList/",
        name: "addPriceListView",
        component: AddPriceListView,
        meta: {
            pageTitle: "Новый прайс лист"
        }
    },
]

const router = new VueRouter({
    mode: "history",
    base: "/",
    routes
})

router.beforeEach((to, _, next) => {
    if (to.meta?.pageTitle) {
        document.title = to.meta.pageTitle;
    }

    next();
});

export default router
