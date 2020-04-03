using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderArray
{
    using System;
    using System.IO;
    using System.Text;

    namespace FirstLabaAlg
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("************* Лабораторные работы №2 и №3 *************\n************* Реализация алгоритмов поиска максимальной грани" +
                    " и построения массива граней *************\n");
                // переменная в которую будут записываться данные из файла
                String textInFile;
                // объявление массива граней
                int[] masBP;
                // считываем исходный текст из файла в переменную TextInFile
                Console.WriteLine("Введите название файла в котором будет происходить поиск\n");
               
                
                try
                {   // чтение данных из файла
                    using (StreamReader sr = new StreamReader(Console.ReadLine()))
                    {
                        textInFile = sr.ReadToEnd();
                        Console.WriteLine("Текст файла: ");
                        Console.WriteLine(textInFile);






                    }
                    masBP = new int[textInFile.Length];

                    // демонстрация алгоритма по построению массива граней
                    PrefixBorderArray(textInFile, masBP);
                    // вывод полученного массива граней
                    Console.WriteLine("Массив граней: ");
                    for (int i = 0; i < masBP.Length; i++)
                        Console.Write(masBP[i] + " ");
                    // демонстрация алгоритма поиска максимальной грани в строке
                    algorithmnextnext(textInFile);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Файл не найден");
                    Console.WriteLine(ex.Message);
                }





                Console.Read();



            }

            //Алгоритм создания массива граней

            static void PrefixBorderArray(string S, int[] bp)
            {
                int n = S.Length;
                int bpRight;
                bp[0] = 0;
                for (int i = 1; i < n; ++i)
                { // i –длина рассматриваемого префикса
                    bpRight = bp[i - 1]; // Позиция справа от предыдущей грани
                    while (bpRight != 0 && S[i] != S[bpRight]) bpRight = bp[bpRight - 1];
                    // Длина на 1 больше, чем позиция
                    if (S[i] == S[bpRight]) bp[i] = bpRight + 1;
                    else bp[i] = 0;

                }


            }

            // Алгоритм поиска максимальной грани

            static void algorithmnextnext(string textInFile)
            {
                int n = textInFile.Length;
                int br = 0;
                int j;
                for (int i = n - 1; i != 0; --i)
                { // i – предполагаемая длина грани
                    j = 0;
                    while (j < i && textInFile[j] == textInFile[n - i + j]) ++j;
                    if (j == i && br <= i) br = i;

                }

                if (br == 0)
                    Console.WriteLine("Данная строка не имеет граней");
                else
                    Console.WriteLine("\nДлинна максимальной грани: " + br);
            }


           



        }






    }




}
