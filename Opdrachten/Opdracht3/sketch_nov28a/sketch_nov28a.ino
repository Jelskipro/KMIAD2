#include <Servo.h>

#define PIN_LED 11
#define PIN_LED2 6
#define PIN_BUTTON 2


#define STEP_FORWARD 1
#define STEP_BACK -1

Servo myServo;

int ledState = LOW;
long ledChangeTime  = 0L;

int servoState = STEP_FORWARD;
int servoPos = 0;
long servoStepTime = 0L;

volatile int buttonState = LOW; 

void setup() {
  Serial.begin(9600);
  myServo.attach(9);

  pinMode(PIN_LED, OUTPUT);
  pinMode(A0, INPUT);
  
  pinMode(PIN_LED2, OUTPUT);
  digitalWrite(PIN_BUTTON, HIGH);

  attachInterrupt(0, PIN_ISR, FALLING);
}
void loop() {
  int value = analogRead(A0);
  //Serial.print("value=");
  //Serial.println(value);

  long currentTimeMS = millis();
  if(currentTimeMS - ledChangeTime > 500){
    if(ledState == LOW){
      ledState = HIGH;
    } else{
      ledState = LOW;
    }
    digitalWrite(PIN_LED, ledState);
    ledChangeTime = currentTimeMS;
  }

  if (currentTimeMS - servoStepTime > 200){
    //myServo.write(servoPos);
    servoStepTime = currentTimeMS;
    
    if(servoState == STEP_FORWARD){
      servoPos +=10;
    } else{
      servoPos -=10;
    }
    if(servoPos > 180){
      servoPos = 180;
      servoState = STEP_BACK;
    } else if (servoPos < 0){
      servoPos = 0;
      servoState = STEP_FORWARD;
    }
  }
}
 void PIN_ISR() {
  if(buttonState == LOW) {
    buttonState = HIGH;
  } else {
    buttonState = LOW; 
  }
  digitalWrite(PIN_LED2, buttonState);
}
