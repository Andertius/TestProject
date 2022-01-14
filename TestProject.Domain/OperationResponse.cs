namespace TestProject.Domain
{
    public class OperationResponse
    {
        public OperationResponse(OperationResult result = OperationResult.Failure, string error = "")
        {
            Result = result;
            Error = error;
        }

        public OperationResult Result { get; set; }

        public string Error { get; set; }
    }
}
