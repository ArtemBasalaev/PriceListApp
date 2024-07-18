export default {
    namespaced: true,

    state: {
        notifications: []
    },

    mutations: {
        success(state, text) {
            state.notifications.push({
                text,
                type: "success"
            });
        },

        error(state, error) {
            if (typeof error === "string") {
                state.notifications.push({
                    text: error,
                    type: "error"
                });

                return;
            }

            if (typeof error === "object") {
                let message = "Неизвестная ошибка";

                if (error.response && error.response.data && error.response.data.message && typeof error.response.data.message === "string") {
                    message = error.response.data.message;
                } else if (error.message && typeof error.message === "string") {
                    message = error.message;
                }

                state.notifications.push({
                    text: message,
                    type: "error"
                });

                return;
            }

            state.notifications.push({
                text: "Неизвестная ошибка",
                type: "error"
            });
        },

        warning(state, text) {
            state.notifications.push({
                text,
                type: "warning"
            });
        },

        removeNotification(state, notification) {
            state.notifications = state.notifications.filter(n => n !== notification);
        }
    }
};
