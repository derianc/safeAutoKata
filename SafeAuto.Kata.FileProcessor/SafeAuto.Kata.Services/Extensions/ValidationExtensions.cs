﻿using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using System;

namespace SafeAuto.Kata.Services.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsCorrectFormatForNewUser(this string[] line, ILogger<FileReaderService> logger)
        {
            if (line.Length == 2)
                return true;

            logger.LogError($"Formatting Error: {line}");
            throw new FormatException("Formatting Error");
        }

        public static bool IsCorrectFormatForTripDetails(this string[] line, ILogger<FileReaderService> logger)
        {
            if (line.Length == 5)
                return true;

            logger.LogError($"Formatting Error: {line}");
            throw new FormatException("Formatting Error");
        }

        public static bool IsTripValid(this TripDetails tripDetails)
        {
            // if avg speed is less than 5 or greater than 100, trip is invalid
            return !tripDetails.IsAvgSpeedLessThan5Mph() ||
                   !tripDetails.IsAvgSpeedGreaterThan100Mph();

        }
    }
}
