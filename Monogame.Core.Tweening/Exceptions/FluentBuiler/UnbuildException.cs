namespace Monogame.Core.Tweening.Exceptions.FluentBuilder;

public class UnbuildException : Exception
{
    private const string _message = "Object is not build.";

    public UnbuildException() : base(_message) { }

    public UnbuildException(string message)
        : base($"{_message} {message}")
    {
    }

    public UnbuildException(string message, Exception inner)
        : base($"{_message} {message}", inner)
    {
    }
}
