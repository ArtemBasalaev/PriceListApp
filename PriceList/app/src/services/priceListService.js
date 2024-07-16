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

    //getFormerInstruments() {
    //    return axios.get(this.url + "GetFormerInstruments");
    //}

    //getInstrumentOperations(figi) {
    //    return axios.get(this.url + "GetInstrumentOperations", {
    //        params: {
    //            figi
    //        }
    //    });
    //}

    //getInstrumentSessions(figi) {
    //    return axios.get(this.url + "GetInstrumentSessions", {
    //        params: {
    //            figi
    //        }
    //    });
    //}

    //getInstrumentLastPrice(figi) {
    //    return axios.get(this.url + "GetInstrumentLastPrice", {
    //        params: {
    //            figi
    //        }
    //    });
    //}

    //findInstruments(query) {
    //    return axios.get(this.url + "FindInstruments", {
    //        params: {
    //            query
    //        }
    //    });
    //}

    //getMarginCommissionFees() {
    //    return axios.get(this.url + "GetMarginCommissionFees");
    //}

    //getDividendsFees() {
    //    return axios.get(this.url + "GetDividendsFees");
    //}

    //getTaxesFees() {
    //    return axios.get(this.url + "GetTaxesFees");
    //}

    //getCouponsFees() {
    //    return axios.get(this.url + "GetCouponsFees");
    //}

    //addNotification(request) {
    //    return axios.post(this.url + "AddNotification", request);
    //}

    //deleteNotification(notificationId) {
    //    return axios.post(this.url + "DeleteNotification", notificationId);
    //}

    //getNotifications() {
    //    return axios.get(this.url + "GetNotifications");
    //}

    //updateNotification(request) {
    //    return axios.post(this.url + "UpdateNotification", request);
    //}
}