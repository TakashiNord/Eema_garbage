/* Restore align */

// #if defined(__IC286__)
// #if defined(__NOALIGN__)
// #pragma noalign
// #else
// #pragma align
// #endif /* __NOALIGN__ */
// 
// #elif defined(__IC386__)
// #if defined(__NOALIGN__)
// #pragma noalign
// #else
// #pragma align
// #endif /* __NOALIGN__ */
// 
// #elif defined(__BORLANDC__)
// #pragma option -a.
// 
// #elif defined(__WATCOMC__)
// #pragma pack()
// 
// #elif defined(_MSC_VER)
// #pragma pack()
// 
// #else
// #pragma pack()
// 
// #endif    /* __IC286__, __IC386__, __BORLANDC__, __WATCOMC__, _MSC_VER */

