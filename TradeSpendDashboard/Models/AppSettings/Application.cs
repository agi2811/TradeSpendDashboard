namespace TradeSpendDashboard.Model.AppSettings
{
    public class Application
    {
        public string Name { get; set; }
        public int Version { get; set; }
        public string AccessToken { get; set; }
        public string HostMasterData { get; set; }
        public string HostInvoiceData { get; set; }
        public int? DBCommandTimeout { get; set; }
        public bool SQLLogging { get; set; }
        public string ConnectionStrings { get; set; }
        public string BaseUrl { get; set; }
        public string MailFrom { get; set; }
        public string MailSMTP { get; set; }
        public string MailSmtpPort { get; set; }
        public string MailSmtpSsl { get; set; }
        public string MailUsername { get; set; }
        public string MailPassword { get; set; }
        public string Receiver { get; set; }
        public string CcEmail { get; set; }
        public string UploadStagingDir { get; set; }
        public string UploadTargetDir { get; set; }
    }
}
