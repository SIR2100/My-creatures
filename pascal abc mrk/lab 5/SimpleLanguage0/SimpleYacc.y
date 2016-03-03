%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO WRITE OPENBRACKET CLOSEBRACKET IF THEN ELSE COMMA VAR PLUS SUBS MULT DIV

%%

progr   : block
		;

stlist	: statement 
		| stlist SEMICOLON statement 
		;

statement: assign
		| block  
		| cycle  
		| while
		| repeat
		| for
		| write
		| if
        | var
		;

ident 	: ID 
		;
	
assign 	: ident ASSIGN expr 
		;

expr	: T
        | expr PLUS T
        | expr SUBS T
		;
        
T       : F
        | T MULT F
        | T DIV F
        ;
        
F       : ident
        | INUM
        | OPENBRACKET expr CLOSEBRACKET
        ;
        
block	: BEGIN stlist END 
		;

cycle	: CYCLE expr statement 
		;
		
while	: WHILE expr DO stlist
		;
		
repeat	: REPEAT stlist UNTIL expr
		;
		
for		: FOR assign TO expr DO stlist
		;

write	: WRITE OPENBRACKET expr CLOSEBRACKET
		;
		
if		: IF expr THEN stlist
		| IF expr THEN stlist ELSE stlist
		;
		
var     : VAR ident
        | var COMMA ident
        ;
%%
