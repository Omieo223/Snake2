using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public static class Lodowka
    {
        static Random rand = new Random();


        public static Point Generowanieposilku(int maxX, int maxY)
        {
            
           var X = rand.Next(maxX);
           var Y = rand.Next(maxY);

            return new Point(X,Y);
        }
        public static Point Generowanieposilku(IKoordynaty kokordynaty) // przeciążenie
        {
            return Generowanieposilku(kokordynaty.maxX, kokordynaty.maxY);
        }
    }
}
