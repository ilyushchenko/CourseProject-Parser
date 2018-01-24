namespace ParserLibrary
{
    /// <summary>
    /// Структура символов грамматики
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Стандартный конструктор слова грамматики
        /// </summary>
        public Word()
        {
            Number = 0;
            Value = string.Empty;
            Temp = string.Empty;
        }

        /// <summary>
        /// Задает номер правила и значение слова
        /// </summary>
        /// <param name="number">Номер правила</param>
        /// <param name="value">Значение слова</param>
        public Word(int number, string value)
        {
            Number = number;
            Value = value;
            Temp = string.Empty;
        }

        /// <summary>
        /// Конструктор копирования слова грамматики
        /// </summary>
        /// <param name="wordToCopy"></param>
        public Word(Word wordToCopy)
        {
            Number = wordToCopy.Number;
            Value = wordToCopy.Value;
            Temp = wordToCopy.Temp;
        }

        /// <summary>
        /// Номер слова
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Значение слова
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Дополнительная информация о слове
        /// </summary>
        public string Temp { get; set; }
    }
}
