using HHL.PostreSQL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.DataAccess
{
    public class AuthBaseRepository<T> : BaseRepository<T> where T : class
    {
        public AuthBaseRepository()
        {
        }
    }
}
