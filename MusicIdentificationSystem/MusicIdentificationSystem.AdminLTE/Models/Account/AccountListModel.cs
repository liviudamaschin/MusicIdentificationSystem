using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Account
{
    public class AccountListModel
    {
        public AccountListModel()
        {
            AccountModelsList = new List<AccountModel>();
        }

        public List<AccountModel> AccountModelsList { get; set; }
    }
}