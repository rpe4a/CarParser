using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parser.Models;
using Timer = System.Timers.Timer;

namespace Parser
{
    internal class Program
    {
        private static readonly Uri baseUri = new Uri("http://crwl.ru/api/rest/");
        private static readonly HttpClient httpClient = new HttpClient {BaseAddress = baseUri};

        private static void Main(string[] args)
        {
            var timer = new Timer(5000);
            var spin = new ConsoleSpiner();
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            timer.Elapsed += async (sender, e) => await Parse(spin, token).ConfigureAwait(false);
            timer.Start();

            Console.CancelKeyPress += (sender, e) => CancelHandler(cts, timer);
            Console.ReadKey();
        }

        private static void CancelHandler(CancellationTokenSource cts, Timer timer)
        {
            cts.Cancel();
            timer.Stop();
            timer.Dispose();
            httpClient.Dispose();
        }

        private static async Task Parse(ConsoleSpiner spin, CancellationToken token)
        {
            try
            {
                spin.Turn();

                var result = await httpClient.SendAsync(QueryFactory.GetNewOffers(), token).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                var cars = JsonConvert.DeserializeObject<List<Car>>(content);

                if (cars != null)
                    foreach (var car in cars.Where(c => c.CarStatus() != Status.Bad))
                    {

                        CarFileSaver.Save(car);
                        MailSender.Send(car);
                    }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public class ConsoleSpiner
        {
            private int counter;

            public ConsoleSpiner()
            {
                counter = 0;
            }

            public void Turn()
            {
                counter++;
                switch (counter % 4)
                {
                    case 0:
                        Console.Write("/");
                        break;
                    case 1:
                        Console.Write("-");
                        break;
                    case 2:
                        Console.Write("\\");
                        break;
                    case 3:
                        Console.Write("|");
                        break;
                }

                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }
}