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
        __delay_ms(5);
    }
}

void main(void) 
{
    SYSTEM_Initialize() ;
    while(1) {
        ClearBuffer();
        write_adc();
        __delay_ms(2);
    }
    return;
}

void write_adc() {
    char *s = GetString();
    int n = ADC_Read(0);
    int m = ADC_Read(1);
    //float f = ((float)n / 1023) * 5;
    char str[15];
    sprintf(str, "%d,%d\n", n, m);
    UART_Write_Text(str);
}