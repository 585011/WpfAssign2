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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAssign2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<SpaceObject> solarSystem;
        public static List<SpaceObject> displaySystem;
        public static List<Moon> moons = new List<Moon>();
        private double xOri;
        private double yOri;
        int time = 0;
        public event Action<int> MoveIt;
        SpaceObject Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto;
        private System.Windows.Threading.DispatcherTimer t;
        public MainWindow()
        {
            InitializeComponent();
            solarSystem = makeObjects();
            displaySystem = displayObjects();
            makeMoons();

            Loaded += delegate
            {
                xOri = WinScr.Width/2;
                yOri = WinScr.Height/2;
                lableDisplayer();
                makeSpeedButton();
                createSolarS();

                t = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = new TimeSpan(200000)
                };
                t.Tick += T_Tick;
                t.Start();
            };


        }
        
        private void T_Tick(Object sender, EventArgs e)
        {
            clearSolarSystem();
            time += 1;
            createSolarS();
            lableDisplayer();
            makeSpeedButton();
            
        }

        private void clearSolarSystem()
        {
            for (int i = PCanvas.Children.Count - 1; i >= 0; i--)
            {
                UIElement spaceobj = PCanvas.Children[i];
                if(spaceobj is Ellipse)
                {
                    PCanvas.Children.Remove(spaceobj);
                }
            }
        }



        

        private void speedUp(object sender, RoutedEventArgs e)
        {
            time += 1;
        }
        private void speedDown(object sender, RoutedEventArgs e)
        {
            time += -1;
        }

        private void makeSpeedButton()
        {
            Button b1 = new Button()
            {
                Content = "Speed Up!",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            Button b2 = new Button()
            {
                Content = "Slow down!",
                VerticalAlignment= VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };


            b1.Click += new RoutedEventHandler(speedUp);
            PCanvas.Children.Add(b1);
            b2.Click += new RoutedEventHandler(speedDown);
            PCanvas.Children.Add(b2);

        }
        private List<SpaceObject> makeObjects()
        {
            List<SpaceObject> solarSystem = new List<SpaceObject>();
            Sun = new Star("Sun", 0, 0.0, 69634.0, 27.0, "Yellow");
            Mercury = new Planet("Mercury", 57910, 87.97, 2439.0, 58.65, "LightGray");
            Venus = new Planet("Venus", 108200, 224.7, 6052.0, 118.75, "LightGoldenrodYellow");
            Earth = new Planet("Earth", 149600, 365.26, 6371.0, 23.93, "Blue");
            Mars = new Planet("Mars", 227940, 686.98, 3389.0, 1.0, "Red");
            Jupiter = new Planet("Jupiter", 778330, 4332.71, 69911.0, 0.414, "Beige");
            Saturn = new Planet("Saturn", 1429400, 10759.5, 58232.0, 0.445, "Yellow");
            Uranus = new Planet("Uranus", 28700990, 30685.0, 25362.0, 0.718, "LightBlue");
            Neptune = new Planet("Neptune", 4504300, 60190.0, 24622.0, 0.67, "Azure");
            Pluto = new DwarfPlanet("Pluto", 5913520, 90550.0, 1188.0, 6.39, "White");
            
            solarSystem.AddRange(new List<SpaceObject>
        {
            Sun, Earth, Mercury, Mars, Venus, Jupiter, Saturn, Uranus, Neptune, Pluto,
            //new Asteroid("Asteroid belt", 478713, 1095.0, 308171, 0.0, "Gray")
        });
            return solarSystem;
        }
        private List<SpaceObject> displayObjects()
        {
            List<SpaceObject> displaySystem = new List<SpaceObject>();
            Sun = new Star("Sun", 0, 0.0, 69634.0, 27.0, "Yellow");
            Mercury = new Planet("Mercury", 100, 87.97, 2439.0, 58.65, "LightGray");
            Venus = new Planet("Venus", 130, 224.7, 6052.0, 118.75, "LightGoldenrodYellow");
            Earth = new Planet("Earth", 160, 365.26, 6371.0, 23.93, "Blue");
            Mars = new Planet("Mars", 190, 686.98, 3389.0, 1.0, "Red");
            Jupiter = new Planet("Jupiter", 300, 4332.71, 69911.0, 0.414, "Beige");
            Saturn = new Planet("Saturn", 350, 10759.5, 58232.0, 0.445, "Yellow");
            Uranus = new Planet("Uranus", 390, 30685.0, 25362.0, 0.718, "LightBlue");
            Neptune = new Planet("Neptune", 440, 60190.0, 24622.0, 0.67, "Azure");
            Pluto = new DwarfPlanet("Pluto", 470, 90550.0, 1188.0, 6.39, "White");

            displaySystem.AddRange(new List<SpaceObject>
        {
            Sun, Earth, Mercury, Mars, Venus, Jupiter, Saturn, Uranus, Neptune, Pluto,
            //new Asteroid("Asteroid belt", 478713, 1095.0, 308171, 0.0, "Gray")
        });
            return displaySystem;
        }

        private void makeMoons()
        {
            moons.AddRange(new List<Moon>
            {
            new Moon("The Moon", 384, 27.32, 1.737, 27.3,"White", Earth),//Earth
            new Moon("Ganymede", 1070, 7.15, 2.634, 7.15, "White", Jupiter),//Jupiter
            new Moon("Titan", 1222, 15.95, 2.574, 15.92, "Yellow", Saturn),//Saturn
            new Moon("Triton", 355, -5.88, 1.353, 5.87, "Red", Neptune),//Neptune
            new Moon("Titania", 436, 26146.0, 0.789, 8.7, "White", Uranus),//Uranus
            new Moon("Phobos", 9, 0.32, 0.011, 0.32, "White", Mars),//Mars
            new Moon("Nix", 49, 24.86, 0.019, 44.0, "White", Pluto),//pluto
            });
        }

    

        public Ellipse drawObjects(Tuple<double,double> coord, int i)
        {
            double x = 0;
            double y = 0;
            Canvas c = new Canvas();

            Color col = (Color)ColorConverter.ConvertFromString(solarSystem[i].color);
            SolidColorBrush scb = new SolidColorBrush(col);

            Ellipse ellip = new Ellipse()
            {

                Name = solarSystem[i].name,
                Fill = scb,
                Width = 30 * Math.Log(solarSystem[i].objectRadius),
                Height = 30 * Math.Log(solarSystem[i].objectRadius)

            };
            // To scale, use 30*log(objectRadius)
            x = (10*Math.Log(coord.Item1)) + xOri - ellip.ActualWidth;
            y = (10*Math.Log(coord.Item2)) + yOri - ellip.ActualHeight;
            Canvas.SetLeft(ellip, x);
            Canvas.SetTop(ellip, y);
            

            return ellip;

        }

        //public Ellipse drawOrbit (Tuple<double,double> coord, int i)
        //{
        //    double x = 0;
        //    double y = 0;
        //    Ellipse elOrb = new Ellipse()
        //    {
        //        Width = 30 * Math.Log(solarSystem[i].objectRadius),
        //        Height = 30 * Math.Log(solarSystem[i].objectRadius),

        //    };
        //}

        //public void moveObjects()
        //{
        //    for(int i = 0; i < solarSystem.Count; i++)
        //    {
        //        Ellipse ellip;
        //    }
        //}

        private Label makeLabels(SpaceObject o)
        {
            Label label = new Label()
            {
                Content = o.name,
                Foreground = Brushes.Black
            };
            return label;
            
        }
    
        public Tuple<double,double> calcPos(SpaceObject obj, int time)
        {
            return obj.calcPosition(time);
        }
        public static SpaceObject findParentObj(String name, List<SpaceObject> list)
        {
            SpaceObject parent = null;
            foreach (SpaceObject o in list)
            {
                if (o.name.Equals(name))
                {
                    parent = o;
                }
            }
            return parent;
        }

        public void createSolarS()
        {
            double x;
            double y;

            foreach (SpaceObject o in displaySystem)
            {

                if (o.name.Equals("Sun"))
                {
                    x = (calcPos(o, time).Item1) + (PCanvas.ActualWidth - (10 * Math.Log(o.objectRadius))) - 400;
                    y = (calcPos(o, time).Item2) + (PCanvas.ActualHeight - (10 * Math.Log(o.objectRadius)))- 250;

                }
                else
                {
                    x = (calcPos(o, time).Item1) + (PCanvas.ActualWidth - (10 * Math.Log(o.objectRadius))) - 400;
                    y = (calcPos(o, time).Item2) + (PCanvas.ActualHeight - (10 * Math.Log(o.objectRadius))) - 250;
                }
                //x = calcPos(o, 0).Item1 + ((PCanvas.ActualWidth - ((o.objectRadius) / 500)) / 2);
                //y = calcPos(o, 0).Item2 + ((PCanvas.ActualHeight - ((o.objectRadius) / 500)) / 2);
                //x = Scale(o.objectRadius, 69911.0, 35.0);
                //y = Scale(o.objectRadius, 69911.0, 35.0);
                Color col = (Color)ColorConverter.ConvertFromString(o.color);
                SolidColorBrush scb = new SolidColorBrush(col);

                Ellipse ellip = new Ellipse()
                {


                    Name = o.name,
                    Fill = scb,
                    StrokeThickness = 1,
                    Stroke = Brushes.Black


                    //Width = Math.Log(o.objectRadius),
                    //Height = Math.Log(o.objectRadius)
                };

                //ellip.MouseLeftButtonDown += LButtonDown;

                if (ellip.Name.Contains("Sun"))
                {
                    ellip.Width = 8*Math.Log(o.objectRadius);
                    ellip.Height = ellip.Width;
                } else
                {
                    ellip.Width = 2*Math.Log(o.objectRadius);
                    ellip.Height = ellip.Width;
                }
                //ellip.Width = o.objectRadius / 500;
                //ellip.Height = o.objectRadius / 500;
                //if (ellip.Name.Contains("Sun"))
                //{
                //    ellip.Width = 3 * Scale(o.objectRadius, 69911.0, 35.0);
                //    ellip.Height = ellip.Width;
                //}
                //else
                //{

                //    ellip.Width = Scale(o.objectRadius, 69911.0, 35.0);
                //    ellip.Height = Scale(o.objectRadius, 69911.0, 35.0);
                //}
                //x = xOri + calcPos(o, time).Item1 - ellip.Width * 0.5;
                //y = yOri + calcPos(o, time).Item2 - ellip.Height * 0.5;
                
              

                Canvas.SetLeft(ellip, x);
                Canvas.SetTop(ellip, y);

                Label label = makeLabels(o);
                Canvas.SetLeft(label, x+10);
                Canvas.SetTop(label, y+10);

                PCanvas.Children.Add(ellip);
                ObjLabels.Children.Add(label);
            }
        }

        private void LButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpaceObject pl = null;
            Ellipse ellip = (Ellipse)sender;
            foreach(SpaceObject o in displaySystem)
            {
                if (o.name.Equals(ellip.Name))
                {
                    pl = o;
                }
            }
            MessageBox.Show(findParentPlanet(pl));
        }

        private void lableDisplayer()
        {
            Button b = new Button
            {
                Content = "Show/Hide labels",
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            
            b.Click += new RoutedEventHandler(showLabels);
            PCanvas.Children.Add(b);
        }

        private void showLabels(object sender, RoutedEventArgs e)
        {
            if(ObjLabels.Visibility == Visibility.Visible)
            {
                ObjLabels.Visibility = Visibility.Collapsed;
            } 
            
            else
            {
                ObjLabels.Visibility = Visibility.Visible;
            }
        }

        public static String findParentPlanet(SpaceObject plan)
        {
            foreach (Moon moon in moons)
            {
                if (moon.parentPlanet.name.Equals(plan.name))
                {
                    return moon.ToString();
                }
            }
            return "Moons displayed";
        }

    }
}
