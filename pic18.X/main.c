#include "setting_hardaware/setting.h"
#include <stdlib.h>
#include "stdio.h"
#include "string.h"
// using namespace std;

char str[20];
#define _XTAL_FREQ 4000000

void write_adc();

void __interrupt(high_priority) ISR(void)
{
    if (INTCONbits.INT0IF) {
        ClearBuffer();
        UART_Write_Text("i\n");
        INTCONbits.INT0IF = 0;
    }
}

void main(void) 
{
    SYSTEM_Initialize() ;
    while(1) {
        ClearBuffer();
        write_adc();
        __delay_ms(50);
    }
    return;
}

void write_adc() {
    char *s = GetString();
    int n = ADC_Read(0);
    //float f = ((float)n / 1023) * 5;
    char str[12];
    sprintf(str, "%d\n", n);
    UART_Write_Text(str);
}