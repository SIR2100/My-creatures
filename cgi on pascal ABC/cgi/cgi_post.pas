uses System;
var f:TextFile; 
    s:array of char;
    n,i,err:integer;
begin 
assign(f,'zapros_get.txt');
append(f);
Val(Environment.GetEnvironmentVariable('CONTENT_LENGTH'),n,err);
SetLength(s,n);
for i:=0 to n-1 do read(s[i]); 
writeln(f,'<NEWSTRING>');
writeln(f,#13#10);

for i:=183 to n-46 do
begin
  if (s[i]=#13) then 
    writeln(f,'<br>')
    else
    write(f,s[i]);
end;
writeln(f,'<br>');
writeln(f,#13#10);
close(f);
writeln('Content-Type: text/html; charset=cp-866');
writeln('');
writeln('<html> <head> <title> Œ  </title> </head> <body> <h2>Article was added. <a href=http://stud.mmcs.sfedu.ru/~SorokanyukIgor/cgi/cgi_get.cgi?mode=read>Main</a></h2></body>')
end.

