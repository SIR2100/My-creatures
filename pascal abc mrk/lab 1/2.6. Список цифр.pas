// Список цифр, разделенных одним или несколькими пробелами. 
// В качестве семантического действия должно быть накопление 
// списка цифр в списке и вывод этого списка в конце программы.

var
  ch: Char;
  pos: integer;
  flag: integer;
  root: List<char>;

procedure error();
begin
  writeln('^':pos);
  writeln('Ошибка: ', ch);
  halt;
end;

procedure NextCh;
begin
  read(ch);
  pos += 1;
end;

begin
  root := new List<char>();
  NextCh;
  //Проверяем, цифра ли это
  if ch in ['0'..'9'] then
  begin
    root.Add(ch);
    NextCh;
  end
  else 
	error;
  flag := 0;
  //Если это Цифра или пробел
  while (ch in ['0'..'9']) or (ch = ' ') do
  begin
    if (ch in ['0'..'9']) and (flag = 1) then
    begin
      root.add(ch);
      flag := 0;
      NextCh;
    end
    else 
		if ch = ' ' then 
		begin 
			NextCh;flag := 1 
		end
    else 
		error;
  end;
  
  if ch <> #13 then
    error;
  root.Print();
  
end.