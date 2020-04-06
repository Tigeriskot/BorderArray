using System;
using System.IO;

namespace BorderArray
{
   

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************* Лабораторные работы по 2 лекции *************\n************* Реализация алгоритмов по работе с гранями *************\n");
           
            // считываем исходный текст из файла в переменную TextInFile
            
            Console.WriteLine("Введите название файла в котором будет происходить поиск: ");
               
                
            try
            {   // чтение данных из файла
                using (StreamReader sr = new StreamReader(Console.ReadLine()))
                {
                    string TextInFile = sr.ReadToEnd();
                    Console.WriteLine("Текст файла: ");
                    Console.WriteLine(TextInFile);
                    // метод, который демонстрирует работу алгоритмов
                    Realization(TextInFile);

                }


            }
            catch (Exception ex)
            {

                Console.WriteLine("Файл не найден");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Если хотите ввести текст вручную, напишите 1");

                if (Console.ReadLine() == "1")
                {
                    string Text = Console.ReadLine();
                    Realization(Text);
                }


            }





            Console.Read();



        }


        // в этом методе происходит демонстрация работы всех алгоритмов из 2-ой лекции
        static void Realization(string Text)
        {   // объявление массива граней префиксов
            int[] MasBP;

            // объявление массива граней суффиксов
            int[] MasBS;

            // объявление модифицированного массива граней префиксов
            int[] MasBPM;

            // объявление модифицированного массива граней суффиксов
            int[] MasBSM;

            MasBP = new int[Text.Length];
            MasBS = new int[Text.Length];
            MasBPM = new int[Text.Length];
            MasBSM = new int[Text.Length];

            // демонстрация алгоритма по построению массива граней префиксов
            PrefixBorderArray(Text, MasBP);
            // вывод полученного массива граней
            Console.WriteLine("Массив граней префиксов: ");
            for (int i = 0; i < MasBP.Length; i++)
                Console.Write(MasBP[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению массива граней суффиксов
            SuffixBorderArray(Text, MasBS);
            // вывод полученного массива граней
            Console.WriteLine("Массив граней суффиксов: ");
            for (int i = 0; i < MasBS.Length; i++)
                Console.Write(MasBS[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению модифицированного массива граней префиксов
            PrefixBorderArrayM(Text, MasBP, MasBPM);
            // вывод полученного массива граней
            Console.WriteLine("Модифицированны массив граней префиксов, построенный с использованием исходного файла: ");
            for (int i = 0; i < MasBPM.Length; i++)
                Console.Write(MasBPM[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению модифицированного массива граней префиксов без использования исходного файла
            BPToBPM(MasBP, MasBPM, Text.Length);
            // вывод полученного массива граней
            Console.WriteLine("Модифицированны массив граней префиксов, построенный без использованием исходного файла: ");
            for (int i = 0; i < MasBPM.Length; i++)
                Console.Write(MasBPM[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению массива граней префиксов
            BPMToBP(MasBPM, MasBP, Text.Length);
            // вывод полученного массива граней
            Console.WriteLine("Массив граней префиксов, построенный из модифицированного: ");
            for (int i = 0; i < MasBP.Length; i++)
                Console.Write(MasBP[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению модифицированного массива граней суффиксов без использования исходного файла
            BSToBSM(MasBS, MasBSM, Text.Length);

            // вывод полученного массива граней
            Console.WriteLine("Модифицированны массив граней суффиксов, построенный без использованием исходного файла: ");
            for (int i = 0; i < MasBSM.Length; i++)
                Console.Write(MasBSM[i] + " ");
            Console.WriteLine();

            // демонстрация алгоритма по построению массива граней cуффиксов
            BSMToBS(MasBSM, MasBS, Text.Length);

            // вывод полученного массива граней
            Console.WriteLine("Массив граней суффиксов, построенный из модифицированного: ");
            for (int i = 0; i < MasBS.Length; i++)
                Console.Write(MasBS[i] + " ");
            Console.WriteLine();


            // демонстрация алгоритма поиска максимальной грани в строке
            AlgorithmMaxBorder(Text);


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
                while (bpRight != 0 && TextInFile[i] != TextInFile[bpRight]) 
                    bpRight = bp[bpRight - 1];
                // Длина на 1 больше, чем позиция
                if (TextInFile[i] == TextInFile[bpRight]) 
                    bp[i] = bpRight + 1;
                else bp[i] = 0;

            }


        }

        // алгоритм поиска максимальной грани(префикса)
        static void AlgorithmMaxBorder(string TextInFile)
        {
            int n = TextInFile.Length;
            int br = 0;
            int j;
            for (int i = n - 1; br == 0 && i > 0; --i)
            { // i – предполагаемая длина грани
                j = 0;
                while (j < i && TextInFile[j] == TextInFile[n - i + j])
                    ++j;
                if (j == i) br = i;

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
                while (bsLeft != 0 && (TextInFile[i] != TextInFile[n - bsLeft - 1])) 
                    bsLeft = bs[n - bsLeft];
                // Длина на 1 больше, чем позиция
                if (TextInFile[i] == TextInFile[n - bsLeft - 1]) 
                    bs[i] = bsLeft + 1;
                else bs[i] = 0;
            }
        }

        // алгоритм создания массива граней префиксов (модифицированный)
        static void PrefixBorderArrayM(string TextInFile, int[] bp, int[] bpm)
        {
            int n = TextInFile.Length;
            bpm[0] = 0; bpm[n - 1] = bp[n - 1];
            for (int i = 1; i < n - 1; ++i)
            { // Проверка совпадения следующих символов
                if (bp[i] != 0 && (TextInFile[bp[i]] == TextInFile[i + 1]))
                    bpm[i] = bpm[bp[i] - 1];
                else bpm[i] = bp[i];
            }
               
                
        }

        // алгоритм построения модифицированного массива граней префиксов
        // при построение не используется исходный текст
        static void BPToBPM(int[] bp, int[] bpm, int n)
        {
            bpm[0] = 0; bpm[n - 1] = bp[n - 1];
            for (int i = 1; i < n - 1; ++i)
            {
                // Проверка совпадения следующих символов
                if (bp[i] != 0 && (bp[i] + 1 == bp[i + 1]))
                    bpm[i] = bpm[bp[i] - 1];
                else bpm[i] = bp[i];
            }
        }

        // алгоритм перестроения модифицированного массива граней префиксов в обычный массив
        static void BPMToBP(int[] bpm, int[] bp, int n)
        {
            bp[n - 1] = bpm[n - 1]; 
            bp[0] = 0;
            for (int i = n - 2; i > 0; --i) 
                bp[i] = Math.Max(bp[i + 1] - 1, bpm[i]);
        }

        // алгоритм построения модифицированного массива суффиксов
        // при построение не используется исходный текст
        static void BSToBSM(int[] bs, int[] bsm, int  n)
        {
            bsm[n - 1] = 0; bsm[0] = bs[0];
            for (int i = n - 2; i > 0; --i)
            {
                // Проверка совпадения предыдущих символов
                if (bs[i] != 0 && (bs[i] + 1 == bs[i - 1])) bsm[i] = bsm[n - bs[i]];
                else bsm[i] = bs[i];
            }
        }

        // алгоритм перестроения модифицированного массива граней суффиксов в обычный массив
        static void BSMToBS(int[] bsm, int[] bs, int n)
        {
            bs[0] = bsm[0];
            bs[n - 1] = 0;
            for (int i = 1; i < n - 1; ++i) 
                bs[i] = Math.Max(bs[i - 1] - 1, bsm[i]);
        }










    }




}
