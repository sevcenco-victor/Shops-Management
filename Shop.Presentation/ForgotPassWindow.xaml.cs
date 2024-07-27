using Shop.ApplicationServices.Services;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Windows;

namespace Shop.Presentation
{
    public partial class ForgotPassWindow : Window
    {
        private string oneTimeCode = String.Empty;
        private readonly UserRepository _userRespository = new UserRepository(new NexusDbContext());
        public ForgotPassWindow()
        {
            InitializeComponent();
        }
        private void SendOneTimeCode(string toMail, string userName)
        {
            string fromMail = "victor.sevcenco22@gmail.com";
            string fromPassword = "tyld bwky egwb cruh";
            string bodyMessage = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
            }}
            .container {{
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
                border: 1px solid #ccc;
                border-radius: 10px;
            }}
            .header {{
                text-align: center;
                padding: 10px 0;
                border-bottom: 1px solid #eee;
            }}
            .content {{
                padding: 20px;
            }}
            .otp {{
                display: block;
                width: fit-content;
                margin: 10px auto;
                padding: 10px;
                font-size: 20px;
                color: #fff;
                background-color: #007BFF;
                border-radius: 5px;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
                font-size: 12px;
                color: #777;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h2>Password Reset Request</h2>
            </div>
            <div class='content'>
                <p>Dear {userName},</p>
                <p>We received a request to reset your password for your account associated with this email address. Please use the following One-Time Password (OTP) to reset your password:</p>
                <span class='otp'>{oneTimeCode}</span>
                <p>This OTP is valid for the next 15 minutes. If you did not request a password reset, please ignore this email or contact our support team immediately.</p>
                <p>For security reasons, please ensure your new password is strong and not easily guessable. A good password typically includes a combination of upper and lower case letters, numbers, and special characters.</p>
                <p>If you have any questions or need further assistance, feel free to contact our support team at nexusSupport@yahoo.com or call us at (373) 787-67-311.</p>
                <p>Thank you for your prompt attention to this matter.</p>
                <p>Best regards,<br/>
                Nexus Team</p>
            </div>
            <div class='footer'>
                <p>This is an automated message, please do not reply directly to this email.</p>
            </div>
        </div>
    </body>
    </html>";


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail, "Nexus Company");
            message.Subject = "Password Reset Request";
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
        public string GetOneTimeCode(int length = 6)
        {
            const string validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                for (int i = 0; i < length; i++)
                {
                    int randomIndex = randomBytes[i] % validCharacters.Length;
                    code[i] = validCharacters[randomIndex];
                }
            }
            return new string(code);
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var user = _userRespository.GetUserByEmail(userEmail.Text);
            if (user != null)
            {
                userEmail.IsEnabled = false;
                codeStPanel.Visibility = Visibility.Visible;
                oneTimeCode = GetOneTimeCode();
                SendOneTimeCode(user.Email, user.Name);
                emailButton.Visibility = Visibility.Collapsed;
                verifyButton.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("There is no user with this email, please try again!");
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (userOneTimeCode.Text.Equals(oneTimeCode))
            {
                verifyButton.Visibility = Visibility.Collapsed;
                userOneTimeCode.IsEnabled = false;
                newPassStPan.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Incorrect Code");
            }
        }
        private void UpdatePass_Click(object sender, RoutedEventArgs e)
        {
            var user = _userRespository.GetUserByEmail(userEmail.Text);
            if (user != null)
            {
                if (UserServices.IsValidPassword(userPassword.Password))
                {
                    string newHashPassword = UserServices.GetHashedPassword(userPassword.Password);
                    _userRespository.UpdateUserPassword(user, newHashPassword);
                    MessageBoxResult result = MessageBox.Show("Done \n Back to Login Page?", "Successful", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        new LoginWindow().Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Password must be at least 8 characters long and contain at least three of the following: uppercase letter, lowercase letter, digit, and special character.");
                }
            }
        }
    }
}
