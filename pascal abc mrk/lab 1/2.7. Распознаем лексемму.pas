//������� ���� aa12c23dd1, � ������� ���������� ������ ���� � ����, � ������ ������ �� ����� 2 ���������.
//� �������� �������������� �������� ���������� �������� ������ ������� � ���� ������.

var
  ch: Char;
  pos: integer;
  s: string;
  i: integer;
  c: integer;

procedure error();
begin
  writeln('^':pos);
  writeln('������ � ������� ', ch);
  halt;
end;

procedure NextCh;
begin
  read(ch);
  pos += 1;  
end;

begin
  i := 0;
  c := 0;
  NextCh;
  if char.IsLetter(ch) then
  begin
    s += ch;
    NextCh;
    c += 1;
  end
  else if char.IsDigit(ch) then
  begin
    s += ch;
    NextCh;
    i += 1;
  end
  else error;
  
  while char.IsDigit(ch) or char.IsLetter(ch) do
    if char.IsLetter(ch) and (c < 2) then
    begin
      s += ch;
      NextCh;
      c += 1;
      i := 0;
    end
    else if char.IsDigit(ch) and (i < 2) then
    begin
      s += ch;
      NextCh;
      i += 1;
      c := 0
    end
    else error;
  
  if ch <> #13 then
    error;
  
  writeln('����������: ', s);
  
end.