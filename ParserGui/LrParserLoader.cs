using System;
using System.IO;
using ParserLibrary;

namespace ParserGui
{
    public class LrParserLoader
    {
        public LrParserLoader(string pathToWords, string pathToRules, string pathToTable)
        {
            GrammarLoader = new GrammarLoader(pathToWords, pathToRules);
            Words = GrammarLoader.Words;
            Rules = GrammarLoader.Rules;
            ControlTable = LoadTable(pathToTable);
        }

        public GrammarLoader GrammarLoader { get; }

        public Word[] Words { get; private set; }

        public Rule[] Rules { get; private set; }
        
        /// <summary>
        /// Управляющая таблица грамматики
        /// </summary>
        public int[,] ControlTable { get; private set; }

        public ICodeСompiler Compiler { get; set; }

        /// <summary>
        /// Загружает управляющую таблицу из файла
        /// </summary>
        /// <param name="pathToTable">Путь до файла с управляющей таблицей</param>
        public virtual void LoadControlTable(string pathToTable)
        {
            ControlTable = LoadTable(pathToTable);
        }

        /// <summary>
        /// Задает управляющую таблицу
        /// </summary>
        /// <param name="controlTable">Управляющая таблица</param>
        public virtual void LoadControlTable(int[,] controlTable)
        {
            ControlTable = controlTable;
        }

        /// <summary>
        /// Загружает управляющую таблицу из файла
        /// </summary>
        /// <param name="path">Пкть до файла управляющей таблицы</param>
        /// <returns>Управляющая таблица</returns>
        public int[,] LoadTable(string path)
        {
            int[,] arr;
            using (StreamReader sr = new StreamReader(path))
            {
                int count = Int32.Parse(sr.ReadLine());
                arr = new int[count, count];
                for (int row = 0; row < count; row++)
                {
                    string str = sr.ReadLine();
                    for (int col = 0; col < count; col++)
                    {
                        arr[row, col] = Convert.ToInt32(str[col].ToString());
                    }
                }
            }

            return arr;
        }
    }
}
