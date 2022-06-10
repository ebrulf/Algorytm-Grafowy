using System;
using Algorytm;
using System.Collections.Generic;

namespace grafy
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            MacInc Walaszek = new MacInc(8, 11);
            Walaszek.DodajKrawędź(0, 1);
            Walaszek.DodajKrawędź(0, 2);
            Walaszek.DodajKrawędź(0, 3);
            Walaszek.DodajKrawędź(0, 5);
            Walaszek.DodajKrawędź(1,4);
            Walaszek.DodajKrawędź(1,5);
            Walaszek.DodajKrawędź(2,3);
            Walaszek.DodajKrawędź(3,6);
            Walaszek.DodajKrawędź(3,7);
            Walaszek.DodajKrawędź(4,5);
            Walaszek.DodajKrawędź(6,7);
            Walaszek.Wypisz();
            Klasa.Wypisz(Klasa.WyszukajDwuspójne(Walaszek));
            //ma wyjść 0, 3
            //drugi przykład https://www.geeksforgeeks.org/biconnected-components/
            //trzeci https://en.wikipedia.org/wiki/Biconnected_component#Pseudocode
        }
    }
}
