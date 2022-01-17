namespace TestProject.Domain.Responses
{
    public class OperationResponse<T> where T : class
    {
        public OperationResponse(
            T model,
            OperationResult result = OperationResult.Failure,
            string error = "")
        {
            Model = model;
            Result = result;
            Error = error;
        }

        public OperationResult Result { get; set; }

        public string Error { get; set; }

        public T Model { get; set; }
    }
}
