using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ParserGui.Annotations;
using ParserLibrary;

namespace ParserGui
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITextParser _textParser;
        private readonly CompileData _compileData;
        private readonly LrParser _lrParser;

        private bool _updateCompiled = false;

        public MainWindow()
        {
            _compileData = new CompileData();
            
            DataContext = _compileData;

            InitializeComponent();

            _compileData.CodeToCompile = "#= var      5 #if #= jjj      #[ #- kk       #sqr var      #] 0 #if #:= namename 345 #> 123 51213 #if #:= name4321 1241 #= 05435 7 #if #:= name1234 #[ name4321 #] #- 3 1";

            GrammarLoader grammarLoader = new GrammarLoader();
            grammarLoader = grammarLoader
                .SetWord("S")
                .SetWord("C")
                .SetWord("W")
                .SetWord("X")
                .SetWord("A")
                .SetWord("Y")
                .SetWord("L")
                .SetWord("R")
                .SetWord("if")
                .SetWord(":=")
                .SetWord("id")
                .SetWord("[")
                .SetWord("]")
                .SetWord("const")
                .SetWord("sqr")
                .SetWord("=")
                .SetWord("!")
                .SetWord(">")
                .SetWord("<")
                .SetWord("-")
                .SetWord("$");
            grammarLoader = grammarLoader
                .SetRule("S -> C if W")
                .SetRule("S -> C if W S")
                .SetRule("C -> L A")
                .SetRule("W -> S")
                .SetRule("W -> X A")
                .SetRule("W -> X C")
                .SetRule("X -> := id")
                .SetRule("X -> := id [ Y ]")
                .SetRule("A -> id")
                .SetRule("A -> const")
                .SetRule("Y -> A")
                .SetRule("A -> id [ Y ]")
                .SetRule("A -> sqr A")
                .SetRule("A -> R A")
                .SetRule("L -> = A")
                .SetRule("L -> ! A")
                .SetRule("L -> > A")
                .SetRule("L -> < A")
                .SetRule("R -> - A");
            int[,] ruleTable =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,0,3,},
                {0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,3,3,3,3,0,3,},
                {2,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,3,},
                {0,2,0,0,2,0,1,1,0,0,1,0,0,1,1,1,1,1,1,1,0,},
                {0,0,0,0,0,0,0,0,3,0,3,0,3,3,3,3,3,3,3,3,3,},
                {0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {1,1,2,1,0,0,1,0,0,1,0,0,0,0,0,1,1,1,1,0,0,},
                {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,3,0,3,2,3,3,3,3,3,3,3,3,3,},
                {0,0,0,0,1,2,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,0,0,0,0,3,0,3,0,3,3,3,3,3,3,3,3,3,},
                {0,0,0,0,0,0,0,0,3,0,3,0,3,3,3,3,3,3,3,3,3,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,0,0,0,2,0,0,1,0,0,1,0,0,1,1,0,0,0,0,1,0,},
                {0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,},
            };

            _textParser = new TextParser(grammarLoader.Words.Skip(grammarLoader.CountOfRules).ToArray());

            _lrParser = new LrParser(grammarLoader.Words, grammarLoader.Rules, ruleTable);
            _lrParser.Compiler = new Compiler();
            _lrParser.CompileError += LrParserCompileError;
            _lrParser.CompileDone += LrParserCompileDone;
            _lrParser.CompileNextStep += LrParserCompileNextStep;
        }

        private void LrParserCompileError(object sendler, CompileInfoEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void LrParserCompileDone(object sendler, CompileInfoEventArgs e)
        {
            if (_updateCompiled)
            {
                _compileData.CompiledCode = _lrParser.CompiledText;
            }
        }

        private void LrParserCompileNextStep(object sendler, CompileInfoEventArgs e)
        {
            _compileData.LrRuleInfo += e + Environment.NewLine;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.ScrollToEnd();
            }
        }

        private void UpRule_Click(object sender, RoutedEventArgs e)
        {
            _updateCompiled = false;
            _compileData.LrRuleInfo = String.Empty;
            ((ICompiler) _lrParser).Compile(_textParser.Parse(_compileData.CodeToCompile));
        }

        private void StartCompile_Click(object sender, RoutedEventArgs e)
        {
            _updateCompiled = true;
            _compileData.LrRuleInfo = String.Empty;
            _compileData.CompiledCode = String.Empty;
            ((ICompiler)_lrParser).Compile(_textParser.Parse(_compileData.CodeToCompile));
        }
    }
}
