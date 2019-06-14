Author: Derian Conteh-Morgan, 06/13/2019

The included solution contains 5 projects: 
  SafeAuto.Kata.Data
  SafeAuto.Kata.FileProcessor
  SafeAuto.Kata.Repositories
  SafeAuto.Kata.Services
  SafeAuto.Kata.Tests

Getting Started: 
  Clone GitHub Repository, https://github.com/derianc/safeAutoKata
  Open Solution (Visual Studio 2017) 
  Run Test Project, SafeAuto.Kata.Tests, and ensure all tests pass
  Set SafeAuto.Kata.FileProcessor as startup project 
  Run Program and follow instructions

The Code: 
  Interfaces: 
    IDriverService - defines the service methods associated with driver object
    IFileReaderService - defines the service methods associated with the file reader object
    IPrintService - defines service methods associated with the print service
    IDriverRepository - defines repository methods associated with driver object. This example is using a list as a fake database

  Extension Methods: 
    ValidationExtensions - extension methods for validating trip speeds, file formatting, etc
