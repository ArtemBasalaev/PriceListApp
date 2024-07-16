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

    createPriceList(createPriceListRequest) {
        return axios.post(this.url + "PriceList", createPriceListRequest);
    }

    getPriceLists() {
        return axios.get(this.url + "PriceList");
    }

    getPriceList(priceListId) {
        return axios.get(this.url + "PriceList/" + priceListId);
    }

    deleteProduct(productId) {
        return axios.post(this.url + "Product", productId);
    }
}