
using System.Diagnostics;

namespace MonacoEditorSandbox.Data;


public class PythonService
{
    public async Task<string> RunScriptAsync(string script)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "python",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();

        await using (var sw = process.StandardInput)
        {
            await sw.WriteLineAsync(script);
        }

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        return output + error;
    }
}
