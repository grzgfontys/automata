grammar Assignment1;

// Parser Rules
file                : human* EOF;
human               : fullName | nameAge | nameTel;
fullName            : NAME WHITESPACE NAME SEMICOLON; // match keyword hello followed by an identifier
nameAge             : NAME WHITESPACE AGE SEMICOLON; // match keyword hello followed by an identifier
nameTel             : NAME WHITESPACE TEL SEMICOLON; // match keyword hello followed by an identifier

// Lexer Rules
NAME                : [A-Z]([a-z])+ ; // match lower-case identifiers
AGE                 : [1-9]([0-9])+; // match lower-case identifiers
TEL                 : [+][1-9]([0-9])+; // match lower-case identifiers
SEMICOLON           : ';';
WHITESPACE          : (' ' | '\t')+ ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;