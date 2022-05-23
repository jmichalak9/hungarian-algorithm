using System.Text;
using graph;

namespace program;

public static class GraphReader
{
    public static readonly int MaxWeight = 1_000_000;
    public static BipartiteGraph ReadGraph(string path)
    {
        using (var fileStream = File.OpenRead(path))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 65536))
        {
            String line = streamReader.ReadLine()!;
            var n = Convert.ToInt32(line);
            if (n < 1)
            {
                throw new Exception("number of vertices must be greater than 0");
            }
            int[,] costs = new int[n,n];
            for (int i = 0; i < n; i++)
            {
                var row = streamReader.ReadLine()?.Split()!;
                if (row.Length != n)
                {
                    throw new Exception($"incorrect number of values in a row: expected {n}, got {row.Length}");
                }
                for (int j = 0; j < n; j++)
                {
                    costs[i, j] = int.Parse(row[j]);
                    if (costs[i, j] > MaxWeight)
                    {
                        throw new Exception($"weight cannot be bigger than {MaxWeight}");
                    }

                    if (costs[i, j] == -1)
                    {
                        costs[i,j] = MaxWeight;
                    }
                }
            }
            return new BipartiteGraph(costs);
        }
    }
}