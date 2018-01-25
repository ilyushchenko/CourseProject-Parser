using System;
using ParserLibrary;

namespace ParserGui
{
    public class Compiler : ICodeСompiler
    {
        /// <summary>
        /// Производит компиляцию, на основе цепочки данных в стеке
        /// </summary>
        /// <param name="ruleNumber">Испольщуемое правило</param>
        /// <param name="wordsInStack">Коллекция символов в стеке</param>
        public string Compil(int ruleNumber, Word[] wordsInStack)
        {
            // S -> C if W
            if (ruleNumber == 0)
                //return $"if ( {wordsInStack[2].Temp} ) {{ {wordsInStack[0].Temp}; }}";
                return $"if ( {wordsInStack[2].Temp} ) {wordsInStack[0].Temp};";
            // S -> C if W S
            if (ruleNumber == 1)
                //return $"if ( {wordsInStack[3].Temp} ) {{ {wordsInStack[1].Temp}; }}{Environment.NewLine}{wordsInStack[0].Temp}";
                return $"if ( {wordsInStack[3].Temp} ) {wordsInStack[1].Temp};{Environment.NewLine}{wordsInStack[0].Temp}";
            // C -> L A
            if (ruleNumber == 2)
                return $"{wordsInStack[1].Temp} {wordsInStack[0].Temp}";
            // W -> S
            if (ruleNumber == 3)
                return wordsInStack[0].Temp;
            // W -> X A
            if (ruleNumber == 4)
                return $"{wordsInStack[1].Temp} {wordsInStack[0].Temp}";
            // W -> X C
            if (ruleNumber == 5)
                return $"{wordsInStack[1].Temp} {wordsInStack[0].Temp}";
            // X -> := id
            if (ruleNumber == 6)
                return $"{wordsInStack[0].Temp} =";
            // X -> := id [ Y ]
            if (ruleNumber == 7)
                return $"{wordsInStack[3].Temp} [ {wordsInStack[1].Temp} ] =";
            // A -> id
            if (ruleNumber == 8)
                return wordsInStack[0].Temp;
            // A -> const
            if (ruleNumber == 9)
                return wordsInStack[0].Temp;
            // Y -> A
            if (ruleNumber == 10)
                return wordsInStack[0].Temp;
            // A -> id [ Y ]
            if (ruleNumber == 11)
                return $"{wordsInStack[3].Temp} [ {wordsInStack[1].Temp} ]";
            // A -> sqr A
            if (ruleNumber == 12)
                return $"{wordsInStack[0].Temp} * {wordsInStack[0].Temp}";
            // A -> R A
            if (ruleNumber == 13)
                return $"{wordsInStack[1].Temp} {wordsInStack[0].Temp}";
            // L -> = A
            if (ruleNumber == 14)
                return $"{wordsInStack[0].Temp} ==";
            // L -> ! A
            if (ruleNumber == 15)
                return $"{wordsInStack[0].Temp} !=";
            // L -> > A
            if (ruleNumber == 16)
                return $"{wordsInStack[0].Temp} >";
            // L -> < A
            if (ruleNumber == 17)
                return $"{wordsInStack[0].Temp} <";
            // R -> - A
            if (ruleNumber == 18)
                return $"{wordsInStack[0].Temp} -";
            return "";
        }
    }
}