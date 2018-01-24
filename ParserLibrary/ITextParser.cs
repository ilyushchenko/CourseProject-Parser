using System.Collections.Generic;

namespace ParserLibrary
{
    /// <summary>
    /// Интерфейс парсера входной строки
    /// </summary>
    public interface ITextParser
    {
        /// <summary>
        /// Коллекция терминалов
        /// </summary>
        IEnumerable<Word> Words { get; }

        /// <summary>
        /// Символ конца цепочки
        /// </summary>
        Word EndTerminal { get; }

        /// <summary>
        /// Парсит входную строку, используя коллекцию терминалов
        /// </summary>
        /// <param name="text">Входная строка</param>
        /// <returns>Коллекция терминалов грамматики</returns>
        IEnumerable<Word> Parse(string text);
    }
}