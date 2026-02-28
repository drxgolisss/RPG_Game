namespace ConsoleRpgStage1.Entities;

public sealed class EquipResult
{
    private EquipResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }

    public static EquipResult Success(string message)
    {
        return new EquipResult(true, message);
    }

    public static EquipResult Fail(string message)
    {
        return new EquipResult(false, message);
    }
}
