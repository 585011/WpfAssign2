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
                makeMenuBar();
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
            clearLabels();
            time += 1;
            createSolarS();
            lableDisplayer();
            //makeSpeedButton();
            makeMenuBar();
            
        }


        // Method that clears the space objects from the canvas (used to make sure it doesnt draw the objs in "lines")
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

        // Method that clears the labels from the canvas (so it doesnt make "circles" of labels in the canvas)
        private void clearLabels()
        {
            for(int i = ObjLabels.Children.Count-1; i >= 0; i--)
            {
                UIElement labelobj = ObjLabels.Children[i];
                if(labelobj is Label)
                {
                    ObjLabels.Children.Remove(labelobj);
                }
            }
        }


        

        private void speedUp(object sender, RoutedEventArgs e)
        {
            t.Interval = t.Interval.Subtract(new TimeSpan(5000));
        }
        private void speedDown(object sender, RoutedEventArgs e)
        {
            t.Interval = t.Interval.Add(new TimeSpan(5000));
        }


        // Method for creating the speed up/down buttons in the canvas and adding events when clicking them.
        private void makeSpeedButton()
        {
            Button b1 = new Button()
            {
                Content = "Speed Up!"
            };
            Canvas.SetLeft(b1, 0);
            Canvas.SetTop(b1, 30);

            Button b2 = new Button()
            {
                Content = "Slow down!"
            };
            Canvas.SetLeft(b2, 0);
            Canvas.SetTop(b2, 50);
            


            b1.Click += new RoutedEventHandler(speedUp);
            PCanvas.Children.Add(b1);
            b2.Click += new RoutedEventHandler(speedDown);
            PCanvas.Children.Add(b2);

        }

        // Method for creating the list with actual values of the solar system.
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
            Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto,
            //new Asteroid("Asteroid belt", 478713, 1095.0, 308171, 0.0, "Gray")
        });
            return solarSystem;
        }

        // Method for creating the list of the objects to be displayed
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
            Pluto = new DwarfPlanet("Pluto", 450, 90550.0, 1188.0, 6.39, "White");

            displaySystem.AddRange(new List<SpaceObject>
        {
            Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto,
            //new Asteroid("Asteroid belt", 478713, 1095.0, 308171, 0.0, "Gray")
        });
            return displaySystem;
        }


        // Method for creating the moon list
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

   
        // Method for creating labels for a space object
        private Label makeLabels(SpaceObject o)
        {
            Label label = new Label()
            {
                Content = o.name,
                Foreground = Brushes.Black
            };
            return label;
            
        }
    
        // Calculates the position of a space object and returns a tuple with x and y coords.
        public Tuple<double,double> calcPos(SpaceObject obj, int time)
        {
            return obj.calcPosition(time);
        }


        // Method for drawing the objects in the canvas.
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


        // Method that creates a menu item from a space object and adds event when menu item is clicked
        private void makeMenuBar()
        {
            foreach (SpaceObject o in solarSystem)
            {
                MenuItem m = new MenuItem()
                {
                    Name = o.name
                };
                m.Header = o.name;

                Meny.Items.Add(m);
                m.Click += new RoutedEventHandler(ShowInfo);
            }
        }

        // Method to display when menu item is clicked
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            MenuItem m = (MenuItem)sender;
            foreach (SpaceObject o in solarSystem)
            {
                if (o.name.Equals(m.Name))
                {
                  MessageBox.Show(findParentPlanet(o));
                }
            }
        }

        // Method for adding labels to the canvas
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


        // Method that uses routed event and show/hides the labels in the canvas
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


        // Method that returns a string form of the info about a moon, with a space object/parent as input
        public static String findParentPlanet(SpaceObject plan)
        {
            foreach (Moon moon in moons)
            {
                if (moon.parentPlanet.name.Equals(plan.name))
                {
                    return moon.ToString();
                } if (plan.name.Equals("Sun"))
                {
                    return plan.ToString();
                }
            }
            return "No moons added to display";
        }

    }
}
