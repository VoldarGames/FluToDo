using System;

namespace FluToDo.Commands
{
    public interface ICrudCommand<T>
    {
        Action<T> CustomAction { get; set; }
        Action<T> FinallyAction { get; set; }
    }
}