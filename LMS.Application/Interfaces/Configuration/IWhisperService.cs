

namespace LMS.Application.Interfaces.Configuration
{
    public interface IWhisperService
    {
        Task<string> TranscribeAsync(string mediaFilePath);
    }
}
