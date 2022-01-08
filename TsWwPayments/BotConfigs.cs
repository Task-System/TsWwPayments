namespace TsWwPayments;

/// <summary>
/// Telegram bot configurations.
/// </summary>
public struct BotConfigs
{
    /// <summary>
    /// Telegram bot token.
    /// </summary>
    public string BotToken { get; init; }

    /// <summary>
    /// Host address to set webhook.
    /// </summary>
    public string HostAddress { get; init; }
}
