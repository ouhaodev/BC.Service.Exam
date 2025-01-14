using BC.Service.Exam.Domain;
using BC.Service.Exam.Utilities;

namespace BC.Service.Exam.Services
{
    public interface ICandidateService
    {
        Task<ServiceResult<Candidate>> GetCandidateByIdAsync(long id);
        Task<ServiceResult<Candidate>> CreateCandidateAsync(CreateCandidateVM dto);
        Task<ServiceResult<Candidate>> DeleteCandidateByIdAsync(long id);

        ServiceResult<IList<int>> GenerateRandomCandidates(int num);
    }
}
