using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StickersTemplate;
using StickersTemplate.Config;
using StickersTemplate.Interfaces;
using StickersTemplate.Providers;

[assembly: FunctionsStartup(typeof(Startup))]

namespace StickersTemplate;

/// <summary>
/// Startup class for Function App
/// </summary>
public class Startup : FunctionsStartup
{
    /// <summary>
    /// Configure services
    /// </summary>
    /// <param name="builder">The <see cref="IFunctionsHostBuilder"/></param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<ISettings, ConfigurationSettings>();
        builder.Services.AddTransient<IStickerSetRepository, StickerSetRepository>();
        builder.Services.AddTransient<IStickerSetIndexer, StickerSetIndexer>();
        builder.Services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();
    }
}
