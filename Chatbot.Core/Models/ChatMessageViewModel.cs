
using Chatbot.Core.Constants;

namespace Chatbot.Core.Models;

public sealed record ChatMessageViewModel(DateTimeOffset SendAt, string Message, int UserId, string? UserName, string? DisplayName = null)
{
    private bool IsNotice => Message.StartsWith("[Notice]");

    public string ApplyCSS => UserId == HubConstants.CHAT_BOT_ID
        ? (IsNotice ? "notice" : "received")
        : "sent";
}
