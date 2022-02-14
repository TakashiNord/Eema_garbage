Ext.define('MyPanels.testtes', {
id: 'testtes',
title: 'Union Panel',
width: 1300,
height: 544,
bodyStyle: 'background-color:#f0f0f0',
deferredRender: true,
items: [{ 
  id: 'test0', 
  xtype: 'tabpanel', 
  items: [
          {
            id: 'obshhaya_ces', 
            title: 'ЦЭС',
	        items: [{
               xtype : 'panel',
			   vpPanelId : '335',
			   id: '335',
			   layout: { type: 'absolute'},
			   bodyStyle: 'background-color:#ffffcc',
	           items: [

{
xtype: 'button',
x: 1068,
y: 282,
height: 23,
width: 36,
style:{'z-index': 9696},
text: '?',
id: 'COMMANDBUTTON_448',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 420,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9710',
id: 'DECORATION_150'
},
{
xtype: 'textfield',
x: 214,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_267',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 218,
y: 271,
height:15,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9999;font-weight:bold',
id: 'NUMERIC_U154_267_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 5,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_259',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 9,
y: 271,
height:15,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9724;font-weight:bold',
id: 'NUMERIC_U154_259_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 950,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_266',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 954,
y: 272,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9736;font-weight:bold',
id: 'NUMERIC_U154_266_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 845,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_265',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 849,
y: 272,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9735;font-weight:bold',
id: 'NUMERIC_U154_265_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 740,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_264',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 744,
y: 272,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9733;font-weight:bold',
id: 'NUMERIC_U154_264_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 635,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_263',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 639,
y: 272,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9732;font-weight:bold',
id: 'NUMERIC_U154_263_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 530,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_262',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 534,
y: 272,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9731;font-weight:bold',
id: 'NUMERIC_U154_262_LABEL',
text: 't окр.'
},
{
xtype: 'textfield',
x: 425,
y: 286,
width: 44,
height: 20,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_261',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '9px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 429,
y: 271,
height:14,
width:30,
style: 'padding:2px 0 2px 4px;z-index: 9730;font-weight:bold',
id: 'NUMERIC_U154_261_LABEL',
text: 't окр.'
},
{
xtype: 'label',
x: 423,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9711;font-size: 12pt;font-weight:bold',
id: 'TEXT_455',
text: '11Е'
},
{
xtype: 'button',
x: 452,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9712},
text: '',
id: 'COMMANDBUTTON_455',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 350,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9706',
id: 'DECORATION_149'
},
{
xtype: 'label',
x: 353,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9707;font-size: 12pt;font-weight:bold',
id: 'TEXT_454',
text: '11Д'
},
{
xtype: 'button',
x: 382,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9708},
text: '',
id: 'COMMANDBUTTON_453',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 280,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9702',
id: 'DECORATION_148'
},
{
xtype: 'label',
x: 283,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9703;font-size: 12pt;font-weight:bold',
id: 'TEXT_453',
text: '11Г'
},
{
xtype: 'button',
x: 312,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9704},
text: '',
id: 'COMMANDBUTTON_451',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 210,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9698',
id: 'DECORATION_147'
},
{
xtype: 'label',
x: 213,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9699;font-size: 12pt;font-weight:bold',
id: 'TEXT_452',
text: '11В'
},
{
xtype: 'button',
x: 242,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9700},
text: '',
id: 'COMMANDBUTTON_449',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 140,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9682',
id: 'DECORATION_143'
},
{
xtype: 'label',
x: 143,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9683;font-size: 12pt;font-weight:bold',
id: 'TEXT_192',
text: '11Б'
},
{
xtype: 'button',
x: 172,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9684},
text: '',
id: 'COMMANDBUTTON_343',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 490,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9602',
id: 'DECORATION_122'
},
{
xtype: 'label',
x: 563,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9689;font-size: 12pt;font-weight:bold',
id: 'TEXT_193',
text: '15'
},
{
xtype: 'label',
x: 493,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9603;font-size: 12pt;font-weight:bold',
id: 'TEXT_125',
text: '12'
},
{
xtype: 'button',
x: 592,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9691},
text: '',
id: 'COMMANDBUTTON_347',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 522,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9604},
text: '',
id: 'COMMANDBUTTON_312',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 70,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9598',
id: 'DECORATION_121'
},
{
xtype: 'label',
x: 73,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9765;font-size: 12pt;font-weight:bold',
id: 'TEXT_124',
text: '11А'
},
{
xtype: 'button',
x: 610,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9671},
text: '',
id: 'COMMANDBUTTON_335',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1030,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9674},
text: '',
id: 'COMMANDBUTTON_338',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 925,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9672},
text: '',
id: 'COMMANDBUTTON_336',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 505,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9670},
text: '',
id: 'COMMANDBUTTON_334',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 820,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9734},
text: '',
id: 'COMMANDBUTTON_459',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 715,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9669},
text: '',
id: 'COMMANDBUTTON_333',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 120,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9601},
text: '',
id: 'COMMANDBUTTON_309',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 102,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9599},
text: '',
id: 'COMMANDBUTTON_310',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 72,
y: 42,
height: 14,
width: 14,
id: 'LED_362',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#3300ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 42,
height: 14,
width: 14,
id: 'LED_306',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#3300ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 0,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9545',
id: 'DECORATION_3'
},
{
xtype: 'label',
x: 3,
y: 243,
height:35,
width:95,
style: 'padding:2px 0 2px 4px;z-index: 9546;font-size: 9pt;font-weight:bold',
id: 'TEXT_4',
text: 'ПС 330 кВ Мончегорск'
},
{
xtype: 'button',
x: 85,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9548},
text: '',
id: 'COMMANDBUTTON_5',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 67,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9547},
text: '',
id: 'COMMANDBUTTON_6',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 437,
y: 42,
height: 14,
width: 14,
id: 'LED_351',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 42,
height: 14,
width: 14,
id: 'LED_349',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 42,
height: 14,
width: 14,
id: 'LED_347',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 42,
height: 14,
width: 14,
id: 'LED_345',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 102,
height: 14,
width: 14,
id: 'LED_448',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 102,
height: 14,
width: 14,
id: 'LED_396',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 162,
height: 14,
width: 14,
id: 'LED_380',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 577,
y: 162,
height: 14,
width: 14,
id: 'LED_444',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 507,
y: 162,
height: 14,
width: 14,
id: 'LED_364',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 42,
height: 14,
width: 14,
id: 'LED_343',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 222,
height: 14,
width: 14,
id: 'LED_393',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 42,
height: 14,
width: 14,
id: 'LED_475',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 437,
y: 162,
height: 14,
width: 14,
id: 'LED_474',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 162,
height: 14,
width: 14,
id: 'LED_390',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 437,
y: 102,
height: 14,
width: 14,
id: 'LED_482',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 102,
height: 14,
width: 14,
id: 'LED_387',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 102,
height: 14,
width: 14,
id: 'LED_376',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 42,
height: 14,
width: 14,
id: 'LED_447',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 102,
height: 14,
width: 14,
id: 'LED_458',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 42,
height: 14,
width: 14,
id: 'LED_457',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 42,
height: 14,
width: 14,
id: 'LED_372',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 647,
y: 42,
height: 14,
width: 14,
id: 'LED_370',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 577,
y: 42,
height: 14,
width: 14,
id: 'LED_337',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 162,
height: 14,
width: 14,
id: 'LED_335',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0000ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 213,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9783;font-size: 12pt;font-weight:bold',
id: 'TEXT_459',
text: '70'
},
{
xtype: 'label',
x: 563,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9936;font-size: 12pt;font-weight:bold',
id: 'TEXT_483',
text: '83'
},
{
xtype: 'fieldset',
x: 770,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9574',
id: 'DECORATION_112'
},
{
xtype: 'label',
x: 493,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9726;font-size: 12pt;font-weight:bold',
id: 'TEXT_456',
text: '80'
},
{
xtype: 'button',
x: 260,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9784},
text: '',
id: 'COMMANDBUTTON_477',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 773,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9575;font-size: 12pt;font-weight:bold',
id: 'TEXT_116',
text: '87'
},
{
xtype: 'button',
x: 610,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9938},
text: '',
id: 'COMMANDBUTTON_521',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 592,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9937},
text: '',
id: 'COMMANDBUTTON_522',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 1050,
y: 250,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9695',
id: 'DECORATION_86'
},
{
xtype: 'button',
x: 242,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9787},
text: '',
id: 'COMMANDBUTTON_478',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 540,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9728},
text: '',
id: 'COMMANDBUTTON_457',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 522,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9727},
text: '',
id: 'COMMANDBUTTON_458',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 890,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9666},
text: '',
id: 'COMMANDBUTTON_330',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 820,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9577},
text: '',
id: 'COMMANDBUTTON_298',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 802,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9576},
text: '',
id: 'COMMANDBUTTON_299',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 367,
y: 222,
height: 14,
width: 14,
id: 'LED_451',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 162,
height: 14,
width: 14,
id: 'LED_481',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 222,
height: 14,
width: 14,
id: 'LED_480',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 927,
y: 102,
height: 14,
width: 14,
id: 'LED_479',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 507,
y: 222,
height: 14,
width: 14,
id: 'LED_478',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 222,
height: 14,
width: 14,
id: 'LED_477',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 222,
height: 14,
width: 14,
id: 'LED_384',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 222,
height: 14,
width: 14,
id: 'LED_355',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 17,
y: 222,
height: 14,
width: 14,
id: 'LED_461',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1067,
y: 162,
height: 14,
width: 14,
id: 'LED_333',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0033ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 980,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9595',
id: 'DECORATION_120'
},
{
xtype: 'label',
x: 983,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9596;font-size: 10pt;font-weight:bold',
id: 'TEXT_123',
text: 'КАЭС'
},
{
xtype: 'button',
x: 907,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9673},
text: '',
id: 'COMMANDBUTTON_337',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1012,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9597},
text: '',
id: 'COMMANDBUTTON_308',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 945,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9592',
id: 'DECORATION_117'
},
{
xtype: 'label',
x: 948,
y: 243,
height:34,
width:106,
style: 'padding:2px 0 2px 4px;z-index: 9593;font-size: 9pt;font-weight:bold',
id: 'TEXT_120',
text: 'Княжегубская ГЭС 11'
},
{
xtype: 'button',
x: 1012,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9594},
text: '',
id: 'COMMANDBUTTON_305',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 840,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9590',
id: 'DECORATION_118'
},
{
xtype: 'label',
x: 843,
y: 243,
height:34,
width:65,
style: 'padding:2px 0 2px 4px;z-index: 9591;font-size: 9pt;font-weight:bold',
id: 'TEXT_121',
text: 'Йовская ГЭС 10'
},
{
xtype: 'fieldset',
x: 735,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9587',
id: 'DECORATION_119'
},
{
xtype: 'label',
x: 738,
y: 243,
height:35,
width:65,
style: 'padding:2px 0 2px 4px;z-index: 9588;font-size: 9pt;font-weight:bold',
id: 'TEXT_122',
text: 'Кумская ГЭС 9'
},
{
xtype: 'button',
x: 802,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9589},
text: '',
id: 'COMMANDBUTTON_307',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 630,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9584',
id: 'DECORATION_116'
},
{
xtype: 'label',
x: 633,
y: 243,
height:34,
width:44,
style: 'padding:2px 0 2px 4px;z-index: 9585;font-size: 9pt;font-weight:bold',
id: 'TEXT_119',
text: 'Нива ГЭС 3'
},
{
xtype: 'button',
x: 697,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9586},
text: '',
id: 'COMMANDBUTTON_304',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 525,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9581',
id: 'DECORATION_115'
},
{
xtype: 'label',
x: 528,
y: 243,
height:34,
width:44,
style: 'padding:2px 0 2px 4px;z-index: 9582;font-size: 9pt;font-weight:bold',
id: 'TEXT_118',
text: 'Нива ГЭС 2'
},
{
xtype: 'button',
x: 592,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9583},
text: '',
id: 'COMMANDBUTTON_303',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 420,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9578',
id: 'DECORATION_114'
},
{
xtype: 'label',
x: 423,
y: 243,
style: 'padding:2px 0 2px 4px;z-index: 9579;font-size: 9pt;font-weight:bold',
id: 'TEXT_117',
text: 'Нива ГЭС 1'
},
{
xtype: 'button',
x: 487,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9580},
text: '',
id: 'COMMANDBUTTON_302',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 910,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9570',
id: 'DECORATION_111'
},
{
xtype: 'label',
x: 913,
y: 183,
height:22,
width:51,
style: 'padding:2px 0 2px 4px;z-index: 9571;font-size: 10pt;font-weight:bold',
id: 'TEXT_115',
text: 'АТЭЦ'
},
{
xtype: 'button',
x: 960,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9573},
text: '',
id: 'COMMANDBUTTON_296',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 662,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9676},
text: '',
id: 'COMMANDBUTTON_340',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 942,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9677},
text: '',
id: 'COMMANDBUTTON_341',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1012,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9675},
text: '',
id: 'COMMANDBUTTON_339',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 213,
y: 183,
height:22,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9795;font-size: 12pt;font-weight:bold',
id: 'TEXT_461',
text: '106'
},
{
xtype: 'button',
x: 942,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9572},
text: '',
id: 'COMMANDBUTTON_297',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 703,
y: 183,
height:22,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9754;font-size: 12pt;font-weight:bold',
id: 'TEXT_457',
text: '370'
},
{
xtype: 'label',
x: 633,
y: 183,
height:22,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9656;font-size: 12pt;font-weight:bold',
id: 'TEXT_143',
text: '368'
},
{
xtype: 'fieldset',
x: 980,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9650',
id: 'DECORATION_141'
},
{
xtype: 'label',
x: 983,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9651;font-size: 12pt;font-weight:bold',
id: 'TEXT_142',
text: '49'
},
{
xtype: 'fieldset',
x: 910,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9648',
id: 'DECORATION_140'
},
{
xtype: 'label',
x: 913,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9649;font-size: 12pt;font-weight:bold',
id: 'TEXT_141',
text: '48'
},
{
xtype: 'fieldset',
x: 840,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9617',
id: 'DECORATION_127'
},
{
xtype: 'label',
x: 843,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9618;font-size: 12pt;font-weight:bold',
id: 'TEXT_130',
text: '44'
},
{
xtype: 'button',
x: 872,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9619},
text: '',
id: 'COMMANDBUTTON_317',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 70,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9644',
id: 'DECORATION_136'
},
{
xtype: 'label',
x: 73,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9645;font-size: 12pt;font-weight:bold',
id: 'TEXT_139',
text: '95'
},
{
xtype: 'button',
x: 102,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9646},
text: '',
id: 'COMMANDBUTTON_327',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 0,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9641',
id: 'DECORATION_135'
},
{
xtype: 'label',
x: 3,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9642;font-size: 12pt;font-weight:bold',
id: 'TEXT_138',
text: '94'
},
{
xtype: 'button',
x: 32,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9643},
text: '',
id: 'COMMANDBUTTON_326',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 1050,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9638',
id: 'DECORATION_133'
},
{
xtype: 'label',
x: 1053,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9639;font-size: 12pt;font-weight:bold',
id: 'TEXT_136',
text: '93*'
},
{
xtype: 'button',
x: 1082,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9640},
text: '',
id: 'COMMANDBUTTON_324',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 840,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9635',
id: 'DECORATION_134'
},
{
xtype: 'label',
x: 843,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9636;font-size: 12pt;font-weight:bold',
id: 'TEXT_137',
text: '88'
},
{
xtype: 'button',
x: 872,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9637},
text: '',
id: 'COMMANDBUTTON_325',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 700,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9632',
id: 'DECORATION_131'
},
{
xtype: 'label',
x: 703,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9633;font-size: 12pt;font-weight:bold',
id: 'TEXT_134',
text: '85'
},
{
xtype: 'button',
x: 732,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9634},
text: '',
id: 'COMMANDBUTTON_322',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 630,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9629',
id: 'DECORATION_132'
},
{
xtype: 'label',
x: 633,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9630;font-size: 12pt;font-weight:bold',
id: 'TEXT_135',
text: '84'
},
{
xtype: 'button',
x: 662,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9631},
text: '',
id: 'COMMANDBUTTON_323',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 140,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9626',
id: 'DECORATION_130'
},
{
xtype: 'label',
x: 143,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9627;font-size: 12pt;font-weight:bold',
id: 'TEXT_133',
text: '69'
},
{
xtype: 'button',
x: 172,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9628},
text: '',
id: 'COMMANDBUTTON_321',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 210,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9782',
id: 'DECORATION_154'
},
{
xtype: 'fieldset',
x: 350,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9828',
id: 'DECORATION_161'
},
{
xtype: 'label',
x: 353,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9829;font-size: 12pt;font-weight:bold',
id: 'TEXT_466',
text: '203A'
},
{
xtype: 'button',
x: 382,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9830},
text: '',
id: 'COMMANDBUTTON_487',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 1050,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9824',
id: 'DECORATION_160'
},
{
xtype: 'label',
x: 1053,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9825;font-size: 12pt;font-weight:bold',
id: 'TEXT_465',
text: '54'
},
{
xtype: 'fieldset',
x: 560,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9935',
id: 'DECORATION_178'
},
{
xtype: 'button',
x: 1082,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9826},
text: '',
id: 'COMMANDBUTTON_485',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 0,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9623',
id: 'DECORATION_129'
},
{
xtype: 'label',
x: 3,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9624;font-size: 12pt;font-weight:bold',
id: 'TEXT_132',
text: '62'
},
{
xtype: 'button',
x: 32,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9625},
text: '',
id: 'COMMANDBUTTON_320',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 490,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9725',
id: 'DECORATION_151'
},
{
xtype: 'fieldset',
x: 770,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9614',
id: 'DECORATION_126'
},
{
xtype: 'label',
x: 773,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9615;font-size: 12pt;font-weight:bold',
id: 'TEXT_129',
text: '42'
},
{
xtype: 'button',
x: 802,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9616},
text: '',
id: 'COMMANDBUTTON_316',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 700,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9611',
id: 'DECORATION_125'
},
{
xtype: 'label',
x: 703,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9612;font-size: 12pt;font-weight:bold',
id: 'TEXT_128',
text: '41А'
},
{
xtype: 'button',
x: 732,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9613},
text: '',
id: 'COMMANDBUTTON_315',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 700,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9608',
id: 'DECORATION_124'
},
{
xtype: 'label',
x: 703,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9609;font-size: 12pt;font-weight:bold',
id: 'TEXT_127',
text: '18А'
},
{
xtype: 'button',
x: 312,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9823},
text: '',
id: 'COMMANDBUTTON_484',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 732,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9610},
text: '',
id: 'COMMANDBUTTON_314',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 630,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9605',
id: 'DECORATION_123'
},
{
xtype: 'label',
x: 633,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9606;font-size: 12pt;font-weight:bold',
id: 'TEXT_126',
text: '18'
},
{
xtype: 'fieldset',
x: 210,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9794',
id: 'DECORATION_156'
},
{
xtype: 'button',
x: 662,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9607},
text: '',
id: 'COMMANDBUTTON_313',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 560,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9688',
id: 'DECORATION_146'
},
{
xtype: 'fieldset',
x: 700,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9753',
id: 'DECORATION_152'
},
{
xtype: 'fieldset',
x: 630,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9647',
id: 'DECORATION_138'
},
{
xtype: 'fieldset',
x: 315,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9567',
id: 'DECORATION_110'
},
{
xtype: 'label',
x: 318,
y: 243,
style: 'padding:2px 0 2px 4px;z-index: 9568;font-size: 9pt;font-weight:bold',
id: 'TEXT_114',
text: 'ПС 330  Княжегубская'
},
{
xtype: 'button',
x: 382,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9569},
text: '',
id: 'COMMANDBUTTON_295',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 210,
y: 242,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9563',
id: 'DECORATION_109'
},
{
xtype: 'label',
x: 213,
y: 243,
height:34,
width:74,
style: 'padding:2px 0 2px 4px;z-index: 9564;font-size: 9pt;font-weight:bold',
id: 'TEXT_113',
text: 'ПС 330 кВ Титан'
},
{
xtype: 'button',
x: 400,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9995},
text: '',
id: 'COMMANDBUTTON_534',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 295,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9566},
text: '',
id: 'COMMANDBUTTON_292',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 277,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9565},
text: '',
id: 'COMMANDBUTTON_293',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 105,
y: 240,
height: 70,
width: 105,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9559',
id: 'DECORATION_108'
},
{
xtype: 'label',
x: 108,
y: 243,
height:35,
width:95,
style: 'padding:2px 0 2px 4px;z-index: 9560;font-size: 9pt;font-weight:bold',
id: 'TEXT_112',
text: 'ПС 330 кВ Оленегорск'
},
{
xtype: 'button',
x: 190,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9562},
text: '',
id: 'COMMANDBUTTON_290',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 172,
y: 290,
height: 17,
width: 17,
style:{'z-index': 9561},
text: '',
id: 'COMMANDBUTTON_291',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 630,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9620',
id: 'DECORATION_128'
},
{
xtype: 'button',
x: 540,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9743},
text: '',
id: 'COMMANDBUTTON_465',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 470,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9713},
text: '',
id: 'COMMANDBUTTON_456',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 633,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9621;font-size: 12pt;font-weight:bold',
id: 'TEXT_131',
text: '41'
},
{
xtype: 'fieldset',
x: 560,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9992',
id: 'DECORATION_182'
},
{
xtype: 'button',
x: 260,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9796},
text: '',
id: 'COMMANDBUTTON_480',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 490,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9989',
id: 'DECORATION_181'
},
{
xtype: 'label',
x: 563,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9993;font-size: 12pt;font-weight:bold',
id: 'TEXT_487',
text: '40В'
},
{
xtype: 'button',
x: 662,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9622},
text: '',
id: 'COMMANDBUTTON_319',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 420,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9549',
id: 'DECORATION_105'
},
{
xtype: 'label',
x: 493,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9990;font-size: 12pt;font-weight:bold',
id: 'TEXT_486',
text: '40Б'
},
{
xtype: 'button',
x: 400,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9709},
text: '',
id: 'COMMANDBUTTON_454',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 750,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9755},
text: '',
id: 'COMMANDBUTTON_475',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 423,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9550;font-size: 12pt;font-weight:bold',
id: 'TEXT_110',
text: '40А'
},
{
xtype: 'button',
x: 1100,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9678},
text: '',
id: 'COMMANDBUTTON_342',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 400,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9831},
text: '',
id: 'COMMANDBUTTON_488',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 680,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9752},
text: '',
id: 'COMMANDBUTTON_474',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 120,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9751},
text: '',
id: 'COMMANDBUTTON_473',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 50,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9750},
text: '',
id: 'COMMANDBUTTON_472',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1100,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9827},
text: '',
id: 'COMMANDBUTTON_486',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 750,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9749},
text: '',
id: 'COMMANDBUTTON_471',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 680,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9748},
text: '',
id: 'COMMANDBUTTON_470',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 190,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9747},
text: '',
id: 'COMMANDBUTTON_469',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 50,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9746},
text: '',
id: 'COMMANDBUTTON_468',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1030,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9745},
text: '',
id: 'COMMANDBUTTON_467',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 960,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9744},
text: '',
id: 'COMMANDBUTTON_466',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 890,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9741},
text: '',
id: 'COMMANDBUTTON_463',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 820,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9740},
text: '',
id: 'COMMANDBUTTON_462',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 750,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9739},
text: '',
id: 'COMMANDBUTTON_461',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 680,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9738},
text: '',
id: 'COMMANDBUTTON_460',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 470,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9552},
text: '',
id: 'COMMANDBUTTON_284',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 330,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9705},
text: '',
id: 'COMMANDBUTTON_452',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 770,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9812',
id: 'DECORATION_159'
},
{
xtype: 'button',
x: 592,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9994},
text: '',
id: 'COMMANDBUTTON_533',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 773,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9813;font-size: 12pt;font-weight:bold',
id: 'TEXT_464',
text: '412'
},
{
xtype: 'fieldset',
x: 350,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9789',
id: 'DECORATION_155'
},
{
xtype: 'fieldset',
x: 280,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9806',
id: 'DECORATION_158'
},
{
xtype: 'label',
x: 283,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9807;font-size: 12pt;font-weight:bold',
id: 'TEXT_463',
text: '75'
},
{
xtype: 'button',
x: 522,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9991},
text: '',
id: 'COMMANDBUTTON_531',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 353,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9790;font-size: 12pt;font-weight:bold',
id: 'TEXT_460',
text: '35'
},
{
xtype: 'fieldset',
x: 280,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9800',
id: 'DECORATION_157'
},
{
xtype: 'label',
x: 283,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9801;font-size: 12pt;font-weight:bold',
id: 'TEXT_462',
text: '34'
},
{
xtype: 'button',
x: 820,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9814},
text: '',
id: 'COMMANDBUTTON_483',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 452,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9551},
text: '',
id: 'COMMANDBUTTON_285',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 210,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9776',
id: 'DECORATION_153'
},
{
xtype: 'label',
x: 213,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9777;font-size: 12pt;font-weight:bold',
id: 'TEXT_458',
text: '33'
},
{
xtype: 'button',
x: 330,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9808},
text: '',
id: 'COMMANDBUTTON_482',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 910,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9929',
id: 'DECORATION_177'
},
{
xtype: 'fieldset',
x: 840,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9923',
id: 'DECORATION_176'
},
{
xtype: 'label',
x: 913,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9933;font-size: 12pt;font-weight:bold',
id: 'TEXT_482',
text: '25'
},
{
xtype: 'fieldset',
x: 560,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9972',
id: 'DECORATION_180'
},
{
xtype: 'fieldset',
x: 490,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9917',
id: 'DECORATION_175'
},
{
xtype: 'label',
x: 843,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9927;font-size: 12pt;font-weight:bold',
id: 'TEXT_481',
text: '415'
},
{
xtype: 'label',
x: 563,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9976;font-size: 12pt;font-weight:bold',
id: 'TEXT_485',
text: '361'
},
{
xtype: 'fieldset',
x: 420,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9911',
id: 'DECORATION_174'
},
{
xtype: 'label',
x: 493,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9921;font-size: 12pt;font-weight:bold',
id: 'TEXT_480',
text: '360'
},
{
xtype: 'fieldset',
x: 280,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9905',
id: 'DECORATION_173'
},
{
xtype: 'label',
x: 423,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9915;font-size: 12pt;font-weight:bold',
id: 'TEXT_479',
text: '331'
},
{
xtype: 'fieldset',
x: 140,
y: 180,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9899',
id: 'DECORATION_172'
},
{
xtype: 'label',
x: 283,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9909;font-size: 12pt;font-weight:bold',
id: 'TEXT_478',
text: '112'
},
{
xtype: 'fieldset',
x: 980,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9893',
id: 'DECORATION_171'
},
{
xtype: 'button',
x: 960,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9931},
text: '',
id: 'COMMANDBUTTON_519',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 942,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9930},
text: '',
id: 'COMMANDBUTTON_520',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 143,
y: 183,
style: 'padding:2px 0 2px 4px;z-index: 9903;font-size: 12pt;font-weight:bold',
id: 'TEXT_477',
text: '96'
},
{
xtype: 'fieldset',
x: 910,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9887',
id: 'DECORATION_170'
},
{
xtype: 'button',
x: 890,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9925},
text: '',
id: 'COMMANDBUTTON_517',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 872,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9924},
text: '',
id: 'COMMANDBUTTON_518',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 610,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9974},
text: '',
id: 'COMMANDBUTTON_528',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 592,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9973},
text: '',
id: 'COMMANDBUTTON_529',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 983,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9897;font-size: 12pt;font-weight:bold',
id: 'TEXT_476',
text: '92'
},
{
xtype: 'fieldset',
x: 420,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9881',
id: 'DECORATION_169'
},
{
xtype: 'button',
x: 540,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9919},
text: '',
id: 'COMMANDBUTTON_515',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 522,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9918},
text: '',
id: 'COMMANDBUTTON_516',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 913,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9891;font-size: 12pt;font-weight:bold',
id: 'TEXT_475',
text: '91'
},
{
xtype: 'fieldset',
x: 350,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9875',
id: 'DECORATION_168'
},
{
xtype: 'button',
x: 470,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9913},
text: '',
id: 'COMMANDBUTTON_513',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 452,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9912},
text: '',
id: 'COMMANDBUTTON_514',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 423,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9885;font-size: 12pt;font-weight:bold',
id: 'TEXT_474',
text: '79'
},
{
xtype: 'label',
x: 353,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9876;font-size: 12pt;font-weight:bold',
id: 'TEXT_473',
text: '78'
},
{
xtype: 'button',
x: 330,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9907},
text: '',
id: 'COMMANDBUTTON_511',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 312,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9906},
text: '',
id: 'COMMANDBUTTON_512',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 70,
y: 120,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9869',
id: 'DECORATION_167'
},
{
xtype: 'label',
x: 73,
y: 123,
style: 'padding:2px 0 2px 4px;z-index: 9870;font-size: 12pt;font-weight:bold',
id: 'TEXT_472',
text: '65'
},
{
xtype: 'button',
x: 190,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9901},
text: '',
id: 'COMMANDBUTTON_509',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 172,
y: 220,
height: 17,
width: 17,
style:{'z-index': 9900},
text: '',
id: 'COMMANDBUTTON_510',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 140,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9962',
id: 'DECORATION_179'
},
{
xtype: 'label',
x: 143,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9963;font-size: 12pt;font-weight:bold',
id: 'TEXT_484',
text: '32'
},
{
xtype: 'fieldset',
x: 70,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9863',
id: 'DECORATION_166'
},
{
xtype: 'label',
x: 73,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9864;font-size: 12pt;font-weight:bold',
id: 'TEXT_471',
text: '31'
},
{
xtype: 'button',
x: 1030,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9895},
text: '',
id: 'COMMANDBUTTON_507',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1012,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9894},
text: '',
id: 'COMMANDBUTTON_508',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 1050,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9857',
id: 'DECORATION_165'
},
{
xtype: 'label',
x: 1053,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9858;font-size: 12pt;font-weight:bold',
id: 'TEXT_470',
text: '26'
},
{
xtype: 'button',
x: 960,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9889},
text: '',
id: 'COMMANDBUTTON_505',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 942,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9888},
text: '',
id: 'COMMANDBUTTON_506',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 980,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9851',
id: 'DECORATION_164'
},
{
xtype: 'label',
x: 983,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9852;font-size: 12pt;font-weight:bold',
id: 'TEXT_469',
text: '25A'
},
{
xtype: 'button',
x: 470,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9883},
text: '',
id: 'COMMANDBUTTON_503',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 452,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9882},
text: '',
id: 'COMMANDBUTTON_504',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 927,
y: 26,
height: 14,
width: 14,
id: 'LED_442',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 840,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9845',
id: 'DECORATION_163'
},
{
xtype: 'button',
x: 400,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9878},
text: '',
id: 'COMMANDBUTTON_501',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 382,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9877},
text: '',
id: 'COMMANDBUTTON_502',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 843,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9846;font-size: 12pt;font-weight:bold',
id: 'TEXT_468',
text: '24'
},
{
xtype: 'fieldset',
x: 857,
y: 206,
height: 14,
width: 14,
id: 'LED_440',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 770,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9839',
id: 'DECORATION_162'
},
{
xtype: 'button',
x: 120,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9872},
text: '',
id: 'COMMANDBUTTON_499',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 102,
y: 160,
height: 17,
width: 17,
style:{'z-index': 9871},
text: '',
id: 'COMMANDBUTTON_500',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 773,
y: 3,
style: 'padding:2px 0 2px 4px;z-index: 9840;font-size: 12pt;font-weight:bold',
id: 'TEXT_467',
text: '22'
},
{
xtype: 'button',
x: 190,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9965},
text: '',
id: 'COMMANDBUTTON_523',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 577,
y: 206,
height: 14,
width: 14,
id: 'LED_470',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 382,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9970},
text: '',
id: 'COMMANDBUTTON_527',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 312,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9969},
text: '',
id: 'COMMANDBUTTON_526',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 242,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9968},
text: '',
id: 'COMMANDBUTTON_525',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 172,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9964},
text: '',
id: 'COMMANDBUTTON_524',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 507,
y: 206,
height: 14,
width: 14,
id: 'LED_438',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 0,
y: 60,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9553',
id: 'DECORATION_106'
},
{
xtype: 'button',
x: 120,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9866},
text: '',
id: 'COMMANDBUTTON_497',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 102,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9865},
text: '',
id: 'COMMANDBUTTON_498',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'label',
x: 3,
y: 63,
style: 'padding:2px 0 2px 4px;z-index: 9554;font-size: 12pt;font-weight:bold',
id: 'TEXT_111',
text: '30'
},
{
xtype: 'fieldset',
x: 437,
y: 206,
height: 14,
width: 14,
id: 'LED_436',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 400,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9791},
text: '',
id: 'COMMANDBUTTON_479',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1100,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9860},
text: '',
id: 'COMMANDBUTTON_495',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1082,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9859},
text: '',
id: 'COMMANDBUTTON_496',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 330,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9802},
text: '',
id: 'COMMANDBUTTON_481',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 367,
y: 276,
height: 14,
width: 14,
id: 'LED_483',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 206,
height: 14,
width: 14,
id: 'LED_434',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 260,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9701},
text: '',
id: 'COMMANDBUTTON_450',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1030,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9854},
text: '',
id: 'COMMANDBUTTON_493',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1012,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9853},
text: '',
id: 'COMMANDBUTTON_494',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 944,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_443',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'button',
x: 750,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9668},
text: '',
id: 'COMMANDBUTTON_332',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 157,
y: 206,
height: 14,
width: 14,
id: 'LED_432',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 610,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9690},
text: '',
id: 'COMMANDBUTTON_346',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 890,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9848},
text: '',
id: 'COMMANDBUTTON_491',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 872,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9847},
text: '',
id: 'COMMANDBUTTON_492',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 874,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_441',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 594,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_471',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'button',
x: 260,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9778},
text: '',
id: 'COMMANDBUTTON_476',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 997,
y: 146,
height: 14,
width: 14,
id: 'LED_430',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 190,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9685},
text: '',
id: 'COMMANDBUTTON_344',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 820,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9842},
text: '',
id: 'COMMANDBUTTON_489',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 802,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9841},
text: '',
id: 'COMMANDBUTTON_490',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 524,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_439',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 206,
height: 14,
width: 14,
id: 'LED_385',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 927,
y: 146,
height: 14,
width: 14,
id: 'LED_428',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'button',
x: 680,
y: 40,
height: 17,
width: 17,
style:{'z-index': 9667},
text: '',
id: 'COMMANDBUTTON_331',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 50,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9556},
text: '',
id: 'COMMANDBUTTON_286',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 32,
y: 100,
height: 17,
width: 17,
style:{'z-index': 9555},
text: '',
id: 'COMMANDBUTTON_287',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 454,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_437',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 437,
y: 86,
height: 14,
width: 14,
id: 'LED_317',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 437,
y: 146,
height: 14,
width: 14,
id: 'LED_427',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 206,
height: 14,
width: 14,
id: 'LED_356',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 146,
height: 14,
width: 14,
id: 'LED_424',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 206,
height: 14,
width: 14,
id: 'LED_325',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 330,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_476',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 314,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_435',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 1067,
y: 146,
height: 14,
width: 14,
id: 'LED_334',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 276,
height: 14,
width: 14,
id: 'LED_352',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 17,
y: 206,
height: 14,
width: 14,
id: 'LED_327',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 146,
height: 14,
width: 14,
id: 'LED_422',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 206,
height: 14,
width: 14,
id: 'LED_394',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 174,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_433',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 86,
height: 14,
width: 14,
id: 'LED_467',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 162,
height: 14,
width: 14,
id: 'LED_455',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 146,
height: 14,
width: 14,
id: 'LED_456',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 162,
height: 14,
width: 14,
id: 'LED_473',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 927,
y: 162,
height: 14,
width: 14,
id: 'LED_472',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 162,
height: 14,
width: 14,
id: 'LED_353',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 146,
height: 14,
width: 14,
id: 'LED_328',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 86,
height: 14,
width: 14,
id: 'LED_420',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 86,
height: 14,
width: 14,
id: 'LED_329',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1014,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_431',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 26,
height: 14,
width: 14,
id: 'LED_350',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 146,
height: 14,
width: 14,
id: 'LED_391',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 86,
height: 14,
width: 14,
id: 'LED_397',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1067,
y: 26,
height: 14,
width: 14,
id: 'LED_418',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 927,
y: 86,
height: 14,
width: 14,
id: 'LED_330',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 944,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_429',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 146,
height: 14,
width: 14,
id: 'LED_378',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 26,
height: 14,
width: 14,
id: 'LED_348',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 86,
height: 14,
width: 14,
id: 'LED_382',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 26,
height: 14,
width: 14,
id: 'LED_416',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 297,
y: 86,
height: 14,
width: 14,
id: 'LED_388',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 454,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_426',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 367,
y: 206,
height: 14,
width: 14,
id: 'LED_409',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1067,
y: 86,
height: 14,
width: 14,
id: 'LED_406',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 577,
y: 146,
height: 14,
width: 14,
id: 'LED_445',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 86,
height: 14,
width: 14,
id: 'LED_326',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 384,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_425',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 857,
y: 26,
height: 14,
width: 14,
id: 'LED_414',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 26,
height: 14,
width: 14,
id: 'LED_346',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 507,
y: 146,
height: 14,
width: 14,
id: 'LED_365',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 227,
y: 86,
height: 14,
width: 14,
id: 'LED_374',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 104,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_452',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 104,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_423',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 787,
y: 26,
height: 14,
width: 14,
id: 'LED_412',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 174,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_468',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 647,
y: 206,
height: 14,
width: 14,
id: 'LED_324',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 804,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_395',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 26,
height: 14,
width: 14,
id: 'LED_344',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 86,
height: 14,
width: 14,
id: 'LED_459',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 647,
y: 86,
height: 14,
width: 14,
id: 'LED_318',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 104,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_421',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 17,
y: 86,
height: 14,
width: 14,
id: 'LED_367',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 577,
y: 26,
height: 14,
width: 14,
id: 'LED_338',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#b0b0b0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 146,
height: 14,
width: 14,
id: 'LED_336',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 314,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_392',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 72,
y: 26,
height: 14,
width: 14,
id: 'LED_363',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1084,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_419',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 261,
y: 276,
height: 14,
width: 14,
id: 'LED_487',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 437,
y: 26,
height: 14,
width: 14,
id: 'LED_342',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 156,
y: 276,
height: 14,
width: 14,
id: 'LED_485',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 26,
height: 14,
width: 14,
id: 'LED_320',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 51,
y: 276,
height: 14,
width: 14,
id: 'LED_321',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 384,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_383',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 314,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_389',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 1014,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_417',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 874,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_449',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 804,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_398',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 717,
y: 26,
height: 14,
width: 14,
id: 'LED_331',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 647,
y: 26,
height: 14,
width: 14,
id: 'LED_332',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 174,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_401',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 874,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_415',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 384,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_411',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 244,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_379',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 157,
y: 222,
height: 14,
width: 14,
id: 'LED_454',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0000ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 222,
height: 14,
width: 14,
id: 'LED_453',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0000ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 594,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_446',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 87,
y: 162,
height: 14,
width: 14,
id: 'LED_450',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0000ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 997,
y: 102,
height: 14,
width: 14,
id: 'LED_341',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0000ff"}],1:[{background: "#c0c0c0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'fieldset',
x: 1084,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_408',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 244,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_375',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 944,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_469',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 804,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_413',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 0,
y: 0,
height: 60,
width: 70,
style: 'margin: 0; padding:0;;background-color:#ffffcc;z-index: 9557',
id: 'DECORATION_107'
},
{
xtype: 'fieldset',
x: 1014,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_369',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 524,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_366',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 734,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_373',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 664,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_400',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 34,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_368',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 244,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_386',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 664,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_371',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 874,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_404',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 734,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_460',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 804,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_403',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 664,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_405',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 454,
y: 86,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_360',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 594,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_361',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 454,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_466',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 384,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_465',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 314,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_464',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 244,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_463',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 174,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_359',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 104,
y: 26,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_358',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'fieldset',
x: 734,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_357',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'button',
x: 5,
y: 3,
height: 23,
width: 60,
style:{'z-index': 9742},
text: 'схема',
id: 'COMMANDBUTTON_464',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 34,
y: 206,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_462',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'button',
x: 5,
y: 27,
height: 28,
width: 60,
style:{'z-index': 9558},
text: 'мнемо- схема',
id: 'COMMANDBUTTON_289',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 1084,
y: 146,
height: 14,
width: 14,
style:{'z-index': 9999},
id: 'LED_254',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#ffffcc"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'label',
x: 1051,
y: 258,
style: 'padding:2px 0 2px 4px;z-index: 9697;font-size: 10pt;font-weight:bold',
id: 'TEXT_451',
text: 'Справка'
}

			   ]				   

			}]	

          },
          {
              id: 'test2', 
              title: 'Общая откл', 
              html: 'Это вкладка 2.',
              disabled: true 
          },
          {
              id: 'test3', 
              title: 'СЭС', 
              bodyStyle: 'background-color:#ffffcc',
			  html: 'Это вкладка 2.'
          }
        ]

    }]
});


















