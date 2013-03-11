#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "stack.h"
#include "memory.h"
#include "commands.h"
#include "parser.h"
#include "string.h"

#define NO_ERROR 0
#define FEW_DATA 4
#define BAD_END 5
#define DIVISION_BY_ZERO 6
#define REPEAT_LABEL 9
#define CANT_FIND_LABEL 10

//save-load commands
typedef struct
{
    int error;
	int ip;
	int is_transit;
}lbl;

void ldc(struct STACK ** top_pointer, int element)
{
    push_in_stack(top_pointer, element);
}

void ld(struct STACK ** top_pointer, unsigned int address)
{
    int tempElem = get_memory_elem(address);
    push_in_stack(top_pointer, tempElem);
}

int st(struct STACK ** top_pointer, unsigned int address)
{
    int error = NO_ERROR;
    if (stack_size() < 1) 
    {
	    error = FEW_DATA;
    }
	else
	{
        int temp_elem = pop_from_stack(top_pointer);
	    set_memory_elem(temp_elem, address);
	}
	return error;
}

//ariphmetick

int addition(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int y = pop_from_stack(top_pointer);
		int x = pop_from_stack(top_pointer);
		push_in_stack(top_pointer, x + y);
	}
	return error;
}

int substruction(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int y = pop_from_stack(top_pointer);
		int x = pop_from_stack(top_pointer);
		push_in_stack(top_pointer, x - y);
	}
	return error;
}

int multiplication(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int y = pop_from_stack(top_pointer);
		int x = pop_from_stack(top_pointer);
		push_in_stack(top_pointer, x * y);
	}
	return error;
}

int division(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int y = pop_from_stack(top_pointer);
		int x = pop_from_stack(top_pointer);
		if (y != 0)
		{
			push_in_stack(top_pointer, x / y);
		}	
		else
		{
			error = DIVISION_BY_ZERO;
		}
	}	
	return error;	
}

int modul(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
        int y = pop_from_stack(top_pointer);
        int x = pop_from_stack(top_pointer);
        push_in_stack(top_pointer, x % y);
	}
	return error;
}

int compare(struct STACK ** top_pointer) 
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int y = pop_from_stack(top_pointer);
		int x = pop_from_stack(top_pointer);
	
		if (x > y) 
		{ 
			push_in_stack(top_pointer, 1);
		}
		else if (x == y)
		{ 
			push_in_stack(top_pointer, 0);
		}
		else
		{
			push_in_stack(top_pointer, -1);
		}  
	}	
	return error;
}

//transitions

lbl jmp (cmd* programm, char* label)
{
    lbl jmp;
    jmp.error = NO_ERROR;
	int i = 0;
	int ip = 0;
	int counter = 0;
	while (programm[i].opcode != HLT)
	{
	    if (!strcmp(programm[i].label_def, label))
		{
		    ip = i;
		    counter++;
		}
		i++;
	}
	switch (counter)
	{
	    case 0:
		    jmp.error = CANT_FIND_LABEL;
	        break;
	    case 1:	
	        jmp.ip = ip;
			break;
        default:
		    jmp.error = REPEAT_LABEL;
			break;
	}
	return jmp;
}

lbl br (cmd* programm, char* label, struct STACK ** top_pointer)
{
    lbl br;
    br.error = NO_ERROR;
	int i = 0;
	int ip = 0;
	br.is_transit = 1;
	
	int stack_top_element = pop_from_stack(top_pointer);	
	if (stack_top_element)
	{
		int counter = 0;
	    while (programm[i].opcode != HLT)
	    {
	        if (!strcmp(programm[i].label_def, label))
		   {
		        ip = i;
		        counter++;
		   }
		   i++;
	    }
	    switch (counter)
	    {
	        case 0:
	    	    br.error = CANT_FIND_LABEL;
	            break;
	        case 1:	
    	        br.ip = ip;
	    		break;
            default:
	    	    br.error = REPEAT_LABEL;
	    		break;
	    }
	
	}
	else
	{
	    br.is_transit = 0;
	}
	return br;
}
//exit

int hlt(struct STACK ** top_pointer)
{   
    int error = NO_ERROR;
	
	if (stack_size() < 1) 
    {
	    error = FEW_DATA;
    }
	else
	{
		int stack_top_element = pop_from_stack(top_pointer);
		if (stack_top_element != 0)
		{
			error = BAD_END;
		}
	}	
}