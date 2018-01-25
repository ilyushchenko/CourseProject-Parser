using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ParserLibrary;

namespace ParserGui
{
    public class GrammarLoader
    {    
        protected List<Word> WordsList;
        protected List<Rule> RulesList;
        
        /// <summary>
        /// Коллекция слов грамматики
        /// </summary>
        public Word[] Words => WordsList.ToArray();
        
        /// <summary>
        /// Коллекция правил грамматики
        /// </summary>
        public Rule[] Rules => RulesList.ToArray();

        /// <summary>
        /// Количество правил грамматики (количество уникальных нетерминалов)
        /// </summary>
        public int CountOfRules => Rules.Select(rule => rule.RuleWordNumber).Distinct().Count();
        
        
        public GrammarLoader()
        {
            WordsList = new List<Word>();
            RulesList = new List<Rule>();
        }
        
        /// <summary>
        /// Загружет слова, правила грамматики
        /// </summary>
        /// <param name="pathToWordFile">Путь до файла слов грамматики</param>
        /// <param name="pathToRuleFile">Путь до файла правил грамматики</param>
        public GrammarLoader(string pathToWordFile, string pathToRuleFile)
        {
            WordsList = new List<Word>();
            RulesList = new List<Rule>();

            using (StreamReader wordsReader = new StreamReader(pathToWordFile))
            {
                while (!wordsReader.EndOfStream)
                {
                    string word = wordsReader.ReadLine().Trim();
                    SetWord(word);
                }
            }
            using (StreamReader rulesReader = new StreamReader(pathToRuleFile))
            {
                while (!rulesReader.EndOfStream)
                {
                    string rule = rulesReader.ReadLine().Trim();
                    SetRule(rule);
                }
            }
        }

        /// <summary>
        /// Задает правило в формате [Слово правила] -> [Слово 1] [Слово 2] [Слово N]
        /// </summary>
        /// <param name="word">Строка правила. Задается по формату, с разделением пробелами между словами</param>
        /// <returns></returns>
        public GrammarLoader SetRule(string word)
        {
            string[] words = word.Split();

            if (words.Length > 2 && words[1] == "->")
            {
                // Принимаем в качестве имени правила первый символ
                Word ruleWord = GetWord(words[0]);
                if (ruleWord == null)
                {
                    // Если такого символа не нашлось, то загружаем его
                    SetWord(words[0]);
                    // И получаем ссылку на него
                    ruleWord = GetWord(words[0]);
                }

                List<int> wordNumbers = new List<int>();
                    
                // Создаем список правил
                for (int i = 2; i < words.Length; i++)
                {
                    Word tmpWord = GetWord(words[i]);
                    if (tmpWord == null)
                    {
                        SetWord(words[i]);
                        tmpWord = GetWord(words[i]);
                    }
                    wordNumbers.Add(tmpWord.Number);
                }
                // Задаем правило
                RulesList.Add(new Rule(ruleWord.Number, wordNumbers.ToArray()));
            }
            else
            {
                throw new Exception("Правило не соотвествует формату");
            }
            return this;
        }

        /// <summary>
        /// Задает слово грамматики
        /// </summary>
        /// <param name="word">Задает терминал или нетерминал грамматики</param>
        /// <returns></returns>
        public GrammarLoader SetWord(string word)
        {
            if (GetWord(word) == null)
            {
                WordsList.Add(new Word(WordsList.Count, word));
            }
            return this;
        }

        /// <summary>
        /// Возвращает слово грамматики
        /// </summary>
        /// <param name="word">Символьное представление слова грамматики</param>
        /// <returns>Возвращает слово грамматики типа <see cref="Word"/> Word</returns>
        public Word GetWord(string word)
        {
            return Words.FirstOrDefault(item => item.Value == word);
        }
    }
}