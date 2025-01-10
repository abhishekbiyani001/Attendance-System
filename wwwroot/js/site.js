function downloadPdf(link) {
    const a = document.createElement("a");
    a.href = link;
    a.download = "AttendanceSummary.pdf";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}