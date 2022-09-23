
namespace RealitSystem_CLI
{
    public enum ReturnStatus
    {
        Success = 0,
        Failure = 1,
    }
    public struct RealitReturnCode
    {
        public readonly ReturnStatus returnStatus;
        public readonly string message;

        public RealitReturnCode(ReturnStatus returnStatus, string message)
        {
            this.returnStatus = returnStatus;
            this.message = message;
        }

        public RealitReturnCode(ReturnStatus returnStatus)
        {
            this.returnStatus = returnStatus;
            this.message = string.Empty;
        }
    }
}