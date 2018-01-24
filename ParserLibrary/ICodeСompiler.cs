namespace ParserLibrary
{
    /// <summary>
    /// Интерфейс компиляции на основе правила
    /// </summary>
    public interface ICodeСompiler
    {
        /// <summary>
        /// Компилирует входные данные в строку на основе правила
        /// </summary>
        /// <param name="ruleNumber">Номер правила грамматики</param>
        /// <param name="wordsInStack">Входные данные, для компиляции</param>
        /// <returns>Скомпилированная строка</returns>
        string Compil(int ruleNumber, Word[] wordsInStack);
    }
}