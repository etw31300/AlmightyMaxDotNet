using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;

namespace AlmightyMax.Config
{
    public class LavalinkConfig
    {
        /// <summary>
        /// Lavalink Configuration object used to connect to a node.
        /// </summary>
        public LavalinkConfiguration Config { get; private set; }
        /// <summary>
        /// Process that will be running the Lavalink.jar file
        /// </summary>
        public static Process LavalinkProcess { get; set; }
        /// <summary>
        /// Method that returns the LavalinkConfiguration from a default LavalinkConfig object.
        /// </summary>

        public static LavalinkConfiguration UseDefault => new LavalinkConfig().Config;

        /// <summary>
        /// Initializes the process to run the Lavalink.jar file
        /// </summary>
        public static void InitializeLavalinkProcess()
        {
            LavalinkProcess = new Process()
            {
                StartInfo =
                {
                    FileName = "java",
                    Arguments = @$"-jar {AppDomain.CurrentDomain.BaseDirectory}Util\Lavalink.jar",
                    UseShellExecute = false,
                    
                }
            };

            // Start the process
            LavalinkProcess.Start();
            
            // Process event handlers
            LavalinkProcess.Disposed += (sender, args) =>
            {
                Console.WriteLine("Lavalink has been disposed.");
            };

            LavalinkProcess.Exited += (sender, args) =>
            {
                Console.WriteLine("Lavalink process has been exited.");
            };

            LavalinkProcess.ErrorDataReceived += (sender, args) =>
            {
                Console.Error.WriteLine($"Error occurred in Lavalink process: {args.Data}");
            };
        }

        /// <summary>
        /// Default constructor.
        /// Sets the Hostname to 127.0.0.1 (localhost) and the port to 2333 for Lavalink connections.
        /// </summary>
        public LavalinkConfig()
        {
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1",
                Port = 2333
            };

            Config = new LavalinkConfiguration
            {
                Password = "AlmightyMaxMusic",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };
        }
    }
}