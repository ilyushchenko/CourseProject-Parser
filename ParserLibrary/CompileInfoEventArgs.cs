using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary
{
    public class CompileInfoEventArgs : EventArgs
    {

        public CompileInfoEventArgs(Queue<Word> words, LinkedList<Word> stack, List<int> rules)
        {
            Words = words;
            Rules = rules;
            Stack = stack;
            Message = GetCompileInfo();
        }

        public CompileInfoEventArgs(Queue<Word> words, LinkedList<Word> stack, List<int> rules, string message)
        {
            Words = words;
            Rules = rules;
            Stack = stack;
            Message = message;
        }

        public Queue<Word> Words { get; }
        public LinkedList<Word> Stack { get; }
        public List<int> Rules { get; }
        public string Message { get; }

        public string GetCompileInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Входная строка: ");
            foreach (var word in Words)
            {
                sb.Append($"{word.Value} ");
            }
            sb.AppendLine();
            sb.Append("Магазин: ");
            foreach (var arrVal in Stack)
            {
                sb.Append($"{arrVal.Value} ");
            }
            sb.AppendLine();
            sb.Append("Правила: ");
            foreach (var rule in Rules)
            {
                sb.Append($"{rule} ");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
