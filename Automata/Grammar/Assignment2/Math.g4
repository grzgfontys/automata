grammar Math;

expression 
    : NUMBER                                 #Literal
    | expression op=('*'|'/') expression     #BinaryOperation
    | expression op=('+'|'-') expression     #BinaryOperation
    | '(' expression ')'                     #ParenthesizedExpression
    ;



NUMBER      : NONZERO_DIGIT DIGIT*;
WHITESPACE  : [ \t\n\r]+ -> skip;

fragment NONZERO_DIGIT : [1-9];
fragment ZERO : '0';
fragment DIGIT: ZERO | NONZERO_DIGIT;