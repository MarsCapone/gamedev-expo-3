#include <Arduino.h>
#include <SerialCommand.h>;

SerialCommand sCmd;
int DELAY_TIME = 10;

int PURPLE_PIN = 0;
int THRESHHOLD = 550;
int hrSignal; // value: 0 - 1024

int lastTime;
float rate;

void setup() {
    Serial.begin(9600);
    while (!Serial);

    sCmd.addCommand("PING_RATE", pingRateHandler);
    sCmd.addCommand("PING_SIGNAL", pingSignalHandler);
    sCmd.addCommand("PING", pingHandler);
    sCmd.addCommand("ECHO", echoHandler);
    sCmd.addDefaultHandler(errorHandler);

    lastTime = 0;
}

void loop() {
    if (Serial.available() > 0) {
        sCmd.readSerial();
    }

    hrSignal = analogRead(PURPLE_PIN);
    if (hrSignal > THRESHHOLD) {
        if (lastTime > 0) {
            rate = getRate(lastTime);
        }
        lastTime = 0;
    }

    lastTime += DELAY_TIME;
    delay(DELAY_TIME);
}

void pingHandler() {
    Serial.println("PONG");
}

void pingRateHandler() {
    Serial.println(rate);
}

void pingSignalHandler() {
    Serial.println(hrSignal);
}

void echoHandler() {
    char *arg;
    arg = sCmd.next();
    if (arg != NULL) {
        Serial.println(arg);
    } else {
        Serial.println("No echo message.");
    }
}

void errorHandler(const char *command) {
    // error handling
}

float getRate(int msBetweenRR) {
    rate = 60 / (msBetweenRR / 1000);
}
