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
        priceListsColumns: []
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

        createPriceList({ state }, createPriceListRequest) {
            return state.priceListService.createPriceList(createPriceListRequest);
        },

        getPriceLists({ state }) {
            return state.priceListService.getPriceLists();
        }
    },

    modules: {
        toast: ToastModule
    }
});

//SubscribeSignalREvents(SignalR, store);

export default store;
