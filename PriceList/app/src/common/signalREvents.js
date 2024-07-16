export default function subscribeEvents(signalR, store) {
    const connection = signalR.connection;

    connection.on("InstrumentPriceReached", price => {
        store.commit("toast/success", `Цена достигла занчения: ${price}`);
    });
       
    signalR.startConnection();    
}