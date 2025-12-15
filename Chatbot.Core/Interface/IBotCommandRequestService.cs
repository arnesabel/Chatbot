using Chatbot.Core.Models;

namespace Chatbot.Core.Interface;

public interface IBotCommandRequestService
{
    Task ExecuteCommand(CommandInformation command, CancellationToken ct = default);
}
