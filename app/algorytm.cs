﻿using System.Collections.Generic;
using System;
using wierzcholki_rozdzielajace;
namespace Algorytm
{
    public class Graph
    {
        private int V;

        private List<int>[] adj;
        int time = 0;
        static readonly int NIL = -1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public Graph(int v)
        {
            V = v;
            adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        public void addEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="visited"></param>
        /// <param name="disc"></param>
        /// <param name="low"></param>
        /// <param name="parent"></param>
        /// <param name="ap"></param>
        void APUtil(int u, bool[] visited, int[] disc,
                    int[] low, int[] parent, bool[] ap)
        {

            int children = 0;

            visited[u] = true;

            disc[u] = low[u] = ++time;

            foreach (int i in adj[u])
            {
                int v = i;

                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    APUtil(v, visited, disc, low, parent, ap);

                    low[u] = Math.Min(low[u], low[v]);


                    if (parent[u] == NIL && children > 1)
                        ap[u] = true;

                    if (parent[u] != NIL && low[v] >= disc[u])
                        ap[u] = true;
                }

                else if (v != parent[u])
                    low[u] = Math.Min(low[u], disc[v]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool[] AP()
        {
            bool[] visited = new bool[V];
            int[] disc = new int[V];
            int[] low = new int[V];
            int[] parent = new int[V];
            bool[] ap = new bool[V];


            for (int i = 0; i < V; i++)
            {
                parent[i] = NIL;
                visited[i] = false;
                ap[i] = false;
            }

            for (int i = 0; i < V; i++)
                if (visited[i] == false)
                    APUtil(i, visited, disc, low, parent, ap);

            return ap;
        }

    }
}