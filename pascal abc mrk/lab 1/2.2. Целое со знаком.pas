    // Разбор целого со знаком с помощью синтаксических диаграмм
    var 
      ch: Char;
      pos: integer;
      st: String;
     
    procedure error();
    begin
      writeln('^':pos);
      writeln('Ошибка в символе ',ch);
      halt;
    end;
     
    procedure NextCh;
    begin
      read(ch);
      pos += 1;  
    end; 
     
    begin
      NextCh;
      if ch in ['+','-'] then
      begin
        st+=ch;
        NextCh;
      end;
     
      if char.IsDigit(ch) then
      begin
        st+=ch;
        NextCh
      end
      else error;
     
      while char.IsDigit(ch) do
      begin
        st+=ch;
        NextCh
      end;
    
      if ch<>#13 then
        error;
     
      writeln('Распознано целое число: ',st);
    end.