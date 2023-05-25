grammar Math;

NUMBER      : [1-9][0-9]*;
OPENPAREN   : '(';
CLOSEPAREN  : ')';
OPERATOR    : [*/+-];
WHITESPACE  : (' ' | [\t\r\n]) -> skip;

expression : NUMBER
           | expression OPERATOR expression
           | OPENPAREN expression CLOSEPAREN
           ;