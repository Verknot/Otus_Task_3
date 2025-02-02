namespace Otus_Task_3;

public class CommandException : Exception
{
    public CommandException(string message) : base(message) { }
}