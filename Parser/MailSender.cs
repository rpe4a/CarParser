using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Parser.Models;

namespace Parser
{
    internal static class MailSender
    {
        private static readonly SmtpClient client;
        private static readonly HashSet<int> SendedMailCars;

        static MailSender()
        {
            client = new SmtpClient
            {
                Port = Config.Get().GmailPort,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Timeout = 10000,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential(Config.Get().MailFrom, Config.Get().MailFromPass)
            };

            SendedMailCars = new HashSet<int>();
        }

        public static void Send(Car car)
        {
            if (car == null)
                return;

            if(SendedMailCars.Contains(car.Id))
                return;

            var mm = new MailMessage(
                Config.Get().MailFrom,
                Config.Get().MailTo,
                car.MailTitle(),
                car.MainBody())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                IsBodyHtml = true
            };

            client.Send(mm);
            SendedMailCars.Add(car.Id);

            Console.WriteLine($"Mail sended. Count of sended mails = {SendedMailCars.Count}");
        }
    }
}