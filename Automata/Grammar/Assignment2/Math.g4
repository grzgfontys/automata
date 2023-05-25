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
    : NUMBER
    | expression (MULT|DIV) expression
    | expression (PLUS|MINUS) expression
    | OPENPAREN expression CLOSEPAREN
    ;