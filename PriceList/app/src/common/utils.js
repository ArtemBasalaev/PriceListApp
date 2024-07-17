export default class PagesNavigation {
    static formatDate(dateTime) {
        if (dateTime === null) {
            return "-";
        }

        const date = new Date(Date.parse(dateTime));

        const options = {
            year: "numeric",
            month: "long",
            day: "numeric"
        };

        return date.toLocaleString("ru", options);
    }
}