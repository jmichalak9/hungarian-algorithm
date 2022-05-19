namespace graph;

public class BipartiteGraph
{
    public readonly int[,] Costs;
    private int n;

    public BipartiteGraph(int[,] costs)
    {
        this.Costs = costs;
        this.n = costs.GetLength(0);
    }

    public (List<(int x, int y)>, int) MinWeightPerfectMatching()
    {
        // Labeling
        var lx = new int[n];
        var ly = new int[n];

        // Sets
        var S = new bool[n];
        var T = new bool[n];

        // Matches
        var matchX = new int[n];
        var matchY = new int[n];

        // Slack
        var slack = new int[n];
        var slack_X = new int[n];

        InitLabels(lx, ly);
        InitMatches(matchX, matchY);

        // Queue for searching augmenting path (only vertices from X are inserted)
        var q = new Queue<int>();

        // Main loop
        for (int i = 0; i < n; i++)
        {
            q.Clear();
            ClearSets(S, T);

            var root = -1;
            // Find root
            for (int k = 0; k < n; k++)
            {
                if (matchX[k] == -1)
                {
                    q.Enqueue(k);
                    S[k] = true;
                    root = k;
                    break;
                }
            }

            // Init Slack
            for (int k = 0; k < n; k++)
            {
                slack[k] = Costs[root, k] - lx[root] - ly[k];
                slack_X[k] = root;
            }

            int current_X = root;
            int current_Y = 0;
            int[] prev_X = new int[n];
            bool is_augmenting_path = false;

            prev_X[root] = -1;
            
            // Find augmenting path
            while(!is_augmenting_path)
            {
                while(q.Count != 0 && !is_augmenting_path)
                {
                    current_X = q.Dequeue();

                    for (current_Y = 0; current_Y < n; current_Y++)
                    {
                        // Edge in equality graph and vertex not visited
                        if (Costs[current_X, current_Y] == lx[current_X] + ly[current_Y] && !T[current_Y])
                        {
                            // Augmenting path found
                            if (matchY[current_Y] == -1)
                            {
                                is_augmenting_path = true;
                                break;
                            }

                            T[current_Y] = true;
                            var next_X = matchY[current_Y];
                            q.Enqueue(next_X);

                            S[next_X] = true;
                            prev_X[next_X] = current_X;

                            // Update slack
                            for (int t = 0; t < n; t++)
                            {
                                if (Costs[next_X, t] - lx[next_X] - ly[t] < slack[t])
                                {
                                    slack[t] = Costs[next_X, t] - lx[next_X] - ly[t];
                                    slack_X[t] = next_X;
                                }
                            }
                        }
                    }

                    // Augmenting path not found -> Update labels
                    if (!is_augmenting_path)
                    {
                        // Update labels and slack
                        UpdateLabels(slack, lx, ly, S, T);

                        // Continue searching for augmenting path
                        for (current_Y = 0; current_Y < n; current_Y++)
                        {
                            // When added edge is found (edge added to equality graph after labels modification)
                            if (!T[current_Y] && slack[current_Y] == 0)
                            {
                                // Augmenting path found
                                if (matchY[current_Y] == -1)
                                {
                                    // Get vertex from set X that have new edge
                                    current_X = slack_X[current_Y];
                                    is_augmenting_path = true;
                                    break;
                                }

                                T[current_Y] = true;
                                var next_X = matchY[current_Y];

                                if (!S[next_X])
                                {
                                    q.Enqueue(next_X);

                                    S[next_X] = true;
                                    prev_X[next_X] = slack_X[current_Y];

                                    // Update slack
                                    for (int t = 0; t < n; t++)
                                    {
                                        if (Costs[next_X, t] - lx[next_X] - ly[t] < slack[t])
                                        {
                                            slack[t] = Costs[next_X, t] - lx[next_X] - ly[t];
                                            slack_X[t] = next_X;
                                        }
                                    }
                                }
                            }
                        }

                        throw new NoAugmentingPathFoundException("Augmenting path was not found after labels update!");
                    }
                }
            }

            // Inverse edges in augmenting path
            int previous_match;
            for(int x = current_X, y = current_Y; x != -1; y = previous_match, x = prev_X[x])
            {
                previous_match = matchX[x];
                matchY[y] = x;
                matchX[x] = y;
            }
        }

        int result = 0;
        var edges = new List<(int x, int y)>();
        for (int i = 0; i < n; i++)
        {
            if (matchX[i] != -1)
            {
                result += Costs[i, matchX[i]];
                edges.Add((i, matchX[i]));
            }
        }

        return (edges, result);
    }

    private void UpdateLabels(int[] slack, int[] lx, int[] ly, bool[] S, bool[] T)
    {
        var minValue = int.MaxValue;

        for (int i = 0; i < n; i++)
        {
            if (!T[i] && minValue > slack[i])
            {
                minValue = slack[i];
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (S[i])
            {
                lx[i] += minValue;
            }

            if (T[i])
            {
                ly[i] -= minValue;
            }
            else
            {
                slack[i] -= minValue;
            }
        }
    }

    private void ClearSets(bool[] S, bool[] T)
    {
        for (int i = 0; i < n; i++)
        {
            S[i] = T[i] = false;
        }
    }

    private void InitLabels(int[] lx, int[] ly)
    {
        for (int j = 0; j < n; j++)
        {
            var minValue = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (Costs[i, j] != -1 && Costs[i, j] < minValue)
                {
                    minValue = Costs[i, j];
                }
            }

            ly[j] = minValue;
            lx[j] = 0;
        }
    }

    public void InitMatches(int[] matchX, int[] matchY)
    {
        for (int i = 0; i < n; i++)
        {
            matchX[i] = matchY[i] = -1;
        }
    }
}