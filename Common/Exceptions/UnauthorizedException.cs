using System;

namespace Common.Exceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException() : base("İstifadəçi sayta giriş etməlidir")
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}