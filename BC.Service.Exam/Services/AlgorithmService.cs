namespace BC.Service.Exam.Services
{
    public static class AlgorithmService
    {
        public static IList<int> GenerateRandomNumbers(int num)
        {
            var numbers = new List<int>();

            for (int i = 1; i <= num; i++)
            {
                numbers.Add(i);
            }

            return SortList(numbers);
        }


        public static List<int> SortList(List<int> candidates)
        {
            List<int> rearranged = new List<int>();
            int start = 0;
            int end = candidates.Count - 1;

            while (start <= end)
            {
                if (start == end)
                {
                    rearranged.Add(candidates[start]);
                }
                else
                {
                    rearranged.Add(candidates[start]);
                    rearranged.Add(candidates[end]);
                }
                start++;
                end--;
            }

            return rearranged;
        }
    }
}
