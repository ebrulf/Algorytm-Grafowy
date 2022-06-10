using System;
using System.Collections.Generic;

namespace Algorytm
{
    public abstract class GrafNiesk
    {
        int wierzchołków;
        int krawędzi;
        object mac;
        public void DodajKrawędź(int u, int v)
        {
            krawędzi++;
        }
        public bool CzySąsiad(int u, int v) => true;
        public int Wierzchołków
        {
            get { return wierzchołków; }
        }
        public int Krawędzi
        {
            get { return krawędzi; }
        }
        public void Wypisz()
        {

        }
    }
    public class MacSas
    {
        int wierzchołków;
        int krawędzi;
        int[,] mac;
        public MacSas(int wierzchołków)
        {
            mac = new int[wierzchołków, wierzchołków];
            krawędzi = 0;
        }
        public void DodajKrawędź(int u, int v)
        {
            if (u < 0 || u >= wierzchołków || v < 0 || v >= wierzchołków)
                return;
            mac[u, v]++;
            mac[v, u]++;
            krawędzi++;
        }
        public bool CzySąsiad(int u, int v)
        {
            if (u < 0 || u >= wierzchołków || v < 0 || v >= wierzchołków)
                return false;
            return mac[u, v] > 0 && mac[v, u] > 0;
        }
        public void PrzejmijTablicę(int[,] M)
        {
            mac = M;
            wierzchołków = M.GetLength(0);
            krawędzi = ZliczKrawędzie;
        }
        public int ZliczKrawędzie
        {
            get
            {
                int suma = 0;
                foreach (int n in mac)
                    suma += n;
                return suma;
            }
        }
        public int Wierzchołków
        {
            get { return wierzchołków; }
        }
        public int Krawędzi
        {
            get { return krawędzi; }
        }
        public void Wypisz()
        {
            for(int i = 0; i < wierzchołków; i++)
            {
                for(int j=0; j<wierzchołków; j++)
                {
                    Console.Write(mac[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }
    }
    public class MacInc
    {
        int wierzchołków;
        int krawędzi;
        int krawędzi_max;
        //int[,] mac;
        List<int[]> mac;
        public MacInc(int n, int m)
        {
            //mac = new int[n, m];
            mac = new List<int[]>();//tworzę maciwerz transponowaną
            wierzchołków = n;
            krawędzi_max = m;
        }
        public void DodajKrawędź(int u, int v)//wierzch i, kraw j
        {
            if (u < 0 || v < 0 || u>=wierzchołków || v>=wierzchołków || krawędzi>=krawędzi_max)
                return;
            int[] temp = new int[wierzchołków];
            temp[u]++;
            temp[v]++;
            mac.Add(temp);
            krawędzi++;
        }
        public bool CzySąsiad(int u, int v)
        {
            if (u < 0 || v < 0 || u >= wierzchołków || v >= wierzchołków)
                return false;
            return mac[u][v] !=0 && mac[v][u] != 0;
        }
        public void PrzejmijTablicę(int[,] M)
        {
            
        }
        public int Wierzchołków
        {
            get { return wierzchołków; }
        }
        public int Krawędzi
        {
            get { return krawędzi; }
        }
        public void Wypisz()
        {
            Klasa.Wypisz(mac);
        }
    }
    public class ListSas
    {
        int wierzchołków;
        int krawędzi;
        List<int>[] mac;
        public ListSas(int wierzchołków)
        {
            mac = new List<int>[wierzchołków];
            this.wierzchołków = wierzchołków;
        }
        public void DodajKrawędź(int u, int v)
        {
            if (u < 0 || v < 0 || u >= wierzchołków || v >= wierzchołków)
                return;
            mac[u].Add(v);
            mac[v].Add(u);
            krawędzi++;
        }
        public bool CzySąsiad(int u, int v)
        {
            if (u < 0 || v < 0 || u >= wierzchołków || v >= wierzchołków)
                return false;
            return mac[u].IndexOf(v) != -1 && mac[v].IndexOf(u) != -1;
        }
        public int Wierzchołków
        {
            get { return wierzchołków; }
        }
        public int Krawędzi
        {
            get { return krawędzi; }
        }
        public void Wypisz()
        {
            Klasa.Wypisz(mac);
        }
    }
    public class Klasa
    {
        public static string Wypisz<T>(T i)
        {
            return i.ToString();
        }
        public static void Wypisz<T>(T[] t)
        {
            foreach (T i in t)
                Console.Write(Wypisz<T>(i) + "\t");
            Console.Write("\n");
        }
        public static void Wypisz<T>(List<T> l)
        {
            foreach (T i in l)
                Console.Write(Wypisz<T>(i) + "\t");
            Console.Write("\n");
        }
        private static int DFSap(int v,int vf, MacInc T, int[] D, int dv, List<int> L)
        {
            int Low;
            int temp;
            bool test;

            D[v] = dv;
            Low = dv;
            dv++;
            test = false;
            foreach (int u in D)
            {
                if (!T.CzySąsiad(u, v) || u==vf)
                    continue;
                if (D[u]<Low)
                {
                    Low = D[u];
                    continue;
                }
                if (D[u]==0)
                {
                    temp = DFSap(u, v, T, D, dv, L);
                    if (temp < Low)
                        Low = temp;
                    if (temp >= D[v])
                        test = true;
                }
            }
            if (test)
                L.Add(v);
            return Low;

        }
        public static List<int> WyszukajDwuspójne(MacInc T)
        {
            //algorytm Tarjana za https://eduinf.waw.pl/inf/alg/001_search/0130b.php rozw 2
            //nie zrozumiałem ani oryginalnego papieru ani pseudokodu na wiki
            int[] D = new int[T.Wierzchołków];
            int dv;
            int nc;//lista synów
            List<int> punkty = new List<int>();
            foreach(int v in D)
            {
                if (D[v] > 0)
                    continue;
                dv = 2;
                nc = 0;
                D[v] = 1;
                for(int u=0; u<T.Wierzchołków; u++)
                {
                    if (!T.CzySąsiad(u, v) || D[u]>0)
                        continue;
                    nc++;
                    DFSap(u, v, T, D, dv, punkty);

                }
                if (nc > 1)
                    punkty.Add(v);
            }
            return punkty;
        }
    }
}
