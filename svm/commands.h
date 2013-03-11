#ifndef _sort_h
#define _sort_h
//save-load

void ldc(struct STACK **, int);
void ld(struct STACK **, int);
int st(struct STACK **, int);
int ldi(struct STACK **)
//ariphmetics

int addition(struct STACK **);
int substruction(struct STACK **);
int multiplication(struct STACK **);
int division(struct STACK **); 
int modul(struct STACK **);
int compare(struct STACK **);

//transitions

br (cmd*, char*, struct STACK **);
jmp (cmd*, char*);
//exit 
int hlt();
#endif