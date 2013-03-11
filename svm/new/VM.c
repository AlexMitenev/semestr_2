#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include "stack.h"
#include "memory.h"
#include "commands.h"
#include "parser.h"

#define NO_ERROR 0
#define INPUT_ERROR 1
#define BAD_CHAR 3
#define FEW_DATA 4
#define BAD_END 5
#define DIVISION_BY_ZERO 6
#define BAD_END_OF_STRING 7
#define WRONG_ARGUMENT 8
#define REPEAT_LABEL 9
#define CANT_FIND_LABEL 10
#define TOO_MANY_ARGS 11
#define UNKNOWN_COMMAND 12

#define MAX_LABEL_SIZE 10
#define START_MEM_SIZE 256
#define PATH_SIZE 30

typedef struct
{
    int error;
	int ip;
	int is_transit;
}lbl;

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
	
	int is_transit;
	int current_memory_size = START_MEM_SIZE;
    result result;
	result.error = 0;
	result.value = 0;
	lbl transit_cmd;
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
				transit_cmd = jmp(programm.code, programm.code[ip].arg.label);
				ip = transit_cmd.ip - 1;
				result.error = transit_cmd.error;
				break;
			case BR:
			    transit_cmd = br(programm.code, programm.code[ip].arg.label);
				if (transit_cmd.is_transit != 0)
				{
				    ip = transit_cmd.ip - 1;
				}
				result.error = transit_cmd.error;
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
	    case 0:
		    printf("%d", result.value);
			break;
		case 1:
		    printf("error input file");
			break;
		case 2:
		    printf("sintacsis error");
			break;	
		case 3:
		    printf("unknown simvol");
			break;	
		case 4:
		    printf("few data");
			break;	
		case 5:
		    printf("bad end of file");
			break;
		case 6:
		    printf("devision by zero");
			break;	
        case 7:
		    printf("bad end of string");
			break;	
        case 8:
		    printf("wrong arguments");
			break;			
		case 9:
		    printf("repeat label");
			break;
		case 10:
		    printf("cant find label");
			break;
		case 11:
		    printf("too many arguments");
			break;	
		case 12:
		    printf("unknown command");
			break;
	}
	return 0;
}	