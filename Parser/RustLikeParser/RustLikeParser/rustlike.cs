
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                 =  0, // (EOF)
        SYMBOL_ERROR               =  1, // (Error)
        SYMBOL_WHITESPACE          =  2, // Whitespace
        SYMBOL_MINUS               =  3, // '-'
        SYMBOL_LPAREN              =  4, // '('
        SYMBOL_RPAREN              =  5, // ')'
        SYMBOL_TIMES               =  6, // '*'
        SYMBOL_COMMA               =  7, // ','
        SYMBOL_DIV                 =  8, // '/'
        SYMBOL_SEMI                =  9, // ';'
        SYMBOL__                   = 10, // '_'
        SYMBOL_LBRACE              = 11, // '{'
        SYMBOL_RBRACE              = 12, // '}'
        SYMBOL_PLUS                = 13, // '+'
        SYMBOL_LT                  = 14, // '<'
        SYMBOL_LTEQ                = 15, // '<='
        SYMBOL_LTGT                = 16, // '<>'
        SYMBOL_EQ                  = 17, // '='
        SYMBOL_EQEQ                = 18, // '=='
        SYMBOL_EQGT                = 19, // '=>'
        SYMBOL_GT                  = 20, // '>'
        SYMBOL_GTEQ                = 21, // '>='
        SYMBOL_BREAKSEMI           = 22, // 'break;'
        SYMBOL_ELSE                = 23, // else
        SYMBOL_FOR                 = 24, // for
        SYMBOL_IDENTIFIER          = 25, // Identifier
        SYMBOL_IF                  = 26, // if
        SYMBOL_IN                  = 27, // in
        SYMBOL_INTEGER             = 28, // Integer
        SYMBOL_LET                 = 29, // let
        SYMBOL_LOOP                = 30, // loop
        SYMBOL_MATCH               = 31, // match
        SYMBOL_MUT                 = 32, // mut
        SYMBOL_STRINGLITERAL       = 33, // StringLiteral
        SYMBOL_WHILE               = 34, // while
        SYMBOL_ADDEXP              = 35, // <Add Exp>
        SYMBOL_ARMLIST             = 36, // <ArmList>
        SYMBOL_ASSIGNMENT          = 37, // <Assignment>
        SYMBOL_DEFAULT             = 38, // <Default>
        SYMBOL_EXPRESSION          = 39, // <Expression>
        SYMBOL_FORLOOP             = 40, // <ForLoop>
        SYMBOL_IFSTATEMENT         = 41, // <IfStatement>
        SYMBOL_LOOP2               = 42, // <Loop>
        SYMBOL_MATCHSTATEMENT      = 43, // <MatchStatement>
        SYMBOL_MULTEXP             = 44, // <Mult Exp>
        SYMBOL_MUTEXPR             = 45, // <MutExpr>
        SYMBOL_NEGATEEXP           = 46, // <Negate Exp>
        SYMBOL_PROGRAM             = 47, // <Program>
        SYMBOL_STATEMENT           = 48, // <Statement>
        SYMBOL_STATEMENTLIST       = 49, // <StatementList>
        SYMBOL_VALUE               = 50, // <Value>
        SYMBOL_VARIABLEDECLARATION = 51, // <VariableDeclaration>
        SYMBOL_WHILELOOP           = 52  // <WhileLoop>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                                         =  0, // <Program> ::= <StatementList>
        RULE_STATEMENTLIST                                   =  1, // <StatementList> ::= 
        RULE_STATEMENTLIST2                                  =  2, // <StatementList> ::= <StatementList> <Statement>
        RULE_STATEMENTLIST3                                  =  3, // <StatementList> ::= <Statement>
        RULE_STATEMENT                                       =  4, // <Statement> ::= <VariableDeclaration>
        RULE_STATEMENT2                                      =  5, // <Statement> ::= <Assignment>
        RULE_STATEMENT3                                      =  6, // <Statement> ::= <IfStatement>
        RULE_STATEMENT4                                      =  7, // <Statement> ::= <MatchStatement>
        RULE_STATEMENT5                                      =  8, // <Statement> ::= <ForLoop>
        RULE_STATEMENT6                                      =  9, // <Statement> ::= <WhileLoop>
        RULE_STATEMENT7                                      = 10, // <Statement> ::= <Loop>
        RULE_MUTEXPR_MUT                                     = 11, // <MutExpr> ::= mut
        RULE_MUTEXPR                                         = 12, // <MutExpr> ::= 
        RULE_VARIABLEDECLARATION_LET_IDENTIFIER_EQ_SEMI      = 13, // <VariableDeclaration> ::= let <MutExpr> Identifier '=' <Expression> ';'
        RULE_ASSIGNMENT_IDENTIFIER_EQ                        = 14, // <Assignment> ::= Identifier '=' <Expression>
        RULE_IFSTATEMENT_IF_LBRACE_RBRACE                    = 15, // <IfStatement> ::= if <Expression> '{' <StatementList> '}'
        RULE_IFSTATEMENT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 16, // <IfStatement> ::= if <Expression> '{' <StatementList> '}' else '{' <StatementList> '}'
        RULE_IFSTATEMENT_IF_LBRACE_RBRACE_ELSE               = 17, // <IfStatement> ::= if <Expression> '{' <StatementList> '}' else <IfStatement>
        RULE_MATCHSTATEMENT_MATCH_IDENTIFIER_LBRACE_RBRACE   = 18, // <MatchStatement> ::= match Identifier '{' <ArmList> <Default> '}'
        RULE_ARMLIST                                         = 19, // <ArmList> ::= 
        RULE_ARMLIST_EQGT_COMMA                              = 20, // <ArmList> ::= <ArmList> <Value> '=>' <StatementList> ','
        RULE_ARMLIST_EQGT_COMMA2                             = 21, // <ArmList> ::= <Value> '=>' <StatementList> ','
        RULE_DEFAULT___EQGT                                  = 22, // <Default> ::= '_' '=>' <StatementList>
        RULE_FORLOOP_FOR_IDENTIFIER_IN_LBRACE_RBRACE         = 23, // <ForLoop> ::= for Identifier in <Statement> '{' <StatementList> '}'
        RULE_WHILELOOP_WHILE_LBRACE_RBRACE                   = 24, // <WhileLoop> ::= while <Expression> '{' <StatementList> '}'
        RULE_LOOP_LOOP_LBRACE_BREAKSEMI_RBRACE               = 25, // <Loop> ::= loop '{' <StatementList> 'break;' '}'
        RULE_EXPRESSION_GT                                   = 26, // <Expression> ::= <Expression> '>' <Add Exp>
        RULE_EXPRESSION_LT                                   = 27, // <Expression> ::= <Expression> '<' <Add Exp>
        RULE_EXPRESSION_LTEQ                                 = 28, // <Expression> ::= <Expression> '<=' <Add Exp>
        RULE_EXPRESSION_GTEQ                                 = 29, // <Expression> ::= <Expression> '>=' <Add Exp>
        RULE_EXPRESSION_EQEQ                                 = 30, // <Expression> ::= <Expression> '==' <Add Exp>
        RULE_EXPRESSION_LTGT                                 = 31, // <Expression> ::= <Expression> '<>' <Add Exp>
        RULE_EXPRESSION                                      = 32, // <Expression> ::= <Add Exp>
        RULE_ADDEXP_PLUS                                     = 33, // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        RULE_ADDEXP_MINUS                                    = 34, // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        RULE_ADDEXP                                          = 35, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_TIMES                                   = 36, // <Mult Exp> ::= <Mult Exp> '*' <Negate Exp>
        RULE_MULTEXP_DIV                                     = 37, // <Mult Exp> ::= <Mult Exp> '/' <Negate Exp>
        RULE_MULTEXP                                         = 38, // <Mult Exp> ::= <Negate Exp>
        RULE_NEGATEEXP_MINUS                                 = 39, // <Negate Exp> ::= '-' <Value>
        RULE_NEGATEEXP                                       = 40, // <Negate Exp> ::= <Value>
        RULE_VALUE_IDENTIFIER                                = 41, // <Value> ::= Identifier
        RULE_VALUE_INTEGER                                   = 42, // <Value> ::= Integer
        RULE_VALUE_STRINGLITERAL                             = 43, // <Value> ::= StringLiteral
        RULE_VALUE_LPAREN_RPAREN                             = 44  // <Value> ::= '(' <Expression> ')'
    };

    public class MyParser
    {
        private LALRParser parser;
        public ListBox ErrorBox;
        public ListBox LexicalAnalysisBox;

        public MyParser(string filename, ListBox ErrorBox, ListBox LexicalAnalysisBox)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
            this.ErrorBox = ErrorBox;
            this.LexicalAnalysisBox = LexicalAnalysisBox;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream, ListBox ErrorBox)
        {
            this.ErrorBox = ErrorBox;
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__ :
                //'_'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTGT :
                //'<>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQGT :
                //'=>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAKSEMI :
                //'break;'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //Integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LET :
                //let
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //loop
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MATCH :
                //match
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MUT :
                //mut
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARMLIST :
                //<ArmList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //<Default>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORLOOP :
                //<ForLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTATEMENT :
                //<IfStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP2 :
                //<Loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MATCHSTATEMENT :
                //<MatchStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MUTEXPR :
                //<MutExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEGATEEXP :
                //<Negate Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTLIST :
                //<StatementList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATION :
                //<VariableDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILELOOP :
                //<WhileLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST :
                //<StatementList> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST2 :
                //<StatementList> ::= <StatementList> <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST3 :
                //<StatementList> ::= <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <VariableDeclaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <Assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<Statement> ::= <IfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<Statement> ::= <MatchStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<Statement> ::= <ForLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<Statement> ::= <WhileLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT7 :
                //<Statement> ::= <Loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUTEXPR_MUT :
                //<MutExpr> ::= mut
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUTEXPR :
                //<MutExpr> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATION_LET_IDENTIFIER_EQ_SEMI :
                //<VariableDeclaration> ::= let <MutExpr> Identifier '=' <Expression> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<Assignment> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_LBRACE_RBRACE :
                //<IfStatement> ::= if <Expression> '{' <StatementList> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<IfStatement> ::= if <Expression> '{' <StatementList> '}' else '{' <StatementList> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_LBRACE_RBRACE_ELSE :
                //<IfStatement> ::= if <Expression> '{' <StatementList> '}' else <IfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MATCHSTATEMENT_MATCH_IDENTIFIER_LBRACE_RBRACE :
                //<MatchStatement> ::= match Identifier '{' <ArmList> <Default> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARMLIST :
                //<ArmList> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARMLIST_EQGT_COMMA :
                //<ArmList> ::= <ArmList> <Value> '=>' <StatementList> ','
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARMLIST_EQGT_COMMA2 :
                //<ArmList> ::= <Value> '=>' <StatementList> ','
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT___EQGT :
                //<Default> ::= '_' '=>' <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORLOOP_FOR_IDENTIFIER_IN_LBRACE_RBRACE :
                //<ForLoop> ::= for Identifier in <Statement> '{' <StatementList> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILELOOP_WHILE_LBRACE_RBRACE :
                //<WhileLoop> ::= while <Expression> '{' <StatementList> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP_LOOP_LBRACE_BREAKSEMI_RBRACE :
                //<Loop> ::= loop '{' <StatementList> 'break;' '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_GT :
                //<Expression> ::= <Expression> '>' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LT :
                //<Expression> ::= <Expression> '<' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTEQ :
                //<Expression> ::= <Expression> '<=' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_GTEQ :
                //<Expression> ::= <Expression> '>=' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQEQ :
                //<Expression> ::= <Expression> '==' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTGT :
                //<Expression> ::= <Expression> '<>' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_TIMES :
                //<Mult Exp> ::= <Mult Exp> '*' <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Mult Exp> '/' <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP_MINUS :
                //<Negate Exp> ::= '-' <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP :
                //<Negate Exp> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_IDENTIFIER :
                //<Value> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_INTEGER :
                //<Value> ::= Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_STRINGLITERAL :
                //<Value> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        public void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            ErrorBox.Items.Add(message);
        }

        public void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            ErrorBox.Items.Add(message);
            string message2 = "Expected Token: " + args.ExpectedTokens.ToString();
            ErrorBox.Items.Add(message2);
        }

        public void TokenReadEvent(LALRParser parse, TokenReadEventArgs args)
        {
            string message = args.Token.Text + "    \t \t" + (SymbolConstants)args.Token.Symbol.Id;
            LexicalAnalysisBox.Items.Add(message);
        }

    }
}
