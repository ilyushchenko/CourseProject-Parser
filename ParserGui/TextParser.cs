using System;
using System.Collections.Generic;
using System.Linq;
using ParserLibrary;

namespace ParserGui
{
    public class TextParser : ITextParser
    {
        public TextParser(IEnumerable<Word> words)
        {
            Words = words;
            Word endTerminal = Words.Last();
            EndTerminal = new Word(endTerminal);
        }

        public IEnumerable<Word> Words { get; }
        public Word EndTerminal { get; }

        /// <summary>
        /// Парсит входную строку
        /// </summary>
        /// <param name="text">Строка, для парсинга</param>
        /// <returns>Очередь распарщенных слов</returns>
        public IEnumerable<Word> Parse(string text)
        {
            text = text.Trim();
            string[] list = text.Split(' ');

            Queue<string> words = new Queue<string>(list);

            //if (words.Contains("const"))
            //{
            //    throw new Exception("const - служебное слово");
            //}

            // Коллекция строк, которые были получены в результате парсинга строки
            Queue<Word> splittedWords = new Queue<Word>();

            while (words.Count > 0)
            {
                string word = words.Dequeue();

                Word foundWord = Words.FirstOrDefault(item =>
                    item.Value == word && 
                    item.Value != "const" && item.Value != "id");
                if (foundWord != null)
                {
                    throw new Exception($"Ошибка при разборе строки{Environment.NewLine}Служебные символы должны выделяться специяальным символом #{Environment.NewLine}Пример: #{word}#");
                }
                word = word.Trim('#');
                foundWord = Words.FirstOrDefault(item =>
                    item.Value == word &&
                    item.Value != "const" && item.Value != "id");

                if (foundWord != null)
                {
                    splittedWords.Enqueue(new Word(foundWord.Number, foundWord.Value));
                }
                else
                {
                    if (word.ToLower() == "true" || word.ToLower() == "false")
                    {
                        splittedWords.Enqueue(new Word(13, "const") { Temp = word.ToLower() });
                    }
                    else
                    {
                        int nuber;
                        if (int.TryParse(word, out nuber))
                        {
                            if (word.Contains('9') || word.Contains('8'))
                            {
                                throw new Exception($"Ошибка, при определении константы{Environment.NewLine}Допускается ввод цифр от 0 до 7");
                            }
                            //if (word[0] != '0')
                            //{
                            //    throw new Exception($"Ошибка, при определении константы{Environment.NewLine}Числа вводятся в 8 системе, начиная с 0{Environment.NewLine}Пример: 0{nuber}");
                            //}
                            splittedWords.Enqueue(new Word(13, "const") { Temp = $"0{word}" });
                        }
                        else
                        {
                            const int lengthLimit = 8;
                            if (word.Length > lengthLimit)
                            {
                                throw new Exception($"Ошибка, при определении переменной {word}{Environment.NewLine}Длинна идентификатора не должена быть больше {lengthLimit} символов");
                            }
                            else
                            {

                                int length = word.Length;
                                while (words.Peek() == "")
                                {
                                    words.Dequeue();
                                    length++;
                                }
                                if (length < lengthLimit)
                                {
                                    throw new Exception($"Ошибка, при определении переменной {word}{Environment.NewLine}Длинна идентификатора не должена быть меньше {lengthLimit} символов.{Environment.NewLine}Оставшаяся длинна заполнятся пробелами{Environment.NewLine}Текущая длинна: {length}");
                                }
                                if (length > lengthLimit)
                                {
                                    throw new Exception($"Ошибка, при определении переменной {word}{Environment.NewLine}Длинна идентификатора не должена быть больше {lengthLimit} символов. Избыточное количество пробелов{Environment.NewLine}Текущая длинна: {length}");
                                }
                                splittedWords.Enqueue(new Word(10, "id") { Temp = word });
                            }
                        }
                    }
                }
            }
            Word endWord = Words.FirstOrDefault(item => item.Value == "$");
            if (endWord != null)
            {
                splittedWords.Enqueue(new Word(endWord.Number, endWord.Value));
            }
            else
            {
                throw new Exception("Символ конца строки не найден");
            }


            return splittedWords;
        }
    }
}