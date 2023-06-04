grammar Calculator;

statements    
    : NEWLINE* statement (NEWLINE+ statement)* NEWLINE*
    | NEWLINE*
    | EOF
    ;
    
statement
    : functionCall  
    | ifStatement
    | whileStatement                             
    | variableAssignment                                         
    ;
    
ifStatement
    : 'if' booleanExpression statementBlock NEWLINE? elseBlock?
    ;
    
elseBlock
    : 'else' statementBlock
    ;
    
whileStatement
    : 'while' booleanExpression statementBlock
    ;

statementBlock
    : '{' NEWLINE statements NEWLINE '}'
    | '{' NEWLINE? '}' // empty
    ;
    
functionCall
    : keyword '(' (expression ','?)+ ')'
    ;

variableAssignment
    : IDENT '=' expression           
    ;
    
booleanExpression
    : expression COMP_OPERATOR expression           # Comparison
    | '!' booleanExpression                         # Negation
    | booleanExpression 'and' booleanExpression     # LogicalAnd
    | booleanExpression 'or' booleanExpression      # LogicalOr
    | '(' booleanExpression ')'                     # ParenthesizedBooleanExpression
    ;

expression 
    : NUMBER                                        # Literal
    | IDENT                                         # NestedVar
    | expression '!'                                # Factorial
    | <assoc=right> expression op='^' expression    # BinaryOperation
    | expression op=('*'|'/') expression            # BinaryOperation
    | expression op=('+'|'-') expression            # BinaryOperation
    | '(' expression ')'                            # ParenthesizedExpression
    ;
    
keyword
    : KW_PRINT
    ;


KW_PRINT        : 'print';
COMP_OPERATOR   : '>' | '>=' | '<' | '<=' | '==' | '!=' ;
NUMBER          : NONZERO_DIGIT DIGIT* | ZERO;
IDENT           : LETTER (LETTER | DIGIT)*;
NEWLINE         : '\r'? '\n';
WHITESPACE      : [ \t]+ -> skip;

fragment NONZERO_DIGIT  : [1-9];
fragment ZERO           : '0';
fragment DIGIT          : ZERO | NONZERO_DIGIT;
fragment LETTER         : [a-zA-Z];