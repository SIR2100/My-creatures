uses SimpleLangLexer_2;
 
begin
  Init('a.txt');
  repeat
    writeln(TokToString(LexKind));
    NextLexem;
  until LexKind = Tok.EOF;
end.