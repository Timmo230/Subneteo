using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Subneteo_basico
{
    internal class Program
    {
        static void Main()
        {
            // datos principales
            int[] IPInicial = null;
            int[] Mascara = null;

            // datos de output
            string[] outputSubredes = new string[4];
            int numeroSubredes = -1;

            // Binarios
            string IPBinario;
            string MascaraBinario;

            // Binario separado
            string RedBinario = "";
            string subredBinario = "";
            string hostBinario = "";

            //Binario separado de Redgeneral
            string RedBinarioRedgeneral = "";
            string hostBinarioRedgeneral = "";

            //Binario array
            string[] RedBinarioArray = new string[4];
            string[] SubredBinarioArray = new string[4];
            string[] HostBinarioArray = new string[4];

            //Binario array redGeneral
            string[] RedBinarioArrayRedgeneral = new string[4];
            string[] SubredBinarioArrayRedgeneral = new string[4];
            string[] HostBinarioArrayRedgeneral = new string[4];

            //Ingreso de datos
            while (IPInicial == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Inserte una IP: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string IPInicialTexto = Console.ReadLine();
                IPInicial = comprovarIPValida(IPInicialTexto.Trim(), false);
            }
            IPBinario = CanversionBinario(IPInicial, 8);

            while (Mascara == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Inserte una la mascara: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string mascaraTexto = Console.ReadLine();
                Mascara = comprovarIPValida(mascaraTexto.Trim(), true);
            }
            MascaraBinario = CanversionBinario(Mascara, 8);

            while (numeroSubredes == -1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Inserte todas las subredes: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string numeroSubredesTexto = Console.ReadLine();
                outputSubredes = subredesValido(numeroSubredesTexto, MascaraBinario, IPBinario, false);
                numeroSubredes = Int32.Parse(outputSubredes[0]);
                RedBinario = outputSubredes[1];
                subredBinario = outputSubredes[2];
                hostBinario = outputSubredes[3];

                outputSubredes = subredesValido("", MascaraBinario, IPBinario, true);
                RedBinarioRedgeneral = outputSubredes[1];
                hostBinarioRedgeneral = outputSubredes[3];
            }

            List<int> hostPorSubred = new List<int>();
            List<int> Host = new List<int>();

            for (int i = 0; i < numeroSubredes; i++)
            {
                bool repetir = true;
                while (repetir)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Inserte el numero maximo de host que tendrá la red numero {0}: ", i + 1);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string posible = Console.ReadLine();

                    ArrayList output = NumeroHostSubred(posible, subredBinario, hostBinario.Length);

                    repetir = (bool)output[0];
                    if (!repetir)
                    {
                        Host.Add((int)output[1]);
                    }
                }

            }

            bool valido = false;
            string input = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nPulse 1 en caso de querer hacer subneteto basico o pulse 2 en caso de querer hacerlo con VLSM: ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        valido = true;
                        break;

                    case "2":
                        valido = true;
                        break;
                }

                if (input != "1" && input != "2")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Respuesta no valida");
                    valido = false;
                }

            } while (!valido);

            //Operaciones necesarias
            List<int> HostCopia = Host;
            Host.Sort((a, b) => b.CompareTo(a));

            HostBinarioArray = BinarioAArray(hostBinario);
            RedBinarioArray = BinarioAArray(RedBinario);
            SubredBinarioArray = BinarioAArray(subredBinario);

            RedBinarioArrayRedgeneral = BinarioAArray(RedBinarioRedgeneral);
            HostBinarioArrayRedgeneral = BinarioAArray(hostBinarioRedgeneral);

            string[,] RedGeneral = subneteo(RedBinarioArrayRedgeneral, HostBinarioArrayRedgeneral, SubredBinarioArray, 0, true);

            string[,] ResultadosFinales = null;
            switch (input)
            {
                case "1":
                    ResultadosFinales = subneteo(RedBinarioArray, HostBinarioArray, SubredBinarioArray, numeroSubredes, false);
                    break;
                case "2":
                    ResultadosFinales = SubneteoConVLSM(RedBinarioArray, HostBinarioArray, SubredBinarioArray, numeroSubredes, false, Host);
                    break;

            }

            string[,] ResultadosFinalesConPunto = AñadirPuntosBinario(ResultadosFinales, numeroSubredes, false);

            string[,] ResultadosFinalesDecimal = IPDecimalConPuntos(ResultadosFinalesConPunto);

            string[,] ResultadosFinalesConPuntoRedGeneral = AñadirPuntosBinario(RedGeneral, 0, true);
            string[,] ResultadosFinalesDecimalRedGeneral = IPDecimalConPuntos(ResultadosFinalesConPuntoRedGeneral);


            //Escribe todos los datos en consola
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Red general:");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("IP de red:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Binario: ");
                ImprimirGeneral(ResultadosFinalesConPuntoRedGeneral[0, 0], RedBinario.Length, subredBinario.Length);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Decimal: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, 0]);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("IP de breadcast:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Binario: ");
                ImprimirGeneral(ResultadosFinalesConPuntoRedGeneral[1, 0], RedBinario.Length, subredBinario.Length);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Decimal: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, 0]);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Primera IP disponible:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Binario: ");
                ImprimirGeneral(ResultadosFinalesConPuntoRedGeneral[2, 0], RedBinario.Length, subredBinario.Length);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Decimal: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, 0]);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ultima IP disponible:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Binario: ");
                ImprimirGeneral(ResultadosFinalesConPuntoRedGeneral[3, 0], RedBinario.Length, subredBinario.Length);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Decimal: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, 0]);


                switch (input)
                {
                    case "1":

                        for (int i = 0; i < numeroSubredes; i++)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Subred numero {0}:", i + 1);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("IP de red:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[0, i], RedBinario.Length, subredBinario.Length);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[0, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("IP de breadcast:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[1, i], RedBinario.Length, subredBinario.Length);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[1, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Primera IP disponible:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[2, i], RedBinario.Length, subredBinario.Length);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[2, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Ultima IP disponible:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[3, i], RedBinario.Length, subredBinario.Length);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, i]);

                            double desperdicio = Math.Pow(2, hostBinario.Length) - 1 - HostCopia[i];
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Y se desperdician: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", (int)desperdicio);
                        }
                        break;
                    case "2":
                        for (int i = 0; i < numeroSubredes; i++)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Subred con {0} host:", Host[i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("IP de red:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[0, i], RedBinario.Length, Int32.Parse(ResultadosFinales[4, i]));
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[0, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("IP de breadcast:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[1, i], RedBinario.Length, Int32.Parse(ResultadosFinales[4, i]));
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[1, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Primera IP disponible:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[2, i], RedBinario.Length, Int32.Parse(ResultadosFinales[4, i]));
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[2, i]);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Ultima IP disponible:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Binario: ");
                            Imprimir(ResultadosFinalesConPunto[3, i], RedBinario.Length, Int32.Parse(ResultadosFinales[4, i]));
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Decimal: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", ResultadosFinalesDecimal[3, i]);

                            double desperdicio = Math.Pow(2, hostBinario.Length) - 1 - HostCopia[i];
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Y se desperdician: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}\n", (int)desperdicio);
                        }
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Parte de red");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Parte de subred");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Parte de host");
            }
            Console.ReadKey();

        }

        private static ArrayList NumeroHostSubred(string posible, string subredes, int hostLargo)
        {
            int a = 0;
            bool x = Int32.TryParse(posible, out a);
            bool repetir = true;
            int numero = 0;
            if (x)
            {
                numero = Int32.Parse(posible);
                repetir = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valido");
                repetir = true;
            }

            if (numero < 0 || numero > Math.Pow(2, (double)hostLargo) - 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valido");
                repetir = true;
            }

            ArrayList output = new ArrayList();
            output.Add(repetir);
            output.Add(numero);

            return output;
        }

        //Comprueva si la mascara y la IP ingresadas son validas y convierte los textos en arrays de numeros
        private static int[] comprovarIPValida(string IPIniciarTexto, bool mascara)
        {
            int[] IPInicial = new int[4];
            int counter = 0;
            if (IPIniciarTexto == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valida");
                return null;
            }
            string[] IPIniciar = IPIniciarTexto.Split('.');

            if (IPIniciar.Length > 4 || IPIniciar.Length < 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valida");
                return null;
            }

            try
            {
                foreach (string IP in IPIniciar)
                {
                    IPInicial[counter++] = int.Parse(IP);
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valida");
                return null;
            }

            if (IPInicial[0] > 255 || IPInicial[0] < 0 || IPInicial[1] > 255 || IPInicial[1] < 0 || IPInicial[2] > 255 || IPInicial[2] < 0 || IPInicial[3] > 255 || IPInicial[3] < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es valida");
                return null;
            }

            if (mascara)
            {
                int maximo = 255;
                bool primera = true;

                foreach (int masc in IPInicial)
                {
                    if (masc != 255 && primera)
                    {
                        maximo = masc;
                        primera = false;
                    }
                    else if (masc != 255 && masc != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("La mascara no es valida");
                        return null;
                    }

                    if (masc != 255 && masc != 128 && masc != 192 && masc != 224 && masc != 240 && masc != 248 && masc != 252 && masc != 254 && masc != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("La mascara no es valida");
                        return null;
                    }
                }
                if (IPInicial[3] == 255)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("La mascara no es valida");
                    return null;
                }

                if (IPInicial[0] == 0 && IPInicial[1] == 0 && IPInicial[2] == 0 && IPInicial[3] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("La mascara no es valida");
                    return null;
                }

            }

            return IPInicial;
        }

        //Convierte las IP, mascaras, etc en un string en el que se guarda su binario
        private static string CanversionBinario(int[] IP, int reyenar)
        {
            string IPBinario = "";
            string prebinario;
            List<int> conversion = new List<int>();
            int _Conversion = 0;
            foreach (int i in IP)
            {
                conversion.Clear();
                conversion.Add(-1);
                _Conversion = i;
                prebinario = "";

                if (_Conversion == 1 || _Conversion == 0)
                {
                    conversion.Add(i);
                }
                else
                {
                    while (conversion == null || conversion[conversion.Count - 1] != 1)
                    {
                        conversion.Add(_Conversion);

                        _Conversion /= 2;
                    }
                }

                conversion.Reverse();

                foreach (int can in conversion)
                {
                    if (can == -1)
                    {
                        break;
                    }
                    else if (can % 2 == 0)
                    {
                        prebinario += "0";
                    }
                    else
                    {
                        prebinario += "1";
                    }
                }

                if (prebinario.Length != reyenar)
                {
                    int count = prebinario.Length;

                    for (int a = count; a < reyenar; a++)
                    {
                        IPBinario += "0";
                    }

                }
                IPBinario += prebinario;
            }

            return IPBinario;
        }

        //Comprueba si las subredes ingresadas son validad y devuelve el numero binario que le corresponde a la red, subred y los host
        private static string[] subredesValido(string numeroSubredesTexto, string MascaraBinario, string IPBinario, bool redGeneral)
        {
            string[] outputSubred = new string[4];
            int numeroSubredes = 0;
            int Mascara = MascaraBinario.Count(c => c == '1');

            string numeroRedBinario = "";
            string hostBinario = "";
            string subredSeparado = "";
            string hostConSubred = "";
            if (!redGeneral)
            {
                try
                {
                    numeroSubredes = Int32.Parse(numeroSubredesTexto);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Numero de subredes no valido");
                    outputSubred[0] = "-1";
                    return outputSubred;
                }
                if (numeroSubredes < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Numero de subredes no valido");
                    outputSubred[0] = "-1";
                    return outputSubred;
                }
            }

            for (int i = 0; i < Mascara; i++)
            {
                numeroRedBinario += IPBinario[i];
            }

            for (int i = 0; i < 32 - numeroRedBinario.Length; i++)
            {
                hostBinario += IPBinario[i + numeroRedBinario.Length];
            }

            int[] numeroSubred = { numeroSubredes };
            string NumeroSubredes = CanversionBinario(numeroSubred, 0);

            if (!redGeneral)
            {
                if (NumeroSubredes.Length > hostBinario.Length - 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Numero de subredes no valido");
                    outputSubred[0] = "-1";
                    return outputSubred;
                }
                int[] numeroSubredesArray = { numeroSubredes - 1 };
                string a = CanversionBinario(numeroSubredesArray, 0);
                for (int i = 0; i < a.Length; i++)
                {
                    subredSeparado += hostBinario[i];
                }
            }

            if (subredSeparado != "" && subredSeparado != "")
            {
                for (int i = 0; i < hostBinario.Length - subredSeparado.Length; i++)
                {
                    hostConSubred += hostBinario[i + subredSeparado.Length];
                }
            }
            else
            {
                for (int i = 0; i < hostBinario.Length; i++)
                {
                    hostConSubred += hostBinario[i];
                }
            }


            outputSubred[0] = numeroSubredes.ToString();
            outputSubred[1] = numeroRedBinario;
            outputSubred[2] = subredSeparado;
            outputSubred[3] = hostConSubred;

            if (subredSeparado != "")
            {
                if (numeroRedBinario.Length + subredSeparado.Length + hostConSubred.Length != 32) { throw new ArgumentException("No es 32"); }
            }
            else
            {
                if (numeroRedBinario.Length + hostConSubred.Length != 32) { throw new ArgumentException("No es 32"); }
            }

            return outputSubred;
        }

        //Separa los numeros binarios en arrays separados byte por byte
        private static string[] BinarioAArray(string hostBinario)
        {
            string[] Binario = { "", "", "", "" };
            int indice = 0;
            int counter = 0;

            for (int i = 0; i < hostBinario.Length; i++)
            {
                Binario[indice] += hostBinario[i].ToString();
                counter++;

                if (counter % 8 == 0)
                {
                    indice++;
                }
            }
            return Binario;
        }

        //Los siguientes 2 metodos realizan el subneteo
        private static string[,] subneteo(string[] RedBinario, string[] hostBinario, string[] subredBinario, int numeroSubredes, bool redGeneral)
        {
            //Listas donde se guardan todos los resultados
            string[] IPRedRsult = new string[numeroSubredes];
            string[] IPBroadcastRsult = new string[numeroSubredes];
            string[] IPPrimeraRsult = new string[numeroSubredes];
            string[] IPUltimaRsult = new string[numeroSubredes];

            string[,] Resultados = null;

            // Lista resultadoFinal
            if (!redGeneral)
            {
                Resultados = new string[4, numeroSubredes];
            }
            else
            {
                Resultados = new string[4, 1];
            }


            if (!redGeneral)
            {
                for (int numeroIndice = 0; numeroIndice < numeroSubredes; numeroIndice++)
                {
                    //IP de red general
                    IPRedRsult[numeroIndice] = conversionesDeDatosFinales(RedBinario, hostBinario, false, false, true, numeroIndice, numeroSubredes, false);

                    //IP de broadcast general
                    IPBroadcastRsult[numeroIndice] = conversionesDeDatosFinales(RedBinario, hostBinario, false, false, false, numeroIndice, numeroSubredes, false);

                    // Primera red general
                    IPPrimeraRsult[numeroIndice] = conversionesDeDatosFinales(RedBinario, hostBinario, true, true, true, numeroIndice, numeroSubredes, false);

                    // Ultima red general
                    IPUltimaRsult[numeroIndice] = conversionesDeDatosFinales(RedBinario, hostBinario, true, false, false, numeroIndice, numeroSubredes, false);
                }


                for (int b = 0; b < numeroSubredes; b++)
                {
                    Resultados[0, b] = IPRedRsult[b];
                    Resultados[1, b] = IPBroadcastRsult[b];
                    Resultados[2, b] = IPPrimeraRsult[b];
                    Resultados[3, b] = IPUltimaRsult[b];
                }

                for (int b = 0; b < numeroSubredes; b++)
                {
                    Resultados[0, b] = IPRedRsult[b];
                    Resultados[1, b] = IPBroadcastRsult[b];
                    Resultados[2, b] = IPPrimeraRsult[b];
                    Resultados[3, b] = IPUltimaRsult[b];
                }
            }
            else
            {
                Resultados[0, 0] = conversionesDeDatosFinales(RedBinario, hostBinario, false, false, true, 0, 0, true);
                Resultados[1, 0] = conversionesDeDatosFinales(RedBinario, hostBinario, false, false, false, 0, 0, true);
                Resultados[2, 0] = conversionesDeDatosFinales(RedBinario, hostBinario, true, true, true, 0, 0, true);
                Resultados[3, 0] = conversionesDeDatosFinales(RedBinario, hostBinario, true, false, false, 0, 0, true);
            }

            return Resultados;
        }

        private static string conversionesDeDatosFinales(string[] RedBinario, string[] hostBinario, bool PrimeraUltima, bool primera, bool ConCeros, int Indice, int numeroSubredes, bool RedGeneral)
        {
            // retorno
            string IPResult = "";

            //Variables dependiendo del dato
            int primeraUltima = 0;
            string UltimoDigito = "";

            string redBroad = "";

            //Variables de tramsferemcia
            int[] subRedes = { Indice };
            int[] NumeroSubredes = { numeroSubredes - 1 };

            //Darle valor a variables que dependen del dato
            redBroad = ConCeros ? "0" : "1";

            primeraUltima = PrimeraUltima ? 1 : 0;

            UltimoDigito = primera ? "1" : "0";

            // Añade el apartado de red que no varia
            foreach (string s in RedBinario)
            {
                IPResult += s;
            }

            // Se calcula con cada subred, INCOMPLETO
            if (!RedGeneral)
            {
                string SubredesBinarioSinArray = CanversionBinario(NumeroSubredes, 0);
                string indiceBinario = CanversionBinario(subRedes, SubredesBinarioSinArray.Length);

                IPResult += indiceBinario;
            }


            //Añade el apartado de host, que varia en cada uno
            foreach (string s in hostBinario)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (PrimeraUltima && 32 - IPResult.Length == 1)
                    {
                        IPResult += UltimoDigito;
                    }
                    else
                    {
                        IPResult += redBroad;
                    }

                }
            }

            if (IPResult.Length != 32) { throw new ArgumentException("No es 32"); }
            return IPResult;
        }

        //Hace las IP en binario mas legible
        private static string[,] AñadirPuntosBinario(string[,] SinPuntos, int numSubredes, bool RedGeneral)
        {
            string[,] ConPuntos = null;

            if (!RedGeneral)
            {
                ConPuntos = new string[4, numSubredes];
                for (int counter = 0; counter < 4; counter++)
                {
                    for (int i = 0; i < numSubredes; i++)
                    {
                        string copy = SinPuntos[counter, i];
                        for (int j = 0; j < copy.Length; j++)
                        {
                            if (j % 8 == 0 && j != 0)
                            {
                                ConPuntos[counter, i] += ".";
                            }

                            ConPuntos[counter, i] += copy[j].ToString();
                        }
                    }
                }
            }
            else
            {
                ConPuntos = new string[4, 1];
                for (int counter = 0; counter < 4; counter++)
                {
                    string copy = SinPuntos[counter, 0];
                    for (int j = 0; j < copy.Length; j++)
                    {
                        if (j % 8 == 0 && j != 0)
                        {
                            ConPuntos[counter, 0] += ".";
                        }

                        ConPuntos[counter, 0] += copy[j].ToString();
                    }
                }
            }


            return ConPuntos;
        }

        // Convierte las Listas de IP con puntos a decimal
        private static string[,] IPDecimalConPuntos(string[,] BinarioConPuntos)
        {
            string[,] DecimalConPuntos = new string[4, BinarioConPuntos.Length / 4];

            for (int counter = 0; counter < 4; counter++)
            {
                for (int i = 0; i < BinarioConPuntos.Length / 4; i++)
                {
                    string[] Sepacion = BinarioConPuntos[counter, i].Split('.');

                    DecimalConPuntos[counter, i] = DeBinarioADecimalConPuntos(Sepacion);
                }
            }

            return DecimalConPuntos;
        }

        private static string DeBinarioADecimalConPuntos(string[] separacion)
        {
            string resultado = "";
            int counter = 0;

            foreach (string s in separacion)
            {
                resultado += DeBinarioADecimal(s).ToString();
                counter++;

                if (counter != 4)
                    resultado += ".";
            }

            return resultado;
        }

        private static int DeBinarioADecimal(string s)
        {
            int multplicador = s.Length - 1;
            int sumador = 0;
            string resultado = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '1')
                {
                    sumador += (int)Math.Pow(2, multplicador);

                }
                multplicador--;
            }

            resultado += sumador.ToString();

            return Int32.Parse(resultado);
        }

        // VLSM
        private static string[,] SubneteoConVLSM(string[] RedBinario, string[] hostBinario, string[] subredBinario, int numeroSubredes, bool redGeneral, List<int> Host)
        {
            //Listas donde se guardan todos los resultados
            string[] IPRedRsult = new string[numeroSubredes];
            string[] IPBroadcastRsult = new string[numeroSubredes];
            string[] IPPrimeraRsult = new string[numeroSubredes];
            string[] IPUltimaRsult = new string[numeroSubredes];

            int[] LongitudPropiedades = new int[numeroSubredes];

            ArrayList IPRedRsultList = new ArrayList();
            ArrayList IPBroadcastRsultList = new ArrayList();
            ArrayList IPPrimeraRsultList = new ArrayList();
            ArrayList IPUltimaRsultList = new ArrayList();

            string subredAnterior = "";

            string[,] Resultados = new string[5, numeroSubredes];
            bool PrimeraVez = true;
            bool segundaVez = true;

            int longPrimeraVez = 0;

            for (int numeroIndice = 0; numeroIndice < numeroSubredes; numeroIndice++)
            {
                int HostActual = Host[numeroIndice];
                //IP de red general
                IPRedRsultList = conversionesDeDatosFinalesVLSM(RedBinario, hostBinario, false, false, true, numeroIndice, numeroSubredes, PrimeraVez, subredAnterior, HostActual, segundaVez, longPrimeraVez);
                IPRedRsult[numeroIndice] = IPRedRsultList[0].ToString();

                //IP de broadcast general
                IPBroadcastRsultList = conversionesDeDatosFinalesVLSM(RedBinario, hostBinario, false, false, false, numeroIndice, numeroSubredes, PrimeraVez, subredAnterior, HostActual, segundaVez, longPrimeraVez);
                IPBroadcastRsult[numeroIndice] = IPBroadcastRsultList[0].ToString();

                // Primera red general
                IPPrimeraRsultList = conversionesDeDatosFinalesVLSM(RedBinario, hostBinario, true, true, true, numeroIndice, numeroSubredes, PrimeraVez, subredAnterior, HostActual, segundaVez, longPrimeraVez);
                IPPrimeraRsult[numeroIndice] = IPPrimeraRsultList[0].ToString();

                // Ultima red general
                IPUltimaRsultList = conversionesDeDatosFinalesVLSM(RedBinario, hostBinario, true, false, false, numeroIndice, numeroSubredes, PrimeraVez, subredAnterior, HostActual, segundaVez, longPrimeraVez);
                IPUltimaRsult[numeroIndice] = IPUltimaRsultList[0].ToString();


                subredAnterior = IPRedRsultList[1].ToString();

                longPrimeraVez = (int)IPRedRsultList[2];
                LongitudPropiedades[numeroIndice] = subredAnterior.Length;

                segundaVez = !PrimeraVez ? false : true;
                PrimeraVez = false;
            }

            for (int b = 0; b < numeroSubredes; b++)
            {
                Resultados[0, b] = IPRedRsult[b];
                Resultados[1, b] = IPBroadcastRsult[b];
                Resultados[2, b] = IPPrimeraRsult[b];
                Resultados[3, b] = IPUltimaRsult[b];
            }

            for(int b = 0; b < numeroSubredes; b++)
            {
                Resultados[4, b] = LongitudPropiedades[b].ToString();
            }

            return Resultados;
        }

        private static ArrayList conversionesDeDatosFinalesVLSM(string[] RedBinario, string[] hostBinario, bool PrimeraUltima, bool primera, bool ConCeros, int Indice, int numeroSubredes, bool PrimeraVez, string subredAnterior, int Host, bool segundaVez, int LongUltimaVez)
        {
            // retorno
            string IPResult = "";
            string indiceBinario = "";

            //Variables dependiendo del dato
            int primeraUltima = 0;
            string UltimoDigito = "";

            string redBroad = "";

            //Variables de tramsferemcia
            int[] subRedes = { 0 };
            int[] NumeroSubredes = { numeroSubredes - 1 };

            //Darle valor a variables que dependen del dato
            redBroad = ConCeros ? "0" : "1";

            primeraUltima = PrimeraUltima ? 1 : 0;

            UltimoDigito = primera ? "1" : "0";

            // Añade el apartado de red que no varia
            foreach (string s in RedBinario)
            {
                IPResult += s;
            }



            //Añade el apartado de host, que varia en cada uno

            string HostActualBinario = CanversionBinario(new int[] { Host - 1 }, 0);

            string todoHost = "";

            for (int i = 0; i < HostActualBinario.Length; i++)
            {
                if (PrimeraUltima && HostActualBinario.Length - todoHost.Length == 1)
                {
                    todoHost += UltimoDigito;
                }
                else
                {
                    todoHost += redBroad;
                }

            }

            int numeroDeCerosPrimeraVez = 0;
            // Se calcula con cada subred
            if (PrimeraVez)
            {
                string SubredesBinarioSinArray = CanversionBinario(NumeroSubredes, 0);
                indiceBinario = CanversionBinario(subRedes, 32 - todoHost.Length - IPResult.Length);

                IPResult += indiceBinario;

                numeroDeCerosPrimeraVez = indiceBinario.Length;
                LongUltimaVez = indiceBinario.Length;
            }
            else
            {
                int SubredAnterior = DeBinarioADecimal(subredAnterior);

                string numeroAnterior = CanversionBinario(new int[] { SubredAnterior + 1 }, 0);
                string PreSubredNueva = "";

                if (segundaVez)
                {
                    PreSubredNueva = "";

                    for (int i = 0; i != LongUltimaVez - 1; i++)
                    {
                        PreSubredNueva += "0";
                    }
                    PreSubredNueva += "1";

                    for (int i = PreSubredNueva.Length; i + todoHost.Length + IPResult.Length != 32; i++)
                    {
                        PreSubredNueva += "0";
                    }
                }
                else
                {
                    for (int i = 0; i < LongUltimaVez - numeroAnterior.Length; i++)
                    {
                        PreSubredNueva += "0";
                    }

                    PreSubredNueva += numeroAnterior;

                    for (int i = PreSubredNueva.Length; i + todoHost.Length + IPResult.Length != 32; i++)
                    {
                        PreSubredNueva += "0";
                    }
                }

                IPResult += PreSubredNueva;
                indiceBinario = PreSubredNueva;
                LongUltimaVez = PreSubredNueva.Length;

            }

            IPResult += todoHost;

            ArrayList output = new ArrayList();
            output.Add(IPResult);
            output.Add(indiceBinario);
            output.Add(LongUltimaVez);

            if (IPResult.Length != 32) { throw new ArgumentException("No es 32"); }
            return output;
        }

        //Imprimir en consola
        private static void Imprimir(string IP, int longRed, int longSubred)
        {
            int counter = 0;
            char Color = 'R';
            foreach (char c in IP)
            {
                if (counter < longRed)
                {
                    Color = 'R';
                }
                else if (counter < longRed + longSubred)
                {
                    Color = 'S';
                }
                else
                {
                    Color = 'H';
                }

                switch(Color)
                {
                    case 'R':
                        Console.ForegroundColor = ConsoleColor.Magenta; 
                        break;
                    case 'S':
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 'H':
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.Write(c);

                if (c != '.')
                {
                    counter++;
                }
            }
            Console.WriteLine();
        }

        private static void ImprimirGeneral(string IP, int longRed, int longSubred)
        {
            int counter = 0;
            char Color = 'R';
            foreach (char c in IP)
            {
                if (counter < longRed)
                {
                    Color = 'R';
                }
                else
                {
                    Color = 'H';
                }

                switch(Color)
                {
                    case 'R':
                        Console.ForegroundColor = ConsoleColor.Magenta; 
                        break;
                    case 'H':
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.Write(c);

                if (c != '.')
                {
                    counter++;
                }
            }
            Console.WriteLine();
        }
    }
}