/*----------------------------------------------------------------------------\
|  FILE       : TUGAB.h         |  This header gives you a predefined IOs.    |
|  DEVELOPER  : Orlin Dimitrov  |  LED display decoder.                       |
|  STAND      : See History     |  Celsius and Kelvin temperature maths.      |
|  VERSION    : 1.00            |                                             |
|-----------------------------------------------------------------------------|
|                                                        | Copyright          |
|                                                        |   TU - Gabrovo,    |
|                        TUGAB.h                         |   Hadji Dimitar 4, |
|                                                        |   5300 Gabrovo,    |
|                                                        |   Bulgaria         |
|-----------------------------------------------------------------------------|
|  TUGAB is part of TU-Gabrovo development library for Arduino.               |
|                                                                             |
|  This library is free software: you can redistribute it and/or modify       |
|  it under the terms of the GNU General Public License as published by       |
|  the Free Software Foundation, either version 3 of the License, or          |
|  (at your option) any later version.                                        |
|                                                                             |
|  TUGAB is distributed in the hope that it will be useful,                   |
|  but WITHOUT ANY WARRANTY; without even the implied warranty of             |
|  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the              |
|  GNU General Public License for more details.                               |
|                                                                             |
|  You should have received a copy of the GNU General Public License          |
|  along with TUGAB.  If not, see <http://www.gnu.org/licenses/>.             |                                                               |
|-----------------------------------------------------------------------------|
|                              H I S T O R Y                                  |
|                                                                             |
| 141128 DiO  Creation of the file.                                           |
| 141213 DiO, BoN Editing naming of definitions.                              |
| 150128 DiO, Add a license to the file.                                      |
\----------------------------------------------------------------------------*/

// LEDs
#define LED_GREEN        2
#define LED_YELLOW       3
#define LED_RED          4

// Buttons
#define BUTTON_1         5
#define BUTTON_2         6
#define BUTTON_3         7

// Indicator digits.
#define DISPLAY_DIGIT1   8
#define DISPLAY_DIGIT2   9

// Pin connected to Strobe (pin 1) of 74HC4094
#define DISPLAY_STROBE   10
// Pin connected to Data (pin 2) of 74HC4094
#define DISPLAY_DATA     11
// Pin connected to Clock (pin 3) of 74HC4094
#define DISPLAY_CLOCK    13

// Buzzer pin.
#define AUDIO_OUT_BUZZER 12

// Microphone pin.
#define AUDIO_IN_MIC     A2

// Potentiometers.
#define SENSOR_POT1      A0
#define SENSOR_POT2      A1

// Light sensor pin.
#define SENSOR_LIGHT     A6

// Temperature sensor pin.
#define SENSOR_TEMP      A7

// 
#define KELVIN 273

// 
#define Celsius(value) ((((value / 1023.0) * 5.0) * 100.0) - KELVIN)

// 
#define Kelvin(value) (((value / 1023.0) * 5.0) * 100.0)

//
const unsigned char DecSegData[10] =
{
  0b00111111, // 0
  0b00000110, // 1
  0b01011011, // 2
  0b01001111, // 3
  0b01100110, // 4
  0b01101101, // 5
  0b01111101, // 6
  0b00000111, // 7
  0b01111111, // 8
  0b01101111  // 9
};

const unsigned char HexSegData[16] =
{
  0b00111111, // 0
  0b00000110, // 1
  0b01011011, // 2
  0b01001111, // 3
  0b01100110, // 4
  0b01101101, // 5
  0b01111101, // 6
  0b00000111, // 7
  0b01111111, // 8
  0b01101111, // 9
  0b01110111, // A
  0b01111100, // B
  0b01011000, // C
  0b01011110, // D
  0b01111001, // E
  0b01110001  // F
};
