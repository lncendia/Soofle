let scroller = new Scroller('/ReportElements/LikeReportElements', '.elements', '#dataForm')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});