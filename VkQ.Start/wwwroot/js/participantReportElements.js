let scroller = new Scroller('/ReportElements/ParticipantReportElements', '.elements', '#dataForm')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});