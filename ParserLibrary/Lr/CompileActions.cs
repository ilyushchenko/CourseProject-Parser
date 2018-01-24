namespace ParserLibrary.Lr
{
    /// <summary>
    /// Список действий, при компиляции кода
    /// </summary>
    public enum CompileActions
    {
        /// <summary>
        /// Начальное состояние компилятора
        /// </summary>
        Start,
        /// <summary>
        /// Продолжение компиляции
        /// </summary>
        Next,
        /// <summary>
        /// Ошибка компиляции
        /// </summary>
        Error
    }
}