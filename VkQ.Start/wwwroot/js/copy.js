function copy(id, el) {
    let copyText = document.getElementById("link" + id)
    copyText.setAttribute("type", "text")
    copyText.select()
    document.execCommand("copy")
    copyText.setAttribute("type", "hidden")
    el.innerText="Скопировано"
    return false
}
function mouseout(el){
    el.innerText="Скопировать"
}