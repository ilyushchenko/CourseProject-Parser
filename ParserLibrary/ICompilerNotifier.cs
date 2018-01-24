namespace ParserLibrary
{
    /// <summary>
    /// Информационный делегат компиляции
    /// </summary>
    /// <param name="sendler">Объект, сгенерировавший вызов</param>
    /// <param name="e">Дополнительные параметры компиляции (вхадная цепочка, стек, правила)</param>
    public delegate void CompileInfoDelegate(object sendler, CompileInfoEventArgs e);

    /// <summary>
    /// Интерфейс уведомления о событиях процесса компиляции
    /// </summary>
    public interface ICompilerNotifier
    {
        /// <summary>
        /// Событие ошибки компиляции
        /// </summary>
        event CompileInfoDelegate CompileError;

        /// <summary>
        /// Событие конца компиляции
        /// </summary>
        event CompileInfoDelegate CompileDone;

        /// <summary>
        /// Событие в мечении шага компиляции
        /// </summary>
        event CompileInfoDelegate CompileNextStep;
    }
}