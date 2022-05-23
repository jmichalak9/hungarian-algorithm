// See https://aka.ms/new-console-template for more information

using graph;
using program;
using System.Diagnostics;
try
{

    if (args.Length != 2)
    {
        throw new Exception($"expected 2 arguments for input and output file, got {args.Length}");
    }

    var graph = GraphReader.ReadGraph(args[0]);
    
    List<(int x, int y)> matching = new List<(int x, int y)>();
    int weight = 0;
    Clock.BenchmarkCpu(() =>
    {
        (matching, weight) = graph.MinWeightPerfectMatching();
    }, 1);

    
    using (StreamWriter writer = new StreamWriter(args[1]))
    {
        foreach(var match in matching)
        {
            if(graph.Costs[match.x, match.y] == GraphReader.MaxWeight)
            {
                writer.WriteLine("no matching found");
                return;
            }
        }

        writer.WriteLine(weight);
        foreach (var e in matching)
        {
            writer.WriteLine($"{e.x} {e.y}");
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"error: {e.Message}");
}
