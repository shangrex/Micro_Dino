#include <xc.h>
#include <proc/pic18f4520.h>
#include "seven.h"

void seven_Init(){
    ADCON1bits.PCFG = 0b1110;
    TRISD = 0;
    LATD = 0;
    TRISAbits.TRISA1 = 0;
    TRISAbits.TRISA2 = 0;
    TRISAbits.TRISA3 = 0;
    LATA = 0b00001110;
    TRISBbits.TRISB0 = 1;
    PORTBbits.RB0;
    
    RCONbits.IPEN = 1;      //enable Interrupt Priority mode
    INTCONbits.GIEH = 1;    //enable high priority interrupt
    INTCONbits.GIEL = 1;    //disable low priority interrupt
    
    INTCONbits.TMR0IE = 1;
    INTCONbits.TMR0IF = 0;
    T0CONbits.TMR0ON = 1;
    T0CONbits.T08BIT = 0;   //16 bit timer
    T0CONbits.T0CS = 0;     //as a timer
    T0CONbits.PSA = 0;
    TMR0H = 11;
    TMR0L = 219;
    T0CONbits.T0PS = 0b011;
    
    TRISCbits.TRISC3 = 0;
    LATCbits.LC3 = 0;
}

void change_num(int num){
    digit2 = num / 100;
    num %= 100;
    digit1 = num / 10;
    digit0 = num % 10;
    switch(digit0){
        case 0: digit0 = num0;  break;
        case 1: digit0 = num1;  break;
        case 2: digit0 = num2;  break;
        case 3: digit0 = num3;  break;
        case 4: digit0 = num4;  break;
        case 5: digit0 = num5;  break;
        case 6: digit0 = num6;  break;
        case 7: digit0 = num7;  break;
        case 8: digit0 = num8;  break;
        case 9: digit0 = num9;  break;
    }
    switch(digit1){
        case 0: digit1 = num0;  break;
        case 1: digit1 = num1;  break;
        case 2: digit1 = num2;  break;
        case 3: digit1 = num3;  break;
        case 4: digit1 = num4;  break;
        case 5: digit1 = num5;  break;
        case 6: digit1 = num6;  break;
        case 7: digit1 = num7;  break;
        case 8: digit1 = num8;  break;
        case 9: digit1 = num9;  break;
    }
    switch(digit2){
        case 0: digit2 = num0;  break;
        case 1: digit2 = num1;  break;
        case 2: digit2 = num2;  break;
        case 3: digit2 = num3;  break;
        case 4: digit2 = num4;  break;
        case 5: digit2 = num5;  break;
        case 6: digit2 = num6;  break;
        case 7: digit2 = num7;  break;
        case 8: digit2 = num8;  break;
        case 9: digit2 = num9;  break;
    }
}
