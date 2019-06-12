using SafeAuto.Kata.Data;
using System.Collections.Generic;

namespace SafeAuto.Kata.Services.Interfaces
{
    public interface IPrintService
    {
        List<string> PrintOutput(List<Output> tripDetails);
    }
}
