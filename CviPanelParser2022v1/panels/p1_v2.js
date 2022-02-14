Ext.define('MyPanels.p1', {
id: 'p1',
title: 'Panel система',
width: 595,
height: 383,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#f0f0f0',
items: [
{
xtype: 'textfield',
x: 412,
y: 62,
width: 75,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '11px'
}
},
{
xtype: 'label',
x: 412,
y: 41,
style: 'padding:2px 0 2px 4px;z-index: 9998;background-color:#f0f0f0',
id: 'NUMERIC_LABEL',
text: 'Untitled Control'
},
{
xtype: 'fieldset',
x: 437,
y: 225,
height: 20,
width: 20,
id: 'LED',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#0b0f0f"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 410,
y: 204,
style: 'padding:2px 0 2px 4px;z-index: 9999;background-color:#f0f0f0',
id: 'LED_LABEL',
text: 'Untitled Control'
}
]
});
