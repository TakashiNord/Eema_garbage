unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, DB, ADODB, Grids, DBGrids;

type
  TForm2 = class(TForm)
    DataSource1: TDataSource;
    ADOConnection1: TADOConnection;
    ADOTable1: TADOTable;
    ADOQuery1: TADOQuery;
    DBGrid1: TDBGrid;
    procedure DBGrid1TitleClick(Column: TColumn);
    procedure DBGrid1DblClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;

implementation

{$R *.dfm}


procedure TForm2.DBGrid1DblClick(Sender: TObject);
begin
 //
 //DBGrid1.
 Form2.CloseModal();
end;

procedure TForm2.DBGrid1TitleClick(Column: TColumn);
begin
  ADOTable1.sort:=Column.FieldName;
end;

end.
