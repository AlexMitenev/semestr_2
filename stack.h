#ifndef _sort_h
#define _sort_h

struct STACK 
{ 
    int value; 
    struct STACK *next;  
};

int find_top(struct STACK **);
int pop_from_stack(struct STACK **);
void push_in_stack(struct STACK **, int);  
int stack_size();

#endif