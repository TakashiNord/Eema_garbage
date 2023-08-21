//---------------------------------------------------------------------------

#include <vcl.h>
#include <math.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
	Memo1->Clear();
	RadioGroup1Click(NULL) ;
	StringGridControl->Cells[0][0]="x";
	StringGridControl->Cells[2][0]="Fmax(x)" ;
	StringGridControl->Cells[1][0]="Fmin(x)" ;
	StringGridControl->Enabled = false ;
	ButtonAdd->Enabled = false ;
	ButtonDel->Enabled = false ;
	ButtonBuild->Enabled = false ;
}
//---------------------------------------------------------------------------
void __fastcall TForm1::RadioGroup1Click(TObject *Sender)
{
  const double Pi = 3.1415926 ;
  //
  if (RadioGroup1->ItemIndex==0) {
	   StringGridControl->Enabled = false ;
	   ButtonAdd->Enabled = false ;
	   ButtonDel->Enabled = false ;
	   ButtonBuild->Enabled = false ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = значение1" ) ;
	   Memo1->Lines->Add( "min = значение2" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();
	   for (int i=1; i<7; i++) {
		 Series1->AddXY(i*10, 50);
		 Series2->AddXY(i*10, 10);
	   }
	   for (int i=1; i<10; i++) {
		 Series3->AddXY(i*5, 4*sin(0.6*Pi*i)+30);
	   }
	  ChartControl->LeftAxis->Automatic = true ;
	  ChartControl->BottomAxis->Automatic = true ;

  }
  if (RadioGroup1->ItemIndex==1) {
	   StringGridControl->Enabled = false ;
	   ButtonAdd->Enabled = false ;
	   ButtonDel->Enabled = false ;
	   ButtonBuild->Enabled = false ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = значение1" ) ;
	   Memo1->Lines->Add( "min = -999999999.999" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();
	   // Вывод графика.
	   for (int i=1; i<60; i+=10) {
		 Series1->AddXY(i, 6);
		 Series2->AddXY(i, -9);
	   }
	   for (int i=1; i<40; i++) {
		 Series3->AddXY(i, cos(Pi*i)*sin(2*Pi*i)*0.5 + log(Pi*i));
	   }
  	  ChartControl->LeftAxis->Automatic = true ;
	  ChartControl->BottomAxis->Automatic = true ;
  }
  if (RadioGroup1->ItemIndex==2) {
	   StringGridControl->Enabled = false ;
	   ButtonAdd->Enabled = false ;
	   ButtonDel->Enabled = false ;
	   ButtonBuild->Enabled = false ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = +999999999.999" ) ;
	   Memo1->Lines->Add( "min = значение2" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();
	   // Вывод графика.
	   for (int i=1; i<60; i+=10) {
		 Series1->AddXY(i, 9);
		 Series2->AddXY(i, 0);
	   }
	   for (int i=1; i<40; i+=5) {
		 Series3->AddXY(i, 3 + (2*i+25) / ( i*i*i+2*i+1 ) );
	   }
   	  ChartControl->LeftAxis->Automatic = true ;
	  ChartControl->BottomAxis->Automatic = true ;
  }
  if (RadioGroup1->ItemIndex==3) {
	   StringGridControl->Enabled = true ;
	   ButtonAdd->Enabled = true ;
	   ButtonDel->Enabled = true ;
	   ButtonBuild->Enabled = true ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = Fmax(значение)" ) ;
	   Memo1->Lines->Add( "min = Fmin(значение)" ) ;
	   Memo1->Lines->Add( "Fmax\\Fmin функции, задаваемые отрезками" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();

  }
  if (RadioGroup1->ItemIndex==4) {
	   StringGridControl->Enabled = true ;
  	   ButtonAdd->Enabled = true ;
	   ButtonDel->Enabled = true ;
	   ButtonBuild->Enabled = true ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = Fmax(значение)" ) ;
	   Memo1->Lines->Add( "min = -999999999.999" ) ;
	   Memo1->Lines->Add( "Fmax - функция, заданная отрезками" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();

  }
   if (RadioGroup1->ItemIndex==5) {
       StringGridControl->Enabled = true ;
  	   ButtonAdd->Enabled = true ;
	   ButtonDel->Enabled = true ;
       ButtonBuild->Enabled = true ;
	   Memo1->Clear();
	   Memo1->Lines->Add( "max = +999999999.999" ) ;
	   Memo1->Lines->Add( "min = Fmin(значение)" ) ;
	   Memo1->Lines->Add( "Fmin - функция, заданная отрезками" ) ;

	   Series1->Clear();
	   Series2->Clear();
	   Series3->Clear();

  }
}
//---------------------------------------------------------------------------



void __fastcall TForm1::ButtonDelClick(TObject *Sender)
{
   int i ;
   //
   int row = StringGridControl->Row;

   if (row<=0) {
	  return ;
   }

   if (StringGridControl->RowCount!=(row+1)) {

	 for (i = row; i<(StringGridControl->RowCount-1); i++) {
	   //StringGridControl->Rows[i] = StringGridControl->Rows[i + 1];

	   StringGridControl->Cells[0][i]=StringGridControl->Cells[0][i+1] ;
	   StringGridControl->Cells[2][i]=StringGridControl->Cells[2][i+1] ;
	   StringGridControl->Cells[1][i]=StringGridControl->Cells[1][i+1] ;

	 }

   }

   StringGridControl->RowCount = StringGridControl->RowCount - 1;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ButtonAddClick(TObject *Sender)
{
   StringGridControl->RowCount = StringGridControl->RowCount + 1 ;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ButtonBuildClick(TObject *Sender)
{
   if (StringGridControl->RowCount<3) {
	   MessageDlg("Для построения графика, необходимо задать минимум 2-е точки!\n L=(x1,y1)-(x2,y2)", mtConfirmation, TMsgDlgButtons() << mbYes ,0);
	   return ;
   }


 int Col_sort = 0;//колонка для сортировки
 int rCnt = StringGridControl->RowCount ;
 int cCnt = StringGridControl->ColCount ;

 String value;
 double v1, v2 ;
 for (int i = 1; i < rCnt; i++)
  for (int j = i; j < rCnt; j++)
  {
	 // проверка вводимых чисел
	 for(int k=0;k<cCnt;k++)  // количество сортируемых столбцов
	 {
	   v1=StrToFloat(StringGridControl->Cells[k][i]);
	 }

	 value = StringGridControl->Cells[Col_sort][i] ; // .ToDouble()
	 v1=StrToFloat(value);
	 value = StringGridControl->Cells[Col_sort][j] ; // .ToDouble()
	 v2=StrToFloat(value);

	 if (v1>v2)
	 {
		 for(int k=0;k<cCnt;k++)  // количество сортируемых столбцов
		 {
			  value=StringGridControl->Cells[k][i];
			  StringGridControl->Cells[k][i]= StringGridControl->Cells[k][j];
			  StringGridControl->Cells[k][j]=value;
		 }
	 }
  }

 for (int i = 1; i < (rCnt-1); i++)
 {
	 value = StringGridControl->Cells[Col_sort][i] ; // .ToDouble()
	 v1=StrToFloat(value);
	 value = StringGridControl->Cells[Col_sort][i+1] ; // .ToDouble()
	 v2=StrToFloat(value);

	 if (v1==v2)
	 {
	   MessageDlg("2 точки совпадают.\n t=" + FloatToStr(v1) + "\n Должна быть только Одна!", mtConfirmation, TMsgDlgButtons() << mbYes ,0);
	   return ;
	 }
  }


  // for (int i = 1; i<StringGridControl->RowCount; i++)
  //	 StringGridControl->Rows[i]->Clear();

   double mX[rCnt-1];
   double mFmin[rCnt-1];
   double mFmax[rCnt-1];

   for(int i = 1; i < rCnt; i++)
   {
	 mX[i-1] = StringGridControl->Cells[0][i].ToDouble();
	 mFmin[i-1] = StringGridControl->Cells[1][i].ToDouble();
	 mFmax[i-1] = StringGridControl->Cells[2][i].ToDouble();
	 Series1->AddXY(i,mFmax[i-1]) ; // max
	 Series2->AddXY(i,mFmin[i-1]) ; // min
	 Series3->AddXY(i,mX[i-1]) ; //value
   }

	  ChartControl->LeftAxis->Automatic = true ;
	  ChartControl->BottomAxis->Automatic = true ;

}
//---------------------------------------------------------------------------

void __fastcall TForm1::ButtonListClick(TObject *Sender)
{
 	   Memo1->Clear();

 int rCnt = StringGridControl->RowCount ;
 int cCnt = StringGridControl->ColCount ;

 String str, value;
 for (int i = 0; i < rCnt; i++)
  {
	 str = "" ;
	 for(int k=0;k<cCnt;k++)  // количество сортируемых столбцов
	 {
	   str += StringGridControl->Cells[k][i]  + "  " ;
	 }

	 Memo1->Lines->Add(str);

  }


}
//---------------------------------------------------------------------------

void __fastcall TForm1::ButtonCreateClick(TObject *Sender)
{
   Form2->Show();
}
//---------------------------------------------------------------------------

