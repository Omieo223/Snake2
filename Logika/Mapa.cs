using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class Mapa : IKoordynaty
    {
        public Mapa(int wysokoscMapy, int szerokoscMapy)
        {
            graniceMapy  = new GraniceMapy(wysokoscMapy, szerokoscMapy);
            this.wysokoscMapy = wysokoscMapy; 
            this.szerokoscMapy = szerokoscMapy;
        }

        public int wysokoscMapy;
        public int szerokoscMapy;

        readonly GraniceMapy graniceMapy;

        public int maxX => szerokoscMapy;

        public int maxY => wysokoscMapy;
    }
    public class GraniceMapy
    {
        Point rogLewyG = Point.Empty;
        Point rogPrawyG = Point.Empty;
        Point rogLewyD = Point.Empty;
        Point rogPrawyD = Point.Empty;

        public GraniceMapy(int wysokoscMapy, int szerokoscMapy)
        {
            rogPrawyG = new Point( szerokoscMapy, 0);
            rogPrawyD = new Point(szerokoscMapy, wysokoscMapy);
            rogLewyD = new Point( 0, wysokoscMapy);

        }
    }

}
