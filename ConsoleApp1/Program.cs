using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERZRECHNER
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string eingabe;
            int klein = 0;
            int mittel = 0;
            int groß = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("===========================================ERZRECHNER====================================================");
                Console.WriteLine("Bitte gib alle Würfelwerte ein und drücke nach jedem wert ENTER wenn alles eingetragen wurde Starte mit Y");
                do
                {

                    eingabe = Console.ReadLine();
                    if (eingabe != "Y" && eingabe != "y")
                    {
                        int erz = (Convert.ToInt32(eingabe));
                        if (erz >= 16 && erz <= 20)
                        {
                            groß++;
                        }
                        else if (erz >= 11 && erz <= 15)
                        {
                            mittel++;
                        }
                        else if (erz >= 6 && erz <= 10)
                        {
                            klein++;
                        }
                    }
                }
                while (eingabe != "Y" && eingabe != "y");


                Console.WriteLine("Kleine Brocken= " + klein);
                Console.WriteLine("Mittlere Brocken= " + mittel);
                Console.WriteLine("Große Brocken= " + groß);

                Console.WriteLine("NOCHMAL? Y/N");
                eingabe = Console.ReadLine();
            }
            while (eingabe == "Y" || eingabe == "y");

        
    }
    }
}
