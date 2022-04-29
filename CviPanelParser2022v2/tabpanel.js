Ext.define('MyPanels.tabpanel', {
id: 'tabpanel',
title: 'tabpanel',
width: 916 ,
height:595 ,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#f0f0f0',
items: [
 {
  id: 'TAB1',
  xtype: 'tabpanel',
  x: 48,
  y: 47,
  height: 434,
  width: 440,
  activeTab: 0,
  items: [
   {
    id: 'id1001',
    title: 'Tab___0',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10020',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#c0c0c0',
 items: [
 {
  id: 'TAB3',
  xtype: 'tabpanel',
  x: 79,
  y: 87,
  height: 251,
  width: 284,
  activeTab: 0,
  items: [
   {
    id: 'id1003',
    title: 'Tab___000',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10040',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#66cc99',
 items: [
     ]
    } ]
  },
   {
    id: 'id1005',
    title: 'Tab___11111',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10061',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#ff66ff',
 items: [
     ]
    } ]
  }
    ]
  },
{
xtype: 'textfield',
x: 37,
y: 37,
height: 21,
width: 75,
style:'z-index: 1',
decimalPrecision: 2,
id: 'NUMERIC3',
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '11px','fontWeight': 'normal'}
},
{
xtype: 'label',
x: 37,
y: 16,
style: 'padding:1px 0 2px 4px;z-index: 1;font-family: Arial;font-size: 8pt;background-color:#f0f0f0',
id: 'NUMERIC3_LABEL',
text: 'Untitled0'
},
{
xtype: 'button',
x: 137,
y: 37,
height: 23,
width: 40,
style:{'z-index': 0},
text: '__OK',
id: 'COMMANDBUTTON23',
onClick: function(){ panelsHelper.openWindow(this.up('window').id,this.id);}
//onClick: function(){ panelsHelper.openWindow(this.up('panel').up('panel').up('panel').up('panel').id,this.id);}
}     ]
    } ]
  },
   {
    id: 'id1007',
    title: 'Tab___1',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10081',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#663333',
 items: [
{
xtype: 'textfield',
x: 45,
y: 47,
height: 21,
width: 75,
style:'z-index: 1',
decimalPrecision: 2,
id: 'NUMERIC1',
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '11px','fontWeight': 'normal'}
},
{
xtype: 'label',
x: 45,
y: 26,
style: 'padding:1px 0 2px 4px;z-index: 1;font-family: Arial;font-size: 8pt;background-color:#f0f0f0',
id: 'NUMERIC1_LABEL',
text: 'Untitled1'
},
{
xtype: 'fieldset',
x: 13,
y: 162,
height: 5,
width: 406,
style: 'margin: 0; padding:0;;z-index: 0;border: 5px solid #f0f0f0;background-color:#f0f0f0',
id: 'SPLITTER'
}     ]
    } ]
  },
   {
    id: 'id1009',
    title: 'Tab___2',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10102',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#f0f0f0',
 items: [
     ]
    } ]
  },
   {
    id: 'id1011',
    title: 'Tab___3',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10123',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#f0f0f0',
 items: [
{
xtype: 'textfield',
x: 37,
y: 62,
height: 21,
width: 75,
style:'z-index: 2',
decimalPrecision: 0,
id: 'STRING',
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '11px','fontWeight': 'normal'}
},
{
xtype: 'fieldset',
x: 137,
y: 112,
height: 24,
width: 34,
style:'z-index: 1',
id: 'LED',
vstyle:{ 0:[{background: "#143200"}],1:[{background: "#8cff00"}]},
cls: 'wasutp_led_sq_state_default'
},
{
xtype: 'label',
x: 117,
y: 89,
style: 'padding:1px 0 2px 4px;z-index: 1;font-family: Arial;font-size: 8pt;background-color:#f0f0f0',
id: 'LED_LABEL',
text: 'Untitled Control'
},
{
xtype: 'fieldset',
x: 262,
y: 200,
height: 20,
width: 20,
id: 'LED_2',
style:'z-index: 0',
vstyle:{ 0:[{background: "#0b0f0f"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 287,
y: 204,
style: 'padding:1px 0 2px 4px;z-index: 0;font-family: Arial;font-size: 8pt;background-color:#f0f0f0',
id: 'LED_2_LABEL',
text: 'Untitled Control'
}     ]
    } ]
  }
    ]
  },
 {
  id: 'TAB_2',
  xtype: 'tabpanel',
  x: 506,
  y: 28,
  height: 185,
  width: 332,
  activeTab: 0,
  items: [
   {
    id: 'id1013',
    title: 'Tab___11',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10140',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#f0f0f0',
 items: [
{
xtype: 'button',
x: 87,
y: 37,
height: 23,
width: 40,
style:{'z-index': 0},
text: '__OK',
id: 'COMMANDBUTTON_51',
onClick: function(){ panelsHelper.openWindow(this.up('window').id,this.id);}
//onClick: function(){ panelsHelper.openWindow(this.up('panel').up('panel').up('panel').up('panel').id,this.id);}
}     ]
    } ]
  },
   {
    id: 'id1015',
    title: 'Tab___22',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10161',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#f0f0f0',
 items: [
     ]
    } ]
  },
   {
    id: 'id1017',
    title: 'Tab___2',
    layout: 'fit',
    items: [ {
 xtype : 'panel',
 id: 'id10182',
 layout: { type: 'absolute'},
 bodyStyle: 'background-color:#f0f0f0',
 items: [
     ]
    } ]
  }
    ]
  },
{
xtype: 'textfield',
x: 529,
y: 275,
height: 21,
width: 75,
style:'z-index: 2',
decimalPrecision: 2,
id: 'NUMERIC',
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '11px','fontWeight': 'normal'}
},
{
xtype: 'label',
x: 529,
y: 254,
style: 'padding:1px 0 2px 4px;z-index: 2;font-family: Arial;font-size: 8pt;background-color:#f0f0f0',
id: 'NUMERIC_LABEL',
text: 'Untitled0000'
},
{
xtype: 'button',
x: 562,
y: 337,
height: 23,
width: 40,
style:{'z-index': 1},
text: '__OK',
id: 'COMMANDBUTTON45',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'fieldset',
x: 537,
y: 398,
height: 5,
width: 96,
style: 'margin: 0; padding:0;;z-index: 0;border: 5px solid #f0f0f0;background-color:#f0f0f0',
id: 'SPLITTER'
}
]
});
