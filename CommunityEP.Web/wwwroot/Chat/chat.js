let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5001/messageHub")//https://localhost:7001/messagehub
        .build();

    connection.on("GetLeftMessage", function (obj) {
        const messageList = document.getElementById("messageList");
        messageList.innerHTML += `<div><img src="${obj.image}" class="Img"><div class="left"><span>${obj.userName}</span><div><span>${obj.message}</span></div></div></div><span class="time">${obj.sendTime}</span>`;
    });

    connection.on("GetRightMessage", function (obj) {
        console.log(obj);
        const messageList = document.getElementById("messageList");
        messageList.innerHTML += `<div><div class="right"><span>${obj.userName}</span><div><span>${obj.message}</span></div></div><img src="${obj.image}" class="Img"></div><span class="time">${obj.sendTime}</span>`;
    });

    connection.on("GetBriefMessage", function (obj) {
        const briefMessage = document.getElementById("ChatBrief");
        briefMessage.innerHTML = `<h2>聊天室</h2><span>(在线人数${obj})</span>`;
    });

    connection.on("Finished", function () {
        connection.stop();
        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "Finished";
    });

    connection.start()
        .catch(err => console.error(err.toString()));
}

setupConnection();//调用上面的函数

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    let message = document.getElementById("message").value;
    document.getElementById("message").value = "";
    if (message != "") {
        connection.invoke("SendMessages", message);//有多个值就用逗号隔开
    }
});