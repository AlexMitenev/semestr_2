#ifndef _sort_h
#define _sort_h
//save-load

void ldc(struct STACK **, int);
void ld(struct STACK **, unsigned int);
int st(struct STACK **, unsigned int);
int ldi(struct STACK **);
int sti(struct STACK **);

//ariphmetics

int addition(struct STACK **);
int substruction(struct STACK **);
int multiplication(struct STACK **);
int division(struct STACK **); 
int modul(struct STACK **);
int compare(struct STACK **);

//transitions

jmp (cmd*, char*);

//exit 

int hlt();

#endif