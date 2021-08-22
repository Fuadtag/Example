using System;

namespace Common.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) verilənlər bazasında tapılmadı")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}