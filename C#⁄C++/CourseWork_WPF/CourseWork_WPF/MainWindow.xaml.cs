using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Device.Location;
using CourseWork_WPF.information;
using Point = CourseWork_WPF.information.Point;
using CourseWork_WPF.algoritms;
using Image = System.Windows.Controls.Image;
using Microsoft.Win32;
using System.IO;
using Path = System.Windows.Shapes.Path;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;

namespace CourseWork_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string MarkerImage = @"C:\Users\Maksim\Desktop\CourseWork_WPF\CourseWork_WPF\City.png";
        private int globalMapInt = 0;
        string TextVertexCosts= string.Empty;
        string TextEdgesCostsEnd = string.Empty; 
        int VertexsCost = 0;
        string TextEdgesCosts= "------------------";
        int EdgesCost = 0;
        

        Dictionary<string, int> Vcosts = new Dictionary<string, int>();//стоимость оборудования для вершин
        Dictionary<string, int> ob = new Dictionary<string, int>();
        int kabellong = 0;
        int endCost = 0;
        Dictionary<string, GMapMarker> markers = new Dictionary<string, GMapMarker>();



        List<PointLatLng> points = new List<PointLatLng>();
        List<Point> MemoryPoint = new List<Point>();
        Graph graph = new Graph();
        private int number = 0;
        public MainWindow()
        {
            InitializeComponent();
            CreateInfoInComboBox();
            gMapControl1.MouseDown += gMapControl1_OnMarkerClick;
           
        }
        private void doCreateEdge(object sender, EventArgs e)
        {
            int throughput = -1;
            int delay = -1;
            if (comboBox1.SelectedValue != null && comboBox2.SelectedValue != null)
            {
                if (comboBox1.SelectedValue == comboBox2.SelectedValue)
                {
                    MessageBox.Show("Вы выбрали одну и ту же точку", "Ошибка", MessageBoxButton.OK);
                }
                if(obComboBox.SelectedItem==null)
                {
                    MessageBox.Show("Вы не выбрали оборудование", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                    try
                    {
                        throughput = ob[obComboBox.SelectedItem.ToString()];

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Вы не ввели дополнительные параметры", "Ошибка", MessageBoxButton.OK);
                        return;
                    }

                    List<PointLatLng> pointlatlang = new List<PointLatLng>();
                    pointlatlang.Add(new PointLatLng(markers[comboBox1.SelectedItem.ToString()].Position.Lat, markers[comboBox1.SelectedItem.ToString()].Position.Lng));
                    pointlatlang.Add(new PointLatLng(markers[comboBox2.SelectedItem.ToString()].Position.Lat, markers[comboBox2.SelectedItem.ToString()].Position.Lng));
                    GMapPolygon polygon = new GMapPolygon(pointlatlang);
                    gMapControl1.RegenerateShape(polygon);
                    (polygon.Shape as Path).Stroke = Brushes.Red;
                    (polygon.Shape as Path).StrokeThickness = 1.5;
                    (polygon.Shape as Path).Effect = null;
                    gMapControl1.Markers.Add(polygon);
                    Graph.MemoryPolygon.Add(new MPolygons(polygon, comboBox1.SelectedItem.ToString() + comboBox2.SelectedItem.ToString()));
                    GeoCoordinate geoLondon = new GeoCoordinate(markers[comboBox1.SelectedItem.ToString()].Position.Lat, markers[comboBox1.SelectedItem.ToString()].Position.Lng);
                    GeoCoordinate geoMoskow = new GeoCoordinate(markers[comboBox2.SelectedItem.ToString()].Position.Lat, markers[comboBox2.SelectedItem.ToString()].Position.Lng);
                    double distanceTo = geoLondon.GetDistanceTo(geoMoskow);
                    double weight = distanceTo / 1000;
                   
                    graph.AddEdge(new Edge(MemoryPoint.Find(x => x.Name == comboBox1.SelectedItem.ToString()), MemoryPoint.Find(x => x.Name == comboBox2.SelectedItem.ToString()), throughput,  (int)weight));
                    vertexBox1.Items.Add(MemoryPoint.Find(x => x.Name == comboBox1.SelectedItem.ToString()).Name+"-"+ MemoryPoint.Find(x => x.Name == comboBox2.SelectedItem.ToString()).Name);
                    graph.AddEdge(new Edge(MemoryPoint.Find(x => x.Name == comboBox2.SelectedItem.ToString()), MemoryPoint.Find(x => x.Name == comboBox1.SelectedItem.ToString()), throughput,  (int)weight));
                    points.Clear();
                    vertexBox1.Items.Add(MemoryPoint.Find(x => x.Name == comboBox2.SelectedItem.ToString()).Name + "-" + MemoryPoint.Find(x => x.Name == comboBox1.SelectedItem.ToString()).Name);
                    EdgesCost = 0;
                    VertexsCost = 0;
                    kabellong = 0;
                    endCost = 0;
                    foreach (var costs in Graph.Vertexes)
                    {
                        VertexsCost += Vcosts[costs.Model];
                    }
                    foreach (var edge in Graph.Edges)
                    {
                        EdgesCost += edge.EdgeWeight * ob[obComboBox.SelectedItem.ToString()];
                        kabellong += edge.EdgeWeight;
                        endCost = VertexsCost + EdgesCost;
                    }
                    TextVertexCosts = $"Количество вершин: {Graph.Vertexes.Count}\r\nОбщая стоимость: {VertexsCost} руб.\r\n";
                    TextEdgesCosts = $"Протяженность кабелей: {kabellong} км\r\nОбщая стоимость: {EdgesCost} руб.\r\n";
                    TextEdgesCostsEnd = $"Итоговая стоимость сети: {endCost} руб.\r\n";
                    CostBox.Text = TextVertexCosts + TextEdgesCosts + TextEdgesCostsEnd;
                }
            }
            else
            {
                MessageBox.Show("Точки для соединения не были выбраны", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
               MarkerImage = openFileDialog.FileName;
        }

        private void gMapControl1_OnMarkerClick(object sender, MouseEventArgs e)
        {

            double x = gMapControl1.FromLocalToLatLng((int)e.GetPosition(null).X - 290, (int)e.GetPosition(null).Y - 47 - 20).Lat;
            double y = gMapControl1.FromLocalToLatLng((int)e.GetPosition(null).X - 290, (int)e.GetPosition(null).Y - 47 - 20).Lng;
            foreach (var a in markers)
            {
                if ((Math.Round(a.Value.Position.Lat, 2) == Math.Round(x, 2)) && (Math.Round(a.Value.Position.Lng, 2) == Math.Round(y, 2)))
                {
                    if (e.RightButton == MouseButtonState.Pressed)
                    {

                        ContextMenu cm = this.FindResource("contextMenuStrip") as ContextMenu;

                        cm.IsOpen = true;
                        //contextMenuStrip1.Show(this, new System.Drawing.Point(e.X + 200, e.Y + 30));
                    }
                }
            } 

        }
        private void getInfoMarker(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            var result = MessageBox.Show(
                    frm2.DataName,
                    frm2.DataModel + " \n " + frm2.DataDescript + "\n",
                    MessageBoxButton.OK
                    );
        }


        private string CreateMenuEnters(string localX, string localY)
        {
            Form2 testDialog = new Form2();

            testDialog.SetLocalX = localX;
            testDialog.SetLocalY = localY;
            string txtResultName;
            string txtResultDiscript;
            string txtModel;
            // Показывает форму диалога для получения даннх
            if (testDialog.ShowDialog() == true)
            {
                txtResultName = string.Empty;
                txtResultDiscript = string.Empty;
            }
            else
            {
                txtResultName = testDialog.DataName;
                txtResultDiscript = testDialog.DataDescript;
                txtModel = testDialog.DataModel;
                if (txtResultDiscript == "")
                {
                    if (txtResultName != "")
                    {
                        if (Graph.Vertexes.Find(x => x.Name == txtResultName) != null)
                        {
                            MessageBox.Show("Вершина с таким именем уже существует!", "Уведомление", MessageBoxButton.OK);
                            txtResultName = string.Empty;
                        }
                        if(txtModel=="")
                        {
                            MessageBox.Show("Оборудование не выбрано!", "Уведомление", MessageBoxButton.OK);
                            txtResultName= string.Empty;
                        }
                        else
                        {
                            MemoryPoint.Add(new Point(number, testDialog.DataName,"",testDialog.DataModel, localX, localY));
                            number++;
                            graph.AddVert(MemoryPoint.Last());
                        }
                    }
                }
                else
                {
                    
                    if (Graph.Vertexes.Find(x => x.Name == txtResultName) != null)
                    {
                        MessageBox.Show("Вершина с таким именем уже существует!", "Уведомление", MessageBoxButton.OK);
                        txtResultName = string.Empty;
                    }
                    if (txtModel == "")
                    {
                        MessageBox.Show("Оборудование не выбрано!", "Уведомление", MessageBoxButton.OK);
                        txtResultName=string.Empty;
                    }
                    else
                    {
                        if (txtResultDiscript != "" && txtModel != "")
                        {
                            MemoryPoint.Add(new Point(number, testDialog.DataName, testDialog.DataDescript, testDialog.DataModel, localX, localY));
                            number++;
                            graph.AddVert(MemoryPoint.Last());
                        }
                    }
                }
            }

            return txtResultName;
        }
        private void gMapControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {


                double x = gMapControl1.FromLocalToLatLng((int)e.GetPosition(null).X - 290, (int)e.GetPosition(null).Y - 47 - 20).Lat;
                //получаем координаты
                double y = gMapControl1.FromLocalToLatLng((int)e.GetPosition(null).X - 290, (int)e.GetPosition(null).Y - 47 - 20).Lng;
                string localX = x.ToString();
                //Сохраняем координаты для использования
                string localY = y.ToString();

                string Result = CreateMenuEnters(localX, localY);

                if (Result != string.Empty)
                {
                    BitmapImage bm = new BitmapImage();
                    bm.BeginInit();
                    bm.UriSource = new Uri(MarkerImage);
                    bm.EndInit();


                    System.Windows.Controls.Image image = new Image();
                    image.Source = bm;

                    GMapMarker marker = new GMapMarker(new PointLatLng(x, y));

                    marker.Shape = new Image
                    {
                        Source = bm,
                        Width = 50,
                        Height = 50,
                        ToolTip = Result,


                    };
                    marker.Offset = new System.Windows.Point(-25, -25);
                    marker.ZIndex = 1;
                    gMapControl1.Markers.Add(marker);
                    markers.Add(Result, marker);
                    vertexBox.Items.Add(Result);
                    comboBox1.Items.Add(Result);
                    comboBox2.Items.Add(Result);
                    EdgesCost = 0;
                    VertexsCost = 0;
                    kabellong = 0;
                    endCost = 0;
                    foreach (var costs in Graph.Vertexes)
                    {
                        VertexsCost += Vcosts[costs.Model];
                    }
                    foreach (var edge in Graph.Edges)
                    {
                        EdgesCost += edge.EdgeWeight * ob[obComboBox.SelectedItem.ToString()];
                        kabellong += edge.EdgeWeight;
                    }
                    endCost = VertexsCost + EdgesCost;
                    TextVertexCosts = $"Количество вершин: {Graph.Vertexes.Count}\r\nОбщая стоимость: {VertexsCost} руб.\r\n";
                    TextEdgesCosts = $"Протяженность кабелей: {kabellong} км\r\nОбщая стоимость: {EdgesCost} руб.\r\n";
                    TextEdgesCostsEnd = $"Итоговая стоимость сети: {endCost} руб.\r\n";
                    CostBox.Text = TextVertexCosts + TextEdgesCosts + TextEdgesCostsEnd;
                }
                else
                {
                    MessageBox.Show("Создание вершины было отменено", "Уведомление", MessageBoxButton.OK);
                }


            }
        }
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerOnly;//ServerAndCache;
                                                        //выбор подгрузки карты – онлайн или из ресурсов

            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            //gMapControl1.MapProvider = GMapProviders.BingHybridMap;

            //какой провайдер карт используется (в нашем случае гугл) 
            gMapControl1.CacheLocation = @"C:\Users\Maksim\source\repos\CoursWork\CoursWork\bin\Debug\Cache";
            gMapControl1.MinZoom = 2;
            //минимальный зум

            gMapControl1.MaxZoom = 16;
            //максимальный зум

            gMapControl1.Zoom = 12;
            // какой используется зум при открытии

            gMapControl1.Position = new GMap.NET.PointLatLng(55.792318, 49.122549);
            // точка в центре карты при открытии (центр России)

            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // как приближает (просто в центр карты или по положению мыши)

            gMapControl1.CanDragMap = true;
            // перетаскивание карты мышью

            gMapControl1.DragButton = MouseButton.Left;
            // какой кнопкой осуществляется перетаскивание

            gMapControl1.ShowCenter = false;
            //показывать или скрывать красный крестик в центре

            gMapControl1.ShowTileGridLines = false;
            //показывать или скрывать тайлы


            //Контекстное меню


        }
        public static string a;

        private void goS(object sender, RoutedEventArgs e)
        {
            try
            {
                
                a = comboBox5.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
                if (a == "BFS")
                {

                    BFS bfs = new BFS();
                    bfs.Title = "BFS";
                    bfs.ShowDialog();
                }
                if (a == "DFS")
                {

                    BFS bfs = new BFS();
                    bfs.Title = "DFS";
                    bfs.ShowDialog();
                }

                if (a == "Краскала")
                {

                    BFS bfs = new BFS();
                    bfs.Title = "Краскала";
                    bfs.ShowDialog();
                }
                if (a == "Прима")
                {

                    BFS bfs = new BFS();
                    bfs.Title = "Прима";
                    bfs.ShowDialog();
                }
                if (a == "Дейкстры")
                {

                    BFS bfs = new BFS();
                    bfs.Title = "Дейкстры";
                    bfs.ShowDialog();
                }
            }
            catch { }
        }
        private void DeletePoint(object sender, EventArgs e)
        {
            try
            {
                GMapMarker marker = markers[vertexBox.SelectedItem.ToString()];
                List<Edge> edges = Graph.Edges;
                List<Point> points = Graph.Vertexes;
                MemoryPoint.Remove(MemoryPoint.Find(x => x.Name == vertexBox.SelectedItem.ToString()));

                foreach (var edge in edges)
                {
                    if ((edge.To.Name == vertexBox.SelectedItem.ToString()) || (edge.From.Name == vertexBox.SelectedItem.ToString()))
                    {
                        Graph.Edges.Remove(edge);
                        
                    }
                }
                graph.DeleteVert(MemoryPoint.Find(x => x.Name == vertexBox.SelectedItem.ToString()));
              
                foreach (var d in Graph.MemoryPolygon)
                {
                    if ((d.Names.Contains(vertexBox.SelectedItem.ToString())))
                    {
                                
                        gMapControl1.Markers.Remove(d.polygon);
                        continue;
                    }


                }
                markers.Remove(vertexBox.SelectedItem.ToString());
                vertexBox1.Items.Clear();
                gMapControl1.Markers.Remove(marker);
                Graph.Vertexes.Remove(Graph.Vertexes.Find(x=>x.Name== vertexBox.SelectedItem.ToString()));
                comboBox1.Items.Remove(vertexBox.SelectedItem.ToString());
                comboBox2.Items.Remove(vertexBox.SelectedItem.ToString());
                vertexBox.Items.Remove(vertexBox.SelectedItem);
                
                EdgesCost = 0;
                VertexsCost = 0;
                kabellong = 0;
                endCost = 0;    
                foreach (var costs in Graph.Vertexes)
                {
                    VertexsCost += Vcosts[costs.Model];
                }
                foreach (var edge in Graph.Edges)
                {
                    vertexBox1.Items.Add(edge.From.Name+"-"+edge.To.Name);
                    EdgesCost += edge.EdgeWeight * ob[obComboBox.SelectedItem.ToString()];
                    kabellong += edge.EdgeWeight;
                }
                endCost = VertexsCost + EdgesCost;
                TextVertexCosts = $"Количество вершин: {Graph.Vertexes.Count}\r\nОбщая стоимость: {VertexsCost} руб.\r\n";
                TextEdgesCosts = $"Протяженность кабелей: {kabellong} км\r\nОбщая стоимость: {EdgesCost} руб.\r\n";
                TextEdgesCostsEnd = $"Итоговая стоимость сети: {endCost} руб.\r\n";
                CostBox.Text = TextVertexCosts + TextEdgesCosts + TextEdgesCostsEnd;
            }



            catch
            {
                MessageBox.Show("Выберите вершину!", "Уведомление", MessageBoxButton.OK);
            }
        }
        private void DeleteEdge(object sender, EventArgs e)
        {
            try
            {

     

                List<Edge> edges = Graph.Edges;
                var removeedge = vertexBox1.SelectedItem.ToString().Split('-');

                foreach (var edge in edges)
                {
                    if ((edge.To.Name == removeedge[1]) && (edge.From.Name == removeedge[0]))
                    {
                        Graph.Edges.Remove(edge);

                    }
                    if ((edge.From.Name == removeedge[0]) && (edge.To.Name == removeedge[1]))
                    {
                        Graph.Edges.Remove(edge);

                    }
                }
                foreach (var d in Graph.MemoryPolygon)
                {
                    if ((d.Names.Contains(removeedge[0])) && (d.Names.Contains(removeedge[1])))
                    {

                        gMapControl1.Markers.Remove(d.polygon);
                        continue;
                    }


                }

                vertexBox1.Items.Clear();

                EdgesCost = 0;
                VertexsCost = 0;
                kabellong = 0;
                endCost = 0;
                foreach (var costs in Graph.Vertexes)
                {
                    VertexsCost += Vcosts[costs.Model];
                }
                foreach (var edge in Graph.Edges)
                {
                    vertexBox1.Items.Add(edge.From.Name + "-" + edge.To.Name);
                    EdgesCost += edge.EdgeWeight * ob[obComboBox.SelectedItem.ToString()];
                    kabellong += edge.EdgeWeight;
                }
                endCost = VertexsCost + EdgesCost;
                TextVertexCosts = $"Количество вершин: {Graph.Vertexes.Count}\r\nОбщая стоимость: {VertexsCost} руб.\r\n";
                TextEdgesCosts = $"Протяженность кабелей: {kabellong} км\r\nОбщая стоимость: {EdgesCost} руб.\r\n";
                TextEdgesCostsEnd = $"Итоговая стоимость сети: {endCost} руб.\r\n";
                CostBox.Text = TextVertexCosts + TextEdgesCosts + TextEdgesCostsEnd;
            }



            catch
            {
                MessageBox.Show("Выберите связь!", "Уведомление", MessageBoxButton.OK);
            }
        }
        private void GetInfo(object sender, EventArgs e)
        {
            try
            {
                string name = vertexBox.SelectedItem.ToString();
                string Description = Graph.Vertexes.Find(x => x.Name == vertexBox.SelectedItem.ToString()).Discription;
                MessageBox.Show($"Название: {name}\r\nОписание: {Description}", "Информация", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Выберите вершину!", "Уведомление", MessageBoxButton.OK);
            }
        }
        private void картыBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalMapInt != 3)
            {
                globalMapInt = 3;
                gMapControl1.MapProvider = BingMapProvider.Instance;
            }
            else
            {
                MessageBox.Show("Карта уже используется", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void картыWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalMapInt != 5)
            {
                globalMapInt = 5;
                gMapControl1.MapProvider = WikiMapiaMapProvider.Instance;
            }
            else
            {
                MessageBox.Show("Карта уже используется", "Ошибка", MessageBoxButton.OK);
            }

        }
        private void картаArcGISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalMapInt != 2)
            {
                globalMapInt = 2;
                gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Topo_Map; //Рисованая
            }
            else
            {
                MessageBox.Show("Карта уже используется", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void картаGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalMapInt != 1)
            {
                globalMapInt = 1;
                gMapControl1.MapProvider = GoogleMapProvider.Instance;
            }
            else
            {
                MessageBox.Show("Карта уже используется", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void картыBindextraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalMapInt != 4)
            {
                globalMapInt = 4;
                gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            }
            else
            {
                MessageBox.Show("Карта уже используется", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void скачатьКартуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RectLatLng area = gMapControl1.SelectedArea;
            if (area.IsEmpty)
            {
                MessageBoxResult res = MessageBox.Show("Нет области для скачивания", "Ошибка", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                    area = gMapControl1.ViewArea;
            }

            if (!area.IsEmpty)
            {
                MessageBoxResult res = MessageBox.Show("Ready ripp at Zoom = " + (int)gMapControl1.Zoom + " ?", "GMap.NET", MessageBoxButton.YesNo);

                for (int i = 1; i <= gMapControl1.MaxZoom; i++)
                {
                    if (res == MessageBoxResult.Yes)
                    {
                        TilePrefetcher obj = new TilePrefetcher();
                        obj.ShowCompleteMessage = false;
                        obj.Start(area, i, gMapControl1.MapProvider, 100);

                    }
                    else if (res == MessageBoxResult.No)
                        continue;
                    else if (res == MessageBoxResult.Cancel)
                        break;
                }
            }
            else
                MessageBox.Show("Выбери площадь на карте с зажатой ATL", "GMap.NET", MessageBoxButton.OK );
        }
        private void созданиеИзображенияToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialogNewImag = new SaveFileDialog
                {
                    Filter = "PNG (*.png)|*.png",
                    FileName = "Фото моей карты"
                };   
                if (dialogNewImag.ShowDialog() == true)
                {
                    ImageSource img = gMapControl1.ToImageSource();

                    if (img != null)
                    {
                        Image image = new Image
                        {
                            Source = img
                        };
                        Content = image;
                        string fileName = dialogNewImag.FileName;
                        if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                        {
                            fileName += ".png";
                        }
                        SaveToPng(image,fileName);
                        MessageBox.Show("Карта успешно сохранена в папку: " + Environment.NewLine + dialogNewImag.FileName, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ощибка при сохранении карты: " + Environment.NewLine + ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
        void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }
        void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }
        void CreateInfoInComboBox()
        {
            //Сделать добовление оборудование которое будет храниться в БД
            try
            {
                connectBD con = new connectBD();
                string sqlQuery = $"select Name,Model,Price from Router";
                string sqlQuery1 = $"select Name,Price from Cables";
                using (SqlCommand command = new SqlCommand(sqlQuery, con.GetConnections()))
                {
                    using (SqlCommand command1 = new SqlCommand(sqlQuery1, con.GetConnections()))
                    {
                        con.OpenConnections();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                object Name = reader.GetValue(0);
                                object Model = reader.GetValue(1);
                                object Price = reader.GetValue(2);
                                Vcosts.Add(Name + " " + Model, (int)Price);
                            }
                            reader.Close();
                        }
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                object Name = reader1.GetValue(0);
                                object Price = reader1.GetValue(1);
                                ob.Add(Name.ToString(), (int)Price);
                            }
                            reader1.Close();

                        }
                        foreach (var o in ob)
                        {
                            obComboBox.Items.Add(o.Key);
                        }
                    }
                }
                con.CloseConnections();
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к базе данных", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void CreateNewProject(object sender, RoutedEventArgs e)
        {
            try
            {
                EnterName dialog = new EnterName();
                string txtResultName;
                // Показывает форму диалога для получения даннх
                if (MemoryPoint.Count == 0)
                {
                    MessageBox.Show("В сети отсутсвуют вершины", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                    if (dialog.ShowDialog() == true)
                    {
                        txtResultName = string.Empty;
                    }
                    else
                    {
                        txtResultName = dialog.DataName;
                        if (txtResultName != "")
                        {
                            connectBD con = new connectBD(); ;
                            con.OpenConnections();
                            SqlCommand command = new SqlCommand($"SELECT * FROM Graphs WHERE EXISTS(SELECT * FROM Graphs WHERE PointID = N'{txtResultName}')", con.GetConnections());
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                MessageBox.Show("Сеть с таким названием уже существует", "Ошибка", MessageBoxButton.OK);
                                reader.Close();
                            }
                            else
                            {
                                int counter = 0;
                                int counterTo = 0;
                                reader.Close();
                                foreach (var item in MemoryPoint)
                                {
                                    
                                    SqlCommand cmd = new SqlCommand($"INSERT INTO [Graphs] (PointID,Name, Discription,Model,CoordinatX,CoordinatY) VALUES (N'{txtResultName}',N'{MemoryPoint[counter].Name}',N'{MemoryPoint[counter].Discription}',N'{MemoryPoint[counter].Model}',N'{MemoryPoint[counter].X}',N'{MemoryPoint[counter].Y}')", con.GetConnections());
                                    cmd.ExecuteNonQuery();
                                    counter++;
                                }
                                foreach (var item in Graph.Edges)
                                {
                                    SqlCommand cmd = new SqlCommand($"INSERT INTO [Edge] (PointID,MyFrom,MyTo,Throughpu, EdgeWeight) VALUES (N'{txtResultName}',N'{Graph.Edges[counterTo].From}',N'{Graph.Edges[counterTo].To}',N'{Graph.Edges[counterTo].Throughput}',N'{Graph.Edges[counterTo].EdgeWeight}')", con.GetConnections());
                                    cmd.ExecuteNonQuery();
                                    counterTo++;
                                }

                                MessageBox.Show("Сеть сохранена успешно", "Ошибка", MessageBoxButton.OK);
                                con.CloseConnections();
                            }

                        }
                        else
                        { MessageBox.Show("Название сети не введено", "Ошибка", MessageBoxButton.OK); }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к базе данных", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
