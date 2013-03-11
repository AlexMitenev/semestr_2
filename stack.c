#include <stdlib.h>
#include <stdio.h>
#include "stack.h"

int size = 0;

 void push_in_stack(struct STACK **top,int elem) 
{  
    struct STACK *new_elem = ( struct STACK *)malloc(sizeof(struct STACK)); 
    new_elem -> value = elem; 
    new_elem -> next = *top; 
    *top = new_elem;
	size++;
} 
 int pop_from_stack(struct STACK **top) 
{ 
    struct STACK *old_elem = *top; 
    int old_value = 0; 
    if(*top) 
    { 
       old_value = old_elem -> value; 
       *top = (*top) -> next; 
	   size--;
       free(old_elem);  
    } 
	return old_value;
}  
	
 int find_top(struct STACK **top) 
 { 
       return (*top) -> value;  
 } 

 int stack_size()
 {
     return size;
 }
