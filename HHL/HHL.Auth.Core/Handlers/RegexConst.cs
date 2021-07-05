using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Handlers
{
    public static class RegexConst
    {
        public const string EmailValidator = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public const string LerrerAndNumbersOnly = @"^[\w]+$";
    }
}
