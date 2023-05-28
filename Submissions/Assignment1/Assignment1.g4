grammar Assignment1; // Define a grammar called Assignment1

// Parser Rules
humans               : (fullName | nameAge | nameTel | NEWLINE)+; // match either fullName, nameAge, nameTel
fullName            : NAME WHITESPACE NAME; // match NAME identifier, keyword ' ', NAME identifier followed by keyword ';'
nameAge             : NAME WHITESPACE AGE; // match NAME identifier, keyword ' ', AGE identifier followed by keyword ';'
nameTel             : NAME WHITESPACE TEL; // match NAME identifier, keyword ' ', TEL identifier followed by keyword ';'

// Lexer Rules
NAME                : [A-Z]([a-z])+ ; // match name identifiers
AGE                 : [1-9]([0-9])+; // match age identifiers
TEL                 : [+][1-9]([0-9])+; // match telephone identifiers
WHITESPACE          : (' ' | '\t') ; // match white spaces identifiers
NEWLINE             : ('\r'? '\n' | '\r')+ ; // match new line identifiers
// WS : [\t\r\n]+ -> skip ;