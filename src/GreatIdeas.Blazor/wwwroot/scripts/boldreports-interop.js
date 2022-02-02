// Interop file to render the Bold Report Viewer component with properties.
window.BoldReports = {
    RenderViewer: function (elementID, reportViewerOptions) {
        $("#" + elementID).boldReportViewer({
            reportPath: reportViewerOptions.reportName,
            reportServiceUrl: reportViewerOptions.serviceURL,
            printMode: true,
            isResponsive: true,
            exportSettings: {
                exportOptions: ej.ReportViewer.ExportOptions.All & ~ej.ReportViewer.ExportOptions.Html & ~ej.ReportViewer.ExportOptions.Word & ~ej.ReportViewer.ExportOptions.PowerPoint & ~ej.ReportViewer.ExportOptions.XML
            }
        });
    }
}

window.BoldReports = {
    RenderViewerParameter: function (elementID, reportViewerOptions) {
        $("#" + elementID).boldReportViewer({
            reportPath: reportViewerOptions.reportName,
            reportServiceUrl: reportViewerOptions.serviceURL,
            printMode: true,
            parameters: reportViewerOptions.parameters,
            isResponsive: true,
            toolbarSettings: {
                items: ej.ReportViewer.ToolbarItems.All & ~ej.ReportViewer.ToolbarItems.Parameters
            },
            exportSettings: {
                exportOptions: ej.ReportViewer.ExportOptions.All & ~ej.ReportViewer.ExportOptions.Html & ~ej.ReportViewer.ExportOptions.Word & ~ej.ReportViewer.ExportOptions.PowerPoint & ~ej.ReportViewer.ExportOptions.XML
            }
        });
    }
}

function RenderReportViewer(reportPathInfo, elementID) {
    $("#" + elementID).boldReportViewer({
        reportServiceUrl: '/api/BoldReportsAPI',
        reportPath: reportPathInfo,
        printMode: true,
        isResponsive: true,
        enableParameterBlockScroller: false,
        exportSettings: {
            exportOptions: ej.ReportViewer.ExportOptions.All & ~ej.ReportViewer.ExportOptions.Html & ~ej.ReportViewer.ExportOptions.Word & ~ej.ReportViewer.ExportOptions.PowerPoint & ~ej.ReportViewer.ExportOptions.XML
        }
    });
}