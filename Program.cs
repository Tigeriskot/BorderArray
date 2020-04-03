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
                // объявление массива граней префиксов
                int[] masBP;
                // объявление массива граней суффиксов
                int[] masBS;
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
                    masBS = new int[textInFile.Length];
                    // демонстрация алгоритма по построению массива граней префиксов
                    PrefixBorderArray(textInFile, masBP);
                    // вывод полученного массива граней
                    Console.WriteLine("Массив граней префиксов: ");
                    for (int i = 0; i < masBP.Length; i++)
                        Console.Write(masBP[i] + " ");
                    Console.WriteLine();
                    // демонстрация алгоритма по построению массива граней суффиксов
                    SuffixBorderArray(textInFile, masBS);
                    // вывод полученного массива граней
                    Console.WriteLine("Массив граней суффиксов: ");
                    for (int i = 0; i < masBS.Length; i++)
                        Console.Write(masBS[i] + " ");

                    // демонстрация алгоритма поиска максимальной грани в строке
                    AlgorithmMaxBorder(textInFile);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Файл не найден");
                    Console.WriteLine(ex.Message);
                }





                Console.Read();



            }

            // алгоритм создания массива граней префиксов
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

            // алгоритм поиска максимальной грани
            static void AlgorithmMaxBorder(string textInFile)
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

            // алгоритм построения массива суффиксов
            static void SuffixBorderArray(string S, int[] bs)
            {
                int n = S.Length; ;
                bs[n - 1] = 0;
                for (int i = n - 2; i >= 0; --i)
                {
                    int bsLeft = bs[i + 1]; // Позиция с конца слева от предыдущей грани
                    while (bsLeft != 0 && (S[i] != S[n - bsLeft - 1])) bsLeft = bs[n - bsLeft];
                    // Длина на 1 больше, чем позиция
                    if (S[i] == S[n - bsLeft - 1]) bs[i] = bsLeft + 1;
                    else bs[i] = 0;
                }
            }




        }






    }




}
