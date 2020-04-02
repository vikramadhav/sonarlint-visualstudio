using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SonarLint.VisualStudio.Integration.Vsix.TSAnalysis
{
    public class EslintBridgeServerStarter : IDisposable
    {
        private readonly ILogger logger;
        private readonly string serverStartupScriptLocation;
        private readonly int port;

        private TaskCompletionSource<int> startTask;
        private Process process;

        public EslintBridgeServerStarter(ILogger logger, string serverStartupScriptLocation, int port)
        {
            this.logger = logger;
            this.serverStartupScriptLocation = serverStartupScriptLocation;
            this.port = port;
        }

        public Task<int> Start()
        {
            if (startTask == null)
            {
                startTask = new TaskCompletionSource<int>();
                StartServer();
            }

            return startTask.Task;
        }

        public bool IsRunning()
            => !this.process?.HasExited ?? false;

        private void StartServer()
        {
            var nodePath = "node.exe ";
            var command = $"{serverStartupScriptLocation} {port}";

            var psi = new ProcessStartInfo
            {
                FileName = nodePath,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false, // required if we want to capture the error output
                ErrorDialog = false,
                CreateNoWindow = true,
                Arguments = command
            };

            psi.EnvironmentVariables.Add("NODE_PATH", @"C:\Users\jdcp\AppData\Roaming\npm\node_modules");

            process = new Process { StartInfo = psi };
            process.ErrorDataReceived += OnErrorDataReceived;
            process.OutputDataReceived += OnOutputDataReceived;

            process.Start();
            logger.WriteLine($"ESLINT-BRIDGE: Server process id: {process.Id}");
            logger.WriteLine($"ESLINT-BRIDGE: Server process HasExited: {process.HasExited}");


            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            logger.WriteLine("ESLINT-BRIDGE: " + e.Data);

            var portMessage = Regex.Matches(e.Data, @"port\s+(\d+)");

            if (portMessage.Count > 0)
            {
                var portNumber = int.Parse(portMessage[0].Groups[1].Value);

                if (portNumber != 0)
                {
                    startTask.SetResult(portNumber);
                }
            }
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            logger.WriteLine("ESLINT-BRIDGE: " + e.Data);
        }

        public void Dispose()
        {
            if (!process.HasExited)
            {
                process?.Kill();
            }
            process?.Dispose();
            logger.WriteLine("ESLINT-BRIDGE: server disposed");
        }
    }
}
