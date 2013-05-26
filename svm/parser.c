#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include "parser.h"
#include "errors.h"

#define MAX_CMD_SIZE 10
#define CODE_SIZE 100
#define MAX_LABEL_SIZE 10
#define MAX_ARG_SIZE 10
#define INPUT_STR_SIZE 20

str input_string(FILE* f)
{
    str curr_string;
	curr_string.eof = 0;
	curr_string.string = (char*) malloc(sizeof(char) * INPUT_STR_SIZE);	
	int k = 0;
	int char_counter = 0;
    while (1)
	{
	    
	    curr_string.string[k] = fgetc(f);
        		
		if (curr_string.string[k] == '\t') 
		{
		    curr_string.string[k] = ' ';
		}
		
		if ((curr_string.string[k] != '\t') && (curr_string.string[k] != ' ') && (curr_string.string[k] != '\n'))
		{
		    char_counter++;
		}
		
		if (curr_string.string[k] == '\n') 
		{
		    curr_string.string[k] = 0;
			
		    break;
		}		
		
        if (feof(f))
		{
		    curr_string.string[k] = 0;
		    curr_string.eof = 1;
		    break;
		}
				
        if (k % INPUT_STR_SIZE == INPUT_STR_SIZE - 1)
		{
		    curr_string.string = (char *)realloc(curr_string.string, sizeof(char) * INPUT_STR_SIZE * ((k % INPUT_STR_SIZE) + 2));
		}	
        k++;			
	}
	if (char_counter == 0) 
	{
	    curr_string.string[0] = 0;	
	}
    return curr_string;
}

int is_good_char(char c)
{
	return ((isalpha(c)) || (c == ' ') || (c == ':') ||  (c == ';') || (isdigit(c)));	
}

int is_number(char* string)
{
   int i;
   for(i = 0; i < strlen(string); i++)
		{
			if (!isdigit(string[i]))
			{
				return 0;
			}
		}
	return 1;	
}	

pars parser(char* path)
{
    int is_eof = 0;
    pars programm;
	programm.code = (cmd*)malloc(sizeof (cmd) * CODE_SIZE);
	programm.error = NO_ERROR;
    char* command_string;
	char* argument_string;
    int i = 0;
	int start_arg_pos = 0;
	int j = 0;
    FILE* f;
	
	if (path != NULL)
	{
        f = fopen(path, "r");
    }	
	if (f == NULL) 
	{
	    programm.error = INPUT_ERROR;
    }
	i = 0;
	
	while((!is_eof) && (!programm.error))
	{
		command_string = (char*) malloc (sizeof(char) * MAX_CMD_SIZE);
		argument_string = (char*) malloc (sizeof(char) * MAX_CMD_SIZE);
		programm.code[i].arg.label = (char*) malloc (sizeof(char) * MAX_LABEL_SIZE);
		programm.code[i].arg.label[0] = 0;
		
	    str curr_string = input_string(f);
		char* string = curr_string.string;
		is_eof = curr_string.eof;		
		programm.code[i].label_def = (char*) malloc (sizeof(char) * MAX_LABEL_SIZE);
		programm.code[i].label_def[0] = 0;
		j = 0;
		
		if (string[0] == 0)
		{
			continue;
		}
		
		//permit spaces
		while(string[j] == ' ')
		{
		    j++;
		}
		int curr_char = 0;
		//input command
		
		while((string[j] != 0) && (string[j] != ';') && (string[j] != ':') &&
		(string[j] != ' ') && (string[j] != '\n') && (string[j] != EOF))
		{
			if (!is_good_char(string[j]))
			{
			    programm.error = BAD_CHAR;
				break;
			}

			command_string[curr_char] = string[j];
			j++;
			curr_char++;
		}
		command_string[curr_char] = 0;
		
		while(string[j] == ' ')
		{
		    j++;
		}

		switch(string[j])
		{
		    case ':':
			    strcpy(programm.code[i].label_def, command_string);
				programm.code[i].opcode = LABEL;
			    break;			    				
			case '\n':
				programm.error = BAD_END_OF_STRING;
				break;	
			case EOF:
				programm.error = BAD_END_OF_STRING;
				break;
			case ';':
				break;
			default:	
				curr_char = 0;
			    while (is_good_char(string[j]) && (string[j] != 0) && (string[j] != ';') && (string[j] != ':') &&
				(string[j] != ' ') && (string[j] != '\n') && (string[j] != EOF))
				{
					argument_string[curr_char] = string[j];
					j++;
					curr_char++;
				}
				argument_string[curr_char] = 0;
				while(string[j] == ' ')
		        {
		            j++;
		        }
				if (string[j] != ';')
				{
					programm.error = BAD_END_OF_STRING;
					break;
				}
				break;
		}	

		if (!strcmp(command_string, "ld"))
		{	
		    programm.code[i].opcode = LD;
			if (is_number(argument_string))
			{
			    programm.code[i].arg.address = atoi(argument_string);
			}
			else 
			{
			    programm.error = WRONG_ARGUMENT;
			}
		}
		else if (!strcmp(command_string, "ldi"))
		{	
		    programm.code[i].opcode = LDI;
		}
		else if (!strcmp(command_string, "sti"))
		{	
		    programm.code[i].opcode = STI;
		}
		else if (!strcmp(command_string, "ldc"))
		{
		    programm.code[i].opcode = LDC;
			if (is_number(argument_string))
			{
			    programm.code[i].arg.element = atoi(argument_string);
			}
			else 
			{
			    programm.error = WRONG_ARGUMENT;
			}
		}	
		else if (!strcmp(command_string, "st"))
		{
		    programm.code[i].opcode = ST;
			if (is_number(argument_string))
			{
			    programm.code[i].arg.address = atoi(argument_string);
			}
			else 
			{
			    programm.error = WRONG_ARGUMENT;
			}
		}		
		else if (!strcmp(command_string, "add"))
		{
		    programm.code[i].opcode = ADD;
		}	
		else if (!strcmp(command_string, "sub"))
		{

		    programm.code[i].opcode = SUB;
		}	
		else if (!strcmp(command_string, "mul"))
		{
		    programm.code[i].opcode = MUL;
		}
		else if (!strcmp(command_string, "div"))
		{
		    programm.code[i].opcode = DIV;
		}
		else if (!strcmp(command_string, "mod"))
		{
		    programm.code[i].opcode = MOD;
		}
		else if (!strcmp(command_string, "cmp"))
		{
		    programm.code[i].opcode = CMP;
		}
		else if (!strcmp(command_string, "jmp"))
		{
		    programm.code[i].opcode = JMP;
			strcpy(programm.code[i].arg.label, argument_string);
		}
		else if (!strcmp(command_string, "br"))
		{
		    programm.code[i].opcode = BR;
			strcpy(programm.code[i].arg.label, argument_string);
		}
		else if (!strcmp(command_string, "hlt"))
		{
		    programm.code[i].opcode = HLT;
		}
		
		free(argument_string);
		free(command_string);		
        
		i++;
	}
	return programm;
}	