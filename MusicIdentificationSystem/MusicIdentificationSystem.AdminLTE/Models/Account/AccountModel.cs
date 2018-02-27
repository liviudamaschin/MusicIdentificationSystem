using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Account
{
    public class AccountModel
    {
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_Id")]
        public int Id { get; set; } // Id (Primary key)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_AccountName")]
        public string AccountName { get; set; } // AccountName (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_FullName")]
        public string FullName { get; set; } // Name (length: 250)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_Email")]
        public string Email { get; set; } // Email (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_Address")]
        public string Address { get; set; } // Address (length: 250)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_Phone")]
        public string Phone { get; set; } // Address (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Account_IsActive")]
        public bool IsActive { get; set; } // IsActive
    }
}