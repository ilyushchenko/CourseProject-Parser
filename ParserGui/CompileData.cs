using System.ComponentModel;
using System.Runtime.CompilerServices;
using ParserGui.Annotations;

namespace ParserGui
{
    public class CompileData : INotifyPropertyChanged
    {
        private string _codeToCompile;
        private string _compiledCode;
        private string _lrRuleInfo;

        public string LrRuleInfo
        {
            set
            {
                _lrRuleInfo = value;
                OnPropertyChanged(nameof(LrRuleInfo));
            }
            get { return _lrRuleInfo; }
        }

        public string CodeToCompile
        {
            set
            {
                _codeToCompile = value;
                OnPropertyChanged(nameof(CodeToCompile));
            }
            get
            {
                return _codeToCompile;
            }
        }

        public string CompiledCode
        {
            get { return _compiledCode; }
            set
            {
                _compiledCode = value;
                OnPropertyChanged(nameof(CompiledCode));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}