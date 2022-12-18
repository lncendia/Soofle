let $data = new URLSearchParams();
let data = $('#dataForm')

let nextHandler = async function (pageIndex) {
    try {
        let data = await GetList(pageIndex + 1);
        return ParseData(data);
    } catch (e) {
        console.log(e);
        return false;
    }
}

let scroller = new InfiniteAjaxScroll('.elements', {
    item: '.element', next: nextHandler, spinner: '.spinner', delay: 600
});

async function GetList(page) {
    $data.set('page', page)
    let data = await fetch('/ReportElements/ParticipantReportElements?' + $data.toString());
    if (data.status === 200) return await data.text();
    throw new Error(await data.text());
}

let elements = $('.elements')

function ParseData(json) {
    let data = JSON.parse(json);
    let i = 0;
    data.forEach(el => {
        let html = '<tr class="element">\n' +
            '                <th scope="row">' + i++ + '</th>\n' +
            '                <td class="main">\n' +
            '                    <span>' + el.name + '</span>\n'
        if (el.newName !== undefined) html += "<span>' + el.newName + '</span>\n'"
        html += '</td>\n' +
            '                <td class="main">\n' +
            '                    <span> @Model.ParticipantReports[i].Username</span>\n' +
            '                </td>\n' +
            '                <td class="child">\n'
        if (el.type !== undefined) {
            let text;
            switch (el.type) {
                case "new":
                    text = '<span class="badge bg-success">Добавлен в чат</span>\n'
                    break;
                case "leave":
                    text = '<span class="badge bg-danger">Покинул чат</span>\n'
                    break;
                case"rename":
                    text = '<span class="badge bg-primary">Сменил ник</span>\n'
                    break;
            }
            html += text;
        }
        html += '</td>\n' +
            '      </tr>'
        elements.append(html);
    })
    return true;
}

function GetFormData() {
    let d = new FormData(data[0]);
    $data = new URLSearchParams();
    d.forEach((value, key) => {
        $data.set(key, value);
    })
}

data.on('change', Select);

async function Select() {
    scroller.pause();
    elements.empty();
    GetFormData();
    scroller.pageIndex = -1;
    scroller.resume();
    await scroller.next();
}

$('#clearFilter').click(function () {
    let form = data[0];
    form.reset()
    form.dispatchEvent(new Event('change'))
    return false;
});