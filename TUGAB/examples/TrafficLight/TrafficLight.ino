#include <TUGAB.h>
#include <avr/io.h>
#include <avr/interrupt.h>

//
#define DEBONCE_AUTO_CHANGE 100

// Initial light value.
int LightValue = LED_RED;

// Initial delay value.
int DelayValue = 0;

// Current display value.
int DisplayValue = 10;

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
  
  //
  SetIndicator(DisplayValue);
}

//////////////////////////////////////////////////////////////////////////
// Loop sub routine iterate while have electrisity.
//////////////////////////////////////////////////////////////////////////
void loop()
{
  if (timerEvent)
  {
    timerEvent = 0;
    // Set the trafic light.
    SetLight(LightValue);

    if(DelayValue > DEBONCE_AUTO_CHANGE)
    {
      if(DisplayValue == 5 && LightValue == 0)
      {
        LightValue = 1;
      }
      if(DisplayValue <= 0 && LightValue == 1)
      {
        DisplayValue = 10;
        LightValue = 2;
      }
      if(DisplayValue == 5 && LightValue == 2)
      {
        LightValue = 3;
      }
      if(DisplayValue == 0 && LightValue == 3)
      {
        DisplayValue = 10;
        LightValue = 0;
      }
      
      // Set indicator display.
      SetIndicator(DisplayValue);
      // Decrement the time value.
      DisplayValue--;
      
      if(DisplayValue < 0)
      {
        DisplayValue = 10;
      }
      
      // Reset the debonce time.
      DelayValue = 0;
    }
    // Increment debonce value.
    DelayValue++;
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
  OCR0A = 100;
  // Set the enable interupt.
  TIMSK0 = (1 << OCIE0A);
  // enable global interrupts:
  sei(); 
}

void SetLight(int light)
{
  switch(light)
  {
    case 0:
      digitalWrite(LED_GREEN, HIGH); 
      digitalWrite(LED_YELLOW, LOW); 
      digitalWrite(LED_RED,    LOW); 
      break;
    
    case 1:
      digitalWrite(LED_GREEN,   LOW); 
      digitalWrite(LED_YELLOW, HIGH); 
      digitalWrite(LED_RED,     LOW); 
      break;
    
    case 2:
      digitalWrite(LED_GREEN,  LOW); 
      digitalWrite(LED_YELLOW, LOW); 
      digitalWrite(LED_RED,   HIGH); 
      break;

    case 3:
      digitalWrite(LED_GREEN,   LOW); 
      digitalWrite(LED_YELLOW, HIGH); 
      digitalWrite(LED_RED,    HIGH); 
      break;
  }
}

//////////////////////////////////////////////////////////////////////////
// Control the LED display.
//////////////////////////////////////////////////////////////////////////
void SetIndicator(int value)
{
    // Validate the input display value.
  if(value > 99)
  {
    value = 0;
  }
  if(value < 0)
  {
    value = 99;    
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
  static int index = 0;

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
// Timer 0 interupt sub routine.
//////////////////////////////////////////////////////////////////////////
ISR(TIMER0_COMPA_vect)
{
  // Set the display.
  UpdateIndicator();
  // Tell everyone the the time was finish its job.
  timerEvent = 1;
}

void MakeBeep(int time)
{
    digitalWrite(AUDIO_OUT_BUZZER, HIGH);
    delay(time);
    digitalWrite(AUDIO_OUT_BUZZER, LOW);
}
