using Microsoft.AspNetCore.Identity;

namespace Chatbot.Core.Entities
{
    internal class User : IdentityUser<int>
    {
        public required string DisplayName { get; set; }
        public ICollection<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();
    }
}
