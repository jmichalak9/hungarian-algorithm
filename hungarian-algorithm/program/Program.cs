// See https://aka.ms/new-console-template for more information

using graph;
using program;
using System.Diagnostics;
using CommandLine;

namespace program
{
    class Program
    {
        public class Options
        {
            [Value(0)]
            public string InFile { get; set; } = "";
            [Value(1)]
            public string OutFile { get; set; } = "";
            [Option("time", Required = false, Default = false, HelpText = "Show algorithm execution time.")]
            public bool Time { get; set; }
        }
        static void Main(string[] args)
        {   
            try
            {
                bool showTime = false;
                string inFile = "", outFile = "";
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(o =>
                    {
                        showTime = o.Time;
                        inFile = o.InFile;
                        outFile = o.OutFile;
                    });

                var graph = GraphReader.ReadGraph(inFile);
    
                List<(int x, int y)> matching = new List<(int x, int y)>();
                int weight = 0;
                if (showTime)
                {
                    Clock.BenchmarkCpu(() =>
                    {
                        (matching, weight) = graph.MinWeightPerfectMatching();
                    }, 1);     
                }
                else
                {
                    (matching, weight) = graph.MinWeightPerfectMatching();
                }
    
                using (StreamWriter writer = new StreamWriter(outFile))
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
        }
    }
}