unit SimpleLangParser;
 
interface
 
procedure Progr;
procedure Expr;
procedure Assign;
procedure StatementList;
procedure Statement; 
procedure Block;
procedure Cycle;

procedure WhileDoCycle;
procedure ForCycle;
procedure IfThen;

procedure syntaxerror(message: string := '');
 
implementation
 
uses 
  SimpleLangLexer;
 
procedure Progr;
begin
  Block
end;
 
procedure WhileDoCycle;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск while
  Expr;
  if LexKind=Tok.&DO then
  begin
    writeln(TokToString(LexKind));
    NextLexem;
  end
  else syntaxerror('Ожидалось do');
  Statement;
end;

procedure ForCycle;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск for
  if LexKind=Tok.ID then
    begin
      writeln(TokToString(LexKind));
      NextLexem;
    end  
  else syntaxerror('Ожидалось id');
  if LexKind=Tok.ASSIGN then
    begin
      writeln(TokToString(LexKind));
      NextLexem;
    end
  else syntaxerror('Ожидалось assign');
  Expr;
  if LexKind=Tok.&TO then
    begin
      writeln(TokToString(LexKind));
      NextLexem;
    end
  else syntaxerror('Ожидалось to');
  Expr;
    if LexKind=Tok.&DO then
    begin
      writeln(TokToString(LexKind));
      NextLexem;
    end
  else syntaxerror('Ожидалось do');
  Statement;
end;

procedure IfThen;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск if
  Expr;
  if LexKind=Tok.&THEN then
  begin
    writeln(TokToString(LexKind));
    NextLexem;
  end
  else syntaxerror('Ожидалось then');
  Statement;
  if LexKind=Tok.&ELSE then
  begin
    writeln(TokToString(LexKind));
    NextLexem;
  end
  else exit;
  Statement;
end;


 
procedure Statement;
begin
  case LexKind of
    Tok.&Begin: Block; 
    Tok.Cycle: Cycle;
    Tok.Id: Assign;
    Tok.&While: WhileDoCycle;
    Tok.&For: ForCycle;
    Tok.&If: IfThen;
    else syntaxerror('Ожидался оператор');
  end;
end;
 
procedure Block;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск begin
  StatementList;
  if LexKind=Tok.&End then
  begin
    writeln(TokToString(LexKind));
    NextLexem;
  end
  else syntaxerror('Ожидалось end');
end;
 
procedure Cycle;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск cycle
  Expr;
  Statement;
end;
 
procedure Assign;
begin
  writeln(TokToString(LexKind));
  NextLexem; // Пропуск id
  if LexKind = Tok.ASSIGN then
  begin
    writeln(TokToString(LexKind));
    NextLexem;
  end
  else syntaxerror('Ожидалось :=');  
  Expr;  
end;
 
procedure StatementList;
begin
  Statement;
  while LexKind=Tok.SEMICOLON do
  begin
    writeln(TokToString(LexKind));
    NextLexem;
    Statement;
  end
end;

procedure Expr; // Выражение - это id или num
begin
  if LexKind in [Tok.ID,Tok.INUM] then
    begin
      writeln(TokToString(LexKind));
      NextLexem; 
    end
  else syntaxerror('Ожидалось выражение');
end;
 
 
procedure syntaxerror(message: string);
begin
  var ss := System.IO.File.ReadLines(fname).Skip(LexRow-1).First(); // Строка row файла
  writeln('Синтаксическая ошибка в строке ',LexRow,':');
  writeln(ss);
  writeln('^':LexCol-1);
  if message<>'' then 
    writeln(message);
  Done;  
  halt;
end;
 
end.