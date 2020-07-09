unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, ComCtrls, StdCtrls, Grids, DBGrids, DB, ADODB,
  DateUtils;

type
  TForm3 = class(TForm)
    Panel3: TPanel;
    DBGrid1: TDBGrid;
    StatusBar1: TStatusBar;
    Splitter3: TSplitter;
    Memo1: TMemo;
    Panel4: TPanel;
    RadioGroup1: TRadioGroup;
    Button1: TButton;
    Timer1: TTimer;
    Timer2: TTimer;
    DataSource1: TDataSource;
    ADOConnection1: TADOConnection;
    ADOTable1: TADOTable;
    SaveDialog1: TSaveDialog;
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure RadioGroup1Click(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Dt1: LongInt;
    Dt2: LongInt;
    procedure ArcStat(Sender: TObject);
    procedure RSDU(Sender: TObject);
  end;

var
  Form3: TForm3;

implementation

{$R *.dfm}

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

procedure TForm3.RSDU(Sender: TObject);
var
 pass,login,tns : String ;
begin
   tns:='rsdu2';
   login:='rsduadmin';
   pass:='passme';

   ADOConnection1.ConnectionString:='Provider=' +
   ADOConnection1.Provider + ';' +
   'Password=' + pass + ';' +
   'Persist Security Info=True;' +
   'User ID=' + login + ';' +
   'Data Source='+tns+';';
  try
    ADOConnection1.Close();
    ADOConnection1.Connected:=True ;
  except
   on e:Exception do begin
    Exit;
    Application.Terminate ;
   end;
  end;
  //
end;

procedure TForm3.ArcStat(Sender: TObject);
var
 strQry : String ;
 ind : integer ;
begin

 ind := RadioGroup1.ItemIndex ;
 if ind=0 then strQry:='arc_stat';
 if ind=1 then strQry:='ARC_STAT_CURRENT_V';
 if ind=2 then strQry:='ARC_STAT_AVG_V';

 try
ADOTable1.Active:=False ;
ADOTable1.TableName:= strQry ;
ADOTable1.Active:=True;
//DBgrid1.Enabled:=False;
setgridcolumnwidths(DBgrid1);
//DBgrid1.Enabled:=True;
  except
   on e:Exception do
  end;

end;


procedure TForm3.Button1Click(Sender: TObject);
var
  Res : Integer ;
  //fBm : TBookmarkStr;
  fDt : TDataSet;
  fStr, fStr2 : string;
  fName : String ;
  i : Integer;
  fTf : TextFile;
begin

  DateTimeToString(fStr2, '-yyyymmdd_hhnnss', Now);
  with SaveDialog1 do begin
    InitialDir:=ExtractFilePath(Application.ExeName);
    FileName:='Arc_stat'+fStr2+'.cvs';
    Filter:='Xls files (*.cvs)|*.cvs|All files (*.*)|*.*';
  end;
  //Открываем диалог сохранения файла.
  if not SaveDialog1.Execute then Exit;

  fName:=SaveDialog1.FileName;

//Если файл с указанным именем уже существует.
  if FileExists(SaveDialog1.FileName) then begin
    Res := MessageDlg(
      'Файл с именем:' + #10
      + '"' + SaveDialog1.FileName + '"' + #10
      + 'Уже существует. Перезаписать?'
      ,mtConfirmation
      ,[mbYes, mbNo]
      ,0
    );
    //Если пользователь отказался перезаписывать файл - выходим.
    if Res = mrNo then Exit;
  end;

  fDt := DBGrid1.DataSource.DataSet;
  fDt.DisableControls;
  //fName:='Arc_stat.cvs';
  AssignFile(fTf, fName);
  Rewrite(fTf);
  try
    fStr := '';
    for i := 0 to Pred(DBGrid1.Columns.Count) do begin
      fStr := fStr + dbgrid1.Columns[i].Title.Caption + ';';
    end;
    Writeln(fTf, fStr);
    fDt.First;
    while not fDt.Eof do begin
      fStr := '';
      for i := 0 to Pred(DBGrid1.Columns.Count) do begin
        if not DBGrid1.Columns[i].Visible then // Пропускаем невидимые столбцы
          Continue;
        fStr := fStr + fDt.FieldByName(DBGrid1.Columns[i].FieldName).AsString + ';';
      end;
      Writeln(fTf, fStr);
      fDt.Next
    end;
  finally
    fDt.EnableControls;
    CloseFile(fTf);
  end;
 //
end;

procedure TForm3.FormCreate(Sender: TObject);
begin
  try
    ADOConnection1.Connected:=False ;
  except
   on e:Exception do
    Application.Terminate ;
  end;
   RSDU(Sender);
   Timer2.Interval:=7000;
   Dt1:=0;
   Dt2:=0;
   RadioGroup1.ItemIndex:=0;
   RadioGroup1Click(Sender);
   ArcStat(Sender);
end;

procedure TForm3.RadioGroup1Click(Sender: TObject);
begin
 if RadioGroup1.ItemIndex=1 then begin
    Timer2.Enabled:=True ;
 end
 else begin
  Timer2.Enabled:=False ;
 end;
 ArcStat(Sender);
end;

procedure TForm3.Timer1Timer(Sender: TObject);
var
  Res: LongInt;
begin
  Res:=DateTimeToUnix(Now);
  StatusBar1.Panels[0].Text:='Time = ' + FormatDateTime('dd/mm/yyyy hh:nn:ss', Now) + '  |   Unix=' + IntToStr(Res);
end;

procedure TForm3.Timer2Timer(Sender: TObject);
begin
  Dt1:=DateTimeToUnix(Now);
  ArcStat(Sender);
end;

end.
