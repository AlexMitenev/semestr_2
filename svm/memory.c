#include <stdlib.h>
#include <stdio.h>
#include "memory.h"
#define SIZE_OF_MEMORY 256


void memory_initialize()
{
    int i;
    memory = (int*) malloc (sizeof (int) * SIZE_OF_MEMORY);
	for (i = 0; i < SIZE_OF_MEMORY; i++)
	{
	    memory[i] = 0;
	}
}

void memory_realloc(unsigned int memory_size)
{
    memory = (int*) realloc (memory, sizeof (int) * memory_size);
	memory[memory_size] = 0;
}

void set_memory_elem(int element, unsigned int address)
{
    memory[address] = element;
}

int get_memory_elem(unsigned int address)
{
    return memory[address];
}

