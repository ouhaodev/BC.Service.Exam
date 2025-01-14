using BC.Service.Exam.Domain;
using BC.Service.Exam.Services;
using BC.Service.Exam.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BC.Service.Exam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ILogger<CandidatesController> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly ICandidateService _candidateService;

        public CandidatesController(ILogger<CandidatesController> logger,
            ProblemDetailsFactory problemDetailsFactory,
            ICandidateService candidateService)
        {
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
            _candidateService = candidateService;
        }

        /// <summary>
        /// Get candidate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var serviceResult = await _candidateService.GetCandidateByIdAsync(id);

            return serviceResult.ToActionResult(this, _problemDetailsFactory);
        }

        /// <summary>
        ///  Create a new candidate
        /// </summary>
        /// <param name="createCandidateVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCandidateVM createCandidateVM)
        {
            // TODO : Add validation
            var serviceResult = await _candidateService.CreateCandidateAsync(createCandidateVM);

            return serviceResult.ToActionResult(this, _problemDetailsFactory);
        }


        /// <summary>
        /// Delete candidate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var serviceResult = await _candidateService.DeleteCandidateByIdAsync(id);
            return NoContent();
        }


        /// <summary>
        ///  Generate random candidates
        /// </summary>
        /// <param name="num">Generate Random Candidates count</param>
        /// <returns></returns>
        [HttpGet]
        [Route("generateRandomCandidates")]
        public IActionResult GenerateRandomCandidates(int num)
        {
            var serviceResult =  _candidateService.GenerateRandomCandidates(num);
            
            return serviceResult.ToActionResult(this, _problemDetailsFactory);
        }
        
    }
}
