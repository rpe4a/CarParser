using System;
using System.IO;
using System.Text;
using Parser.Models;

namespace Parser
{
    internal class CarFileSaver
    {
        public static void Save(Car car)
        {
            var carDirectory = Config.Get().CarSavePath;

            if (!Directory.Exists(carDirectory))
                Directory.CreateDirectory(carDirectory);

            File.AppendAllText($"{carDirectory}car_{car.Id}_{Guid.NewGuid()}.txt", car.ToJson(), Encoding.UTF8);
        }
    
    }
}