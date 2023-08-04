namespace CED.Contracts;

public class CreateUpdateCommandResult 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public Guid Id { get; set; }
    public CreateUpdateCommandResult(bool isSuccess, string message, Guid id)
    {
        IsSuccess = isSuccess;
        Message = message;
        Id = id;
    }
    public CreateUpdateCommandResult()
    {
        Message = string.Empty;
        IsSuccess = false;
        Id = Guid.Empty;
    }
}