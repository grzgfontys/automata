grammar Assignment5;

statements    
    : (NEWLINE* statement)* NEWLINE*
    | NEWLINE*
    | EOF
    ;
    
statement
    : functionDeclaration
    | functionCall
    | returnStatement
    | ifStatement
    | whileStatement                             
    | variableAssignment
    ;
    
functionDeclaration
    : 'function' functionName expression (',' expression)* statementBlock
    ;  
      
functionCall
    : functionName '(' (expression ','?)+ ')'
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
    : '{' NEWLINE statements returnStatement? NEWLINE '}'
    | '{' NEWLINE* '}' // empty
    ;
    
returnStatement
    : 'return' (expression | statement)?
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
    
functionName
    : KW_PRINT
    | IDENT
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