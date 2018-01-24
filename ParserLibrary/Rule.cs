namespace ParserLibrary
{
    /// <summary>
    /// Структура правила
    /// </summary>
    public struct Rule
    {
        public Rule(int ruleWordNumber, int[] ruleList)
        {
            RuleWordNumber = ruleWordNumber;
            RuleList = ruleList;
        }
        
        /// <summary>
        /// Номер символа правила
        /// </summary>
        public int RuleWordNumber;
        /// <summary>
        /// Количетво символов в правиле
        /// </summary>
        public int CountOfWords => RuleList.Length;
        /// <summary>
        /// Набор правил
        /// </summary>
        public int[] RuleList;
    }
}

