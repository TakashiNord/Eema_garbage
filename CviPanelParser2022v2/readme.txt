
 Запуск : 
  exe uir ?param1? ?param2?
 Вывод :
   js

?param1? - вывод окна
?param2? - вывод в js шрифтов

--------------------------------------------------------------

 1. Создаем общий файл с вкладками
 2. поиск повторов среди ATTR_CONSTANT_NAME
 3. 
 
 -- не понимает utf8
 -- не полная структура
==============================================================
1. Panel.js 

function jP(o,src) {
 if (o.length<=0) { return o; }
 for (let j = 0; j < o.length; j++) {
    let ob = o[j] ;
    if (ob.length<=0) { continue ; }
    for (let key in ob ) {
      if (ob.hasOwnProperty(key) && (typeof ob[key] === "object")&& (key.toLowerCase()=="items")){
        ob[key]=jP(ob[key],src) ;
      } else if (ob.hasOwnProperty(key) && (key.toLowerCase()=="id")) {
                if(ob[key].indexOf(src.uirName)){
                    ob[key]=src.uirName + '_' + ob[key];
                }
        }
    }
 }
 return o;
}

panel.items = jP(p.items,src)
 
2. For Button

  if (level==0)
    fprintf(outfile ,"onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}\n");
  else {
    fprintf(outfile ,"onClick: function(){ panelsHelper.openWindow(this.up('window').id,this.id);}\n");

    for(jj=0,sdp[0]='\0';jj<=level; jj++) {
      strcat(sdp,"up('panel')."); strcat(sdp,"\0");
    }
    fprintf(outfile ,"//onClick: function(){ panelsHelper.openWindow(this.%sid,this.id);}\n",sdp);

  }

==============================================================
 

