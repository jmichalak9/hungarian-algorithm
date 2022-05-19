using graph;
using System;
using Xunit;

namespace HungarianAlgoritmTests
{
    public class HungarianAlgorithmTests
    {
        private const int _maxWeight = 1_000_000;

        [Theory]
        [InlineData(20, 0.7, 1, 5)]
        [InlineData(30, 0.7, 1, 5)]
        [InlineData(50, 0.7, 1, 5)]
        [InlineData(100, 0.7, 1, 500)]
        [InlineData(200, 0.7, 1, 300)]
        [InlineData(300, 0.7, 1, 200)]
        [InlineData(500, 0.7, 1, 1000)]
        [InlineData(1000, 0.7, 1, 5)]
        [InlineData(1500, 0.7, 1, 5)]
        [InlineData(2000, 0.7, 1, 5)]
        [InlineData(2500, 0.7, 1, 5)]
        [InlineData(3000, 0.7, 1, 5)]
        [InlineData(3500, 0.7, 1, 5)]
        [InlineData(4000, 0.7, 1, 5)]
        [InlineData(4500, 0.7, 1, 5)]
        [InlineData(5000, 0.7, 1, 5)]
        public void HungarianAlgorithmTest(int n, double edgesProb, int randMin, int randMax)
        {
            Random _rng = new Random(123);

            var costs = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (_rng.NextDouble() > edgesProb)
                        costs[i, j] = _maxWeight;
                    else
                        costs[i, j] = _rng.Next(randMin, randMax);
                }
            }

            var graph = new BipartiteGraph(costs);
            var solution = graph.MinWeightPerfectMatching();

            int[] reference_solution = HungarianAlgorithm.HungarianAlgorithm.FindAssignments((int[,])costs.Clone());
            var sum = 0;

            for (int i = 0; i < reference_solution.Length; i++)
            {
                sum += costs[i, reference_solution[i]];
            }

            Assert.Equal(sum, solution.Item2);
        }
    }
}