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
    item: '.film', next: nextHandler, spinner: '.spinner', delay: 600
});

async function GetList(page) {
    $data.set('page', page)
    let data = await fetch('/ReportElements/LikeReportElements?' + $data.toString());
    if (data.status === 200) return await data.text();
    throw new Error(await data.text());
}

let elements = $('.elements')

function ParseData(json) {
    let data = JSON.parse(json);
    data.forEach(el => {
        let html = '<tr>\n' +
            '        <th scope="row">@(i + 1)</th>\n' +
            '        <td class="child">\n' +
            '            <a asp-controller="Participants" asp-action="EditParticipant" asp-route-id="@Participant.Id">'+el.name+'</a>\n' +
            '                <div>\n' +
            '                    <a class="text-decoration-none" data-bs-toggle="collapse" href="#userposts@(Model.MediaReports[i].Id)" role="button" aria-expanded="false">\n' +
            '                        Публикации с тегом (@Model.MediaReports[i].UserPosts.Count)\n' +
            '                    </a>\n' +
            '                </div>\n' +
            '                <div class="collapse" id="userposts@(Model.MediaReports[i].Id)">\n' +
            '                    <div class="card card-body bg-transparent border-0">\n' +
            '                        @foreach (var publication in Model.MediaReports[i].UserPosts)\n' +
            '                        {\n' +
            '                            <div class="border border-warning rounded rounded-3 px-1">\n' +
            '                                <a href="@publication.PublicationUrl" class="text-decoration-none text-center">\n' +
            '                                    <img src="@Url.Action("GetImage", "Reports", new { url = publication.ImageUrl }, Context.Request.Scheme)" alt="Фото" width="25px" height="25px"/>\n' +
            '                                    <span class="text-break">@TimeZoneInfo.ConvertTimeFromUtc(publication.CreationDate, timeZone).ToString("g", new CultureInfo("Ru"))</span>\n' +
            '                                </a>\n' +
            '                            </div>\n' +
            '                        }\n' +
            '                    </div>\n' +
            '                </div>\n' +
            '        </td>\n' +
            '        <td class="main">\n' +
            '            <div>\n' +
            '                <a class="text-decoration-none" data-bs-toggle="collapse" href="#values@(Model.MediaReports[i].Id)" role="button" aria-expanded="false">\n' +
            '                    <span>Пройдено: '+el.likes.filter(x=>x.isLiked).length+'<br>Не пройдено: +el.likes.filter(x=>!x.isLiked).length+</span>\n' +
            '                </a>\n' +
            '            </div>\n' +
            '            <div class="collapse" id="values@(Model.MediaReports[i].Id)">\n' +
            '                <div class="card card-body bg-transparent border-0">\n' +
            '                    @foreach (var publication in Model.MediaReports[i].Values)\n' +
            '                    {\n' +
            '                        <div class="border border-primary rounded rounded-3 px-1">\n' +
            '                            <a href="@publication.PublicationUrl" class="text-decoration-none">\n' +
            '                                <div>\n' +
            '                                    <img src="@Url.Action("GetImage", "Reports", new { url = publication.ImageUrl }, Context.Request.Scheme)" alt="Фото" width="25px" height="25px"/>\n' +
            '                                    @if (publication.Value)\n' +
            '                                    {\n' +
            '                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check2 text-success" viewBox="0 0 16 16">\n' +
            '                                            <path d="M13.854 3.646a.5.5 0 0 1 0 .708l-7 7a.5.5 0 0 1-.708 0l-3.5-3.5a.5.5 0 1 1 .708-.708L6.5 10.293l6.646-6.647a.5.5 0 0 1 .708 0z"/>\n' +
            '                                        </svg>\n' +
            '                                    }\n' +
            '                                    else\n' +
            '                                    {\n' +
            '                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x text-danger" viewBox="0 0 16 16">\n' +
            '                                            <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>\n' +
            '                                        </svg>\n' +
            '                                    }\n' +
            '                                </div>\n' +
            '                                <div>\n' +
            '                                    <div class="text-break">Опубликован: @TimeZoneInfo.ConvertTimeFromUtc(publication.CreationDate, timeZone).ToString("g", new CultureInfo("Ru"))</div>\n' +
            '                                </div>\n' +
            '                            </a>\n' +
            '                        </div>\n' +
            '                    }\n' +
            '                </div>\n' +
            '            </div>\n' +
            '        </td>\n' +
            '\n' +
            '        <td class="main">\n' +
            '            @if (Model.MediaReports[i].ChildReports.Any())\n' +
            '            {\n' +
            '                <div class="table-responsive">\n' +
            '                    <table class="table table-sm table-hover table-bordered table-striped table-condensed text-center align-middle">\n' +
            '                        <thead class="align-middle">\n' +
            '                        <tr>\n' +
            '                            <th scope="col">Участник</th>\n' +
            '                            <th scope="col">Публикации</th>\n' +
            '                            <th scope="col">Проверка</th>\n' +
            '                            <th scope="col">Заметка</th>\n' +
            '                            <th scope="col">VIP</th>\n' +
            '                        </tr>\n' +
            '                        </thead>\n' +
            '                        <tbody class="align-middle">\n' +
            '                        @foreach (var childReport in Model.MediaReports[i].ChildReports)\n' +
            '                        {\n' +
            '                            <tr>\n' +
            '                                <td class="child">\n' +
            '                                    <a asp-controller="Participants" asp-action="Participant" asp-route-id="@childReport.Participant.Id">@childReport.Participant.Username</a>\n' +
            '                                    @if (childReport.UserPosts.Any())\n' +
            '                                    {\n' +
            '                                        <div>\n' +
            '                                            <a class="text-decoration-none" data-bs-toggle="collapse" href="#userposts@(childReport.Id)" role="button" aria-expanded="false">\n' +
            '                                                Публикации с тегом (@childReport.UserPosts.Count)\n' +
            '                                            </a>\n' +
            '                                        </div>\n' +
            '                                        <div class="collapse" id="userposts@(childReport.Id)">\n' +
            '                                            <div class="card card-body bg-transparent border-0">\n' +
            '                                                @foreach (var publication in childReport.UserPosts)\n' +
            '                                                {\n' +
            '                                                    <div class="border border-warning rounded rounded-3 px-1">\n' +
            '                                                        <a href="@publication.PublicationUrl" class="text-decoration-none text-center">\n' +
            '                                                            <img src="@Url.Action("GetImage", "Reports", new { url = publication.ImageUrl }, Context.Request.Scheme)" alt="Фото" width="25px" height="25px"/>\n' +
            '                                                            <span class="text-break">@TimeZoneInfo.ConvertTimeFromUtc(publication.CreationDate, timeZone).ToString("g", new CultureInfo("Ru"))</span>\n' +
            '                                                        </a>\n' +
            '                                                    </div>\n' +
            '                                                }\n' +
            '                                            </div>\n' +
            '                                        </div>\n' +
            '                                    }\n' +
            '                                </td>\n' +
            '                                <td class="main">\n' +
            '                                    <div>\n' +
            '                                        <a class="text-decoration-none" data-bs-toggle="collapse" href="#values@(childReport.Id)" role="button" aria-expanded="false">\n' +
            '                                            <span>Пройдено: @childReport.Values.Count(publication => publication.Value)<br>Не пройдено: @childReport.Values.Count(publication => !publication.Value)</span>\n' +
            '                                        </a>\n' +
            '                                    </div>\n' +
            '                                    <div class="collapse" id="values@(childReport.Id)">\n' +
            '                                        <div class="card card-body bg-transparent border-0" style="font-size: 12px">\n' +
            '                                            @foreach (var publication in childReport.Values)\n' +
            '                                            {\n' +
            '                                                <div class="border border-primary rounded rounded-3 px-1">\n' +
            '                                                    <a href="@publication.PublicationUrl" class="text-decoration-none">\n' +
            '                                                        <div>\n' +
            '                                                            <img src="@Url.Action("GetImage", "Reports", new { url = publication.ImageUrl }, Context.Request.Scheme)" alt="Фото" width="25px" height="25px"/>\n' +
            '                                                            @if (publication.Value)\n' +
            '                                                            {\n' +
            '                                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check2 text-success" viewBox="0 0 16 16">\n' +
            '                                                                    <path d="M13.854 3.646a.5.5 0 0 1 0 .708l-7 7a.5.5 0 0 1-.708 0l-3.5-3.5a.5.5 0 1 1 .708-.708L6.5 10.293l6.646-6.647a.5.5 0 0 1 .708 0z"/>\n' +
            '                                                                </svg>\n' +
            '                                                            }\n' +
            '                                                            else\n' +
            '                                                            {\n' +
            '                                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x text-danger" viewBox="0 0 16 16">\n' +
            '                                                                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>\n' +
            '                                                                </svg>\n' +
            '                                                            }\n' +
            '                                                        </div>\n' +
            '                                                        <div>\n' +
            '                                                            <div class="text-break">Опубликован: @TimeZoneInfo.ConvertTimeFromUtc(publication.CreationDate, timeZone).ToString("g", new CultureInfo("Ru"))</div>\n' +
            '                                                        </div>\n' +
            '                                                    </a>\n' +
            '                                                </div>\n' +
            '                                            }\n' +
            '                                        </div>\n' +
            '                                    </div>\n' +
            '                                </td>\n' +
            '                                <td class="child">\n' +
            '                                    @if (childReport.Values.Any(publication => !publication.Value))\n' +
            '                                    {\n' +
            '                                        <span class="badge bg-danger">Нет</span>\n' +
            '                                    }\n' +
            '                                    else\n' +
            '                                    {\n' +
            '                                        <span class="badge bg-success">Да</span>\n' +
            '                                    }\n' +
            '                                </td>\n' +
            '                                <td class="child">\n' +
            '                                    <div class="text-break">@childReport.Participant.Note</div>\n' +
            '                                </td>\n' +
            '                                <td class="child">\n' +
            '                                    @if (childReport.Participant.Vip)\n' +
            '                                    {\n' +
            '                                        <span class="badge bg-success">Да</span>\n' +
            '                                    }\n' +
            '                                    else\n' +
            '                                    {\n' +
            '                                        <span class="badge bg-danger">Нет</span>\n' +
            '                                    }\n' +
            '                                </td>\n' +
            '                            </tr>\n' +
            '                        }\n' +
            '                        </tbody>\n' +
            '                    </table>\n' +
            '                </div>\n' +
            '            }\n' +
            '        </td>\n' +
            '        <td class="child">\n' +
            '            @if (Model.MediaReports[i].Values.Any(publication => !publication.Value))\n' +
            '            {\n' +
            '                <span class="badge bg-danger">Нет</span>\n' +
            '            }\n' +
            '            else\n' +
            '            {\n' +
            '                <span class="badge bg-success">Да</span>\n' +
            '            }\n' +
            '        </td>\n' +
            '        <td class="child">\n' +
            '            <div class="text-break">@Model.MediaReports[i].Participant.Note</div>\n' +
            '        </td>\n' +
            '        <td class="child">\n' +
            '            <span class="badge bg-success">@Model.MediaReports[i].Participant.Instagram.LikeChat</span>\n' +
            '        </td>\n' +
            '        <td class="child">\n' +
            '            @if (Model.MediaReports[i].Participant.Vip)\n' +
            '            {\n' +
            '                <span class="badge bg-success">Да</span>\n' +
            '            }\n' +
            '            else\n' +
            '            {\n' +
            '                <span class="badge bg-danger">Нет</span>\n' +
            '            }\n' +
            '        </td>\n' +
            '    </tr>'
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