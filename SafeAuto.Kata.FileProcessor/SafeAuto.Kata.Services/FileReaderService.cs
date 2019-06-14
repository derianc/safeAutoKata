using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Services.Interfaces;
using System.IO;
using System.Linq;

namespace SafeAuto.Kata.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly ILogger<FileReaderService> _logger;

        public FileReaderService(ILogger<FileReaderService> logger)
        {
            _logger = logger;
        }

        public InputFileDetails ProcessFile(string fileName)
        {
            _logger.LogDebug($"Processing file: {fileName}");

            // read entire file
            string[] lines = File.ReadAllLines(fileName);

            // decided to go this route in case input file is out of order
            // if trip details appear before user registration, this method always works
            var registrationLines = lines.Where(l => l.Split(' ')[0] == InputType.Driver.ToString()).ToArray();
            var tripDetailLines = lines.Where(l => l.Split(' ')[0] == InputType.Trip.ToString()).ToArray();

            return new InputFileDetails
            {
                DriverDetails = registrationLines,
                TripDetails = tripDetailLines
            };
        }
    }
}
