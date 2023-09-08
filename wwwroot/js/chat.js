"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();



connection.on("ReceiveMessage", function (message, time, senderName) {
    const messageList = document.getElementById("messages");
    const li = document.createElement("li");
    li.className = "message";
        li.innerHTML = `
            <p class="sender-name">${senderName}:</p>
            <p class="message-content">${message}</p>
            <p class="message-time">Sent at: ${time}</p>
        `;
    messageList.appendChild(li);
});
connection.start().then(function () {
    var senderId = document.getElementById("senderIdInput").value;
    var receiverId = document.getElementById("receiverIdInput").value;
    connection.invoke("PreparePrivateGroup", senderId, receiverId).catch(function (err) {
        return console.error(err.toString());
    })
    return console.log("Connection has been established ")
}).catch(function (err) {
    return console.error(err.toString());
});



document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var senderId = document.getElementById("senderIdInput").value
    var receiverId = document.getElementById("receiverIdInput").value;
    connection.invoke("SendMessage", message,senderId, receiverId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    
});


