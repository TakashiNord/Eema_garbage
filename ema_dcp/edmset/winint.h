/*
* WINint.h
* Содержит объявление типов, используемых в серверной части ПО, которых нет в Windows.
* 02.08.2011 Ефимов М.Г.
*/
#pragma once

#ifndef _WININT_H
#define _WININT_H

#ifdef _MSC_VER  /*all MS compilers define this (version)*/
    #define snprintf _snprintf
#endif


#ifdef WIN32
typedef signed char		int8_t;
typedef short int		int16_t;
typedef int				int32_t;
typedef long long int	int64_t;

typedef unsigned char			uint8_t;
typedef unsigned short int		uint16_t;
typedef unsigned int			uint32_t;
/* typedef unsigned short		uint16_t; */
typedef unsigned long long int	uint64_t;
#endif // WIN32

#endif

