/// Author: Leon Schneider
/// Version: 1.0
/// Date: 28.05.2024

using System.Text.Json;
using System.IO;
using System.Numerics;

namespace ConsoleApp1
{
    
    internal class Program
    {
        /// <summary>
        /// Enum für die Monate
        /// </summary>
        enum Monate
        {
            Januar = 1,
            Februar = 2,
            Maerz = 3,
            April = 4,
            Mai = 5,
            Juni = 6,
            Juli = 7,
            August = 8,
            September = 9,
            Oktober = 10,
            November = 11,
            Dezember = 12
        }
        /// <summary>
        /// Enum für die Menüoptionen
        /// </summary>
        enum MenuOptionen
        {
            Dateneingabe = 1,
            AlleDatenAusgeben = 2,
            DatenEinesMonatsAusgeben = 3,
            MonatMitDemMeistenNiederschlag = 4,
            MonatMitDemWenigstenNiederschlag = 5,
            DurchschnittlicherNiederschlag = 6,
            GesamtNiederschlag = 7,
            DatenSpeichern = 8,
            DatenLaden = 9,
            VergleichMitGespeichertenDaten = 10,
            Beenden = 11
        }
        static void Main(string[] args)
        {
            bool firstRun = true;

            int[] daten = new int[12];

            MenuOptionen option = MenuOptionen.Dateneingabe;

            while (option != MenuOptionen.Beenden)
            {
                if(!firstRun)
                {
                    Console.WriteLine("Drücken Sie eine Taste um fortzufahren...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.WriteLine("------------------Wetterdaten------------------");
                foreach (MenuOptionen item in Enum.GetValues(typeof(MenuOptionen)))
                {
                    Console.WriteLine($"{(int)item}. {item}");
                }
                Console.WriteLine("----------------------------------------------");

                try
                {
                    option = (MenuOptionen)Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case MenuOptionen.Dateneingabe:
                            daten = DatenEinlesen();
                            break;
                        case MenuOptionen.AlleDatenAusgeben:
                            Console.Clear();
                            DatenAusgeben(daten);
                            break;
                        case MenuOptionen.DatenEinesMonatsAusgeben:
                            Console.Clear();
                            Console.WriteLine("Bitte Monat eingeben: ");
                            int monat = Convert.ToInt32(Console.ReadLine());
                            DatenAusgeben(daten, monat);
                            break;
                        case MenuOptionen.MonatMitDemMeistenNiederschlag:
                            Console.Clear();
                            Console.WriteLine($"Monat mit dem meisten Niederschlag: {MonatMitDemMeistenNiederschlag(daten)}");
                            break;
                        case MenuOptionen.MonatMitDemWenigstenNiederschlag:
                            Console.Clear();
                            Console.WriteLine($"Monat mit dem wenigsten Niederschlag: {MonatMitDemWenigstenNiederschlag(daten)}");
                            break;
                        case MenuOptionen.DurchschnittlicherNiederschlag:
                            Console.Clear();
                            Console.WriteLine($"Durchschnittlicher Niederschlag: {DurchschnittlicherNiederschlag(daten)}");
                            break;
                        case MenuOptionen.GesamtNiederschlag:
                            Console.Clear();
                            Console.WriteLine($"Gesamt Niederschlag: {GesamtNiederschlag(daten)}");
                            break;
                        case MenuOptionen.DatenSpeichern:
                            Console.Clear();
                            Console.WriteLine("Daten speichern");
                            DatenSpeichern(daten);
                            break;
                        case MenuOptionen.DatenLaden:
                            Console.Clear();
                            Console.WriteLine("Daten laden");
                            daten = DatenLaden();
                            break;
                        case MenuOptionen.VergleichMitGespeichertenDaten:
                            Console.Clear();
                            Console.WriteLine("Vergleich mit gespeicherten Daten");
                            VergleichMitGespeichertenDaten(daten);
                            break;
                        case MenuOptionen.Beenden:
                            return;
                        default:
                            Console.WriteLine("Ungültige Option");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler: " + e.Message);
                }
                firstRun = false;
            }

        }
        /// <summary>
        /// Liest die Niederschlagsdaten für 12 Monate ein
        /// </summary>
        /// <returns>Int[12] von Niederschlagsmengen</returns>
        static int[] DatenEinlesen()
        {
            Console.WriteLine("Niederschlag in mm für 12 Monate eingeben: ");
            int[] daten = new int[12];
            for (int i = 0; i < 12; i++)
            {
                // Console.Write("Monat " + (i + 1) + ": ");
                Console.Write("Monat " + ((Monate)i + 1) + ": ");
                try
                {
                    daten[i] = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler: " + e.Message);
                    i--;
                }
            }
            return daten;
        }
        /// <summary>
        /// Gibt die Niederschlagsdaten für 12 Monate aus
        /// </summary>
        /// <param name="daten">Int[12] von Niederschlagsmengen</param>
        /// <param name="monat">optional int Monat der ausgegeben werden soll</param>
        static void DatenAusgeben(int[] daten, int monat = 0)
        {
            if (monat != 0)
            {
                Console.WriteLine("Niederschlag in mm für Monat " + (Monate)monat + ": " + daten[monat - 1]);
                return;
            }
            Console.WriteLine("Niederschlag in mm für 12 Monate: ");
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine("Monat " + ((Monate)i + 1) + ": " + daten[i]);
            }
            return;
        }
        /// <summary>
        /// gibt den Monat mit dem meisten Niederschlag zurück
        /// </summary>
        /// <param name="daten">Int[12] von Niederschlagsmengen</param>
        /// <returns></returns>
        static Monate MonatMitDemMeistenNiederschlag(int[] daten)
        {
            int max = daten[0];
            int index = 0;
            for (int i = 0; i < 12; i++)
            {
                if (daten[i] > max)
                {
                    max = daten[i];
                    index = i;
                }
            }
            return (Monate)index + 1;
        }
        /// <summary>
        /// gibt den Monat mit dem wenigsten Niederschlag zurück
        /// </summary>
        /// <param name="daten">Int[12] von Niederschlagsmengen</param>
        /// <returns></returns>
        static Monate MonatMitDemWenigstenNiederschlag(int[] daten)
        {
            int min = daten[0];
            int index = 0;
            for (int i = 0; i < 12; i++)
            {
                if (daten[i] < min)
                {
                    min = daten[i];
                    index = i;
                }
            }
            return ((Monate)index + 1);
        }
        /// <summary>
        /// Gibt den durchschnittlichen Niederschlag zurück
        /// </summary>
        /// <param name="daten">Int[12] von Niederschlagsmengen</param>
        /// <returns></returns>
        static double DurchschnittlicherNiederschlag(int[] daten)
        {
            double sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += daten[i];
            }
            return sum / 12;
        }
        /// <summary>
        /// Gibt den gesamten Niederschlag zurück
        /// </summary>
        /// <param name="daten">Int[12] von Niederschlagsmengen</param>
        /// <returns></returns>
        static double GesamtNiederschlag(int[] daten)
        {
            double sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += daten[i];
            }
            return sum;
        }
        static void DatenSpeichern(int[] daten)
        {
            string FileName = "daten.json";
            File.WriteAllText(FileName, JsonSerializer.Serialize(daten));
            Console.WriteLine("Daten gespeichert");
        }
        static int[] DatenLaden()
        {
            string FileName = "daten.json";
            if (File.Exists(FileName))
            {
                return JsonSerializer.Deserialize<int[]>(File.ReadAllText(FileName));
            }
            return new int[12];
        }
        static void VergleichMitGespeichertenDaten(int[] daten)
        {
            int[] gespeicherteDaten;
            string FileName = "daten.json";
            if (File.Exists(FileName))
            {
                gespeicherteDaten = JsonSerializer.Deserialize<int[]>(File.ReadAllText(FileName));
            }
            else
            {
                gespeicherteDaten = new int[12];
            }
            double durchshnittGespeicherteDaten = DurchschnittlicherNiederschlag(gespeicherteDaten);
            double durchschnittNeueDaten = DurchschnittlicherNiederschlag(daten);
            Console.WriteLine("Durchschnitt gespeicherte Daten: " + durchshnittGespeicherteDaten);
            Console.WriteLine("Durchschnitt neue Daten: " + durchschnittNeueDaten);
            Console.WriteLine("Differenz: " + (durchschnittNeueDaten - durchshnittGespeicherteDaten));
        }
    }
}