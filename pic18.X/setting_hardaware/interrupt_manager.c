#include <xc.h>

void TIMER0_Interrupt_Initialize(void){
    INTCONbits.TMR0IE = 1;
    INTCONbits.TMR0IF = 0;
    T0CONbits.TMR0ON = 1;
    T0CONbits.T08BIT = 0;//16 bit timer
    T0CONbits.T0CS = 0;//as a timer
    T0CONbits.PSA = 0;
    TMR0H = 11;
    TMR0L = 219;
    T0CONbits.T0PS = 0b011;
}

void INTERRUPT_Initialize (void)
{
    RCONbits.IPEN = 1;      //enable Interrupt Priority mode
    INTCONbits.GIEH = 1;    //enable high priority interrupt
    INTCONbits.GIEL = 1;     //disable low priority interrupt
    INTCONbits.INT0IE = 1;   //external interrupt
    TIMER0_Interrupt_Initialize();
}

