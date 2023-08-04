namespace CED.Contracts;

public class DeleteUpdateCommandResult 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public Guid Id { get; set; }
    public DeleteUpdateCommandResult(bool isSuccess, string message, Guid id)
    {
        IsSuccess = isSuccess;
        Message = message;
        Id = id;
    }
    public DeleteUpdateCommandResult()
    {
        Message = string.Empty;
        IsSuccess = false;
        Id = Guid.Empty;
    }
}