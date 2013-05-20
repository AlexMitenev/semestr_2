#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>
#include "parser.h"
#include "stack.h"
#include "memory.h"
#include "commands.h"

#define NO_ERROR 0
#define FEW_DATA 4
#define BAD_END 5
#define DIVISION_BY_ZERO 6
#define REPEAT_LABEL 9
#define CANT_FIND_LABEL 10
//save-load commands

void ldc(struct STACK ** top_pointer, int element)
{
    push_in_stack(top_pointer, element);
}

int ldi(struct STACK ** top_pointer)
{
    int error = NO_ERROR;
    if (stack_size() < 1) 
    {
	    error = FEW_DATA;
    }
	else
	{
        unsigned int address = pop_from_stack(top_pointer);
	    int element = get_memory_elem(address);
		push_in_stack(top_pointer, element);
	}
	return error;
}
int sti(struct STACK ** top_pointer)
{
    int error = NO_ERROR;
    if (stack_size() < 2) 
    {
	    error = FEW_DATA;
    }
	else
	{
        unsigned int address = pop_from_stack(top_pointer);
	    int element = pop_from_stack(top_pointer);
		set_memory_elem(element, address);
	}
	return error;
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

int jmp (cmd* programm, char* label)
{
    int error = NO_ERROR;
	int i = 0;
	int ip = 0;
	while (programm[i].opcode != HLT)
	{
	    if (!strcmp(programm[i].label_def, label))
		{
		    ip = i;
			break;
		}
		i++;
	}
	return ip;
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
