window.addBeforeUnloadListener = function (dotNetObject) {
    window.addEventListener('beforeunload', function (event) {
        dotNetObject.invokeMethodAsync('MarkUserOffline');
    });
};






//const connection = new signalR.HubConnectionBuilder()
//    .withUrl("/onlineStatusHub")
//    .build();

//connection.on("FriendOnline", function (userId) {
//    alert(`Your friend ${userId} is now online!`);
//});

//connection.on("ReceiveMessage", function (message) {
//    alert(message);
//});

//connection.start().catch(function (err) {
//    return console.error(err.toString());
//});
