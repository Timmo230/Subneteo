using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez1
{
    internal class Program
    {
            public static string[,] Tablero =
            {
            {"A8", "B8", "C8", "D8", "E8", "F8", "G8", "H8"},
            {"A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7"},
            {"A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6"},
            {"A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5"},
            {"A4", "B4", "C4", "D4", "E4", "F4", "G4", "H4"},
            {"A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3"},
            {"A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"},
            {"A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1"}
        };

            public static Peon[] peonesBlancos = new Peon[8];
            public static Peon[] peonesNegros = new Peon[8];
            public static Torre[] torresBlancas = new Torre[2];
            public static Torre[] torresNegras = new Torre[2];
            public static Caballo[] caballosBlancos = new Caballo[2];
            public static Caballo[] caballosNegros = new Caballo[2];
            public static Alfil[] alfilesBlancos = new Alfil[2];
            public static Alfil[] alfilesNegros = new Alfil[2];
            public static Rey[] reyBlanco = new Rey[1];
            public static Rey[] reyNegro = new Rey[1];
            public static Dama[] damaBlanca = new Dama[1];
            public static Dama[] damaNegra = new Dama[1];

            public static Pieza[,] piezas = new Pieza[12, 8];

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            string[] b = { " ", " " };


            inicializacionDeTodo();

            juego();

            string[,] output = LugaresOcupados();

            string[] ocupados = new string[32];
            for (int i = 0; i < 32; i++)
            {
                ocupados[i] = output[0, i];
            }

            string[] ocupadosBlancos = new string[16];
            string[] ocupadosNegros = new string[16];

            for (int i = 0; i < 16; i++)
            {
                ocupadosBlancos[i] = output[1, i];
                ocupadosNegros[i] = output[2, i];
            }


            torresBlancas[0].Mover(ocupados, ocupadosBlancos, ocupadosNegros);

            peonesBlancos[0].Mover(ocupados, ocupadosBlancos, ocupadosNegros);

            Console.ReadKey();
        }

        public static void juego()
        {

            CrearTablero();
            string inputValido;

            do
            {
                string input = Console.ReadLine();

                inputValido = conversion(input);
            } while (inputValido == null);


        }

        public static string conversion(string input)
        {
            char[] output = new char[input.Length];

            if (input.Length != 2 && input[2] == 'x')
            {
                Console.WriteLine("Posición no valida");
                return null;
            }
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i];
            }
            char outputLetra = input[0] == 'A' ? '1' : input[0] == 'B' ? '2' : input[0] == 'C' ? '3' : input[0] == 'D' ? '4' : input[0] == 'E' ? '5' : input[0] == 'F' ? '6' : input[0] == 'G' ? '7' : input[0] == 'H' ? '8' : 'a';

            char outputNumero = input[1] == '1' ? '1' : input[1] == '2' ? '2' : input[1] == '3' ? '3' : input[1] == '4' ? '4' : input[1] == '5' ? '5' : input[1] == '6' ? '6' : input[1] == '7' ? '7' : input[1] == '8' ? '8' : 'a';

            try
            {
                int a = Int32.Parse(outputLetra.ToString());
                a = Int32.Parse(outputNumero.ToString());
            }
            catch
            {
                Console.WriteLine("Posición no valida");
                return null;
            }


            string posicion = outputLetra.ToString() + outputLetra.ToString();

            return posicion;
        }

        public static void CrearTablero()
        {
            bool color = false;
            int counter = 1;
            string InicialDeLaCasilla;
            bool añadirEspacio = true;
            foreach (string a in Tablero)
            {
                color = !color;
                switch (color)
                {
                    case true:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        break;
                    case false:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }

                InicialDeLaCasilla = inicialDevolver(a);


                if (añadirEspacio)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 7)
                        {
                            Console.WriteLine("        ");
                        }
                        else
                        {
                            Console.Write("        ");
                        }
                        color = !color;

                        switch (color)
                        {
                            case true:
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.White;
                                break;
                            case false:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                    }
                }

                if (InicialDeLaCasilla == " ")
                {
                    if (counter % 8 == 0)
                    {
                        Console.WriteLine("        ");
                        color = !color;
                        añadirEspacio = true;
                    }
                    else
                    {
                        Console.Write("        ");
                        añadirEspacio = false;
                    }
                }
                else
                {
                    if (counter % 8 == 0)
                    {
                        Console.WriteLine("   " + InicialDeLaCasilla.ToString().ToUpper() + "   ");
                        color = !color;
                        añadirEspacio = true;
                    }
                    else
                    {
                        Console.Write("   " + InicialDeLaCasilla.ToString().ToUpper() + "   ");
                        añadirEspacio = false;
                    }
                }

                if (añadirEspacio)
                {
                    for (int i = 0; i < 8; i++)
                    {

                        switch (color)
                        {
                            case true:
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.White;
                                break;
                            case false:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                        color = !color;

                        if (i == 7)
                        {
                            Console.WriteLine("        ");
                        }
                        else
                        {
                            Console.Write("        ");
                        }


                    }
                }

                counter++;
            }
        }

        public static string inicialDevolver(string b)
        {
            string InicialDeLaCasilla = " ";


            foreach (Pieza c in piezas)
            {
                if (c != null && c.Posicion == b)
                {
                    InicialDeLaCasilla = DevolverColor(c).ToString();
                    InicialDeLaCasilla += c.InicilPieza.ToString();
                }
            }
            return InicialDeLaCasilla;
        }

        public static char DevolverColor(Pieza c)
        {
            switch (c.Blanca)
            {
                case true:
                    return 'B';
                case false:
                    return 'N';
            }

            return ' ';
        }

        public static string[,] LugaresOcupados()
        {
            string[,] output = new string[3, 32];
            int indice = 0;

            foreach (Pieza p in piezas)
            {
                if (p != null)
                {
                    output[0, indice] = p.Posicion;

                    if (p.Blanca == true)
                    {
                        output[1, indice] = p.Posicion;
                    }
                    else if (p.Blanca == false)
                    {
                        output[2, indice] = p.Posicion;
                    }
                }

                indice++;
            }


            return output;
        }

        public static void inicializacionDeTodo()
        {
            Peon peonA2 = new Peon('p', "A2", true);
            Peon peonB2 = new Peon('p', "B2", true);
            Peon peonC2 = new Peon('p', "C2", true);
            Peon peonD2 = new Peon('p', "D2", true);
            Peon peonE2 = new Peon('p', "E2", true);
            Peon peonF2 = new Peon('p', "F2", true);
            Peon peonG2 = new Peon('p', "G2", true);
            Peon peonH2 = new Peon('p', "H2", true);

            Peon[] IBpeones = { peonA2, peonB2, peonC2, peonD2, peonE2, peonF2, peonG2, peonH2 };
            peonesBlancos = IBpeones;


            Peon peonA8 = new Peon('p', "A7", false);
            Peon peonB8 = new Peon('p', "B7", false);
            Peon peonC8 = new Peon('p', "C7", false);
            Peon peonD8 = new Peon('p', "D7", false);
            Peon peonE8 = new Peon('p', "E7", false);
            Peon peonF8 = new Peon('p', "F7", false);
            Peon peonG8 = new Peon('p', "G7", false);
            Peon peonH8 = new Peon('p', "H7", false);

            Peon[] INpeones = { peonA8, peonB8, peonC8, peonD8, peonE8, peonF8, peonG8, peonH8 };
            peonesNegros = INpeones;

            Torre torreA1 = new Torre('t', "A1", true);
            Torre torreH1 = new Torre('t', "H1", true);

            Torre[] IBtorresBlancas = { torreA1, torreH1 };
            torresBlancas = IBtorresBlancas;

            Torre torreA8 = new Torre('t', "A8", false);
            Torre torreH8 = new Torre('t', "H8", false);

            Torre[] INtorresNegras = { torreA8, torreH8 };
            torresNegras = INtorresNegras;

            Caballo caballoB1 = new Caballo('c', "B1", true);
            Caballo caballoF1 = new Caballo('c', "G1", true);

            Caballo[] IBcaballosBlancos = { caballoB1, caballoF1 };
            caballosBlancos = IBcaballosBlancos;

            Caballo caballoB8 = new Caballo('c', "B8", false);
            Caballo caballoG8 = new Caballo('c', "G8", false);

            Caballo[] INcaballosNegros = { caballoB8, caballoG8 };
            caballosNegros = INcaballosNegros;

            Alfil alfilC1 = new Alfil('a', "C1", true);
            Alfil alfilF1 = new Alfil('a', "F1", true);

            Alfil[] IBalfilesBlancos = { alfilC1, alfilF1 };
            alfilesBlancos = IBalfilesBlancos;

            Alfil alfilC8 = new Alfil('a', "C8", false);
            Alfil alfilF8 = new Alfil('a', "F8", false);

            Alfil[] INalfilesNegros = { alfilC8, alfilF8 };
            alfilesNegros = INalfilesNegros;

            Rey ReyBlanco = new Rey('r', "E1", true);
            Rey[] IBReyBlanco = { ReyBlanco };
            reyBlanco = IBReyBlanco;
            Rey ReyNegro = new Rey('r', "E8", false);
            Rey[] INReyNegro = { ReyNegro };
            reyNegro = INReyNegro;

            Dama DamaBlanca = new Dama('d', "D1", true);
            Dama[] IBDamaBlanca = { DamaBlanca };
            damaBlanca = IBDamaBlanca;
            Dama DamaNegra = new Dama('d', "D8", false);
            Dama[] INDamaNegra = { DamaNegra };
            damaNegra = INDamaNegra;


            int fila = 0;
            for (int col = 0; col < peonesBlancos.Length; col++) piezas[fila, col] = peonesBlancos[col];
            fila++;
            for (int col = 0; col < peonesNegros.Length; col++) piezas[fila, col] = peonesNegros[col];
            fila++;
            for (int col = 0; col < torresBlancas.Length; col++) piezas[fila, col] = torresBlancas[col];
            fila++;
            for (int col = 0; col < torresNegras.Length; col++) piezas[fila, col] = torresNegras[col];
            fila++;
            for (int col = 0; col < caballosBlancos.Length; col++) piezas[fila, col] = caballosBlancos[col];
            fila++;
            for (int col = 0; col < caballosNegros.Length; col++) piezas[fila, col] = caballosNegros[col];
            fila++;
            for (int col = 0; col < alfilesBlancos.Length; col++) piezas[fila, col] = alfilesBlancos[col];
            fila++;
            for (int col = 0; col < alfilesNegros.Length; col++) piezas[fila, col] = alfilesNegros[col];
            fila++;
            piezas[fila++, 0] = reyBlanco[0];
            piezas[fila++, 0] = reyNegro[0];
            piezas[fila++, 0] = damaBlanca[0];
            piezas[fila++, 0] = damaNegra[0];
        }

    }

    public class Pieza
    {
        public char InicilPieza { set; get; }
        public string Posicion { get; set; }
        public bool Blanca { get; set; }
        protected bool Comida { get; set; }

        protected virtual bool PiezaValida(char InicialPieza)
        {
            switch (InicialPieza)
            {
                case 'p':
                    return true;
                case 't':
                    return true;
                case 'c':
                    return true;
                case 'a':
                    return true;
                case 'r':
                    return true;
                case 'd':
                    return true;
            }

            return false;

        }

        protected int LetraNumero(char letra)
        {
            int numero = letra == 'A' ? 1 : letra == 'B' ? 2 : letra == 'C' ? 3 : letra == 'D' ? 4 : letra == 'E' ? 5 : letra == 'F' ? 6 : letra == 'G' ? 7 : 8;

            return numero;
        }

        protected char NumeroLetra(int numero)
        {
            char letra = numero == 1 ? 'A' : numero == 2 ? 'B' : numero == 3 ? 'C' : numero == 4 ? 'D' : numero == 5 ? 'E' : numero == 6 ? 'F' : numero == 7 ? 'G' : 'H';

            return letra;
        }

        public virtual string[] Mover(string[] posicionesOcupadas, string[] posicionesOcupadasBlancas, string[] posicionesOcupadasNegras)
        {
            return null;
        }
    }

    public class Peon : Pieza
    {
        public Peon(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }

        public override string[] Mover(string[] posicionesOcupadas, string[] posicionesOcupadasBlancas, string[] posicionesOcupadasNegras)
        {
            char letra = (char)Posicion[0];
            char numero = (char)Posicion[1];
            string calculoDePosicion = "";

            int letraNumero = LetraNumero(letra);
            int numeroNumero = Int32.Parse(numero.ToString());

            string[] PosicionesValidas = new string[4];

            if (Blanca)
            {
                //Mover 1
                calculoDePosicion = NumeroLetra(letraNumero).ToString() + (numeroNumero + 1).ToString();
                PosicionesValidas[0] = calculoDePosicion;
                //Mover 2 si es el primer movimiento
                if (Posicion[1] == '2')
                {
                    calculoDePosicion = NumeroLetra(letraNumero).ToString() + (numeroNumero + 2).ToString();
                    PosicionesValidas[1] = calculoDePosicion;
                }

                //Comer
                if ((char)Posicion[0] != 'H')
                {
                    calculoDePosicion = NumeroLetra(letraNumero + 1).ToString() + (numeroNumero + 1).ToString();
                    PosicionesValidas[2] = calculoDePosicion;
                }

                if ((char)Posicion[0] != 'A')
                {
                    calculoDePosicion = NumeroLetra(letraNumero - 1).ToString() + (numeroNumero + 1).ToString();
                    PosicionesValidas[3] = calculoDePosicion;
                }
            }

            if (!Blanca)
            {
                //Mover 1
                calculoDePosicion = NumeroLetra(letraNumero).ToString() + (numeroNumero - 1).ToString();
                PosicionesValidas[0] = calculoDePosicion;
                //Mover 2 si es el primer movimiento
                if (Posicion[1] == '7')
                {
                    calculoDePosicion = NumeroLetra(letraNumero).ToString() + (numeroNumero - 2).ToString();
                    PosicionesValidas[1] = calculoDePosicion;
                }

                //Comer
                if ((char)Posicion[0] != 'H')
                {
                    calculoDePosicion = NumeroLetra(letraNumero + 1).ToString() + (numeroNumero - 1).ToString();
                    PosicionesValidas[2] = calculoDePosicion;
                }

                if ((char)Posicion[0] != 'A')
                {
                    calculoDePosicion = NumeroLetra(letraNumero - 1).ToString() + (numeroNumero - 1).ToString();
                    PosicionesValidas[3] = calculoDePosicion;
                }
            }

            //Descartar si esta cogida la casilla o si hay alguna pieza para comer
            int counter = 0;
            foreach (string a in PosicionesValidas)
            {
                bool xs = false;
                foreach (string b in posicionesOcupadas)
                {
                    if (counter == 0 || counter == 1)
                    {
                        if (a == b)
                        {
                            PosicionesValidas[counter] = null;
                        }
                    }

                    if (counter == 2 || counter == 3)
                    {
                        if (a == b)
                        {
                            xs = true;
                        }
                    }
                }

                if (!xs && counter == 2 || counter == 3)
                {
                    PosicionesValidas[counter] = null;
                }
                counter++;
            }

            return PosicionesValidas;
        }
    }

    public class Torre : Pieza
    {
        public Torre(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }

        public override string[] Mover(string[] posicionesOcupadas, string[] posicionesOcupadasBlancas, string[] posicionesOcupadasNegras)
        {
            char letra = (char)Posicion[0];
            char numero = (char)Posicion[1];
            string calculoDePosicion = "";

            int letraNumero = LetraNumero(letra);
            int numeroNumero = Int32.Parse(numero.ToString());

            string[] PosicionesValidas = new string[14];

            int[] letrasPosibles = new int[7];
            int[] numerosPosibles = new int[7];

            int comprobarLetra = letraNumero;
            int comprobarNumero = numeroNumero;


            //Esto mira las posiciones posibles
            bool seguir = true;
            //Letras
            int counter1 = 0;
            while (comprobarLetra != 1 && seguir == true)
            {
                char l = NumeroLetra(comprobarLetra);
                foreach (string c in posicionesOcupadas)
                {
                    if (l.ToString() + Posicion[1] == c)
                    {
                        seguir = false;
                    }
                }


                if (seguir)
                {
                    comprobarLetra--;

                    letrasPosibles[counter1] = comprobarLetra;
                    counter1++;
                }
            }

            seguir = true;
            while (comprobarLetra != 7 && seguir == true)
            {
                char l = NumeroLetra(comprobarLetra);

                foreach (string c in posicionesOcupadas)
                {
                    if (l.ToString() + Posicion[1] == c)
                    {
                        seguir = false;
                    }
                }

                if (seguir)
                {
                    comprobarLetra++;

                    letrasPosibles[counter1] = comprobarLetra;
                    counter1++;
                }
            }

            //Numeros
            counter1 = 0;

            seguir = true;
            while (comprobarNumero != 1 && seguir == true)
            {

                foreach (string c in posicionesOcupadas)
                {
                    if (Posicion[0] + comprobarNumero.ToString() == c)
                    {
                        seguir = false;
                    }
                }

                if (seguir)
                {
                    comprobarNumero--;

                    numerosPosibles[counter1] = comprobarNumero;
                    counter1++;
                }
            }

            seguir = true;
            while (comprobarNumero != 8 && seguir == true)
            {

                foreach (string c in posicionesOcupadas)
                {
                    if (Posicion[0] + comprobarNumero.ToString() == c)
                    {
                        seguir = false;
                    }
                }

                if (seguir)
                {
                    comprobarNumero++;

                    numerosPosibles[counter1] = comprobarNumero;
                    counter1++;
                }
            }

            //Añade las posiciones al array final
            int counter = 0;
            foreach (int i in letrasPosibles)
            {
                char l = NumeroLetra(i);
                if (i != 0)
                {
                    PosicionesValidas[counter] = l.ToString() + Posicion[1];
                    counter++;
                }
            }

            foreach (int i in numerosPosibles)
            {
                if (i != 0)
                {
                    PosicionesValidas[counter] = Posicion[0] + i.ToString();
                    counter++;
                }
            }

            return PosicionesValidas;
        }
    }

    public class Caballo : Pieza
    {
        public Caballo(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }
    }

    public class Alfil : Pieza
    {
        public Alfil(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }
    }

    internal class Rey : Pieza
    {
        public Rey(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }
    }

    internal class Dama : Pieza
    {
        public Dama(char inicial, string posicion, bool blanco)
        {
            this.InicilPieza = inicial;
            this.Posicion = posicion;
            this.Blanca = blanco;
        }
    }
}