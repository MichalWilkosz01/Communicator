"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

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
connection.on("SendMessage", function (message, time) {
    const messageList = document.getElementById("messages");
    const li = document.createElement("li");
    li.className = "message";
    li.innerHTML = `
        <p class="sender-name">You:</p>
        <p class="message-content">${message}</p>
        <p class="message-time">Sent at: ${time}</p>
    `;
    messageList.appendChild(li);
});
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});



document.getElementById("sendButton").addEventListener("click", function (event) {
    var currentdate = new Date();
    var sendingTime = "Sent at: " + currentdate.getDate() + "-"
        + (currentdate.getMonth() + 1) + "-"
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    var message = document.getElementById("messageInput").value;
    var receiverId = document.getElementById("receiverInput").value;
    connection.invoke("SendMessage", message, receiverId, sendingTime).catch(function (err) {
        return console.error(err.toString());
    });
    
    event.preventDefault();
});

