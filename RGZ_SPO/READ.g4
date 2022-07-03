grammar READ;
options
{
    language=CSharp;
}		
prog    : (expr NEWLINE?)+;

expr    : READ term ';';

term    : oper id(','id)*;

oper    : '(' '*' ',' fmt ')';

fmt     : '*' | intg;

intg     : DIGIT (DIGIT)*;
id      : SYMBOL (SYMBOL | DIGIT)*;

READ    : 'r''e''a''d';
NEWLINE : [\r\n]+ ;
SYMBOL  : 'a'..'z'|'A'..'Z';
DIGIT   : '0'..'9' ;
