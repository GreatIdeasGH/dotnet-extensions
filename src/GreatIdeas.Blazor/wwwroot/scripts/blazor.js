function windowPrint() {
    window.print()
}

// Get IP Address
window.getIpAddress = () => {
    return fetch('https://jsonip.com/')
        .then((response) => response.json())
        .then((data) => {
            return data.ip
        })
}

// Format beautify json display
function prettifyAuditJson() {
    let oldJSON = document.getElementById('prettyOldJSONFormat').value;
    let newJSON = document.getElementById('prettyNewJSONFormat').value;

    if (oldJSON == null || oldJSON === "") {
        console.log("oldJSON is null")
    } else {
        let parseOldJSON = JSON.parse(oldJSON);
        document.getElementById('prettyOldJSONFormat').value = JSON.stringify(parseOldJSON, undefined, 4);
    }

    if (newJSON == null || newJSON === "") {
        console.log("newJSON is null")
    } else {
        let parseNewJSON = JSON.parse(newJSON);
        document.getElementById('prettyNewJSONFormat').value = JSON.stringify(parseNewJSON, undefined, 4);
    }
}

// Save file
window.saveAsFile = function (fileName, byteBase64) {
    let link = this.document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    this.document.body.appendChild(link);
    link.click();
    this.document.body.removeChild(link);
    //alert("Downloaded");
}

function download(uri, name)
{
    var link = document.createElement("a");
    // If you don't know the name or want to use
    // the webserver default set name = ''
    link.setAttribute('download', name);
    link.href = uri;
    document.body.appendChild(link);
    link.click();
    link.remove();
}

var blazorInterop = blazorInterop || {};

blazorInterop.showAlert = function (message) {
    alert(message);
}

blazorInterop.logToConsoleTable = function (obj) {
    console.table(obj);
}

blazorInterop.focusElement = function (element) {
    element.focus();
}

blazorInterop.registerResizeHandler = function (dotNetObjectRef) {
    function resizeHandler() {
        dotNetObjectRef.invokeMethodAsync("SetWindowSize",
            {
                width: window.innerWidth,
                height: window.innerHeight
            });
    }

    // Set up initial value
    resizeHandler();

    // Register event handler
    window.addEventListener("resize", resizeHandler)
}

blazorInterop.registerOnlineHandler = function (dotNetObjectRef) {
    function onlineHandler() {
        dotNetObjectRef.invokeMethodAsync("SetOnlineStatus", navigator.onLine);
    };

    // Set up initial value
    onlineHandler();

    // Register event handler
    window.addEventListener("online", onlineHandler)
    window.addEventListener("offline", onlineHandler)
}

// Anchor Navigation for blazor
function ScrollToId(id) {
    const element = document.getElementById(id);
    if (element instanceof HTMLElement) {
        element.scrollIntoView({
            behavior: "smooth",
            block: "start",
            inline: "nearest"
        });
    }
}
