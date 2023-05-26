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
    : NUMBER                                #number
    | expression (MULT|DIV) expression      #multiplication
    | expression (PLUS|MINUS) expression    #addition
    | OPENPAREN expression CLOSEPAREN       #parenthesized
    ;