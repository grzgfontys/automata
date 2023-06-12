grammar Assignment5;

statements    
    : statement*
    ;
    
statement
    : functionDeclaration
    | functionCall
    | returnStatement
    | ifStatement
    | whileStatement                             
    | variableDeclaration
    ;
    
functionDeclaration
    : 'function' IDENT functionParameters statementBlock
    ;

functionParameters
    : '(' (functionParameter (',' functionParameter)* )? ')'
    ;

functionParameter
    : IDENT                     # MandatoryParameter
    | IDENT '=' NUMBER          # DefaultParameter
//    | IDENT '?'                 # OptionalParameter
    ;
      
functionCall
    : IDENT functionArguments
    ;
    
functionArguments
    : '(' (expression (',' expression)* )? ')'
    ;
    
ifStatement
    : 'if' booleanExpression statementBlock elseBlock?
    ;
    
elseBlock
    : 'else' statementBlock
    ;
    
whileStatement
    : 'while' booleanExpression statementBlock
    ;

statementBlock
    : '{' statement* '}'
    ;
    
returnStatement
    : 'return' expression?
    ;

variableDeclaration
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
    | functionCall                                  # FunctionCallExpression
    | expression '!'                                # Factorial
    | <assoc=right> expression op='^' expression    # BinaryOperation
    | expression op=('*'|'/') expression            # BinaryOperation
    | expression op=('+'|'-') expression            # BinaryOperation
    | '(' expression ')'                            # ParenthesizedExpression
    ;
    

COMP_OPERATOR   : '>' | '>=' | '<' | '<=' | '==' | '!=' ;
NUMBER          : NONZERO_DIGIT DIGIT* | ZERO;
IDENT           : LETTER (LETTER | DIGIT)*;
//NEWLINE         : '\r'? '\n';
WHITESPACE      : [ \t\n\r]+ -> skip;

fragment NONZERO_DIGIT  : [1-9];
fragment ZERO           : '0';
fragment DIGIT          : ZERO | NONZERO_DIGIT;
fragment LETTER         : [a-zA-Z];