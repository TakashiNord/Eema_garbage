#pragma once

#ifndef __UTILSAPI_H__INCLUDED__
#define __UTILSAPI_H__INCLUDED__

#ifndef RSDUUTILS_API
#ifdef LINUX
#define RSDUUTILS_API
#endif
#ifdef WIN32
#ifdef  RSDUUTILS_API_EXPORTS
#define RSDUUTILS_API 
#else
#define RSDUUTILS_API 
#endif
#endif
#endif // ndef RSDUUTILS_API


#endif // __UTILSAPI_H__INCLUDED__
