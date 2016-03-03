    // Разбор целого со знаком с помощью синтаксических диаграмм
    var 
      ch: Char;
      pos: integer;
      st: String;
      IsPrevDig: Boolean;
     
    procedure NextCh;
    begin
      read(ch);
      pos += 1;  
    end; 
    
    procedure error();
    begin
      writeln('^':pos);
      writeln('Ошибка в символе: ',ch);
      halt;
    end;
     
    begin
      NextCh;
      if char.IsLetter(ch) then
      begin
        st+=ch;
        NextCh;
        IsPrevDig := false;
      end
      else
      begin
        error;
      end;
      
      while ch <> #13 do
      begin
        if char.IsDigit(ch) then
          if IsPrevDig = false then 
          begin
            st+=ch;
            IsPrevDig := true;
            NextCh;
          end
          else
          error
        else
        if char.IsLetter(ch) then
          if IsPrevDig = true then
          begin
            st+=ch;
            IsPrevDig := false;
            NextCh;
          end
          else
          error;
      end;
     
      writeln('Все окей: ',st);
    end.