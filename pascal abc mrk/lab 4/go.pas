uses SimpleLangLexer,SimpleLangParser;
 
begin 
  Init('input.txt');
  Progr;
  if LexKind=Tok.EOF then
  begin
  writeln('==============================');
  writeln('��������� ���������� ��������� ');
  end
  else syntaxerror('�������� ����� �����');
end.