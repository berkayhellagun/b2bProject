using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class Person : BaseEntity
    {
        public string TCVKN { get; set; }
        public int PersonType { get; set; }
        public string NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public bool isCanSell { get; set; } 
        public string eMail { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }

    }
}
