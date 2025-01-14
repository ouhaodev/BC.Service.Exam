using BC.Service.Exam.DataAccess;
using BC.Service.Exam.Domain;
using BC.Service.Exam.Utilities;

namespace BC.Service.Exam.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ILogger<CandidateService> _logger;
        private readonly AppDbContext _appContext;

        public CandidateService(ILogger<CandidateService> logger, AppDbContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        public async Task<ServiceResult<Candidate>> GetCandidateByIdAsync(long id)
        {
            var candidate = await _appContext.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return new UnsuccessfulServiceResult<Candidate>(StatusCodes.Status404NotFound,
                 $"Candidate with ID {id} not found.");
            }

            return new ServiceResult<Candidate>(StatusCodes.Status200OK, candidate);

        }

        public async Task<ServiceResult<Candidate>> CreateCandidateAsync(CreateCandidateVM createCandidateVM)
        {
            var dto = new Candidate
            {
                Name = createCandidateVM.Name,
                Age = createCandidateVM.Age
            };

            var result = _appContext.Candidates.Add(dto);
            await _appContext.SaveChangesAsync();
            return new ServiceResult<Candidate>(StatusCodes.Status201Created, result.Entity);

        }

        public async Task<ServiceResult<Candidate>> DeleteCandidateByIdAsync(long id)
        {
            var candidate = await _appContext.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return new UnsuccessfulServiceResult<Candidate>(StatusCodes.Status404NotFound, $"Candidate with ID {id} not found.");
            }

            _appContext.Candidates.Remove(candidate);
            await _appContext.SaveChangesAsync();

            return new ServiceResult<Candidate>(StatusCodes.Status202Accepted);

        }

        public ServiceResult<IList<int>> GenerateRandomCandidates(int num)
        {
            if (num < 20)
            {
                return new UnsuccessfulServiceResult<IList<int>>(StatusCodes.Status400BadRequest, "考生数量必须在20以上");
            }

            var list = AlgorithmService.GenerateRandomNumbers(num);

            return new ServiceResult<IList<int>>(StatusCodes.Status200OK, list);
        }
    }
}
