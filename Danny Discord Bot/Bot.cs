//using System;
//using System.Threading.Tasks;

using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Danny_Discord_Bot;

public class Bot
{
    public DiscordClient Client { get; private set; }
    public CommandsNextExtension Commands { get; private set; }
    
    public async Task RunAsync()
    {
        var json = string.Empty;

        using (var fs = File.OpenRead("C:/Users/sofar/Bot only/Danny Discord Bot/config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding((false))))
            json = await sr.ReadToEndAsync().ConfigureAwait(false);

        var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
        
            var config = new DiscordConfiguration
        {
            Token = configJson.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = LogLevel.Debug
        };
        
        Client = new DiscordClient(config);

        Client.Ready += OnClientReady;

        var commandsConfig = new CommandsNextConfiguration
        {
            StringPrefixes = new string[] {configJson.Prefix},
            EnableMentionPrefix = true,
            EnableDms = false
        };

        Commands = Client.UseCommandsNext(commandsConfig);

        await Client.ConnectAsync();

        await Task.Delay(-1);
    }

    private Task OnClientReady(object sender,ReadyEventArgs e)
    {
        return Task.CompletedTask;
    }
}