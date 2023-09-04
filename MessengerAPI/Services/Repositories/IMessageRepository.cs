using Models.Message;

namespace MessengerAPI.Services.Repositories
{
    public interface IMessageRepository
    {
        public Task<ulong?> SendMessageAsync(Guid userId, MessageData messageData);
        public Task GetChatData();
        public Task GetMessagesFromChatAsync();
    }
}
