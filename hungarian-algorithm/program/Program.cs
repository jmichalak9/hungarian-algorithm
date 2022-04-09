// See https://aka.ms/new-console-template for more information

using graph;
using program;

try
{

    //if (args.Length != 2)
    //{
    //    throw new Exception($"expected 2 arguments for input and output file, got {args.Length}");
    //}
    //var gr = new GraphReader();
    //var graph = gr.ReadGraph(args[0]);

    var costs = new int[,]
    {
        {1, 2, 3 },
        {9999, 4, 6 },
        {3, 6, 9999 }
    };

    var graph = new BipartiteGraph(costs);

    var result = graph.MinWeightPerfectMatching();

    Console.WriteLine(result.Item2);
    foreach(var edge in result.Item1)
    {
        Console.WriteLine($"{edge.x}, {edge.y}");
    }
    //using (StreamWriter writer = new StreamWriter(args[1]))
    //{
    //    if (matching.Count == 0)
    //    {
    //        writer.WriteLine("no matching found");
    //        return;
    //    }
    //    writer.WriteLine(weight);
    //    foreach (var e in matching)
    //    {
    //        writer.WriteLine($"{e.Item1} {e.Item2}");
    //    }
    //}
}
catch (Exception e)
{
    Console.WriteLine($"error: {e.Message}");
}
