using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeworkKovshov1
{
    public class Permutation
    {
        /// <summary>
        /// Путь до файла с входными данными.
        /// </summary>
        public const string Input = "input.txt";

        /// <summary>
        /// Путь до файла с выходными данными.
        /// </summary>
        public const string Output = "output.txt";

        /// <summary>
        /// Главный метод класса (единтсвенный публичный) в котором и выполняется вся работа.
        /// </summary>
        public void Run()
        {
            var inputStreamReader = new StreamReader(OpenInputToRead());
            var firstLine = ReadFirstLine(inputStreamReader);
            var secondLine = ReadSecondLine(inputStreamReader);
            var firstDictionary = StringToDictionary(firstLine);
            var secondDictionary = StringToDictionary(secondLine);
            WriteResult(EqualsDictionary(firstDictionary, secondDictionary).ToString());
        }

        /// <summary>
        /// Открытие потока на чтение из входного файла <see cref="Input"/>.
        /// </summary>
        /// <returns>Открытый поток на чтение</returns>
        /// <exception cref="InputFileNotExistException">При отсутсвии файла</exception>
        /// <exception cref="InputFileException">При ошибке открытия потока на чтение</exception>
        private static FileStream OpenInputToRead()
        {
            if (!File.Exists(Input)) throw new InputFileNotExistException();
            try
            {
                return File.OpenRead(Input);
            }
            catch (Exception)
            {
                throw new InputFileException();
            }
        }

        /// <summary>
        /// Чтение первой строки из открытого потока на чтение.
        /// </summary>
        /// <param name="inputStreamReader">Открытый поток на чтение</param>
        /// <returns>Первая строка</returns>
        /// <exception cref="InputFirstLineException">При отсутсвии первой строки</exception>
        private static string ReadFirstLine(TextReader inputStreamReader)
        {
            try
            {
                var line = inputStreamReader.ReadLine();
                if (line == null) throw new InputFirstLineException();
                return line;
            }
            catch (Exception)
            {
                throw new InputFirstLineException();
            }
        }

        /// <summary>
        /// Чтение второй строки из открытого потока на чтение.
        /// </summary>
        /// <param name="inputStreamReader">Открытый поток на чтение</param>
        /// <returns>Вторая строка</returns>
        /// <exception cref="InputSecondLineException">При отсутсвии второй строки</exception>
        private static string ReadSecondLine(TextReader inputStreamReader)
        {
            try
            {
                var line = inputStreamReader.ReadLine();
                if (line == null) throw new InputSecondLineException();
                return line;
            }
            catch (Exception)
            {
                throw new InputSecondLineException();
            }
        }

        /// <summary>
        /// Превращание строки в структуру данных словарь <see cref="Dictionary{TKey,TValue}"/>,
        /// где ключами являются символы строки, а значениями - их количество.
        /// </summary>
        /// <param name="line">Строка, которую надо преобразовать</param>
        /// <returns>Словарь полученный при преобразовании строки</returns>
        private static Dictionary<char, int> StringToDictionary(string line)
        {
            var dictionary = new Dictionary<char, int>(line.Length);
            foreach (var symbol in line)
            {
                if (dictionary.ContainsKey(symbol))
                    dictionary[symbol] = dictionary[symbol] + 1;
                else
                    dictionary.Add(symbol, 1);
            }

            return dictionary;
        }

        /// <summary>
        /// Сравнение двух словарей <see cref="Dictionary{TKey,TValue}"/> на равенство. 
        /// </summary>
        /// <param name="var1">Первый словарь для сравнения</param>
        /// <param name="var2">Второй словарь для сравнения</param>
        /// <returns>True если словари равны, False если не равны</returns>
        private static bool EqualsDictionary(IDictionary<char, int> var1, IDictionary<char, int> var2)
        {
            return var1.Count == var2.Count && var1.Keys.All(key => var2.ContainsKey(key) && var1[key] == var2[key]);
        }

        /// <summary>
        /// Запись результата в выходной файл <see cref="Output"/>. 
        /// </summary>
        /// <param name="result">Результат для записи в файл</param>
        /// <exception cref="OutputFileException">При ошибке записи в файл</exception>
        private static void WriteResult(string result)
        {
            try
            {
                var streamWriter = new StreamWriter(Output);
                streamWriter.WriteLine(result);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw new OutputFileException();
            }
        }
    }

    public class InputFileNotExistException : Exception
    {
        public InputFileNotExistException() : base("Input file can not exist")
        {
        }
    }

    public class InputFileException : Exception
    {
        public InputFileException() : base("Input file can not be open to read")
        {
        }
    }

    public class InputFirstLineException : Exception
    {
        public InputFirstLineException() : base("Input file not contains first line")
        {
        }
    }

    public class InputSecondLineException : Exception
    {
        public InputSecondLineException() : base("Input file not contains second line")
        {
        }
    }

    public class OutputFileException : Exception
    {
        public OutputFileException() : base("Output file can not be open to write")
        {
        }
    }
}