using DevExpress.XtraReports.UI;

namespace BlogBack.PredefinedReports
{
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["TagReport"] = () => new TagReport(),
            ["PostReport"] = () => new PostReport()
        };
    }
}
