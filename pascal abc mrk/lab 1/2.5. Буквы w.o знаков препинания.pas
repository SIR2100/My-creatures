//Список букв, разделенных символом , или ; В качестве семантического действия должно 
//быть накопление списка букв в списке и вывод этого списка в конце программы. 
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
      if (char.IsLetter(ch) or char.IsDigit(ch)) then
      begin
        st+=ch;
        NextCh;
      end
      else
      begin
        error;
      end;
      
      while ch <> #13 do
      begin
        if char.IsLetter(ch)  then
          begin
            st+=ch;
            NextCh;
          end
          else
          if (ch = ',') or (ch = ';') then
          NextCh
          else
          error;
      end;
     
      writeln('Результат: ',st);
    end.