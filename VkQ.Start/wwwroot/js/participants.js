let scroller = new Scroller('/Participants/Participants', '.elements', '#dataForm')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});