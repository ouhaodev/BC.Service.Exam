using BC.Service.Exam.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BC.Service.Exam.UnitTests
{
    [TestClass]
    public sealed class UnitTest
    {
        [DataRow(null, 400)]
        [DataRow(1, 400)]
        [DataRow(20, 200)]
        [DataRow(200, 200)]
        [TestMethod]
        public void GenerateRandomCandidates_Test(int num, int resultCode)
        {
            var service = new CandidateService(null, null);
            var result = service.GenerateRandomCandidates(num);
            Assert.AreEqual(result.StatusCode, resultCode);
        }

        [TestMethod]
        public void SourtCandidates_Test()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            var result = AlgorithmService.SortList(list);

            Assert.AreEqual(result.Count, list.Count);
            Assert.AreEqual(result.First(), list.First());
            Assert.AreEqual(result[1], list.Last());
        }
    }
}
