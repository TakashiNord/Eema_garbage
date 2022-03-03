
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <utility.h>
#include <cvirte.h>     /* needed if linking executable in external compiler; harmless otherwise */
#include <ansi_c.h>
#include <userint.h>

#include "CviPanelParser.h"


//==============================================================================
// Constants
#define MAX_STRING_SIZE 1024

//==============================================================================
// Types

//==============================================================================
// Static global variables
  static char strGen[30];
  static char strColor[64];
//==============================================================================
// Static functions

//==============================================================================
// Global variables
  int numGen = 1000 ;

  int panelHandle = 0;
  int subPanelHandle = 0;

  int z_index = 9999 ;

  int flagNotFirst = 0 ;
  int flagNotFirstSave = 0; // save for sub panel

  int  ic = 0 ;
  char ac[9999][MAX_STRING_SIZE];
//==============================================================================
// Global functions

char * GenId( )
{
  strGen[0]='\0';
  numGen ++ ;
  sprintf(strGen,"id%d", numGen );
  return (strGen);
}


int iFont(int f, int h, int w, int bold) //, char * s)
{
  int fq = f ;
  //int wl = strlen(s);

  h-=2; // убираем межстрочный интервал
  if (bold) h-=2; // если толстый шрифт

  // font-size (F) и line-height (L), используется коэффициент подобия
  // F x 1.618 = L
  if ((f*1.618)>h ) fq = (int) h/1.618 ;

  // между высотой (L) и длиной (W) строки существует экспоненциальная зависимость
  // W = L * 2

  if (fq>f) fq=f ;
  if (h <= 0 || w <= 0) fq=f ;

  return (fq);
}


char * Int2HexWeb(int iVal)
{
  strColor[0]='\0';
  sprintf(strColor,"#%06x", iVal );
  return (strColor);
}


void PanelTextMsg(FILE *outfile, char * textId, int x, int y,int h, int w, char * textMsg,
    int attr_label_bold, int attr_label_color, int attr_bg_color, int attr_text_justify, int attr_text_point_size, int zplane )
{
  int flagColumn = 0;
  int n = 0;

  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  n=0;
  while ( textMsg[n]!= '\0' ) {
    if (textMsg[n] == '\n') textMsg[n]=' ';
    if (textMsg[n] == '\'') textMsg[n]=' ';
    n++;
  }

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'label',\n");

  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);

  if(h != 0 && w != 0)
  {
    fprintf(outfile ,"height:%d,\n",h);
    fprintf(outfile ,"width:%d,\n",w);
  }

  // Courier  "Times New Roman", serif  Geneva, Arial, Helvetica, sans-serif

  fprintf(outfile ,"style: 'padding:1px 0 2px 4px;z-index: %d;font-family: Arial",zplane);

  flagColumn = 1;

  if(attr_label_bold == 1 || attr_label_color != 0 || attr_bg_color != 0 || attr_text_justify != 0 || attr_text_point_size != 16)
  {
    attr_text_point_size=iFont(attr_text_point_size,h,w,attr_label_bold);
    if(attr_text_point_size != 16 )
    {
      if(flagColumn)
        fprintf(outfile ,";");
      else
        flagColumn = 1;

      fprintf(outfile ,"font-size: %dpt", (int)(attr_text_point_size*3/4));
    }

    if(attr_label_bold == 1 )
    {
      if(flagColumn)
        fprintf(outfile ,";");
      else
        flagColumn = 1;

      fprintf(outfile ,"font-weight:bold");
    }

    if(attr_label_color != 0 )
    {
      if(flagColumn)
        fprintf(outfile ,";");
      else
        flagColumn = 1;

      fprintf(outfile ,"color:%s",Int2HexWeb(attr_label_color) );
    }

    if(attr_bg_color != VAL_TRANSPARENT )
    {
      if(flagColumn)
        fprintf(outfile ,";");
      else
        flagColumn = 1;

      fprintf(outfile ,"background-color:%s",Int2HexWeb(attr_bg_color) );
    }

    if(attr_text_justify != 0 )
    {
      if(flagColumn)
        fprintf(outfile ,";");
      else
        flagColumn = 1;

      fprintf(outfile ,"text-align:") ;

      if(attr_text_justify == VAL_RIGHT_JUSTIFIED)
        fprintf(outfile ,"right");
      else
        if(attr_text_justify == VAL_CENTER_JUSTIFIED)
        fprintf(outfile ,"center");
    }
  }

  fprintf(outfile ,"',\n");

  fprintf(outfile ,"id: '%s',\n",textId);
  fprintf(outfile ,"text: '%s'\n",textMsg);
  fprintf(outfile ,"}");
}


void PanelNumeric(FILE *outfile, char * textId, int x,int y, int h, int w, int attr_precision, int attr_label_bold, int attr_text_point_size, int zplane )
{
  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'textfield',\n");
  //fprintf(outfile ,"fieldLabel: '%s',\n",textId);
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h);
  fprintf(outfile ,"width: %d,\n",w);
  fprintf(outfile ,"readOnly: true,\n");
  fprintf(outfile ,"style:'z-index: %d',\n",zplane);
  fprintf(outfile ,"decimalPrecision: %d,\n",attr_precision);
  fprintf(outfile ,"id: '%s',\n",textId);
  fprintf(outfile ,"hideLabel: true,\n");
  fprintf(outfile ,"disabled: true,\n");
  fprintf(outfile ,"//allowBlank:false,\n");
  fprintf(outfile ,"emptyText: '0',\n"); //подсказка в текстовом поле
  fprintf(outfile ,"//hideLabel: true,\n");
  fprintf(outfile ,"'font-family': 'Arial',\n"); // Courier  "Times New Roman", serif  Geneva, Arial, Helvetica, sans-serif
  //fontWeight {normal, bold, bolder, lighter}
  attr_text_point_size=iFont(attr_text_point_size,h,w,attr_label_bold);
  fprintf(outfile ,"fieldStyle: {'fontSize': '%dpx','fontWeight': '%s'}\n", attr_text_point_size, (attr_label_bold ? "bold":"normal"));
  fprintf(outfile ,"}");
}

void PanelPictureRing(FILE *outfile, char * textId, int x, int y, int h, int w)
{
  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'fieldset',\n");
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h+4);
  fprintf(outfile ,"width: %d,\n",w+4);
  fprintf(outfile ,"id: '%s',\n",textId);
  fprintf(outfile ,"vstyle: {0:[{background: \"url('/icons/TS0.png') center no-repeat\"}],1:[{background: \"url('/icons/TS1.png') center no-repeat\"}]},\n");
  fprintf(outfile ,"cls: 'wasutp_ts_state_default'\n");
  fprintf(outfile ,"}");
}

void PanelRoundLed(FILE *outfile, char * textId, int x, int y, int h, int w, int attr_on_color, int attr_off_color, int zplane  )
{
  char s1[10],s2[10];
  sprintf(s1,"%s",Int2HexWeb(attr_on_color));
  sprintf(s2,"%s",Int2HexWeb(attr_off_color));

  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'fieldset',\n");
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h);
  fprintf(outfile ,"width: %d,\n",w);
  fprintf(outfile ,"id: '%s',\n",textId);
  fprintf(outfile ,"style:'z-index: %d',\n",zplane);
  fprintf(outfile ,"vstyle:{ 0:[{background: \"%s\"}],1:[{background: \"%s\"}]},\n",s2,s1);
  fprintf(outfile ,"cls: 'wasutp_led_state_default'\n");
  fprintf(outfile ,"}");
}

void PanelSquareLed(FILE *outfile, char * textId, int x, int y, int h, int w, int attr_on_color, int attr_off_color, int zplane )
{
  char s1[10],s2[10];
  sprintf(s1,"%s",Int2HexWeb(attr_on_color));
  sprintf(s2,"%s",Int2HexWeb(attr_off_color));

  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'fieldset',\n");
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h);
  fprintf(outfile ,"width: %d,\n",w);
  fprintf(outfile ,"style:'z-index: %d',\n",zplane);
  fprintf(outfile ,"id: '%s',\n",textId);
  fprintf(outfile ,"vstyle:{ 0:[{background: \"%s\"}],1:[{background: \"%s\"}]},\n",s2,s1);
  fprintf(outfile ,"cls: 'wasutp_led_sq_state_default'\n" );
  fprintf(outfile ,"}");
}

void PanelSquareBorder(FILE *outfile, char * textId, int x, int y, int h, int w,
                      int attr_fr_thick, int attr_fr_color, int attr_bg_color, int zplane)
{
  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'fieldset',\n");
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h);
  fprintf(outfile ,"width: %d,\n",w);

  fprintf(outfile ,"style: 'margin: 0; padding:0;;z-index: %d",zplane);
  if(attr_fr_thick != 0) //border: 2px solid #f0f0f0
  {
    fprintf(outfile ,";border: %dpx solid %s",attr_fr_thick, Int2HexWeb(attr_fr_color) );
  }
  if(attr_bg_color != 0)
  {
    fprintf(outfile ,";background-color:%s", Int2HexWeb(attr_bg_color) );
  }
  fprintf(outfile ,"',\n");

  fprintf(outfile ,"id: '%s'\n",textId);
  fprintf(outfile ,"}");
}

void PanelCommandButton(FILE *outfile, char * textId, int x, int y, int h, int w, char * textLabel, int zplane )
{
  int n = 0;
  if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

  n=0;
  while ( textLabel[n]!= '\0' ) {
    if (textLabel[n] == '\n') textLabel[n]=' ';
    if (textLabel[n] == '\'') textLabel[n]=' ';
    n++;
  }

  fprintf(outfile ,"{\n");
  fprintf(outfile ,"xtype: 'button',\n");
  fprintf(outfile ,"x: %d,\n",x);
  fprintf(outfile ,"y: %d,\n",y);
  fprintf(outfile ,"height: %d,\n",h);
  fprintf(outfile ,"width: %d,\n",w);
  fprintf(outfile ,"style:{'z-index': %d},\n",zplane); // ----------------------
  fprintf(outfile ,"text: '%s',\n",textLabel);
  fprintf(outfile ,"id: '%s',\n",textId);
  //fprintf(outfile ,"onClick: function(){ panelsHelper.openWindow(this.isContained.id,this.id);}\n");
  fprintf(outfile ,"onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}\n");
  fprintf(outfile ,"}");
}


int GetCtrlElem ( FILE *outfile, int panelHandle, int ctrlID )
{
  //int ctrlID;
  int numControls;
  int counter = 0;
  int len = 0 ;
  
  int jc ;

  int y = 0, x = 0;
  int h = 0, w = 0;

  char attr_constant_name[MAX_STRING_SIZE];
  char attr_dflt_value[MAX_STRING_SIZE];
  int attr_dflt_value_int;
  char attr_label_text[MAX_STRING_SIZE];

  int attr_label_top;
  int attr_label_left;
  int attr_label_visible;
  int attr_ctrl_style;
  int attr_label_bold;
  int attr_label_color;
  int attr_bg_color;
  int attr_text_justify;
  int attr_size_to_text;
  int attr_precision;
  int attr_text_point_size;
  int attr_zplane_position;
  int attr_ctrl_tab_position;
  int attr_on_color,attr_off_color;

  int attr_fr_thick;
  int attr_fr_color;


    //GetPanelAttribute(panelHandle, ATTR_TITLE,        attr_label_text); //!!
    //GetPanelAttribute(panelHandle, ATTR_HEIGHT,       &h);
    //GetPanelAttribute(panelHandle, ATTR_WIDTH,        &w);
    //GetPanelAttribute(panelHandle, ATTR_BACKCOLOR,    &attr_bg_color);

   //GetPanelAttribute (panelHandle, ATTR_NUM_CTRLS, &numControls);

    //GetPanelAttribute(panelHandle,ATTR_PANEL_FIRST_CTRL,&ctrlID);
    //for(counter=0;counter<numControls;counter++)
    //{

        GetCtrlAttribute(panelHandle, ctrlID, ATTR_CONSTANT_NAME,      attr_constant_name); //!!
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_CTRL_STYLE,        &attr_ctrl_style);

        GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_TEXT,        attr_label_text); //!!
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_TOP,         &attr_label_top); //!!
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_LEFT,        &attr_label_left);

        GetCtrlAttribute(panelHandle, ctrlID, ATTR_LEFT,              &x);
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_TOP,               &y);
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_HEIGHT,            &h);
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_WIDTH,             &w);
        GetCtrlAttribute(panelHandle, ctrlID, ATTR_ZPLANE_POSITION,   &attr_zplane_position);
        //GetCtrlAttribute(panelHandle, ctrlID, ATTR_CTRL_TAB_POSITION, &attr_ctrl_tab_position);


        // поиск повторов среди ATTR_CONSTANT_NAME
        if (ic>9999) ic=0;
		strcpy(ac[ic],attr_constant_name);strcat(ac[ic],"\0");
        for (jc=0;jc<ic;jc++) {
			if ( ic!=jc && 0==strcmp(ac[ic],ac[jc]) ) {
		        fprintf (stderr, "! -- SECOND ATTR_CONSTANT_NAME Id: %s [%d]\n",attr_constant_name,attr_ctrl_style ); 
                break ;				
			}
		}       
		ic ++ ;
		//  ------------------------


        switch(attr_ctrl_style)
        {
           case CTRL_TEXT_MSG:
           case CTRL_TEXT_BOX:
           case CTRL_TEXT_BOX_LS:
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_DFLT_VALUE,        attr_dflt_value); //!!
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_BOLD,         &attr_label_bold);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_COLOR,        &attr_label_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_BGCOLOR,      &attr_bg_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_JUSTIFY,      &attr_text_justify);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_SIZE_TO_TEXT,      &attr_size_to_text);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_POINT_SIZE,   &attr_text_point_size); // ATTR_LABEL_POINT_SIZE
             //You can use a LabWindows/CVI-supplied font; any host fontСsuch as Arial or Courier

             if(attr_size_to_text)
             {
               h = 0;
               w = 0;
             }

             PanelTextMsg(outfile,attr_constant_name,x,y,h,w,attr_dflt_value,attr_label_bold,attr_label_color,attr_bg_color,
                      attr_text_justify,attr_text_point_size,attr_zplane_position);
           break;

           case CTRL_NUMERIC:
           case CTRL_NUMERIC_LS:
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_PRECISION,       &attr_precision);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_VISIBLE,   &attr_label_visible); //!!
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_POINT_SIZE, &attr_text_point_size); //ATTR_LABEL_POINT_SIZE
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_BOLD,       &attr_label_bold); //ATTR_TEXT_BOLD
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TEXT_JUSTIFY,    &attr_text_justify);
             //ATTR_TEXT_JUSTIFY : VAL_LEFT_JUSTIFIED VAL_RIGHT_JUSTIFIED VAL_CENTER_JUSTIFIED

             PanelNumeric(outfile,attr_constant_name,x,y,h,w,attr_precision,attr_label_bold,attr_text_point_size,attr_zplane_position);

             len = strlen(attr_label_text);
             if(attr_label_visible && len>0)
             {
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BOLD,      &attr_label_bold);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_COLOR,     &attr_label_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BGCOLOR,   &attr_bg_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_HEIGHT,    &h);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_WIDTH,     &w);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_SIZE_TO_TEXT,  &attr_size_to_text);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_POINT_SIZE,     &attr_text_point_size); //ATTR_LABEL_POINT_SIZE

               if(attr_size_to_text)
               {
                 h = 0;
                 w = 0;
                 //attr_text_point_size = 16 ;
               }

               PanelTextMsg(outfile,strcat(attr_constant_name,"_LABEL"),attr_label_left,attr_label_top,
                               h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color,0,attr_text_point_size,attr_zplane_position);
             }
           break;

           case CTRL_PICTURE_RING:
           case CTRL_PICTURE_RING_LS:
             PanelPictureRing(outfile,attr_constant_name,x,y,h,w);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_VISIBLE,   &attr_label_visible); //!!

             len = strlen(attr_label_text);
             if(attr_label_visible && len>0)
             {
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BOLD,          &attr_label_bold);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_COLOR,         &attr_label_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BGCOLOR,       &attr_bg_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_HEIGHT,        &h);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_WIDTH,         &w);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_SIZE_TO_TEXT,  &attr_size_to_text);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_POINT_SIZE ,   &attr_text_point_size);// ATTR_LABEL_POINT_SIZE

               if(attr_size_to_text)
               {
                 h = 0;
                 w = 0;
                 //attr_text_point_size = 16 ;
               }

               PanelTextMsg(outfile,strcat(attr_constant_name,"_LABEL"),attr_label_left,attr_label_top,
                 h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color,0,attr_text_point_size,attr_zplane_position);
             }
           break;

           case CTRL_ROUND_LIGHT:
           case CTRL_ROUND_LED:
           case CTRL_ROUND_LED_LS:
           {

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_ON_COLOR,  &attr_on_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_OFF_COLOR, &attr_off_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_VISIBLE,   &attr_label_visible); //!!

             PanelRoundLed(outfile,attr_constant_name,x,y,h,w,attr_on_color,attr_off_color, attr_zplane_position);

             len = strlen(attr_label_text);
             if(attr_label_visible && len>0)
             {
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BOLD,      &attr_label_bold);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_COLOR,     &attr_label_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BGCOLOR,     &attr_bg_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_HEIGHT,      &h);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_WIDTH,     &w);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_SIZE_TO_TEXT,  &attr_size_to_text);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_POINT_SIZE ,     &attr_text_point_size); // ATTR_LABEL_POINT_SIZE

               if(attr_size_to_text)
               {
                 h = 0;
                 w = 0;
                 //attr_text_point_size = 16 ;
               }

               PanelTextMsg(outfile,strcat(attr_constant_name,"_LABEL"),attr_label_left,attr_label_top,
                 h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color,0,attr_text_point_size,attr_zplane_position);
             }
           }
           break;

           case CTRL_SQUARE_LIGHT:
           case CTRL_SQUARE_LED:
           case CTRL_SQUARE_LED_LS:
           {
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_ON_COLOR,  &attr_on_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_OFF_COLOR, &attr_off_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_VISIBLE,   &attr_label_visible); //!!

             PanelSquareLed(outfile,attr_constant_name,x,y,h,w,attr_on_color,attr_off_color,attr_zplane_position);

             len = strlen(attr_label_text);
             if(attr_label_visible && len>0)
             {
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BOLD,      &attr_label_bold);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_COLOR,     &attr_label_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_BGCOLOR,     &attr_bg_color);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_HEIGHT,      &h);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_WIDTH,     &w);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_SIZE_TO_TEXT,  &attr_size_to_text);
               GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_POINT_SIZE ,     &attr_text_point_size); // ATTR_LABEL_POINT_SIZE

               if(attr_size_to_text)
               {
                 h = 0;
                 w = 0;
                 //attr_text_point_size = 16 ;
               }

               PanelTextMsg(outfile,strcat(attr_constant_name,"_LABEL"),attr_label_left,attr_label_top,
                 h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color,0,attr_text_point_size,attr_zplane_position);
             }
           }
           break;

           case CTRL_SQUARE_COMMAND_BUTTON:
           case CTRL_OBLONG_COMMAND_BUTTON:
           case CTRL_ROUND_COMMAND_BUTTON:
           case CTRL_ROUNDED_COMMAND_BUTTON:
           case CTRL_PICTURE_COMMAND_BUTTON:
           case CTRL_SQUARE_COMMAND_BUTTON_LS:
           case CTRL_PICTURE_COMMAND_BUTTON_LS:

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LABEL_VISIBLE,   &attr_label_visible); //!!

             PanelCommandButton(outfile,attr_constant_name,x,y,h,w,(attr_label_visible)?attr_label_text:"",attr_zplane_position);

           break;

           case CTRL_PICTURE:
           case CTRL_PICTURE_LS:
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_THICKNESS, &attr_fr_thick);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_COLOR, &attr_fr_color);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_PICT_BGCOLOR, &attr_bg_color);

             PanelSquareBorder(outfile,attr_constant_name,x,y,h,w,attr_fr_thick,attr_fr_color,attr_bg_color,attr_zplane_position);
           break;

           case CTRL_RAISED_BOX:
           case CTRL_RECESSED_BOX:
           case CTRL_FLAT_BOX:
           case CTRL_RAISED_CIRCLE:
           case CTRL_RECESSED_CIRCLE:
           case CTRL_FLAT_CIRCLE:
           case CTRL_RAISED_FRAME:
           case CTRL_RECESSED_FRAME:
           case CTRL_FLAT_FRAME:
           case CTRL_RAISED_ROUND_FRAME:
           case CTRL_RECESSED_ROUND_FRAME:
           case CTRL_FLAT_ROUND_FRAME:
           case CTRL_RAISED_ROUNDED_BOX:
           case CTRL_RECESSED_ROUNDED_BOX:
           case CTRL_FLAT_ROUNDED_BOX:
           case CTRL_RECESSED_BOX_LS:
           case CTRL_SMOOTH_HORIZONTAL_BOX_LS:
           case CTRL_RECESSED_NARROW_FRAME:
           case CTRL_RAISED_BOX_LS:
           case CTRL_SMOOTH_VERTICAL_BOX_LS:
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_COLOR, &attr_bg_color);

             attr_fr_thick = 0;
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_COLOR, &attr_fr_color);

             PanelSquareBorder(outfile,attr_constant_name,x,y,h,w,attr_fr_thick,attr_fr_color,attr_bg_color,attr_zplane_position);
           break;

           case CTRL_HORIZONTAL_SPLITTER:
           case CTRL_HORIZONTAL_SPLITTER_LS:
           case CTRL_VERTICAL_SPLITTER:
           case CTRL_VERTICAL_SPLITTER_LS:
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_COLOR, &attr_bg_color);

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_THICKNESS, &attr_fr_thick);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_FRAME_COLOR, &attr_fr_color);

             PanelSquareBorder(outfile,attr_constant_name,x,y,h,w,attr_fr_thick,attr_fr_color,attr_bg_color,attr_zplane_position);
           break;


           default:

             fprintf (stderr, "! -- Empty element Id: %s [%d] , x=%d, y=%d, h=%d, w=%d \n",attr_constant_name,attr_ctrl_style, x,y,h,w);
           break;
       }

       //fprintf (stderr,"! Get element Id: %s [%d] , x=%d, y=%d, h=%d, w=%d \n",attr_constant_name,attr_ctrl_style, x,y,h,w);

    //   GetCtrlAttribute(panelHandle,ctrlID,ATTR_NEXT_CTRL,&ctrlID);
    //}

    return(0);
}



/// HIFN The main entry-point function.
int main (int argc, char *argv[])
{
    int error = 0;
    int i = 0 ;

    int showPanel = 0; // true, если нужно показать панель, ее закрытие осуществлетс нажатием на крестик

    int ctrlID , ctrl ;
    int numControls = 0;
    int counter = 0;

    int y = 0, x = 0;
    int h = 0, w = 0;

    char driveName[3];
    char dirName[256];
    char fileName[256];

    char panel_attr_label_text[MAX_STRING_SIZE]="\0";
    int panel_h = 0, panel_w = 0 ;
    int panel_attr_backcolor = 0 ;

    char panelName[MAX_STRING_SIZE]="\0";
    char outputFileName[MAX_STRING_SIZE]="\0";

    char attr_label_text[MAX_STRING_SIZE]="\0";
    char attr_constant_name[MAX_STRING_SIZE]="\0";

    int attr_ctrl_style ;
    int attr_zplane_position,attr_ctrl_tab_position ;

    int tabIndex ;
    int tabCount ;
    int tabCountMain ;
    int tabPanel ;
    int tab ;
    char attr_label[256];
    int attr_visible, attr_dimmed ;
    int attr_backcolor ;

    int n = 0;
    char *ach;

    FILE *outfile ;

    char outputFileNameTab[MAX_STRING_SIZE]="\0";

    int tabFlag = 1 ; // output tab


/*======================================================================*/

    if (argc < 2)
    {
      fprintf (stderr, "usage: <exe> uirfile \n");
      return -1;
    }

    // если число аргументов больше 2, то показываем Панель
    if (argc > 2)
    {
      showPanel = 1;
      tabFlag = 1 ;
    }

    if (InitCVIRTE(0, 0, 0) == 0)
    {
      fprintf (stderr, "Don't init CVI");
      return -1;
    }

    SetSleepPolicy(VAL_SLEEP_MORE);

    panelHandle = LoadPanel(0, argv[1], PANEL);
    if (panelHandle < 0)
    {
      fprintf (stderr, "Don't open panel = %s",argv[1]);
      return -1;
    }

    InstallPanelCallback(panelHandle, GraphPanelCallback, 0);
    if (showPanel >= 1 )
    {
      DisplayPanel(panelHandle);
    } 

    fprintf (stderr, "! File in : %s \n",argv[1]);
    SplitPath(argv[1],driveName,dirName,fileName);

    n=0;
    while ( fileName[n]!= '\0' ) {
      if (fileName[n] == ' ') fileName[n]='_';
      if (fileName[n] == '-') fileName[n]='_';
      n++;
    }

    ach=strrchr (fileName,'.');
    if (ach!=NULL)
      strncpy(panelName,fileName,ach-fileName);

    /*
    открываем панель и проверяем наличие Tab (их количество)
    проверяем только 1-ый уровень вложенности
    то0есть примнимаем, что на Главной панели 1-Вкладка
    */

    tabCountMain = 0;
    GetPanelAttribute(panelHandle, ATTR_PANEL_FIRST_CTRL, &ctrlID);
    while (ctrlID != 0)
    {
       GetCtrlAttribute(panelHandle, ctrlID, ATTR_CTRL_STYLE, &attr_ctrl_style);

       switch(attr_ctrl_style)
       {
         case CTRL_TABS:
         {
             GetNumTabPages (panelHandle, ctrlID, &tabCountMain);
         }
         break ;
         default:

         break;
       }

       if (tabCountMain>0) break ;

       // Get the next control
       GetCtrlAttribute(panelHandle,ctrlID,ATTR_NEXT_CTRL,&ctrlID);
    }


    // -1 - base file, 0 ... n - tabpanel
    for(i=-1;i<tabCountMain;i++)
    {
		ic = 0; // обнуляем счетчик элементов для поиска повторов

        GetPanelAttribute(panelHandle, ATTR_TITLE,     panel_attr_label_text); //!!
        GetPanelAttribute(panelHandle, ATTR_HEIGHT,    &panel_h);
        GetPanelAttribute(panelHandle, ATTR_WIDTH,     &panel_w);
        GetPanelAttribute(panelHandle, ATTR_BACKCOLOR, &panel_attr_backcolor); // An RGB value is a 4-byte integer with the hexadecimal format 0x00RRGGBB
        GetPanelAttribute(panelHandle, ATTR_NUM_CTRLS, &numControls);

        n=0;
        while ( panel_attr_label_text[n]!= '\0' ) {
          //if (panel_attr_label_text[n] == ' ') panel_attr_label_text[n]='_';
          if (panel_attr_label_text[n] == '\'') panel_attr_label_text[n]='_';
          if (panel_attr_label_text[n] == '-') panel_attr_label_text[n]='_';
          n++;
        }

        fprintf (stderr, "! Panel Id: %s , bcolor=%d, h=%d, w=%d Count=%d\n",panel_attr_label_text,panel_attr_backcolor, panel_h,panel_w, numControls);

        sprintf( outputFileName, "%s%s%s.js\0",driveName,dirName,panelName) ;
        if (i>=0)
          sprintf( outputFileName, "%s%s%s_%d.js\0",driveName,dirName,panelName,i) ;
        fprintf (stderr, "! File out: %s \n",outputFileName);

        outfile = fopen(outputFileName,"w+");

        fprintf(outfile ,"");
        fprintf(outfile ,"Ext.define('MyPanels.%s', {\n", panelName);
        fprintf(outfile ,"id: '%s',\n", panelName);
        fprintf(outfile ,"title: '%s',\n", panel_attr_label_text);
        fprintf(outfile ,"width: %d ,\n", panel_w+16 );
        fprintf(outfile ,"height:%d ,\n", panel_h+32 );
        fprintf(outfile ,"layout: { type: 'absolute'},\n");
        fprintf(outfile ,"bodyStyle: 'background-color:%s',\n", Int2HexWeb(panel_attr_backcolor) );
        fprintf(outfile ,"items: [\n");

        flagNotFirst = 0;

        GetPanelAttribute(panelHandle,ATTR_PANEL_FIRST_CTRL,&ctrlID);
        while (ctrlID != 0)
        {

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_CTRL_STYLE, &attr_ctrl_style);

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_CONSTANT_NAME,   attr_constant_name); //!!

             GetCtrlAttribute(panelHandle, ctrlID, ATTR_LEFT,       &x);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_TOP,        &y);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_HEIGHT,     &h);
             GetCtrlAttribute(panelHandle, ctrlID, ATTR_WIDTH,      &w);

             fprintf (stderr, "! Panel element Id: %s [%d] , x=%d, y=%d, h=%d, w=%d \n",attr_constant_name,attr_ctrl_style, x,y,h,w);

             switch(attr_ctrl_style)
             {
               case CTRL_TABS:
               {

                   if(flagNotFirst){fprintf(outfile ,",\n");} else {flagNotFirst = 1;};

                 //if (i>-1 && tabFlag) {
                   fprintf(outfile ," {\n");
                   fprintf(outfile ,"  id: '%s',\n", attr_constant_name );
                   fprintf(outfile ,"  xtype: 'tabpanel',\n");
                   fprintf(outfile ,"  //fullscreen: true,\n");
                   fprintf(outfile ,"  //tabBarPosition: 'bottom',\n");
                   fprintf(outfile ,"  x: %d,\n",x);
                   fprintf(outfile ,"  y: %d,\n",y);
                   fprintf(outfile ,"  height: %d,\n",h);
                   fprintf(outfile ,"  width: %d,\n",w);
                   fprintf(outfile ,"  activeTab: 0,\n");
                   fprintf(outfile ,"  renderTo: Ext.getBody(),\n");
                   fprintf(outfile ,"  items: [\n");

                   flagNotFirst = 0;
                 //}

                   GetNumTabPages (panelHandle, ctrlID, &tabCount);
                   for (tabIndex=((i>=0)?i:0);tabIndex<tabCount;tabIndex++)
                   {

                     // Get the tab-page label and handle
                     attr_label_text[0]='\0';
                     GetTabPageAttribute(panelHandle, ctrlID, tabIndex, ATTR_LABEL_TEXT, attr_label_text); // ATTR_LABEL_TEXT_LENGTH
                     GetTabPageAttribute(panelHandle, ctrlID, tabIndex, ATTR_BACKCOLOR,  &attr_backcolor);
                     GetTabPageAttribute(panelHandle, ctrlID, tabIndex, ATTR_DIMMED,     &attr_dimmed); // 0 = Enabled 1 = Dimmed
                     GetTabPageAttribute(panelHandle, ctrlID, tabIndex, ATTR_VISIBLE,    &attr_visible); // 0 1 = Visible

                     fprintf (stderr, "! Tab: (%d of %d) %s  bg=%d, dimmed=%d, visible=%d \n",tabIndex,tabCount,attr_label_text,attr_backcolor, attr_dimmed,attr_visible);

                     if (attr_dimmed) continue ;
                     //if (!attr_visible) continue ;

                     GetPanelHandleFromTabPage(panelHandle, ctrlID, tabIndex, &tabPanel);

                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_LEFT,              &x);
                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_TOP,               &y);
                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_HEIGHT,            &h);
                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_WIDTH,             &w);
                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_ZPLANE_POSITION,   &attr_zplane_position);
                     GetCtrlAttribute(panelHandle, ctrlID, ATTR_CTRL_TAB_POSITION, &attr_ctrl_tab_position);

                     fprintf (stderr, "! Tab tabPanel:  x=%d, y=%d, h=%d,w=%d,zp=%d,ctrltab=%d \n",x,y, h,w,attr_zplane_position,attr_ctrl_tab_position);

                     n=0;
                     while ( attr_label_text[n]!= '\0' ) {
                       if (attr_label_text[n] == ' ') attr_label_text[n]='_';
                       if (attr_label_text[n] == '-') attr_label_text[n]='_';
                       n++;
                     }
                     sprintf( outputFileNameTab, "%s%s%s_%s.js\0",driveName,dirName,panelName,attr_label_text) ;
                     fprintf (stderr, "! File Tab out: %s \n",outputFileNameTab);

                     fprintf(outfile ,"");
                     fprintf(outfile ,"  {\n");
                     fprintf(outfile ,"    id: '%s',\n",GenId());
                     fprintf(outfile ,"    title: '%s',\n",attr_label_text);
                     fprintf(outfile ,"    layout: 'fit',\n");
                     fprintf(outfile ,"    //layout: 'absolute',\n");
                     fprintf(outfile ,"    //border: true,\n");
                     fprintf(outfile ,"    items: [ {\n");

                     flagNotFirst = 0;

                     fprintf(outfile ,"       xtype : 'panel',\n");
                     //fprintf(outfile ,"       vpPanelId: '%s%d',\n",GenId(),tabIndex);
                     fprintf(outfile ,"       id: '%s%d',\n",GenId(),tabIndex);
                     fprintf(outfile ,"       //frame : true,\n");
                     fprintf(outfile ,"       layout: { type: 'absolute'},\n");
                     fprintf(outfile ,"       bodyStyle: 'background-color:%s',\n", Int2HexWeb(attr_backcolor) );
                     fprintf(outfile ,"       items: [\n");

                     flagNotFirst = 0;

                     // Get the first control in the tab-page to convert
                     GetPanelAttribute(tabPanel, ATTR_PANEL_FIRST_CTRL, &ctrl);
                     while (ctrl != 0)
                     {
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_CONSTANT_NAME,   attr_constant_name); //!!
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_CTRL_STYLE, &attr_ctrl_style);

                        GetCtrlAttribute(tabPanel, ctrl, ATTR_LEFT,       &x);
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_TOP,        &y);
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_HEIGHT,     &h);
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_WIDTH,      &w);

                        // fprintf (stderr, "! element Id: %s [%d] , x=%d, y=%d, h=%d, w=%d \n",attr_constant_name,attr_ctrl_style, x,y,h,w);

                        GetCtrlElem ( outfile , tabPanel, ctrl ); // write to main file

                        // Get the next control
                        GetCtrlAttribute(tabPanel, ctrl, ATTR_NEXT_CTRL, &ctrl);
                     }

                     fprintf(outfile ,"       ]\n");

                     fprintf(outfile ,"    } ]\n");
                     fprintf(outfile ,"  }");

                     if (i>=0) break;
                     if (tabIndex!=(tabCount-1)) fprintf(outfile ,","); fprintf(outfile ,"\n");
                   }

                 //if (i>-1 && tabFlag) {
                   fprintf(outfile ,"  ]\n");
                   fprintf(outfile ," }");
                 //}

                   flagNotFirst = 1; // flagNotFirstSave ;

               } break ;

               default:
                  GetCtrlElem ( outfile , panelHandle, ctrlID );
               break;
             }

             GetCtrlAttribute(panelHandle,ctrlID,ATTR_NEXT_CTRL,&ctrlID);
        }

        fprintf(outfile,"\n");
        fprintf(outfile,"]\n");
        fprintf(outfile,"});\n");

        fclose(outfile);

        if (i>=0) {
            if (remove(outputFileNameTab)) {
                fprintf (stderr, "! Error , not remove file = %s\n",outputFileNameTab );
            }
            if (0==rename ( outputFileName , outputFileNameTab )) {
                fprintf (stderr, "! rename file to = %s\n",outputFileNameTab );
            } else {
                fprintf (stderr, "! Error , not rename file = %s\n",outputFileName );
            }
        }

    }

    if (showPanel)
      RunUserInterface();
    else
      if (panelHandle>=0) DiscardPanel(panelHandle);

    CloseCVIRTE();

    return 0;
}

//==============================================================================
// UI callback function prototypes

/// HIFN Exit when the user dismisses the panel.
int CVICALLBACK GraphPanelCallback(int panel, int evnt, void *callbackData, int eventData1, int eventData2)
{
  switch (evnt)
  {
   case EVENT_CLOSE:
     if (panel>=0) DiscardPanel(panel);
     QuitUserInterface(0);
   break;
  }
  return 0;
}




