using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CourseWork_WPF.information;
using System.Threading;
using System.Reflection;
using System.Collections.Immutable;
using LogicClasses;
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using static GMap.NET.Entity.OpenStreetMapGraphHopperRouteEntity;
using Path = System.Windows.Shapes.Path;
using Dijkstra_Algorihm;

namespace CourseWork_WPF.algoritms
{
    /// <summary>
    /// Логика взаимодействия для BFS.xaml
    /// </summary>
    public partial class BFS : Window
    {
        public BFS()
        {
            InitializeComponent();
            if (MainWindow.a == "BFS") {
                doButton.Click += doBFS;
                
            }
            if (MainWindow.a == "DFS")
            {
                doButton.Click += doDFS;
                gBox.Header = "Обход в Глубину";
            }
            if (MainWindow.a == "Краскала")
            {
                doButton.Click += doKraskal;
                gBox.Header = "Алгоритм Краскала";

            }
            if (MainWindow.a == "Прима")
            {
                doButton.Click += doPrima;
                gBox.Header = "Прима";

            }
            if (MainWindow.a == "Дейкстры")
            {
                doButton.Click += doDK;
                gBox.Header = "Дейкстры";

            }
            foreach (var item in Graph.Vertexes) {
                GMapMarker marker = new GMapMarker(new PointLatLng(Convert.ToDouble(item.X), Convert.ToDouble(item.Y)));
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri(MainWindow.MarkerImage);
                bm.EndInit();
                System.Windows.Controls.Image image = new Image();
                image.Source = bm;
                marker.Shape = new Image
                {
                    Source = bm,
                    Width = 50,
                    Height = 50,
                    ToolTip = item.Name,


                };
                marker.Offset = new System.Windows.Point(-25, -25);
                marker.ZIndex = 1;
                Gmap.Markers.Add(marker);
                fromBox1.Items.Add(item.Name);
                toBox2.Items.Add(item.Name);
            }

        }
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerOnly;//ServerAndCache;
                                                        //выбор подгрузки карты – онлайн или из ресурсов

            Gmap.MapProvider = GoogleMapProvider.Instance;
            //gMapControl1.MapProvider = GMapProviders.BingHybridMap;

            //какой провайдер карт используется (в нашем случае гугл) 
            Gmap.CacheLocation = @"C:\Users\Maksim\source\repos\CoursWork\CoursWork\bin\Debug\Cache";
            Gmap.MinZoom = 2;
            //минимальный зум

            Gmap.MaxZoom = 16;
            //максимальный зум

            Gmap.Zoom = 12;
            // какой используется зум при открытии

            Gmap.Position = new GMap.NET.PointLatLng(55.792318, 49.122549);
            // точка в центре карты при открытии (центр России)

            Gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // как приближает (просто в центр карты или по положению мыши)

            Gmap.CanDragMap = true;
            // перетаскивание карты мышью

            Gmap.DragButton = MouseButton.Left;
            // какой кнопкой осуществляется перетаскивание

            Gmap.ShowCenter = false;
            //показывать или скрывать красный крестик в центре

            Gmap.ShowTileGridLines = false;
            //показывать или скрывать тайлы


            //Контекстное меню


        }

      

        private void doBFS(object sender, RoutedEventArgs e)
        {
            try
            {
                routeBox.Text = String.Empty;
                bool yesroute = false;
                Queue<int> q = new Queue<int>();
                int[,] g = Graph.GetMatrix();
                bool[] used = new bool[g.GetLength(0)];  //массив отмечающий посещённые вершины
                int u = Graph.Vertexes.Find(x => x.Name == fromBox1.SelectedItem.ToString()).Number;
                used[u] = true;     //массив, хранящий состояние вершины(посещали мы её или нет)
                int to = Graph.Vertexes.Find(x => x.Name == toBox2.SelectedItem.ToString()).Number;
                q.Enqueue(u);
                routeBox.AppendText("Начинаем с "+ Graph.Vertexes.Find(x => x.Number == u).Name+"\r\n");
                while (q.Count != 0)
                {
                    if (yesroute == true) { break; }
                    u = q.Peek();
                    q.Dequeue();
                    routeBox.AppendText("Пришли " + Graph.Vertexes.Find(x => x.Number == u).Name+"\r\n");

                    for (int i = 0; i < g.GetLength(0); i++)
                    {
                        if (yesroute == true) { break; }
                        if (Convert.ToBoolean(g[u,i]))
                        {
                            if (!used[i])
                            {
                                if (i == to)
                                {
                                    DrawLine(Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).Y), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).Y));
                                    routeBox.AppendText("Пришли в " + Graph.Vertexes.Find(x => x.Number == i).Name + "\r\n");
                                    routeBox.AppendText("Маршрут найден");
                                    yesroute = true;
                                    break;
                                }
                                else
                                {
                                    DrawLine(Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).Y), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).Y));
                                    routeBox.AppendText("Пришли в " + Graph.Vertexes.Find(x => x.Number == i).Name + "\r\n");
                                    used[i] = true;
                                    q.Enqueue(i);
                                }
                            }
                        }
                    }
                }
                if(yesroute==false)
                {
                    MessageBox.Show("Маршрут не найден", "Ошибка", MessageBoxButton.OK);
                }
                
            }
            catch
            {
                MessageBox.Show("Должны быть выбраны все поля!", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void doDFS(object sender, RoutedEventArgs e)
        {
            try
            {
                routeBox.Text = String.Empty;
                bool yesroute = false;
                Stack<int> q = new Stack<int>();
                int[,] g = Graph.GetMatrix();
                bool[] used = new bool[g.GetLength(0)];  //массив отмечающий посещённые вершины
                int u = Graph.Vertexes.Find(x => x.Name == fromBox1.SelectedItem.ToString()).Number;
                used[u] = true;     //массив, хранящий состояние вершины(посещали мы её или нет)
                int to = Graph.Vertexes.Find(x => x.Name == toBox2.SelectedItem.ToString()).Number;
                q.Push(u);
                routeBox.AppendText("Начинаем с " + Graph.Vertexes.Find(x => x.Number == u).Name + "\r\n");
                while (q.Count != 0)
                {
                    
                    if (yesroute == true) { break; }
                    u = q.Peek();
                    if (u == -1) { break; }
                    q.Pop();
                    routeBox.AppendText("Пришли " + Graph.Vertexes.Find(x => x.Number == u).Name + "\r\n");
                    int s = 0;
                    for (int i = 0; i < g.GetLength(0); i++)
                    {
                        
                        if (yesroute == true) { break; }
                        if (Convert.ToBoolean(g[u, i]))
                        {
                            if (!used[i])
                            {
                                DrawLine(Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == u).Y), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).X), Convert.ToDouble(Graph.Vertexes.Find(x => x.Number == i).Y));
                                if (i == to)
                                {
                                    routeBox.AppendText("Пришли " + Graph.Vertexes.Find(x => x.Number == i).Name + "\r\n");
                                    routeBox.AppendText("Маршрут найден");
                                    yesroute = true;
                                    break;
                                }
                                used[i] = true;
                                q.Push(i);
                                break;
                            }
                        }
                        if ((i == g.GetLength(0) - 1) && (s == 0)) { q.Push(u-1); }
                    }
                }
                if (yesroute == false)
                {
                    MessageBox.Show("Маршрут не найден", "Ошибка", MessageBoxButton.OK);
                }

            }
            catch
            {
                MessageBox.Show("Должны быть выбраны все поля!", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void doKraskal(object sender, RoutedEventArgs e)
        {

            routeBox.Text = String.Empty;
            bool yesroute = false;
            KGraph graph = new KGraph();
            foreach(var ed in Graph.Edges)
            {
                graph.Add(new KEdge(ed.From.Name,ed.To.Name,ed.EdgeWeight));
            }
            graph = graph.FindMinimumSpanningTree();
            int weight = 0;
            foreach(var ed in graph)
            {
                double x1 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexA).X);
                double y1 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexA).Y);
                double x2 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexB).X);
                double y2 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexB).Y);
                DrawLine(x1,y1,x2,y2);
                weight += ed.EdgeWeight;
            }
            routeBox.Text = "Длина дерева: " + weight.ToString()+" км";

               
        }
        private void doPrima(object sender, RoutedEventArgs e)
        {
            KGraph MST =new KGraph();
            routeBox.Text = String.Empty;
            bool yesroute = false;
            KGraph graph = new KGraph();
            foreach (var ed in Graph.Edges)
            {
                graph.Add(new KEdge(ed.From.Name, ed.To.Name, ed.EdgeWeight));
            }
            int numberV = Graph.Vertexes.Count;
            List<KEdge> notUsedE = new List<KEdge>(graph);
            //использованные вершины
            List<string> usedV = new List<string>();
            //неиспользованные вершины
            List<string> notUsedV = new List<string>();
            for (int i = 0; i < numberV; i++)
            {
                notUsedV.Add(Graph.Vertexes[i].Name);
            }
            //выбираем случайную начальную вершину
            Random rnd = new Random();
            usedV.Add((Graph.Vertexes[rnd.Next(0, numberV)].Name));
            notUsedV.Remove(usedV[0]);
            while (notUsedV.Count > 0)
            {
                int minE = -1; //номер наименьшего ребра
                               //поиск наименьшего ребра
                for (int i = 0; i < notUsedE.Count; i++)
                {
                    if ((usedV.IndexOf(notUsedE[i].VertexA) != -1) && (notUsedV.IndexOf(notUsedE[i].VertexB) != -1) ||
                        (usedV.IndexOf(notUsedE[i].VertexB) != -1) && (notUsedV.IndexOf(notUsedE[i].VertexA) != -1))
                    {
                        if (minE != -1)
                        {
                            if (notUsedE[i].EdgeWeight < notUsedE[minE].EdgeWeight)
                                minE = i;
                        }
                        else
                            minE = i;
                    }
                }
                //заносим новую вершину в список использованных и удаляем ее из списка неиспользованных
                if (usedV.IndexOf(notUsedE[minE].VertexA) != -1)
                {
                    usedV.Add(notUsedE[minE].VertexB);
                    notUsedV.Remove(notUsedE[minE].VertexB);
                }
                else
                {
                    usedV.Add(notUsedE[minE].VertexA);
                    notUsedV.Remove(notUsedE[minE].VertexA);
                }
                //заносим новое ребро в дерево и удаляем его из списка неиспользованных
                MST.Add(notUsedE[minE]);
                notUsedE.RemoveAt(minE);
            }
            int weight = 0;
            foreach (var ed in MST)
            {
                double x1 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexA).X);
                double y1 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexA).Y);
                double x2 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexB).X);
                double y2 = Convert.ToDouble(Graph.Vertexes.Find(x => x.Name == ed.VertexB).Y);
                DrawLine(x1, y1, x2, y2);
                weight += ed.EdgeWeight;
            }
            routeBox.Text = "Длина дерева: " + weight.ToString() + " км";
        }

        private void doDK(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var p in Graph.Edges)
                {
                    DrawLine(Convert.ToDouble(p.From.X), Convert.ToDouble(p.From.Y), Convert.ToDouble(p.To.X), Convert.ToDouble(p.To.Y));
                }
                int[,] g = Graph.GetMatrix();
                int u = Graph.Vertexes.Find(x => x.Name == fromBox1.SelectedItem.ToString()).Number;
                string route = DGraph.DijkstraAlgo(g, u, g.GetLength(0));
                routeBox.Text = route;
            }
            catch
            {
                MessageBox.Show("Выберите начальную точку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void DrawLine(double X1,double Y1,double X2, double Y2)
        {
            List<PointLatLng> pointlatlang = new List<PointLatLng>();
            pointlatlang.Add(new PointLatLng(X1, Y1));
            pointlatlang.Add(new PointLatLng(X2,Y2));
            GMapPolygon polygon = new GMapPolygon(pointlatlang);
            Gmap.RegenerateShape(polygon);
            (polygon.Shape as Path).Stroke = Brushes.Green;
            (polygon.Shape as Path).StrokeThickness = 1.5;
            (polygon.Shape as Path).Effect = null;
            Gmap.Markers.Add(polygon);
        }
    }
}
