//Вещественное с десятичной точкой 123.45678.
var
  ch: Char;
  pos: integer;
  input: string;
  sum: real;

procedure error();
begin
  writeln('^':pos);
  writeln('Ошибка в символе ', ch);
  halt;
end;

procedure NextCh;
begin
  read(ch);
  pos += 1;  
end;

begin
  NextCh;
  
  if char.IsDigit(ch)  then
  begin
    input += ch;
    NextCh;
  end
  else error;
  
  while char.IsDigit(ch) or (ch = '.') do
  begin
    if (ch = '.') then
    begin
      input += ',';
      NextCh;
      if (ch = #13) then
        error;
    end;
    input += ch;
    NextCh;
    
  end;
  
  if ch <> #13 then
    error;
  sum := double.Parse(input);
  writeln('Распознано вещественное число: ', sum);
  
end.