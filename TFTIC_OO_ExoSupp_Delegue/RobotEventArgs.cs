namespace TFTIC_OO_ExoSupp_Delegue
{
    public enum MessageType { Info, Erreur, Victory }
    internal class RobotEventArgs
    {
        public RobotEventArgs(string message, MessageType messageType)
        {
            Message = message;
            MessageType = messageType;
        }

        public string Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}