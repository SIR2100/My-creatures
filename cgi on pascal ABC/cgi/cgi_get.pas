uses System;
var f:TextFile; 
    s,c:string;
    col,art:integer;
begin 
assign(f,'zapros_get.txt');
col:=1;
reset(f);
while not eof(f) do
  begin
    col+=1;
    readln(f,s);
  end;
col:=(col+1) div 2;
close(f); 
s:=Environment.GetEnvironmentVariable('QUERY_STRING');
writeln('Content-Type: text/html; charset=cp866');
writeln('');
writeln('<html> <head> <Content-Type: text/html; charset=cp866"> <title> OK </title> </head> <body>');
if Length (s) >= 8 then
c:=Copy(s,1,11);
if c='mode=new' then begin
writeln('<h1>Add new article</h1>');
//writeln('<form  method="get" action="cgi_get.cgi"> <input type="hidden" name="mode" value="newend"> <TEXTAREA NAME=Text COLS=25 ROWS=5>Input here</TEXTAREA><br><INPUT TYPE=submit VALUE="Write"></FORM>');
writeln('<form  ENCTYPE="multipart/form-data" method="POST" action="cgi_post.cgi"> <input type="hidden" name="BigText"> <TEXTAREA NAME=Text COLS=25 ROWS=5>Input here</TEXTAREA><br><INPUT TYPE=submit VALUE="Write"></FORM>');

writeln('</body>');
end
//
//else  if c='mode=newend' then begin
//writeln('<h2>Article was added. <a href=http://stud.mmcs.sfedu.ru/~SorokanyukIgor/cgi/cgi_get.cgi?mode=read>Main</a></h2>');
//writeln('</body>');
//assign(f,'zapros_get.txt');
//append(f);
//var t:=Environment.GetEnvironmentVariable('QUERY_STRING');
//Delete(t,1,17);
//writeln(f,'<h2>Article #',col,'</h2><br>',t,'<br>');
//writeln(f);
//close(f);
//end
else begin
assign(f,'zapros_get.txt');
reset(f);
while not eof(f) do
  begin
    var r: string;
    readln(f,r);
    if r='<NEWSTRING>' then
    begin
      art+=1;
      writeln('<h2>Article #',art,'</h2>',r)
    end
    else writeln(r);
  end;
close(f);

writeln('<br>');
writeln('<form  method="get" action="cgi_get.cgi">  <input type="hidden" name="mode" value="new"><INPUT TYPE=submit VALUE="New article"></FORM>');
writeln('</body>');
end;

end.