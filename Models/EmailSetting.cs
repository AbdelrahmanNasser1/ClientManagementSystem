namespace ClientManagementSystemMVC.Models;

public class EmailSettingOptions
{
    public const string EmailSetting = "EmailSettings";
    public string SmtpServer { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
