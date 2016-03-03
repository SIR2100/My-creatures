// Распознаем лексемму - Кавычки (одинарные), без кавычек внутри пары 
var
  ch: Char;
  pos: integer;
  s: string;

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
  
  if ch = #39 then
  begin
    s += ch;
    NextCh;
  end
  else error;
  
  while (ch <> #39) and (ch <> #13) do
  begin
    s += ch;
    NextCh;
  end;
  
  if ch = #39 then
  begin
    s += ch;
    NextCh;
  end
  else error;
  
  if ch <> #13 then
    error;
  
  writeln('Результат: ', s);
  
end.