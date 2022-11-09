let checks = document.getElementsByName("selection[]")
let selectAll = document.getElementsByName("selection_all")[0]
checks.forEach(check => {
    check.onclick = checkOnClick
})
selectAll.onclick = select
let textarea = document.getElementById("usernames")
let form = document.getElementById("usernameForm")
document.getElementById("usernameButton").onclick =
    form.onchange = onChange

function checkOnClick() {
    if (selectAll.checked) selectAll.checked = false
}

function select() {
    let checked = selectAll.checked
    checks.forEach(chek => {
        chek.checked = checked
    })
}

function onChange() {
    let formData = new FormData(form)
    let separator = formData.get("separation") === "space" ? ' ' : "\n"
    let addChat = formData.get("addChat") != null
    let addAt = formData.get("addAt") != null
    let text = ''
    checks.forEach(participant => {
        if (!participant.checked) return
        if (addAt) text += '@'
        text += participant.getAttribute("data-username")
        if (addChat) {
            let chat = participant.getAttribute("data-chat")
            if (chat !== "") text += " - " + chat
        }
        text += separator
    })
    if (text === '') textarea.innerHTML = "Вы не выбрали пользователей."
    else textarea.innerHTML = text.substr(0, text.length - 1)
}

textarea.onclick = () => {
    textarea.select()
    document.execCommand("copy")
}