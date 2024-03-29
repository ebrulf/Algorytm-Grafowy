using Algorytm;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;


namespace wierzcholki_rozdzielajace
{
    public partial class Form1 : Form
    {
        Button addVertexBtn;
        List<string> vertexNames;
        List<List<int>> graph;
        string missing_vertex = "";
        int vertices = 0;
        int edges = 0;
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            vertexNames = new List<string>();
            graph = new List<List<int>>();
            initAddVertexBtn();
            addVertexSlot();
        }
        /// <summary>
        /// 
        /// </summary>
        public void addVertexSlot()
        {
            Panel p = new Panel();
            p.Width = 1000;
            p.Height = 45;
            p.Padding = new Padding(12);
            p.BackColor = Color.Gray;
            p.Controls.Add(new Label() { Text = "Nazwa Wierzchołka", Width = 120, Location = new Point(0, 12) });
            p.Controls.Add(new TextBox() { Width = 100, Location = new Point(120, 10) });
            p.Controls.Add(new Label() { Text = "Sąsiedzi", Width = 60, Location = new Point(240, 12) });
            p.Controls.Add(new TextBox() { Width = 500, Location = new Point(310, 10) });
            flowLayoutPanel1.Controls.Add(p);
            flowLayoutPanel1.Controls.SetChildIndex(addVertexBtn, flowLayoutPanel1.Controls.Count-1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool parseGraph()
        {
            vertexNames.Clear();
            graph.Clear();
            vertices = 0;
            edges = 0;
            missing_vertex = "";
            for(int i=0;i<flowLayoutPanel1.Controls.Count-1;i++)
            {
                vertexNames.Add(flowLayoutPanel1.Controls[i].Controls[1].Text);
                graph.Add(new List<int>());
                vertices++;
            }
            for(int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            {
                string[] neightbours = flowLayoutPanel1.Controls[i].Controls[3].Text.Split(' ');
                foreach(string s in neightbours)
                {
                    if (s != "")
                    {
                        int index = vertexNames.IndexOf(s);
                        if (index != -1)
                        {
                            graph[i].Add(index);
                            edges++;
                        }
                        else
                        {
                            missing_vertex = s;
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void initAddVertexBtn()
        {
            addVertexBtn = new Button() { Text = "Dodaj Wierzchołek",Width=150 };
            addVertexBtn.Click += new System.EventHandler(addVertexBtn_Click);
            flowLayoutPanel1.Controls.Add(addVertexBtn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addVertexBtn_Click(object sender,EventArgs e)
        {
            addVertexSlot();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calcBtn_Click(object sender, EventArgs e)
        {
            if(parseGraph())
            {
                outputTextbox.Text = "Znajdowanie wierzchołków rozdzielających...";
                //Znajdz wierzcholki rozdzielajace
                Graph g = new Graph(11);
                for (int u = 0; u < graph.Count; u++)
                {
                    List<int> edg = graph[u];
                    for (int v = 0; v < edg.Count; v++)
                    {
                        g.addEdge(u, edg[v]);
                    }
                }

                bool[] articulation_points = g.AP();
                string mes = "Wierzchołki rozdzielające to ";
                bool no_verts = true;
                for(int i=0;i<articulation_points.Length;i++)
                {
                    if (articulation_points[i] == true)
                    {
                        mes += vertexNames[i] + ", ";
                        no_verts = false;
                    }
                }
                if (no_verts) outputTextbox.Text = "Brak wierzchołków rozdzielających";
                else outputTextbox.Text = mes;
            }
            else
            {
                outputTextbox.Text = String.Format("Nie istnieje wierzchołek o nazwie {0}", missing_vertex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBtn_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(addVertexBtn);
            addVertexSlot();
        }
    }
}