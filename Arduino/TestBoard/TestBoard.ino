#include <Arduino.h>

int ON_TIME = 50;
int OFF_TIME = 2000;

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
}

void loop() {
  digitalWrite(LED_BUILTIN, HIGH);
  delay(ON_TIME);
  digitalWrite(LED_BUILTIN, LOW);
  delay(OFF_TIME);
}
