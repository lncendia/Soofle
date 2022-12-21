let scroller = new Scroller('/Reports/GetReports', '.elements', '#dataForm')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});