uses SimpleLangLexer,SimpleLangParser;
 
begin 
  Init('input.txt');
  Progr;
  if LexKind=Tok.EOF then
  begin
  writeln('==============================');
  writeln('Программа распознана правильно ');
  end
  else syntaxerror('ожидался конец файла');
end.