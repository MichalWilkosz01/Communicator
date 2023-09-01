"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messages").appendChild(li);
    
    /*li.textContent = `${user} says ${message}`;*/
    li.textContent = `${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});



document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var message = document.getElementById("messageInput").value;
    var receiverId = document.getElementById("receiverInput").value;
    connection.invoke("SendMessage", message, receiverId).catch(function (err) {
        return console.error(err.toString());
    });
    
    event.preventDefault();
});

