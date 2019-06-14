using SafeAuto.Kata.Data;

namespace SafeAuto.Kata.Services.Interfaces
{
    public interface IFileReaderService
    {
        InputFileDetails ProcessFile(string fileName);
    }
}
