using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StickersTemplate.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StickersTemplate.Config;
internal class ConfigurationSettings : ISettings
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationSettings"/> class.
    /// </summary>
    public ConfigurationSettings(IConfiguration configuration, ILogger<ConfigurationSettings> logger)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public string MicrosoftAppId => StringValue("MicrosoftAppId") ?? throw new InvalidOperationException("MicrosoftAppId is required.");

    /// <inheritdoc/>
    public Uri ConfigUri => UriValue("ConfigUri") ?? throw new InvalidOperationException("MicrosoftAppId is required.");

    /// <summary>
    /// Parses a config value into a <see cref="string"/>.
    /// </summary>
    /// <param name="configName">The name of the config value.</param>
    /// <param name="optional">Whether this parameter is optional or not.</param>
    /// <returns>A parsed <see cref="string"/>.</returns>
    private string? StringValue(string configName, bool optional = false)
    {
        var value = _configuration[configName];
        if (value == null)
        {
            if (!optional)
            {
                _logger.LogError($"Config parameter '{configName}' not provided.");
                throw new InvalidOperationException($"Config parameter '{configName}' is required.");
            }
            else
            {
                _logger.LogInformation($"Config parameter '{configName}' not provided.");
            }
        }

        return value;
    }

    /// <summary>
    /// Parses a config value into a <see cref="Uri"/>.
    /// </summary>
    /// <param name="configName">The name of the config value.</param>
    /// <param name="optional">Whether this parameter is optional or not.</param>
    /// <returns>A parsed <see cref="Uri"/>.</returns>
    private Uri? UriValue(string configName, bool optional = false)
    {
        var value = _configuration[configName];
        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            if (!optional)
            {
                _logger.LogError($"Config parameter '{configName}' not provided or is not a valid absolute URI.");
                throw new InvalidOperationException($"Config parameter '{configName}' is required and must be a valid absolute URI.");
            }
            else
            {
                _logger.LogInformation($"Config parameter '{configName}' not provided or is not a valid absolute URI.");
            }
        }

        return uri;
    }
}
