let messageBox = $("#message");

$(document).ready(() => {
    $('#likeForm').data("validator").settings.submitHandler = async (form, ev) => {
        ev.preventDefault()
        await Send('/ReportsManager/StartLikeReport', new FormData(form))
    }
    $('#participantForm').validate({
        submitHandler: async (form, ev) => {
            ev.preventDefault()
            await Send('/ReportsManager/StartParticipantReport', new FormData(form))
        }
    })
})

async function Send(url, form) {
    let result = await fetch(url, {
        method: 'POST',
        body: form
    });
    if (result.ok) Success();
    else BadRequest(await result.text())
}

function Success() {
    messageBox.empty()
    messageBox.html('<div class="alert alert-secondary alert-dismissible fade show" role="alert">Отчёт успешно создан<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
}

function BadRequest(error) {
    messageBox.empty()
    messageBox.html('<div class="alert alert-secondary alert-dismissible fade show" role="alert">' + error + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
}