namespace BC.Service.Exam.Services
{
    public static class AlgorithmService
    {
        public static IList<int> GenerateRandomNumbers(int num)
        {
            var numbers = new List<int>();

            for (var i = 1; i <= num; i++)
            {
                numbers.Add(i);
            }

            return SortList(numbers);
        }


        public static List<int> SortList(List<int> candidates)
        {
            var result = new List<int>();
            var start = 0;
            var end = candidates.Count - 1;

            while (start <= end)
            {
                if (start == end)
                {
                    result.Add(candidates[start]);
                }
                else
                {
                    result.Add(candidates[start]);
                    result.Add(candidates[end]);
                }
                start++;
                end--;
            }

            return result;
        }
    }
}
