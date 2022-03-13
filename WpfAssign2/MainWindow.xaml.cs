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
        public static List<Moon> moons = new List<Moon>();
        private double xOri;
        private double yOri;
        int time = 0;
        public event Action<int> MoveIt;
        SpaceObject Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto;
        public MainWindow()
        {
            InitializeComponent();
            solarSystem = makeObjects();
            makeMoons();

            Loaded += delegate
            {
                xOri = WinScr.Width/2;
                yOri = WinScr.Height/2;
                createSolarS();
            };

        }
        
        private void T_Tick(Object sender, EventArgs e)
        {
            MoveIt(time++);
        }
        private List<SpaceObject> makeObjects()
        {
            List<SpaceObject> solarSystem = new List<SpaceObject>();
            Sun = new Star("Sun", 0, 0.0, 696.34, 27.0, "Yellow");
            Mercury = new Planet("Mercury", 57, 87.97, 2.439, 58.65, "LightGray");
            Venus = new Planet("Venus", 108, 224.7, 6.052, 118.75, "LightGoldenrodYellow");
            Earth = new Planet("Earth", 149, 365.26, 6.371, 23.93, "Blue");
            Mars = new Planet("Mars", 227, 686.98, 3.389, 1.0, "Red");
            Jupiter = new Planet("Jupiter", 778, 4332.71, 69.911, 0.414, "Beige");
            Saturn = new Planet("Saturn", 1429, 10759.5, 58.232, 0.445, "Yellow");
            Uranus = new Planet("Uranus", 2870, 30685.0, 25.362, 0.718, "LightBlue");
            Neptune = new Planet("Neptune", 4504, 60190.0, 24.622, 0.67, "Azure");
            Pluto = new DwarfPlanet("Pluto", 5913, 90550.0, 1.188, 6.39, "White");
            
            solarSystem.AddRange(new List<SpaceObject>
        {
            Sun, Earth, Mercury, Mars, Venus, Jupiter, Saturn, Uranus, Neptune, Pluto,
            //new Asteroid("Asteroid belt", 478713, 1095.0, 308171, 0.0, "Gray")
        });
            return solarSystem;
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
                Width = 30*Math.Log(solarSystem[i].objectRadius),
                Height = 30*Math.Log(solarSystem[i].objectRadius)
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
            foreach (SpaceObject o in solarSystem)
            {
                if (o.name.Equals("Sun"))
                {
                    x = (calcPos(o, time).Item1) + (PCanvas.ActualWidth - (10*Math.Log(o.objectRadius)))-400;
                    y = (calcPos(o, time).Item2) + (PCanvas.ActualHeight - (10*Math.Log(o.objectRadius)));


                }
                else
                {
                    x = (calcPos(o, time).Item1) + (PCanvas.ActualWidth - (10*Math.Log(o.objectRadius)))-450;
                    y = (calcPos(o, time).Item2) + (PCanvas.ActualHeight - (10*Math.Log(o.objectRadius)));
                }

                Color col = (Color)ColorConverter.ConvertFromString(o.color);
                SolidColorBrush scb = new SolidColorBrush(col);
                
                Ellipse ellip = new Ellipse()
                {


                    Name = o.name,
                    Fill = scb,
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    Width = 10*Math.Log(o.objectRadius),
                    Height = 10*Math.Log(o.objectRadius)
                };

                Canvas.SetLeft(ellip, x);
                Canvas.SetTop(ellip, y);

                PCanvas.Children.Add(ellip);
            }
        }

        public static String findParentPlanet(SpaceObject plan)
        {
            foreach (Moon moon in moons)
            {
                if (moon.parentPlanet.name == plan.name)
                {
                    return moon.ToString();
                }
            }
            return "No parent";
        }
    }
}
