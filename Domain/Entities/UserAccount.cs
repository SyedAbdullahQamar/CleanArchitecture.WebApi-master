using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserAccount : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; } = 0;
        public string WalletAccount { get; set; }
    }
}
