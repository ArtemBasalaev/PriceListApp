import * as signalR from "@microsoft/signalr";

class SignalRConnection {
    connection;
    delay;

    constructor() {
        this.delay = 2000;

        const baseUrl = ""; // "https://localhost:44352";

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(baseUrl + "/hub")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this.connection.onclose(() => {
            this.startConnection();
        });
    }

    startConnection() {
        this.connection.start().catch(err => {
            console.error(`Reconnect error: ${err.toString()}`);
            setTimeout(() => this.startConnection(), this.delay);
        });
    }
}

export default new SignalRConnection();
