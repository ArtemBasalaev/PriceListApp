export default class PagesNavigation {
    static goBack() {
        if (window.history.length > 1) {
            this.$router.back();
        } else {
            this.$router.push({
                name: "portfolioInstruments"
            });
        }
    }
}