using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Documents;

namespace Shop.Presentation.Pages
{
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pageHeader = MainWindow.pageHeaderUC;

            MessageBox.Show("Your message has been sent successfully. We will get back to you shortly.", "Message Sent", MessageBoxButton.OK, MessageBoxImage.Information);
            TextRange textRange = new TextRange(problemTxtBox.Document.ContentStart, problemTxtBox.Document.ContentEnd);

            SendSupportEmail(pageHeader.UserEmail, pageHeader.UserName, textRange.Text);
            SendConfirmationEmail(pageHeader.UserName, pageHeader.UserEmail);
        }
        private void SendConfirmationEmail(string userName, string toMail)
        {
            string fromMail = "victor.sevcenco22@gmail.com";
            string fromPassword = "tyld bwky egwb cruh";
            string bodyMessage = $@"
                <html>
                <body>
                    <h2>Confirmation of Received Request</h2>
                    <p>Dear {userName},</p>
                    <p>Thank you for reaching out to us. We have received your request and will get back to you as soon as possible.</p>
                    <p>Best regards, Victor</p>
                    <p>Nexus Team</p>
                </body>
                </html>";


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail, "Nexus Company");
            message.Subject = "Received Request";
            message.To.Add(new MailAddress(toMail));
            message.Body = bodyMessage;
            message.IsBodyHtml = true;
            var smtpclient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpclient.Send(message);
        }
        private void SendSupportEmail(string userEmail, string userName, string description)
        {
            string fromMail = "victor.sevcenco22@gmail.com";
            string toMail = "nicee.sevcenco@gmail.com";
            string fromPassword = "tyld bwky egwb cruh";
            string bodyMessage = $@"
                <html>
                <body>
                    <h2>User Assistance Request</h2>
                    <p><strong>From:</strong>{userName}</p>
                    <small>{userEmail}</small>
                    <p><strong>Description of the issue:</strong></p>
                    <p>{description}</p>
                    <br />
                    <p>Best regards, Victor</p>
                    <p>Nexus Assistance Team</p>
                </body>
                </html>";


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail, "Nexus Company");
            message.Subject = "User Assistance Request";
            message.To.Add(new MailAddress(toMail));
            message.Body = bodyMessage;
            message.IsBodyHtml = true;
            var smtpclient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpclient.Send(message);
        }
    }
}
