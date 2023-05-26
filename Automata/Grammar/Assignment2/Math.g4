grammar Math;

NUMBER      : [1-9][0-9]*;
OPENPAREN   : '(';
CLOSEPAREN  : ')';
PLUS        : '+';
MINUS       : '-';
MULT        : '*';
DIV         : '/';
WHITESPACE  : [ \t\n\r]+ -> skip;

expression 
    : NUMBER                                    #Literal
    | expression op=(MULT|DIV) expression       #MultiplicationDivision
    | expression op=(PLUS|MINUS) expression     #AdditionSubtraction
    | OPENPAREN expression CLOSEPAREN           #ParenthesizedExpression
    ;