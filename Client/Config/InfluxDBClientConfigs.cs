using System;
using System.Collections.Generic;
using InfluxDB3.Client.Write;

namespace InfluxDB3.Client.Config;

public class InfluxDBClientConfigs
{
    private string _hostUrl = "";

    /// <summary>
    /// The configuration of the client.
    /// </summary>
    public InfluxDBClientConfigs()
    {
    }

    /// <summary>
    /// The hostname or IP address of the InfluxDB server.
    /// </summary>
    public string HostUrl
    {
        get => _hostUrl;
        set => _hostUrl = string.IsNullOrEmpty(value) ? value : value.EndsWith("/") ? value : $"{value}/";
    }

    /// <summary>
    /// The authentication token for accessing the InfluxDB server.
    /// </summary>
    public string? AuthToken { get; set; }

    /// <summary>
    /// The organization to be used for operations.
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// The database to be used for InfluxDB operations.
    /// </summary>
    public string? Database { get; set; }

    /// <summary>
    /// The default precision to use for the timestamp of points if no precision is specified in the write API call.
    /// </summary>
    public WritePrecision? WritePrecision { get; set; }

    /// <summary>
    /// Tags added to each point during writing. If a point already has a tag with the same key, it is left unchanged.
    /// <example>
    /// <code>
    /// <![CDATA[
    /// var _client = new InfluxDBClient(new InfluxDBClientConfigs
    /// {
    ///     HostUrl = "some-url",
    ///     Organization = "org",
    ///     Database = "database",
    ///     DefaultTags = new Dictionary \<string, string ()
    ///     {
    ///         { "rack", "main" },
    ///     }
    /// });
    /// 
    /// // Writes with rack=main tag
    /// await _client.WritePointAsync(PointData
    ///     .Measurement("cpu")
    ///     .AddField("field", 1)
    /// );
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public Dictionary<string, string>? DefaultTags { get; set; }



    /// <summary>
    /// Timeout to wait before the HTTP request times out. Default to '10 seconds'.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Automatically following HTTP 3xx redirects. Default to 'false'.
    /// </summary>
    public bool AllowHttpRedirects { get; set; }

    /// <summary>
    /// Disable server SSL certificate validation. Default to 'false'.
    /// </summary>
    public bool DisableServerCertificateValidation { get; set; }

    internal void Validate()
    {
        if (string.IsNullOrEmpty(HostUrl))
        {
            throw new ArgumentException("The hostname or IP address of the InfluxDB server has to be defined.");
        }
    }
}