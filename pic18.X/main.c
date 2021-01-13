#include "setting_hardaware/setting.h"
#include <stdlib.h>
#include "stdio.h"
#include "string.h"
// using namespace std;

char str[20];
#define _XTAL_FREQ 4000000

void write_adc();
void write_acc();

void __interrupt(high_priority) ISR(void)
{
    if (INTCONbits.INT0IF) {
        ClearBuffer();
        UART_Write_Text("i\n");
        INTCONbits.INT0IF = 0;
        __delay_ms(5);
    }
    if (INTCONbits.TMR0IF) {
        ClearBuffer();
        UART_Write_Text("t\n");
        INTCONbits.TMR0IF = 0;
        TMR0H = 11;
        TMR0L = 219;
        __delay_ms(5);
    }
}

#define menu 0
#define fox_game 1
#define fox_game_state 'f'
#define menu_state 'm'
void main(void) 
{
    LATBbits.LB1 = 0;  //reset clock
    SYSTEM_Initialize() ;
    int state = 0;
    while(1) {
        char *s = GetString();
        if (RCREG == 'f') {
            LATBbits.LB1 = 1;
            state = 1;
        } else if (RCREG == 'm') {
            LATBbits.LB1 = 0;
            state = 0;
        }
        
        if (state == 0) {
            write_adc();
        } else if (state == 1) {
            write_acc();
        }
        ClearBuffer();
        __delay_ms(2);
    }
    return;
}

void write_acc() {
    int n = ADC_Read(2);
    char ss[8];
    sprintf(ss, "%d\n", n);
    UART_Write_Text(ss);
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