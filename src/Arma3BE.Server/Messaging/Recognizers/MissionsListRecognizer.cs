using Arma3BE.Server.Models;

namespace Arma3BE.Server.Messaging.Recognizers
{
    public class MissionsListRecognizer : IServerMessageRecognizer
    {
        public ServerMessageType GetMessageType(ServerMessage message)
        {
            return ServerMessageType.MissionList;
        }

        public bool CanRecognize(ServerMessage serverMessage)
        {
            return serverMessage.Message.StartsWith("Missions on server:");
        }
    }
}