using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Core.Enums
{
    public enum AccountType
    {
        [Display(Name = "Kullanıcı")] Account = 1,
        [Display(Name = "Admin")] SuperAdmin = 21
    }
}
