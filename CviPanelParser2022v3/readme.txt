
 Запуск : 
  exe uir ?param1? ?param2?
 Вывод :
   js

?param1? - вывод окна
?param2? - вывод в js шрифтов

--------------------------------------------------------------

1. добавлено преобразование z-index (cvi->css)

2. Добавлено преобразование в utf8

a) 
Находим таблицу соответствия кодов win1251 и юникода. Ненадежный источник http://ru.wikipedia.org/wiki/Windows-1251
Находим описание utf-8. http://ru.wikipedia.org/wiki/UTF-8

Важны следующие строки
0x00000000 — 0x0000007F | 0xxxxxxx
0x00000080 — 0x000007FF | 110xxxxx 10xxxxxx

Строим таблицу соответствия кодов cp1251 и utf8.
Символы 0x00-0x7f переходят без изменений в код utf8. Эту часть не включаем в эту таблицу.
Символы 0x80-0xff -> 110xxxxx 10xxxxxx.
 Для этой части строим таблицу соответствия. Биты для иксов выравниваются по правому краю, 
то есть сперва забивается второй байт а остаток в левый.

b)
int wsize = MultiByteToWideChar(srcCodepage, 0, pSrcStr, -1, NULL, 0);
LPWSTR wbuf = (LPWSTR)mallocz(wsize*sizeof(WCHAR));
MultiByteToWideChar(srcCodepage, 0, pSrcStr, -1, wbuf, wsize);
int size = WideCharToMultiByte(dstCodepage, 0, wbuf, -1, NULL, 0, NULL, NULL);
LPSTR buf = (LPSTR)mallocz(size);
WideCharToMultiByte(dstCodepage, 0, wbuf, -1, buf, size, NULL, NULL);
bfree(wbuf);
return buf;


c)
    char buffer[] = "mbstowcs converts ANSI-string to Unicode-string";
    // определим размер памяти, необходимый для хранения Unicode-строки
    int length = mbstowcs(NULL, buffer, 0);
    wchar_t *ptr = new wchar_t[length]; 
    //  конвертируем ANSI-строку в Unicode-строку
    mbstowcs(ptr, buffer, length);
    wcout << ptr;
    cout << "\nLength of Unicode-string: " << length << endl;
    cout << "Size of allocated memory: " << _msize(ptr) << " bytes" << endl;
    delete[] ptr;



