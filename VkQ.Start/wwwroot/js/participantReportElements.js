let scroller = new Scroller('/ReportElements/ParticipantReportElements', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});