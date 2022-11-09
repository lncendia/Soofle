let checks = document.getElementsByName("ids")
let selectAll = document.getElementsByName("selection_all")[0]
checks.forEach(check => {
    check.onclick = checkOnClick
})
selectAll.onclick = select
    
function checkOnClick() {
    if (selectAll.checked) selectAll.checked = false
}

function select() {
    let checked = selectAll.checked
    checks.forEach(chek => {
        chek.checked = checked
    })
}
