    // ������ ������ �� ������ � ������� �������������� ��������
    var 
      ch: Char;
      pos: integer;
      st: String;
     
    procedure NextCh;
    begin
      read(ch);
      pos += 1;  
    end; 
    
    procedure error();
    begin
      writeln('^':pos);
      writeln('������ � �������: ',ch);
      halt;
    end;
     
    begin
      NextCh;
      if char.IsLetter(ch) then
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
        if (char.IsLetter(ch)) or (char.IsDigit(ch)) then
        begin
          st+=ch;
          NextCh
        end
        else
        error;
      end;
     
      writeln('�������������: ',st);
    end.