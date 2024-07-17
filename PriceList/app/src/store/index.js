import Vue from 'vue';
import Vuex from 'vuex';
import ToastModule from "@/modules/toast";
import PriceListService from "@/services/priceListService";
import SignalR from "@/common/signalr";
import SubscribeSignalREvents from "@/common/signalREvents";

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        priceListService: new PriceListService(),
        priceListsColumns: [],
        connection: SignalR.connection,
    },

    getters: {

    },

    mutations: {
        setPriceListsColumns(state, priceListsColumns) {
            state.priceListsColumns = priceListsColumns;
        }
    },

    actions: {
        getColumnNames({ state }) {
            return state.priceListService.getColumnsNames();
        },

        getColumnTypes({ state }) {
            return state.priceListService.getColumnTypes();
        },

        addPriceList({ state }, request) {
            return state.priceListService.addPriceList(request);
        },

        addDataInPriceList({ state }, request) {
            return state.priceListService.addDataInPriceList(request);
        },

        addNewProduct({ state }, request) {
            return state.priceListService.addNewProduct(request);
        },

        getPriceLists({ state }) {
            return state.priceListService.getPriceLists();
        },

        getPriceList({ state }, id) {
            return state.priceListService.getPriceList(id);
        },
    },

    modules: {
        toast: ToastModule
    }
});

//SubscribeSignalREvents(SignalR, store);

export default store;
