using System;
using CommandLine;

namespace ExampleGenerator
{
    class Program
    {
        public class Options
        {
            [Value(0)]
            public int N { get; set; }
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
            [Option("rand-min", Required = false, Default = 0, HelpText = "Minimal random weight.")]
            public int RandMin { get; set; }
            [Option("rand-max", Required = false, Default = 10, HelpText = "Maximal random weight.")]
            public int RandMax { get; set; }
            [Option('p', "edges-prob", Required = false, Default = 0.8f, HelpText = "Probability of existing edges.")]
            public float EdgesProb { get; set; }
            [Value(1)]
            public string Filename { get; set; }

        }

        static void Main(string[] args)
        {
            try
            {
                var rand = new Random();
                int randMin = 0, randMax = 0;
                float edgesProb = 0;
                var n = 0;
                string filename = "";
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(o =>
                    {
                        randMin = o.RandMin;
                        randMax = o.RandMax;
                        n = o.N;
                        edgesProb = o.EdgesProb;
                        filename = o.Filename;
                    });
                var edges = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (rand.NextDouble() > edgesProb)
                            edges[i, j] = -1;
                        else
                            edges[i, j] = rand.Next(randMin, randMax);
                    }
                }
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"{n}");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            writer.Write($"{edges[i,j]} ");
                        }

                        writer.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e}");
            }
        }
    }
}