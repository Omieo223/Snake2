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
        //---------------------------------------------------------------------
        #region Deklaracja zmiennych

        public int wysokoscMapy;
        public int szerokoscMapy;

        public readonly RzeczywistyRozmiarMapy RozmiarMapy;
        public readonly GraniceMapy? GraniceMapy; //  ? - moze byc nullem 


        #endregion

        //---------------------------------------------------------------------


        #region Konstruktory
        public Mapa(int wysokoscMapy, int szerokoscMapy)
        {
            RozmiarMapy = new RzeczywistyRozmiarMapy(wysokoscMapy, szerokoscMapy);
            GraniceMapy = new GraniceMapy(RozmiarMapy);

            this.wysokoscMapy = wysokoscMapy;
            this.szerokoscMapy = szerokoscMapy;
        }
        #endregion

        //---------------------------------------------------------------------

        #region Wlasciwosci
        public int maxX => szerokoscMapy;

        public int maxY { 
            
            get => wysokoscMapy; 
            set 
            {
                var val2 = value * 2;
                szerokoscMapy =  val2; 
            }
        } 
        //---------------------------------------------------------------------
        #endregion
    }
    public class RzeczywistyRozmiarMapy
    {
        Point _rogLewyG  = Point.Empty;
        public Point rogLewyG { get => _rogLewyG; internal set => _rogLewyG = value; }

        public void _setRogLewyG(Point p)
        {
            _rogLewyG = p;
        }
        public Point _getRogLewyg()
        {
            return _rogLewyG;
        }

        public Point rogPrawyG { get; internal set; } = Point.Empty;
        public Point rogLewyD { get; internal set; } = Point.Empty;
        public Point rogPrawyD { get; internal set; } = Point.Empty;

        public RzeczywistyRozmiarMapy(int wysokoscMapy, int szerokoscMapy)
        {
            rogPrawyG = new Point(szerokoscMapy, 0);
            rogPrawyD = new Point(szerokoscMapy, wysokoscMapy);
            rogLewyD = new Point(0, wysokoscMapy);

        }

        internal RzeczywistyRozmiarMapy()
        {
        }
    }
    public class GraniceMapy : RzeczywistyRozmiarMapy
    {
        public GraniceMapy(int wysokoscMapy, int szerokoscMapy) : base(wysokoscMapy, szerokoscMapy)
        {

        }
        public GraniceMapy(RzeczywistyRozmiarMapy rzeRo) : base()
        {
            rogPrawyG = new Point(rzeRo.rogPrawyG.X - 1, 0 + 1);
            rogPrawyD = new Point(rzeRo.rogPrawyD.X - 1, rzeRo.rogPrawyD.Y - 1);
            rogLewyD = new Point(0 + 1, rzeRo.rogLewyD.Y - 1);
        }
    }
    public static class SprawdzanieJedzenia
    {
        public static bool czywGranicy(GraniceMapy granice, Point jedzenie)
        {
            if (jedzenie.X<=granice.rogLewyG.X)
            {
                return false;
            }
            if (jedzenie.X <= granice.rogLewyD.X)
            {
                return false;
            }
            if (jedzenie.X >= granice.rogPrawyG.X)
            {
                return false;
            }
            if (jedzenie.X >= granice.rogPrawyD.X)
            {
                return false;
            }
            if (jedzenie.Y <= granice.rogLewyG.Y)
            {
                return false;
            }
            if (jedzenie.Y >= granice.rogLewyD.Y)
            {
                return false;
            }
            if (jedzenie.Y <= granice.rogPrawyG.Y)
            {
                return false;
            }
            if (jedzenie.Y >= granice.rogPrawyD.Y)
            {
                return false;
            }
            return true;
        }
    }
}
