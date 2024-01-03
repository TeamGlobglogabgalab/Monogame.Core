using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Graphics.Exceptions;

public class InvalidDrawableException : Exception
{
    public InvalidDrawableException(string? message) : base(message)
    {
    }

    public InvalidDrawableException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
