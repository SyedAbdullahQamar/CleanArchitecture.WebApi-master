using System;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = Guid.Empty.ToString();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
