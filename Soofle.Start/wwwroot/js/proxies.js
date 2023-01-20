let scroller = new Scroller('/Proxy/Proxies', '.elements', '#filter')
let sender = new AsyncSender('#message');

$(document).ready(() => {
    scroller.Start();
    sender.Add('#proxiesForm', "Прокcи успешно загружены")
})


$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});