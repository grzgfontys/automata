grammar Assignment3;

file
    : line*EOF
    ;
    
line
    : functionCall                                         
    | variableAssignment                                         
    ;
    
functionCall
    : keyword '(' (expression ','?)+ ')'
    ;

variableAssignment
    : IDENT '=' expression           
    ;

expression 
    : NUMBER                                 # Literal
    | IDENT                                  # NestedVar
    | expression '!'                         # Factorial
    | expression op='^' expression           # BinaryOperation
    | expression op=('*'|'/') expression     # BinaryOperation
    | expression op=('+'|'-') expression     # BinaryOperation
    | '(' expression ')'                     # ParenthesizedExpression
    ;
    
keyword
    : KW_PRINT
    ;

KW_PRINT    : 'print';
NUMBER      : NONZERO_DIGIT DIGIT* | ZERO;
IDENT       : LETTER (LETTER | DIGIT)*;
WHITESPACE  : [ \t\n\r]+ -> skip;
NEWLINE     : '\r'? '\n';

fragment NONZERO_DIGIT  : [1-9];
fragment ZERO           : '0';
fragment DIGIT          : ZERO | NONZERO_DIGIT;
fragment LETTER         : [a-zA-Z];