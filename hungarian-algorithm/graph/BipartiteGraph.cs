namespace graph;

public class BipartiteGraph
{
    private int[,] costs;

    public BipartiteGraph(int[,] costs)
    {
        this.costs = costs;
    }

    public (List<(int, int)>, int) MinWeightPerfectMatching()
    {
        return (new List<(int, int)>(), 0);
    }
}