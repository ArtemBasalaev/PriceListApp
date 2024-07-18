export default class Utils {
    static goBack() {
        if (window.history.length > 1) {
            this.$router.back();
        } else {
            this.$router.push({
                name: "priceListsView"
            });
        }
    }
}