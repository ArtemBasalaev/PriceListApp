import * as signalR from "@microsoft/signalr";

class SignalRConnection {
    connection;
    delay;

    constructor() {
        this.delay = 2000;

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("/hub")
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
