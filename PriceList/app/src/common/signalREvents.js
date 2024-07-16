export default function subscribeEvents(signalR, store) {
    const connection = signalR.connection;

    connection.on("PriceListCreated", priceListName => {
        store.commit("toast/success", `Создан прайс-лист: ${priceListName}`);
    });
       
    signalR.startConnection();    
}