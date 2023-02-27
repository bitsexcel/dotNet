namespace EmailSender  
{  
    class Program  
    {  
        static void Main(string[] args)  
        {  
            string smtpServer = "smtp-mail.outlook.com";  
            SendEmail(smtpServer);  
        }  
  
        static void SendEmail(string smtpServer)  
        {  
            //Send teh High priority Email  
           EmailManager mailMan = new EmailManager(smtpServer);  
  
            EmailSendConfigure myConfig = new EmailSendConfigure(); 
            // replace with your email userName  
            //myConfig.ClientCredentialUserName = "abc@outlook.com";
            myConfig.ClientCredentialUserName = "daniel.perezvindas@outlook.com";
            // replace with your email account password
            myConfig.ClientCredentialPassword = "xxxxx";
            //myConfig.TOs = new string[] { "user1@outlook.com" }; 
            myConfig.TOs = new string[] { "daniel.perezvindas@gmail.com" };
            myConfig.CCs = new string[] { };
            //myConfig.From = "<YOUR_ACCOUNT>@outlook.com";  
            myConfig.From = "daniel.perezvindas@outlook.com";
            //myConfig.FromDisplayName = "<YOUR_NAME>";
            myConfig.FromDisplayName = "Daniel Perez";
            myConfig.Priority = System.Net.Mail.MailPriority.Normal;  
            myConfig.Subject = "WebSite was down - please investigate";  
  
            EmailContent myContent = new EmailContent();  
            myContent.Content = "The following URLs were down - 1. Foo, 2. bar";  
  
            mailMan.SendMail(myConfig, myContent);  
        }  
  
    }  
}