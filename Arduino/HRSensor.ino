#include "SerialCommand.h";

SerialCommand sCmd;
int DELAY_TIME = 50;

void setup() {
    Serial.begin(9600);
    while (!Serial);

    sCmd.addCommand("PING", pingHandler);
    sCmd.addCommand("ECHO", echoHandler);
    sCmd.setDefaultHandler(errorHandler);
}

void loop() {
    if (Serial.available > 0) {
        sCmd.readSerial();
    }

    delay(DELAY_TIME);
}

void pingHandler() {
    Serial.println("PONG");
    // should send HR info here
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


