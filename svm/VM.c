#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "ctype.h"
#include "stack.h"
#include "memory.h"
#include "parser.h"
#include "commands.h"
#include "errors.h"

#define MAX_LABEL_SIZE 10
#define START_MEM_SIZE 256
#define PATH_SIZE 30

typedef struct 
{
    int value;
	int error;
}result;

result vm(pars programm)
{
    //begin VM
	int ip = 0;
	struct STACK *top_pointer = NULL;
	memory_initialize();
	
	int stack_top_element = 0;
	int is_transit;
	int current_memory_size = START_MEM_SIZE;
    result result;
	result.error = 0;
	result.value = 0;
	while ((programm.code[ip].opcode != HLT) && (!result.error))
	{
	    switch(programm.code[ip].opcode)
		{
		    
			case LD:
			    if (programm.code[ip].arg.address >= current_memory_size)
				{
				    //memory realloc
				    current_memory_size = current_memory_size * (programm.code[ip].arg.address / current_memory_size +1);		
					memory_realloc(current_memory_size);
				}
				ld(&top_pointer, programm.code[ip].arg.address);				
				break;
			case LDC:
				ldc(&top_pointer, programm.code[ip].arg.element); 
				break;
			case LDI:
				result.error = ldi(&top_pointer); 
				break;	
			case STI:
				result.error = sti(&top_pointer); 
				break;	
			case ST:
			    if (programm.code[ip].arg.address >= current_memory_size)
				{
				    //memory realloc
				    current_memory_size = current_memory_size * (programm.code[ip].arg.address / current_memory_size + 1);	
					memory_realloc(current_memory_size);					
				}
				result.error = st(&top_pointer, programm.code[ip].arg.address); 
				break;	
			case ADD:
				result.error = addition(&top_pointer); 
				break;
			case SUB:
				result.error = substruction(&top_pointer); 
				break;	
			case MUL:
				result.error = multiplication(&top_pointer); 
				break;	
			case DIV:
				result.error = division(&top_pointer); 
				break;	
			case MOD:
				result.error = modul(&top_pointer); 
				break;
			case CMP:
				result.error = compare(&top_pointer); 
				break;	
			case JMP:
				ip = jmp(programm.code, programm.code[ip].arg.label) - 1;
				break;
			case BR:
			    stack_top_element = pop_from_stack(&top_pointer);
				if (stack_top_element)
				{
				    ip = jmp(programm.code, programm.code[ip].arg.label) - 1;
				}
				break;
			case HLT:
                result.error = hlt(&top_pointer);
				break;
		    default:
			    if (programm.code[ip].opcode != LABEL)
				{
				    result.error = UNKNOWN_COMMAND;
				}
		}
		ip++;
	}	
	
	if (!result.error)
	{
	    result.value = pop_from_stack(&top_pointer);
	}
	
    return result;
}

str input_validation(int argc, char* argv[])
{
    str curr_string;
    curr_string.error = INPUT_ERROR;
    curr_string.path = (char*) malloc (sizeof(char) * PATH_SIZE);
    if (argc == 1)
    {
		printf("Enter path\n");
		gets(curr_string.path);
        curr_string.error = NO_ERROR;
    }
    
    else if (argc == 2)
    {
        curr_string.path = argv[1];
        curr_string.error = NO_ERROR;
    }
	else 
	{
	    curr_string.error = TOO_MANY_ARGS;
	}
	return curr_string;
}

int main(int argc, char* argv[])
{
    str curr_string;
	result result;
	result.value = 0;
	result.error = 0;
    pars programm;
    curr_string = input_validation(argc, argv);
	result.error = curr_string.error;
	
	if (!result.error)
	{
	    char* path = curr_string.path;
	     programm = parser(path);
	
	    if (programm.error)
	    {
	    result.error = programm.error;
	    }
	    else
	    {
	        result = vm(programm);
	    }
	}	
	
	switch(result.error)
	{
	    case NO_ERROR:
		    printf("%d", result.value);
			break;
		case INPUT_ERROR:
		    printf("error input file");
			break;
		case SYNTAX_ERROR:
		    printf("sintacsis error");
			break;	
		case BAD_CHAR:
		    printf("unknown simvol");
			break;	
		case FEW_DATA:
		    printf("few data");
			break;	
		case BAD_END:
		    printf("bad end of file");
			break;
		case DIVISION_BY_ZERO:
		    printf("devision by zero");
			break;	
        case BAD_END_OF_STRING:
		    printf("bad end of string");
			break;	
        case WRONG_ARGUMENT:
		    printf("wrong arguments");
			break;			
		case REPEAT_LABEL:
		    printf("repeat label");
			break;
		case CANT_FIND_LABEL:
		    printf("cant find label");
			break;
		case TOO_MANY_ARGS:
		    printf("too many arguments");
			break;	
		case UNKNOWN_COMMAND:
		    printf("unknown command");
			break;
	}
	return 0;
}	