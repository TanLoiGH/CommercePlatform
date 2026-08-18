using System;
using System.Collections.Generic;
using System.Text;

namespace CommercePlatform.Domain.Domain.Exceptions
{
    public sealed class DomainException(string message) : Exception(message);
}
