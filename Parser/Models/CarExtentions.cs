using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parser.Models
{
    internal static class CarExtentions
    {
        public static string MailTitle(this Car car)
        {
            return
                $"Объявление на сайте {car.Website} от {car.DateOfAdded}, Автомобиль: {car.Marka} {car.Model} {car.Year}г. {car.Mileage} {car.Price} руб.";
        }

        public static string MainBody(this Car car)
        {
            var sb = new StringBuilder();

            var mainProps = new HashSet<string>
            {
                nameof(car.Company),
                nameof(car.Mileage),
                nameof(car.Year),
                nameof(car.PtsOwner),
                nameof(car.Price),
                nameof(car.Website),
                nameof(car.PhoneFind),
            };

            sb.AppendLine("<div style='text-align:left;'>");

            foreach (var prop in typeof(Car).GetProperties())
                if (mainProps.Contains(prop.Name))
                    sb.AppendLine($"<p style='color: red;'>{prop.Name}: {prop.GetValue(car)}</p>");
                else
                    sb.AppendLine($"<p>{prop.Name}: {prop.GetValue(car)}</p>");

            foreach (var photo in car.Photos) sb.AppendLine($"<img title='{car.Modification}' src='{photo}'/>");

            sb.AppendLine("</div>");

            return sb.ToString();
        }

        public static string ToJson(this Car car)
        {
            return JsonConvert.SerializeObject(car);
        }
    }
}