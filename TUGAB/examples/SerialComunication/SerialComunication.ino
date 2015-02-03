#include <String.h>
#include <stdlib.h>
#include <TUGAB.h>
#include <avr/io.h>
#include <avr/interrupt.h>

// Digits of the dislplay.
volatile byte digits[2];

// Timer event lock bit.
volatile byte timerEvent = 0;

//
String IncommingCommnad = "";

//
boolean Echo = false;

void setup()
{
  // Diplay
  pinMode(DISPLAY_STROBE, OUTPUT);
  pinMode(DISPLAY_CLOCK, OUTPUT);
  pinMode(DISPLAY_DATA, OUTPUT);
  pinMode(DISPLAY_DIGIT1, OUTPUT);
  pinMode(DISPLAY_DIGIT2, OUTPUT);
  
  // LEDs
  pinMode(LED_GREEN,  OUTPUT);
  pinMode(LED_YELLOW, OUTPUT);
  pinMode(LED_RED,    OUTPUT);
  
  // Buzzer
  pinMode(AUDIO_OUT_BUZZER, OUTPUT);
  
  // Buttons
  pinMode(BUTTON_1, INPUT);
  pinMode(BUTTON_2, INPUT);
  pinMode(BUTTON_3, INPUT);
  
  // Setup Serial library at 9600 bps.
  Serial.begin(19200);
  
  // 
  SetupTimer0();
  
  //
  SetIndicator(0);
  
  //
  ShowVersion();
}

void loop()
{
  ReadCommand();
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

//////////////////////////////////////////////////////////////////////////  
// Read incomming data from the serial buffer.
//////////////////////////////////////////////////////////////////////////  
void ReadCommand()
{
  // Fill the command data buffer with command.
  while(Serial.available())
  {
    // Add new char.
    IncommingCommnad += (char)Serial.read();
    // Wait a while for a a new char.
    delay(5);
  }
  
  // If command if not empty parse it.
  if(IncommingCommnad != "")
  {
    boolean isValid = ValidateCommand(IncommingCommnad);
    if(isValid)
    {
      ParseCommand(IncommingCommnad);
    }
    // Print command for feedback.
    if(Echo == true)
    {
      Serial.print("Cmd: ");
      Serial.println(IncommingCommnad);
    }
  }
  
  // Clear the command data buffer.
  IncommingCommnad = ""; 
}

//////////////////////////////////////////////////////////////////////////  
// Validate the incomming data from the serial buffer.
//////////////////////////////////////////////////////////////////////////  
boolean ValidateCommand(String command)
{  
  boolean state = false;
  
  if(command[0] == '?' && command[10] == '\n')
  {
    if(strstr(command.c_str(), "DISPLAY")) //Comparing word entered with word stored in program
    {
      // If is valid.
      state = true;
    }
  }
  
  // LED command
  if(command[0] == '?' && command[6] == '\n')
  {
    if(strstr(command.c_str(), "LED")) //Comparing word entered with word stored in program
    {
      if(command[4] == '1' || command[4] == '2' || command[4] == '3')
      {
        if(command[5] == '0' || command[5] == '1')
        {
          // If is valid.
          state = true;
        }
      }
    } 
  }
  
  // Turn the buzzer OFF.
  if(command == "?BUZZER0\n")
  {
    // If is valid.
    state = true;
  }
  
  // Turn the buzzer ON.
  if(command == "?BUZZER1\n")
  {
    // If is valid.
    state = true;
  }
  
  // Microphone sensor command
  if(command == "?MIC\n")
  {
    // If is valid.
    state = true;
  }

  // Temperature sensor command
  if(command == "?TEMP\n")
  {
    // If is valid.
    state = true;
  }

  // Light sensor command
  if(command == "?LIGHT\n")
  {
    // If is valid.
    state = true;
  }

  // Potintiometer 0 command
  if(command == "?POT1\n")
  {
    // If is valid.
    state = true;
  }

  // Potintiometer 1 command
  if(command == "?POT2\n")
  {
    // If is valid.
    state = true;
  }
    
  // Button 0 command
  if(command == "?BUTTON1\n")
  {
    // If is valid.
    state = true;
  }
    
  // Button 1 command
  if(command == "?BUTTON2\n")
  {
    // If is valid.
    state = true;
  }

  // Button 2 command
  if(command == "?BUTTON3\n")
  {
    // If is valid.
    state = true;
  }
  
  // Show version of the device.
  if(command == "?VERSION\n")
  {
    // If is valid.
    state = true;
  }

  //Serial.println(state);
  // If is not valid.
  return state;
}

//////////////////////////////////////////////////////////////////////////  
// Parse the incomming data from the serial buffer.
//////////////////////////////////////////////////////////////////////////  
void ParseCommand(String command)
{
  if(command[0] == '?' && command[10] == '\n')
  {
    if(strstr(command.c_str(), "DISPLAY")) //Comparing word entered with word stored in program
    {
      int displayValue = command.substring(8, 10).toInt();
      SetIndicator(displayValue);
    }
  }

  // LED command
  if(strstr(command.c_str(), "LED")) //Comparing word entered with word stored in program
  {
    if(command[4] == '1')
    {
      if(command[5] == '0')
      {
        digitalWrite(LED_GREEN, LOW);
      }
      if(command[5] == '1')
      {
        digitalWrite(LED_GREEN, HIGH);
      }
    }
    if(command[4] == '2')
    {
      if(command[5] == '0')
      {
        digitalWrite(LED_YELLOW, LOW);
      }
      if(command[5] == '1')
      {
        digitalWrite(LED_YELLOW, HIGH);
      }
    }
    if(command[4] == '3')
    {
      if(command[5] == '0')
      {
        digitalWrite(LED_RED, LOW);
      }
      if(command[5] == '1')
      {
        digitalWrite(LED_RED, HIGH);
      }
    } 
  }
  
  // Turn the buzzer OFF.
  if(command == "?BUZZER0\n")
  {
    Serial.println("#BUZ:0");
  }
  
  // Turn the buzzer ON.
  if(command == "?BUZZER1\n")
  {
    Serial.println("#BUZ:1");
  }
  
  // Microphone sensor command
  if(command == "?MIC\n")
  {
    int value = analogRead(AUDIO_IN_MIC);
    Serial.print("#MIC:");
    Serial.println(value, DEC);
  }

  // Temperature sensor command
  if(command == "?TEMP\n")
  {
    int value = analogRead(SENSOR_TEMP);
    Serial.print("#TEMP:");
    Serial.println(value, DEC);
  }

  // Light sensor command
  if(command == "?LIGHT\n")
  {
    int value = analogRead(SENSOR_LIGHT);
    Serial.print("#LIGHT:");
    Serial.println(value, DEC);
  }

  // Potintiometer 0 command
  if(command == "?POT1\n")
  {
    int value = analogRead(SENSOR_POT1);
    Serial.print("#POT1:");
    Serial.println(value, DEC);
  }

  // Potintiometer 1 command
  if(command == "?POT2\n")
  {
    int value = analogRead(SENSOR_POT2);
    Serial.print("#POT2:");
    Serial.println(value, DEC);
  }
    
  // Button 0 command
  if(command == "?BUTTON1\n")
  {
    int value = digitalRead(BUTTON_1);
    Serial.print("#BTN1:");
    Serial.println(value, DEC);
  }
    
  // Button 1 command
  if(command == "?BUTTON2\n")
  {
    int value = digitalRead(BUTTON_2);
    Serial.print("#BTN2:");
    Serial.println(value, DEC);
  }

  // Button 2 command
  if(command == "?BUTTON3\n")
  {
    int value = digitalRead(BUTTON_3);
    Serial.print("#BTN3:");
    Serial.println(value, DEC);
  }
  
  if(command == "?VERSION\n")
  {
    // Show version of the device.
    ShowVersion();
  }
}
//////////////////////////////////////////////////////////////////////////
// Print the version of the device.
//////////////////////////////////////////////////////////////////////////
void ShowVersion()
{
  Serial.println("UNI : TU-Gabrovo");
  Serial.println("DEP : AIUT");
  Serial.println("NAME: Arduino AIUTshield v0.1");
  Serial.println("DATE: 02.12.2014y."); 
}