grammar Assignment3;

file
    : line*EOF;
    
line
    : keywords '(' ((VARIABLE | expression) ','?)* ')'      # InvokeFunction
    | varAssignment                                         # VariableAssignment
    | expression                                            # LoneExpression
    ;

varAssignment
    : VARIABLE op='=' NUMBER
    | VARIABLE op='=' expression
    ;

expression 
    : NUMBER                                 # Literal
    | VARIABLE                               # NestedVar
    | expression '!'                         # Factorial
    | expression op='^' expression           # BinaryOperation
    | expression op=('*'|'/') expression     # BinaryOperation
    | expression op=('+'|'-') expression     # BinaryOperation
    | '(' expression ')'                     # ParenthesizedExpression
    ;
    
keywords
    : 'print'       # Print
    ;

// Może być to albo tak jak zrobiłem w keywords, wychodzi na to że on z pierwszeństwem traktuje wyszukiwanie konkretnych stringów jako tokenów w parsing rules a nie w lexer rules
//PRINT       : 'print';
NUMBER      : NONZERO_DIGIT DIGIT* | ZERO;
VARIABLE    : LETTERS+;
WHITESPACE  : [ \t\n\r]+ -> skip;
NEWLINE     : '\r'? '\n';

fragment NONZERO_DIGIT  : [1-9];
fragment ZERO           : '0';
fragment DIGIT          : ZERO | NONZERO_DIGIT;
fragment LETTERS        : [a-zA-Z];