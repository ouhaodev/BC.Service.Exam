namespace BC.Service.Exam.Domain
{
    public class Candidate : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public uint Age { get; set; }
    }

    public class CreateCandidateVM
    {
        public string Name { get; set; } = null!;
        public uint Age { get; set; }
    }
}
