import axios from "axios";

export default class Service {
    constructor() {
        this.url = "/api/";
    }

    getColumnsNames() {
        return axios.get(this.url + "Column");
    }

    getColumnTypes() {
        return axios.get(this.url + "Type");
    }

    addPriceList(request) {
        return axios.post(this.url + "PriceList", request);
    }

    getPriceLists() {
        return axios.get(this.url + "PriceList");
    }

    addDataInPriceList(request) {
        return axios.post(this.url + "PriceData", request);
    }

    getPriceList(priceListId) {
        return axios.get(this.url + "PriceList/" + priceListId);
    }

    addNewProduct(request) {
        return axios.post(this.url + "Product", request);
    }

    deleteProduct(request) {
        return axios.delete(this.url + "PriceData", request);
    }
}