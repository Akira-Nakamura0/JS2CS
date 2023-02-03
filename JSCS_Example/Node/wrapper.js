const WebSocket = require("ws");
var Global = CreateConfiguration();
var WebSocketServer = null;

function Connect() {
    function IsNumerical(str) {
        return Boolean(str.match(/\d/));
    }
    function Sleep(ms) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }
    function SanitizeText(data) {
        return Buffer.from(data).toString().split("|||");
    }
    return new Promise(function (resolve, reject) {
        WebSocketServer = new WebSocket.Server({ port: Global.Port }, () => {
            console.log("201: Created");
        });
        WebSocketServer.on("connection", async function connection(ws) {
            ws.on("close", function () {
                Global.IsAlive = false;
                console.log("499: Client Closed Connection");
            });
            ws.on("message", async function (data) {
                await Sleep(1000);
                Global.Message.Text = SanitizeText(data)[1];
                Global.Message.StatusCode = SanitizeText(data)[0];
                Global.IsNumerical = IsNumerical(Global.Message.StatusCode);
                if (!Global.IsNumerical) return;
                Global.Message.StatusCode = parseInt(SanitizeText(data)[0]);
                switch (Global.Message.StatusCode) {
                    case 201:
                        console.log("202: Accepted");
                        ws.send("202|||Accepted");
                        resolve(true);
                        break;
                    default:
                        if (Global.IsAlive) Global = CreateConfiguration();
                        else {
                            if (!Global.IsInitial) {
                                console.log("200: OK");                                
                                Global.IsInitial = true;
                            }
                            Global.IsAlive = true;                           
                        }
                        ws.send("200|||OK");
                        break;
                }
            });
        });
       
        WebSocketServer.on("error", async function connection(ws) {
            reject(new Error(err.message));
        });
    });
}

function CreateConfiguration() {
    var Global = {
        Port: 8080,
        IsNumerical: false,
        IsInitial: false,
        Message: {
            Test: "",
            StatusCode: 0,
        },
        IsAlive: false,
    };
    return Global;
}

module.exports = async () => {
    return {
        Connect: Connect,
    };
};