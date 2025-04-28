using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(){ }

        public CommandResult(bool success, string message = null) 
        {
            Success = success;
            Message = message;
        }
        public CommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool GetSuccess() => Success;

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
