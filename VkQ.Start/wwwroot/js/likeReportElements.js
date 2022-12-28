let scroller = new Scroller('/ReportElements/LikeReportElements', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});