using System.Collections.Generic;

namespace ParserLibrary
{
    /// <summary>
    /// Интерфейс компиляции коллекции терминалов грамматики в строку
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Скомпилированый текст
        /// </summary>
        string CompiledText { get; }

        /// <summary>
        /// Компилятор входных данных, на основе правил
        /// </summary>
        ICodeСompiler Compiler { get; set; }

        /// <summary>
        /// Метод компиляции входной строки
        /// </summary>
        /// <param name="inputChain">Входная цепочка символов грамматики</param>
        /// <returns>Возвращает true при успешной компиляции или false в случае ошибки</returns>
        bool Compile(IEnumerable<Word> inputChain);
    }
}
