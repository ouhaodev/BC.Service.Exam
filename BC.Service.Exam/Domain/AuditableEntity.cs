using System;

namespace BC.Service.Exam.Domain
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDataTime { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDataTime { get; set; }
    }
}
