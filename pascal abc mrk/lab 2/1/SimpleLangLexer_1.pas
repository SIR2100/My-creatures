unit SimpleLangLexer_1;
 
interface
 
// TLex - ������������ ��� - ��� ������� ����������
// lexEot - ����� ������ ���������
type 
  Tok = (EOF, ID, INUM, COLON, SEMICOLON, ASSIGN, &BEGIN, &END, CYCLE, COMMA, SUM, DIF, MULT, DIVISION, &DIV, &MOD, &AND, &OR, &NOT);
 
var 
  fname: string;           // ��� ����� ���������
  LexRow,LexCol: integer;  // ������-������� ������ �������. ����� ������� = LexCol+Length(LexText)
  LexKind: Tok;            // ��� �������   
  LexText: string;         // ����� �������
  LexValue: integer;       // ����� ��������, ��������� � �������� lexNum
 
procedure NextLexem;
procedure Init(fn: string);
procedure Done;
function TokToString(t: Tok): string;
 
implementation
 
var 
  ch: Char;         // ������� ������
  f: text;          // ������� ����
  row,col: integer; // ������� ������ � ������� � �����
  KeywordsMap := new Dictionary<string,Tok>; // �������, �������������� �������� ������ ��������� ���� TLex. ���������������� ���������� InitKeywords
 
 
procedure LexError(message: string); // ������ ������������ �����������
begin
  var ss := System.IO.File.ReadLines(fname).Skip(row-1).First(); // ������ row �����
  writeln('����������� ������ � ������ ',row,':');
  writeln(ss);
  writeln('^':col-1);
  if message<>'' then 
    writeln(message);
  Done;
  halt;
end;
 
procedure NextCh;
begin
  // � LexText ������������� ���������� ������ � ����������� ��������� ������
  LexText += ch;
  if not f.Eof then
  begin
    read(f,ch);
    if ch<>#10 then
      col += 1
    else
    begin
      row += 1;
      col := 1;
    end;
  end
  else 
  begin
    ch := #0; // ���� ��������� ����� �����, �� ������������ #0
    Done;
  end;
end; 
 
procedure PassSpaces;
begin
  while char.IsWhiteSpace(ch) do
    NextCh;
end;
 
procedure NextLexem;
begin
  PassSpaces;
  // R � ����� ������� ������ ������ ������� ������ � ch
  LexText := '';
  LexRow := Row;
  LexCol := Col;
// ��� ������� ������������ �� �� ������� �������
// ��� ������ ������� �������� �������������� ���������
  case ch of
  ';': begin
         NextCh;
         LexKind := Tok.SEMICOLON;
       end;  
    ':':
      begin
        NextCh;
        if ch = '=' then
        begin
          NextCh;
          LexKind := Tok.ASSIGN;
        end
        else
          LexKind := Tok.COLON;
      end;
  'a'..'z': begin
         while ch in ['a'..'z','0'..'9'] do
           NextCh;
         if KeywordsMap.ContainsKey(LexText) then
           LexKind := KeywordsMap[LexText]
         else LexKind := Tok.ID;
       end;
  '0'..'9': begin
         while char.IsDigit(ch) do
           NextCh;
         LexValue := integer.Parse(LexText);
         LexKind := Tok.INUM;
       end;
   ',':
      begin
        NextCh;
        LexKind := Tok.COMMA;
      end;
    '+':
      begin
        NextCh;
        LexKind := Tok.SUM;
      end;
    '-':
      begin
        NextCh;
        LexKind := Tok.DIF;
      end;
    '*':
      begin
        NextCh;
        LexKind := Tok.MULT;
      end;       
    '/':
      begin
        NextCh;
        LexKind := Tok.DIVISION;
      end;
    #0: LexKind := Tok.EOF;   
    else lexerror('�������� ������ '+ch);
  end;  
end;
 
procedure InitKeywords;
begin
  KeywordsMap['begin'] := Tok.&BEGIN;
  KeywordsMap['end'] := Tok.&END;
  KeywordsMap['cycle'] := Tok.CYCLE;
  KeywordsMap['div'] := Tok.&DIV;
  KeywordsMap['mod'] := Tok.&MOD;
  KeywordsMap['and'] := Tok.&AND;
  KeywordsMap['or'] := Tok.&OR;
  KeywordsMap['not'] := Tok.&NOT;
end;
 
procedure Init(fn: string);
begin
  InitKeywords;
  fname := fn;
  AssignFile(f,fname);
  reset(f);
  row := 1; col := 1;
  NextCh;    // ������� ������ ������ � ch
  NextLexem; // ������� ������ �������, �������� LexText, LexKind �, ��������, LexValue
end;
 
procedure Done;
begin
  close(f);
end;
 
function TokToString(t: Tok): string;
begin
  Result := t.ToString;
  case t of
Tok.ID:   Result += ' ' + LexText;
Tok.INUM: Result += ' ' + LexValue; 
  end;
end;
 
end.