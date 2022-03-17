using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfAssign2
{
    public class SpaceObject
    {

        public String name { get; set; }
        public int orbitalRadius;
        public double orbitalPeriod { get; set; }
        public double objectRadius;
        protected double rotationalPeriod;
        public string color { get; set; }

        public SpaceObject(String name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string color)
        {
            this.name = name;
            this.orbitalRadius = orbitalRadius;
            this.orbitalPeriod = orbitalPeriod;
            this.objectRadius = objectRadius;
            this.rotationalPeriod = rotationalPeriod;
            this.color = color;

        }
        public virtual void Draw()
        {
            Console.WriteLine(name);
            Console.WriteLine("Orbital radius: " + orbitalRadius);
            Console.WriteLine("Orbital peroid: " + orbitalPeriod);
            Console.WriteLine("Object radius: " + objectRadius);
            Console.WriteLine("Rotational peroid: " + rotationalPeriod);
            Console.WriteLine("Color: " + color);
        }



        public Tuple<double, double> calcPosition(double time)
        {
            // Use orbital radius and orbital period to calc pos relative to the planet's pos
            double x;
            double y;
            double ts;
            if (orbitalRadius == 0)
            {
                x = 1; y = 1;
                ts = 0.0;
            }
            else
            {
                double torb = time / orbitalPeriod;
                double rad = 2 * Math.PI * torb;
                ts = torb * 360;
                // 2pi*(time/peroid) = radians
                // x = radius*cos(t)
                // x = radius * sin(Pi*2*angel/360) 
                x = orbitalRadius * Math.Cos(rad);
                y = orbitalRadius * Math.Sin(rad);
            }
            //double degs = Math.Atan2(y, x);
            //Console.WriteLine("Degrees: " + String.Format("{0:0.00}°", ts));
            return Tuple.Create(x, y);

        }
    }

    public class Star : SpaceObject
    {
        public Star(String name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }
        public override void Draw()
        {
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(String name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }
    }

    public class Moon : SpaceObject
    {
        public SpaceObject parentPlanet { get; set; }
        public Moon(String name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color, SpaceObject parentPlanet)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { this.parentPlanet = parentPlanet; }

        public override void Draw()
        {
            Console.Write("Moon  : ");
            base.Draw();
            Console.WriteLine("Orbits: " + parentPlanet.name);
        }

        public override string ToString()
        {
            return ("Name: " + name + " / Orbit radius: " + orbitalRadius + " / Orbital period: " + orbitalPeriod
                + " / Object radius: " + objectRadius + " / Rot.peroid: " + rotationalPeriod + " / Color: " + color);
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(string name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }

        public override void Draw()
        {
            Console.Write("Comet  : ");
            base.Draw();
        }
    }

    public class Asteroid : SpaceObject
    {
        public Asteroid(string name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }

        public override void Draw()
        {
            Console.Write("Asteroid  : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(string name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }

        public override void Draw()
        {
            Console.Write("Asteroidbelt  : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name, int orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String color)
            : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, color) { }

        public override void Draw()
        {
            Console.Write("Dwarf planet  : ");
            base.Draw();
        }
    }
}
