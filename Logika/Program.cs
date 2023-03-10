using System.Drawing;
using System.Net;

namespace Logika
{
    internal class Program
    {
        static public Mapa? rozmiarMapy;

        static void test()
        {
            int ilerazy = 0;

            bool posiłek = false;
            for (int i = 0; i < 10; i++)
            {
                var decydujacyPunkt = Lodowka.Generowanieposilku(rozmiarMapy);
                posiłek = SprawdzanieJedzenia.czywGranicy(rozmiarMapy.GraniceMapy, decydujacyPunkt);
                if (posiłek == false)
                {
                    ilerazy++;
                }
            }

            Console.WriteLine("tyle razy bylo poza granicami: " + ilerazy);
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            rozmiarMapy = new Mapa(20, 20);
            test();

            NamalujGranic(new Point (rozmiarMapy.szerokoscMapy, rozmiarMapy.wysokoscMapy));

            Point nowejedzenie = Point.Empty;
            bool posiłek = false;
            while ( posiłek == false)
            {
                var decydujacyPunkt = Lodowka.Generowanieposilku(rozmiarMapy);
                posiłek = SprawdzanieJedzenia.czywGranicy(rozmiarMapy.GraniceMapy, decydujacyPunkt);
                if(posiłek == true)
                {
                    nowejedzenie = decydujacyPunkt;
                }
            }
        
            NarysujPosilek(nowejedzenie);


            Task.Run(OdczytywaniePrzycisku);

            zebyCzekalo();
            while (true) ;  // to też, żeby nie wywalało programu za wczesnie
        }

        static async void zebyCzekalo()
        {
            //zeby nie wyrzucalo ostrzezen ze graniceMapy moga byc null'em
            if (rozmiarMapy is null)
                return;

            while (true)
            {
                await Task.Delay(800);// poczekać
                                        //Console.WriteLine(kierunek);

                switch (kierunek)
                {
                    case Kierunek.dol:
                        wspGlowy = new Point(wspGlowy.X, wspGlowy.Y + 1);
                        break;
                    case Kierunek.gora:
                        wspGlowy = new Point(wspGlowy.X, wspGlowy.Y - 1);
                        break;
                    case Kierunek.prawo:
                        wspGlowy = new Point(wspGlowy.X + 1, wspGlowy.Y);
                        break;
                    case Kierunek.lewo:
                        wspGlowy = new Point(wspGlowy.X - 1, wspGlowy.Y);
                        break;
                }
                //Console.Write(wspGlowy);
                //Console.Write(Lodowka.Generowanieposilku(graniceMapy.wysokoscMapy, graniceMapy.szerokoscMapy));
                if (wspGlowy.Y < 0)
                    wspGlowy = new Point(wspGlowy.X, 0);
                if (wspGlowy.X < 0)
                    wspGlowy = new Point(0, wspGlowy.Y);

                WyczyscKonsole(ostaniaPozycjaGlowy);
                
                //var decydujacyPunkt = Lodowka.Generowanieposilku(graniceMapy);


                NarysujGlowe(wspGlowy);

                // zapisz ostanie pozycje gdzie byly elementy aby pozniej moc je wyczyscic na konsoli
                ostaniaPozycjaGlowy = wspGlowy;
                var glowa = SprawdzanieJedzenia.czywGranicy(rozmiarMapy.GraniceMapy, wspGlowy);
                if (glowa == false)
                {
                    break;
                }
                if(ostaniaPozycjaGlowy==ostaniaPozycjaJedzenia)
                {
                    Console.WriteLine("wygrana");
                    break;
                }

            }
            Console.WriteLine("game over");
        }
        static Point ostaniaPozycjaGlowy = Point.Empty;
        static Point ostaniaPozycjaJedzenia = Point.Empty;

        private static void NarysujGlowe(Point wsp)
        {
            Console.SetCursorPosition(wsp.X, wsp.Y);
            Console.Write('@');
        }

        private static void NarysujPosilek(Point decydujacyPunkt)
        {
            Console.SetCursorPosition(decydujacyPunkt.X, decydujacyPunkt.Y);
            Console.Write('0');
            ostaniaPozycjaJedzenia = decydujacyPunkt;

        }
        public static void NamalujGranic(Point granica)
        {
            NamalujGranicPION(new Point(0, granica.Y));
            NamalujGranicPION(new Point(granica.X, granica.Y));
            NamalujGranicPOZIOM(new Point(granica.X, 0));
            NamalujGranicPOZIOM(new Point(granica.X, granica.Y));
        }
        public static void NamalujGranicPION(Point granica)
        {
            char kreska = '|';
            for (int i = 0; i < granica.Y; i++)
            {
                Console.SetCursorPosition((int)granica.X, i);
                Console.Write(kreska);
            }
        }
        public static void NamalujGranicPOZIOM(Point granica)
        {
            char kreska = '_';
            for( int i= 0; i < granica.X; i++)
            {
                Console.SetCursorPosition(i, (int)granica.Y);
                Console.Write(kreska);
            }
        }

        /// <summary>
        /// Funkcja do wyczyszczenia punktow w ktorych wczesniej cos namalowalismy,
        /// bo Console.Clear() pierdoli cos srogo.
        /// </summary>
        /// <param name="points"></param>
        private static void WyczyscKonsole(params Point[] points)  // List<Point>
        {
            foreach (var point in points)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(' ');
            }
        }

        static Task OdczytywaniePrzycisku()
        {
            bool exit = false;
            while (!exit)
            {
                var przycisk = Console.ReadKey();

                switch (przycisk.Key)
                {
                    case ConsoleKey.DownArrow:
                        kierunek = Kierunek.dol;
                        break;
                    case ConsoleKey.UpArrow:
                        kierunek = Kierunek.gora;
                        break;
                    case ConsoleKey.LeftArrow:
                        kierunek = Kierunek.lewo;
                        break;
                    case ConsoleKey.RightArrow:
                        kierunek = Kierunek.prawo;
                        break;

                    case ConsoleKey.Escape:
                        exit = true; // sposob 2
                        //return Task.CompletedTask; // sposob 1
                        Environment.Exit(0);//zakonczenie programu
                        break;

                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }
        static Kierunek kierunek = Kierunek.Default; /// wartosc startowa
        static Point wspGlowy = new Point(10, 10);
    }
}