namespace BorderArray
{
    using System;
    using System.IO;

    namespace FirstLabaAlg
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("************* Лабораторные работы по 2 лекции *************\n************* Реализация алгоритмов по работе с гранями *************\n");
                // переменная в которую будут записываться данные из файла
                String TextInFile;

                // объявление массива граней префиксов
                int[] MasBP;

                // объявление массива граней суффиксов
                int[] MasBS;

                // объявление модифицированного массива граней префиксов
                int[] MasBPM;

                // считываем исходный текст из файла в переменную TextInFile

                Console.WriteLine("Введите название файла в котором будет происходить поиск: ");
               
                
                try
                {   // чтение данных из файла
                    using (StreamReader sr = new StreamReader(Console.ReadLine()))
                    {
                        TextInFile = sr.ReadToEnd();
                        Console.WriteLine("Текст файла: ");
                        Console.WriteLine(TextInFile);






                    }
                    MasBP = new int[TextInFile.Length];
                    MasBS = new int[TextInFile.Length];
                    MasBPM = new int[TextInFile.Length];

                    // демонстрация алгоритма по построению массива граней префиксов
                    PrefixBorderArray(TextInFile, MasBP);
                    // вывод полученного массива граней
                    Console.WriteLine("Массив граней префиксов: ");
                    for (int i = 0; i < MasBP.Length; i++)
                        Console.Write(MasBP[i] + " ");
                    Console.WriteLine();

                    // демонстрация алгоритма по построению массива граней суффиксов
                    SuffixBorderArray(TextInFile, MasBS);
                    // вывод полученного массива граней
                    Console.WriteLine("Массив граней суффиксов: ");
                    for (int i = 0; i < MasBS.Length; i++)
                        Console.Write(MasBS[i] + " ");
                    Console.WriteLine();

                    // демонстрация алгоритма по построению модифицированного массива граней префиксов
                    PrefixBorderArrayM(TextInFile, MasBP, MasBPM);
                    // вывод полученного массива граней
                    Console.WriteLine("Модифицированны массив граней префиксов с использованием исходного файла: ");
                    for (int i = 0; i < MasBPM.Length; i++)
                        Console.Write(MasBPM[i] + " ");
                    Console.WriteLine();

                    // демонстрация алгоритма по построению модифицированного массива граней префиксов без использования исходного файла
                    BPToBPM(MasBP, MasBPM, TextInFile.Length);
                    // вывод полученного массива граней
                    Console.WriteLine("Модифицированны массив граней префиксов без использованием исходного файла: ");
                    for (int i = 0; i < MasBPM.Length; i++)
                        Console.Write(MasBPM[i] + " ");
                    // демонстрация алгоритма поиска максимальной грани в строке
                    AlgorithmMaxBorder(TextInFile);

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Файл не найден");
                    Console.WriteLine(ex.Message);
                }





                Console.Read();



            }

            // алгоритм создания массива граней префиксов
            static void PrefixBorderArray(string TextInFile, int[] bp)
            {
                int n = TextInFile.Length;
                int bpRight;
                bp[0] = 0;
                for (int i = 1; i < n; ++i)
                { // i –длина рассматриваемого префикса
                    bpRight = bp[i - 1]; // Позиция справа от предыдущей грани
                    while (bpRight != 0 && TextInFile[i] != TextInFile[bpRight]) bpRight = bp[bpRight - 1];
                    // Длина на 1 больше, чем позиция
                    if (TextInFile[i] == TextInFile[bpRight]) bp[i] = bpRight + 1;
                    else bp[i] = 0;

                }


            }

            // алгоритм поиска максимальной грани
            static void AlgorithmMaxBorder(string TextInFile)
            {
                int n = TextInFile.Length;
                int br = 0;
                int j;
                for (int i = n - 1; i != 0; --i)
                { // i – предполагаемая длина грани
                    j = 0;
                    while (j < i && TextInFile[j] == TextInFile[n - i + j]) ++j;
                    if (j == i && br <= i) br = i;

                }

                if (br == 0)
                    Console.WriteLine("Данная строка не имеет граней");
                else
                    Console.WriteLine("\nДлинна максимальной грани: " + br);
            }

            // алгоритм построения массива суффиксов
            static void SuffixBorderArray(string TextInFile, int[] bs)
            {
                int n = TextInFile.Length; 
                bs[n - 1] = 0;
                for (int i = n - 2; i >= 0; --i)
                {
                    int bsLeft = bs[i + 1]; // Позиция с конца слева от предыдущей грани
                    while (bsLeft != 0 && (TextInFile[i] != TextInFile[n - bsLeft - 1])) bsLeft = bs[n - bsLeft];
                    // Длина на 1 больше, чем позиция
                    if (TextInFile[i] == TextInFile[n - bsLeft - 1]) bs[i] = bsLeft + 1;
                    else bs[i] = 0;
                }
            }

            // алгоритм создания массива граней префиксов (модифицированный)
            static void PrefixBorderArrayM(string TextInFile, int[] bp,int[] bpm)
            {
                int n = TextInFile.Length;
                bpm[0] = 0; bpm[n - 1] = bp[n - 1];
                for (int i = 1; i < n - 1; ++i)
                { // Проверка совпадения следующих символов
                    if (bp[i] != 0 && (TextInFile[bp[i]] == TextInFile[i + 1])) bpm[i] = bpm[bp[i] - 1];
                    else bpm[i] = bp[i];
                }
               
                
            }

            // алгоритм построения модифицированного массива префиксов
            // при построение не используется исходный текст
            static void BPToBPM(int[] bp,int[] bpm,int n)
            {
                bpm[0] = 0; bpm[n - 1] = bp[n - 1];
                for (int i = 1; i < n - 1; ++i)
                {
                    // Проверка «совпадения следующих символов»
                    if (bp[i] != 0 && (bp[i] + 1 == bp[i + 1])) bpm[i] = bpm[bp[i] - 1];
                    else bpm[i] = bp[i];
                }
            }
           







        }






    }




}
