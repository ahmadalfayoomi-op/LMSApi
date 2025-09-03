using LMS.Application.Interfaces.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Text;

namespace LMS.Infrastructure.Repositories.Configuration
{
    public class WhisperService : IWhisperService
    {
        private readonly string _whisperExePath;
        private readonly string _modelPath;
        private readonly string _ffmpegPath;
        private readonly IWebHostEnvironment _env;

        public WhisperService(
            IWebHostEnvironment env,
            string whisperExePath,
            string modelPath,
            string ffmpegPath)
        {
            _env = env;
            _whisperExePath = @"C:\Users\Ahmad Alfayoomi\source\repos\LMS\LMS\LMS.Tools\whisper.cpp\build\bin\Release\whisper-cli.exe";
            _modelPath = @"C:\Users\Ahmad Alfayoomi\source\repos\LMS\LMS\LMS.Tools\whisper.cpp\models\ggml-base.en.bin";
            _ffmpegPath = @"C:\Projects\ffmpeg-2025-08-25-git-1b62f9d3ae-full_build\ffmpeg-2025-08-25-git-1b62f9d3ae-full_build\bin\ffmpeg.exe";


            // Validate paths
            if (!File.Exists(_ffmpegPath))
                throw new FileNotFoundException($"FFmpeg not found at path: {_ffmpegPath}");

            if (!File.Exists(_whisperExePath))
                throw new FileNotFoundException($"Whisper executable not found at path: {_whisperExePath}");

            if (!File.Exists(_modelPath))
                throw new FileNotFoundException($"Whisper model not found at path: {_modelPath}");
        }

        public async Task<string> TranscribeAsync(string mediaFilePath)
        {
            mediaFilePath = mediaFilePath.TrimStart('/', '\\');

            if (!Path.IsPathRooted(mediaFilePath))
                mediaFilePath = Path.Combine(_env.WebRootPath, mediaFilePath.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(mediaFilePath))
                throw new FileNotFoundException($"Media file not found: {mediaFilePath}");

            var audioFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.wav");

            try
            {
                // Extract audio
                var ffmpegOutput = await RunProcessAsync(_ffmpegPath,
                    $"-i \"{mediaFilePath}\" -ar 16000 -ac 1 -c:a pcm_s16le \"{audioFilePath}\" -y");

                if (!File.Exists(audioFilePath))
                    throw new Exception($"FFmpeg failed to create WAV file. Output: {ffmpegOutput}");

                // Run Whisper and capture stdout
                var whisperOutput = await RunProcessAsync(_whisperExePath,
                    $"\"{audioFilePath}\" --model \"{_modelPath}\" --language en --no-timestamps");

                // Get only last non-empty line (Whisper transcription)
                var lines = whisperOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var transcription = lines.Length > 0 ? lines.Last() : string.Empty;

                return transcription;
            }
            finally
            {
                if (File.Exists(audioFilePath))
                    try { File.Delete(audioFilePath); } catch { }
            }
        }

        private async Task<string> RunProcessAsync(string fileName, string arguments)
        {
            var outputBuilder = new StringBuilder();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) outputBuilder.AppendLine(e.Data); };
            process.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) outputBuilder.AppendLine(e.Data); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"Process '{fileName}' exited with code {process.ExitCode}. Output: {outputBuilder}");

            return outputBuilder.ToString();
        }
    }
}
