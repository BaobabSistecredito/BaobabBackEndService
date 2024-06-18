namespace BaobabBackEndService.ExternalServices.MailSendService
{
    public interface IMailSendService
    {
        Task<string> SendEmailAsync(string toEmail,string CodeCoupon,string PurchaseId,string PurchaseValue,string RedemptionDate );
        
    }
}