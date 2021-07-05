using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class ErrorMdl
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public ErrorMdl()
        {

        }


        public ErrorMdl(int _id, string _message)
        {
            Id = _id;
            Message = _message;
        }
    }
}
