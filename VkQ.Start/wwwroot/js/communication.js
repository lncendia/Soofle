function copy(id, el) {
    let copyText = document.getElementById("link" + id);
    copyText.setAttribute("type", "text");
    el.setAttribute("aria-label", "Скопировано")
    copyText.select();
    document.execCommand("copy");
    copyText.setAttribute("type", "hidden");
    return false
}
function mouseout(el){
    el.setAttribute("aria-label", "Скопировать")
}