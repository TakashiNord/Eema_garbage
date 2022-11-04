#ifndef KM5PROTO_H_
#define KM5PROTO_H_

#include <stdint.h>

#define KM5_WAIT_BYTE				 ((uint16_t)10)
#define KM5_QUERY_LENGTH			 ((uint16_t)16)
#define KM5_ANSWER_SHORT_LENGTH		 ((uint16_t)32)
#define KM5_ANSWER_LONG_LENGTH		 ((uint16_t)72)
#define KM5_ANSWER_COMMAND_48_LENGTH ((uint16_t)8)

#define KM5_ADDRES_LENGTH				  ((uint16_t)4)
#define KM5_QUERY_DATA_LENGTH			  ((uint16_t)9)
#define KM5_CRC_LENGTH					  ((uint16_t)2)
#define KM5_ANSWER_SHORT_DATA_LENGTH	  ((uint16_t)25)
#define KM5_ANSWER_LONG_DATA_LENGTH		  ((uint16_t)65)
#define KM5_ANSWER_COMMAND_48_DATA_LENGTH ((uint16_t)1)

#define KM5_COMMAND_TEST			 ((uint8_t)2)
#define KM5_COMMAND_READ_DEVICESTATE ((uint8_t)8)
#define KM5_COMMAND_READ_PARAMETERS  ((uint8_t)123)

typedef struct KM5_msg_s {
	uint8_t address[KM5_ADDRES_LENGTH];
	uint8_t command;
	uint8_t data[KM5_ANSWER_LONG_DATA_LENGTH];
	uint8_t crc[KM5_CRC_LENGTH];
} KM5_msg_t;

#endif /* KM5PROTO_H_ */
