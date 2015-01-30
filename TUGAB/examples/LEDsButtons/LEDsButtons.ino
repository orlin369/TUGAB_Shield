#include <TUGAB.h>

int Btn1 = 0, Btn2 = 0, Btn3 = 0;

void setup()
{
  // LEDs
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_YELLOW, OUTPUT);
  pinMode(LED_RED, OUTPUT);
  
  // Buttons
  pinMode(BUTTON_1, INPUT);
  pinMode(BUTTON_2, INPUT);
  pinMode(BUTTON_3, INPUT);
}

void loop()
{
  // Read the buttons.
  Btn1 = digitalRead(BUTTON_1);
  Btn2 = digitalRead(BUTTON_2);
  Btn3 = digitalRead(BUTTON_3);
  
  if (Btn1 == LOW)
  {
    // turn LED on:    
    digitalWrite(LED_GREEN, HIGH);  
  } 
  else
  {
    // turn LED off:
    digitalWrite(LED_GREEN, LOW); 
  }
  
  if (Btn2 == LOW)
  {
    // turn LED on:    
    digitalWrite(LED_YELLOW, HIGH);  
  } 
  else
  {
    // turn LED off:
    digitalWrite(LED_YELLOW, LOW); 
  }

  if (Btn3 == LOW)
  {
    // turn LED on:    
    digitalWrite(LED_RED, HIGH);  
  } 
  else
  {
    // turn LED off:
    digitalWrite(LED_RED, LOW); 
  }
}
