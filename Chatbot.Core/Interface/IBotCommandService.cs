using Chatbot.Core.Models;

namespace Chatbot.Core.Interface;

public interface IBotCommandService
{
    string? ValidateCommand(string text);
    CommandInformation GetCommandInformation(string text);
    bool IsCommand(string text);
}

