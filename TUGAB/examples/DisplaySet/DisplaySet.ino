/*-------------------------------------------------------------------------------\
|  FILE       : DisplaySet         |  This example is created for demonstrading, |
|  DEVELOPER  : Orlin Dimitrov DiO | debonce for the buttons, display driver,    |
|  STAND      : See History        | timer 0 setup and usage.                    |
|  VERSION    : 1.00               |                                             |
|--------------------------------------------------------------------------------|
|                                                        |                       |
|                     DisplaySet.ino                     | Copyright: GPL        |
|                                                        |                       |
|--------------------------------------------------------------------------------|
|                               H I S T O R Y                                    |
|                                                                                |
| 141201 DiO  Creation of the file                                               |
| 141203 DiO  Write the button readin.                                           |
| 141203 DiO  Write the display driver                                           |
| 141204 DiO  Write TUGAB.h                                                      |
| 141205 DiO  Optimization display driver.                                       |
| 141205 DiO, IsK Optimization display driver, reading buttons, auto control.    |
| 141213 DiO, BoN Naming optimization.                                           |
\-------------------------------------------------------------------------------*/

#include <TUGAB.h>
#include <avr/io.h>
#include <avr/interrupt.h>

#define DEBONCE_AUTO_CHANGE 100
#define CHANGE_CYCLES 90

// Current display value.
int DisplayValue = 0;

// Increment button states.
int Btn0 = 0;
int TmpBtn0 = 0;
int Btn0Rpt = 0;

// Reset button states.
int Btn1 = 0;
int TmpBtn1 = 0;

// Decrement button states.
int Btn2 = 0;
int TmpBtn2 = 0;
int Btn2Rpt = 0;

// Digits of the dislplay.
volatile byte digits[2];

// Timer event lock bit.
volatile byte timerEvent = 0;

//////////////////////////////////////////////////////////////////////////
// Setup, it runs in every restart and MCU startup.
//////////////////////////////////////////////////////////////////////////
void setup()
{
  // Diplay
  pinMode(DISPLAY_STROBE, OUTPUT);
  pinMode(DISPLAY_CLOCK, OUTPUT);
  pinMode(DISPLAY_DATA, OUTPUT);
  pinMode(DISPLAY_DIGIT1, OUTPUT);
  pinMode(DISPLAY_DIGIT2, OUTPUT);
  
  // LEDs
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_YELLOW, OUTPUT);
  pinMode(LED_RED, OUTPUT);
  
  // Buzzer
  pinMode(AUDIO_OUT_BUZZER, OUTPUT);
  
  // Buttons
  pinMode(BUTTON_1, INPUT);
  pinMode(BUTTON_2, INPUT);
  pinMode(BUTTON_3, INPUT);
  
  // Setup Serial library at 9600 bps.
  Serial.begin(9600);
  
  // 
  SetupTimer0();
}

//////////////////////////////////////////////////////////////////////////
// Loop sub routine iterate while have electrisity.
//////////////////////////////////////////////////////////////////////////
void loop()
{
  if (timerEvent)
  {
    timerEvent = 0;
    // Read the buttons.
    Btn0 = digitalRead(BUTTON_1);
    Btn1 = digitalRead(BUTTON_2);
    Btn2 = digitalRead(BUTTON_3);
    
    // Chech which button is pressed.
    if (Btn0 == LOW)
    {
      if (Btn0 != TmpBtn0)
      {
        DisplayValue++;
      }
      else if (Btn0Rpt < DEBONCE_AUTO_CHANGE)
      {
        Btn0Rpt++;
      } 
      else 
      {
        DisplayValue++;
        Btn0Rpt = CHANGE_CYCLES;
      }
    } 
    else
    {
      Btn0Rpt = 0;
    }

    if(Btn1 != TmpBtn1 && Btn1 == HIGH)
    {
      DisplayValue = 0;
    }

    if (Btn2 == LOW)
    {
      if (Btn2 != TmpBtn2)
      {
        DisplayValue--;
      }
      else if (Btn2Rpt < DEBONCE_AUTO_CHANGE)
      {
        Btn2Rpt++;
      } 
      else 
      {
        DisplayValue--;
        Btn2Rpt = CHANGE_CYCLES;
      }
    } 
    else
    {
      Btn2Rpt = 0;
    }
    
    // Write the temporal values of the buttons.
    TmpBtn0 = Btn0;
    TmpBtn1 = Btn1;
    TmpBtn2 = Btn2;
    
    SetIndicator(DisplayValue);
  }
  
}

//////////////////////////////////////////////////////////////////////////
// Initialize Timer 0.
//////////////////////////////////////////////////////////////////////////
void SetupTimer0()
{
  //asm("nop");
  // disable global interrupts
  cli();
  // 
  TCCR0A = (1 << WGM01);
  // Set CS00 and CS02 bits for 1024 prescaler:
  TCCR0B = (1 << CS02) | (1 << CS00);
  // Clear Timer 0.
  TCNT0 = 0;
  // Set point of the timer.
  OCR0A = 130;
  // Set the enable interupt.
  TIMSK0 = (1 << OCIE0A);
  // enable global interrupts:
  sei(); 
}

//////////////////////////////////////////////////////////////////////////
// Control the LED display.
//////////////////////////////////////////////////////////////////////////
void SetIndicator(int value)
{
    // Validate the input display value.
  if(DisplayValue > 99)
  {
    DisplayValue = 0;
  }
  if(DisplayValue < 0)
  {
    DisplayValue = 99;    
  }
  
  digits[0] = HexSegData[(value % 10)];
  digits[1] = HexSegData[(value / 10 % 10)];
}

//////////////////////////////////////////////////////////////////////////
// Control the LED display.
//////////////////////////////////////////////////////////////////////////
void UpdateIndicator()
{
  // 
  static byte index = 0;

  if(index == 0)
  {
    digitalWrite(DISPLAY_DIGIT2, LOW);
    // Segment the data widowt the arguments in the function.
    //set strobe pin low to begin storing bits
    digitalWrite(DISPLAY_STROBE, LOW);
    shiftOut(DISPLAY_DATA, DISPLAY_CLOCK, MSBFIRST, digits[index]);  
    //set strobe pin high to stop storing bits    
    digitalWrite(DISPLAY_STROBE, HIGH);
    // Set the digit.
    digitalWrite(DISPLAY_DIGIT1, HIGH);
  }
  
  if(index == 1)
  {
    digitalWrite(DISPLAY_DIGIT1, LOW);
    //set strobe pin low to begin storing bits
    digitalWrite(DISPLAY_STROBE, LOW);
    shiftOut(DISPLAY_DATA, DISPLAY_CLOCK, MSBFIRST, digits[index]);  
    //set strobe pin high to stop storing bits    
    digitalWrite(DISPLAY_STROBE, HIGH);
    // Set the digit.
    digitalWrite(DISPLAY_DIGIT2, HIGH);
  }
  
  // Incerement the index.
  index++;
  
  // Validate.
  if(index >= 2)
  {
    index = 0; 
  }
}

//////////////////////////////////////////////////////////////////////////
// Timer 0 interrupt service routine.
//////////////////////////////////////////////////////////////////////////
ISR(TIMER0_COMPA_vect)
{
  // Set the display.
  UpdateIndicator();
  // Tell everyone the the time was finish its job.
  timerEvent = 1;
}
