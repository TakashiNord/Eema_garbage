unit Util;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, ToolWin, DB, ADODB, Grids, DBGrids;

implementation


procedure SetGridClient(grid: tdbgrid);
var i,s,s1,n:integer;
begin
  s:=0;s1:=0;
  for i:=0 to Grid.Columns.Count-1 do
    s:=s+Grid.Columns[i].Width;
  n:=Grid.ClientWidth-Grid.Columns.Count-1;
  if dgIndicator in Grid.Options then n:=n-12;
  for i:=0 to Grid.Columns.Count-1 do
    begin
      Grid.Columns[i].Width:=Round(Grid.Columns[i].Width/s*n);
      if Grid.Columns[i].Width<2 then Grid.Columns[i].Width:=2;
      s1:=s1+Grid.Columns[i].Width;
    end;
  i:=Grid.Columns.Count - 1 - n mod Grid.Columns.Count ;
  Grid.Columns[i].Width:=Grid.Columns[i].Width-s1+n;
end;

procedure setgridcolumnwidths(grid: tdbgrid);
const
defborder = 10;
var
temp, n: integer;
lmax: array [0..30] of integer;
begin
with grid do
begin
canvas.font := font;
for n := 0 to columns.count - 1 do
//if columns[n].visible then
lmax[n] := canvas.textwidth(fields[n].fieldname) + defborder;
grid.datasource.dataset.first;
while not grid.datasource.dataset.eof do
begin
for n := 0 to columns.count - 1 do
begin
//if columns[n].visible then begin
temp := canvas.textwidth(trim(columns[n].field.displaytext)) + defborder;
if temp > lmax[n] then lmax[n] := temp;
//end; { if }
end; {for}
grid.datasource.dataset.next;
end; { while }
grid.datasource.dataset.first;
for n := 0 to columns.count - 1 do
if lmax[n] > 0 then
columns[n].width := lmax[n];
end; { with }
end; {setgridcolumnwidths }



end.
