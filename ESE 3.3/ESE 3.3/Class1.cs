using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESE_3._3
{
    public struct orario
    {
        public int ore;
        public int minuti;
    }

    public struct Lezione
    {
        public string codice;
        public DateTime data;
        public orario inizio;
        public orario fine;
        public string auto;
        public string nome;
        public string tipo;
        public decimal costo;
    }

    public struct auto
    {
        public string nome;
        public int usi;
    }

    class Class1
    {
        public static void OrdinaNom(int num, Lezione[] ele)
        {
            bool scambio = true;
            int ultimo = num - 1;

            while (scambio)
            {
                scambio = false;
                for (int i = 0; i < ultimo; i++)
                {
                    if (String.Compare(ele[i].nome, ele[i + 1].nome) > 0)
                    {
                        swap(i, i + 1, ele);
                        scambio = true;
                    }
                }
                ultimo -= 1;
            }
        }

        public static void OrdinaDat(int num, Lezione[] ele)
        {
            bool scambio = true;
            int ultimo = num - 1;

            while (scambio)
            {
                scambio = false;
                for (int i = 0; i < ultimo; i++)
                {
                    if (ele[i].data > ele[i + 1].data)
                    {
                        swap(i, i + 1, ele);
                        scambio = true;
                    }
                }
                ultimo -= 1;
            }
        }

        private static void swap(int a, int b, Lezione[] ele)
        {
            Lezione tmp = ele[a];
            ele[a] = ele[b];
            ele[b] = tmp;

        }

        public static int cercaCod(int num, Lezione[] ele, string daCercare)
        {
            int x = 0;
            while (x < num)
            {
                if (ele[x].codice == daCercare)
                    return x;

                x += 1;
            }

            return -1;

        }

        public static int Elimina(ref int num, Lezione[] ele, string cerca)
        {
            int found = 0;

            int x = 0;

            while (x < num)
            {
                if (ele[x].tipo == cerca)
                {
                    ele[x] = ele[num - 1];
                    num -= 1;
                    found = found + 1;
                    continue;
                }
                x++;
            }

            return found;
        }

        public static bool ConfrontaOrari(orario ora1, orario ora2)
        {
            int min1 = ora1.minuti + (ora1.ore * 60);
            int min2 = ora2.minuti + (ora2.ore * 60);

            if (min2 > min1)
                return true;
            else
                return false;

        }

        public static auto Usata(int num, Lezione[] ele)
        {
            auto result;
            result.nome = "";
            result.usi = -1;

            auto[] calcoli = new auto[100];
            int nAuto = 0;

            int x = 0;
            while(x<num)
            {
                bool fatto = false;
                int i = 0;
                while(i<nAuto)
                {
                    if (calcoli[i].nome == ele[x].auto)
                    {
                        fatto = true;
                        break;
                    }
                    i++;
                }

                if(!fatto)
                {
                    calcoli[nAuto].nome = ele[x].auto;
                    nAuto += 1;

                    for(int j = x; j<num; j++)
                        if(calcoli[nAuto-1].nome == ele[j].auto)
                            calcoli[nAuto - 1].usi += 1;
                }
                x += 1;
            }

            for (int a = 0; a < nAuto; a++)
                if (calcoli[a].usi > result.usi)
                    result = calcoli[a];

            return result;
        }

    }
}
