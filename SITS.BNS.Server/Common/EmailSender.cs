using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Me.SendMail;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
using SITS.BNS.Entities.Common;
using SITS.BNS.Common.Interfaces;
using SITS.BNS.Entities.Entity;

namespace SITS.BNS
{
    public class MailAddress_
    {
        public string? Address { get; set; }
        public string? DisplayName { get; set; }
    }


    public class EmailRequest
    {
        public string? FromAddress { get; set; }
        public MailAddress_[] ToAddresses { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public System.Net.Mail.Attachment? attachement { get; set; }
        public string? Type { get; set; }
    }

    public class InternalEmailRequest
    {
        public MailAddress[] ToAddresses { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public System.Net.Mail.Attachment? Attachment { get; set; }
    }

    public interface IEmailSender
    {
        Task<int> SendEmail(InternalEmailRequest req, SMTPConfig _smtpConfig);
    }

    public class EmailSender : IEmailSender
    {
        private readonly IApplicationLogger _logger;
        private string requestapath = "/BPUserController/";
        private readonly SMTPConfig? _smtpConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private bool _emailNotifications;

        private string _host;
        private string _from;
        private string _alias;
        private string _password;
        private string _port;

        public EmailSender(IWebHostEnvironment hostEnvironment,
           IApplicationLogger applicationLogger,
           IOptions<AppSettings> appSettings)
        {
            _logger = applicationLogger;
            _smtpConfig = appSettings.Value.SMTPConfig;

            if (_smtpConfig != null)
            {
                _host = _smtpConfig.Host;
                _from = _smtpConfig.From;
                _alias = _smtpConfig.Alias;
                _password = _smtpConfig.Password;
                _port = "" + _smtpConfig.Port;
                _emailNotifications = Convert.ToBoolean(_smtpConfig.EmailNotifications);
            }

        }

        public async Task<int> SendEmail(InternalEmailRequest req, SMTPConfig _smtpConfig)
        {
            int successCount = 0;

            var configurationManagedetail = JsonConvert.SerializeObject(_smtpConfig);

            var vm = new SMTPConfig();
            if (_smtpConfig != null)
            {
                vm = _smtpConfig;
           
                if (_emailNotifications)
                {
                    try
                    {
                        var message = new MailMessage();
                        foreach (var mail in req.ToAddresses)
                        {
                            message.To.Add(mail);
                        }
                        message.From = new MailAddress(vm.From, vm.Alias);
                        message.Subject = req.Subject;
                        message.Body = req.Body;
                        message.IsBodyHtml = true;
                        if (req.Attachment != null)
                        {
                            message.Attachments.Add(req.Attachment);
                        }

                        var client = new SmtpClient(vm.Host);
                        client.Port = Convert.ToInt32(vm.Port);
                        client.Credentials = new NetworkCredential(vm.From, vm.Password);
                        client.EnableSsl = true;

                        await Task.Run(() =>
                        {
                            try
                            {
                                client.Send(message);
                                successCount++;
                            }
                            catch (Exception e)
                            {
                                Console.Write(e);
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }
            }
            return successCount;
        }
        public class TokenProvider : IAccessTokenProvider
        {
            private readonly string accessToken;

            public TokenProvider(string accessToken)
            {
                this.accessToken = accessToken;
            }

            public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default,
                CancellationToken cancellationToken = default)
            {
                var token = accessToken;

                // Return the token
                return Task.FromResult(token);
            }

            public AllowedHostsValidator AllowedHostsValidator { get; }
        }

        public async void SendO365Email(List<AppUser> to, string subject, string refNo, string body, string attachement, string Location, DateTime StartTime, DateTime EndTime, string accessToken)
        {
            if (_emailNotifications)
            {
                try
                {

                    var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider(accessToken));
                    var graphClient = new GraphServiceClient(authenticationProvider);

                    var tempRef = "";
                    if (!string.IsNullOrEmpty(refNo))
                    {
                        tempRef = "-" + refNo;
                    }
                    var toList = new List<Recipient>();

                    foreach (var toItem in to)
                    {
                        var sentTo = new Recipient();
                        sentTo.EmailAddress = new EmailAddress { Address = toItem.UserEmail };
                        toList.Add(sentTo);
                    }

                    var message = new Message
                    {
                        Subject = subject + refNo,
                        Body = new ItemBody
                        {
                            ContentType = BodyType.Html,
                            Content = body
                        },
                        ToRecipients = toList,

                    };

                    var saveToSentItems = true;

                    try
                    {

                        SendMailPostRequestBody body_ = new()
                        {
                            Message = message,
                            SaveToSentItems = saveToSentItems  // or true, as you want
                        };

                        await graphClient.Me
                         .SendMail
                         .PostAsync(body_);
                    }
                    catch (Exception ex)
                    {
                        // log your exception here
                    }

                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
        }

    }

}

