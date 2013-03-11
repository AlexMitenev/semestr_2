#ifndef _sort_h
#define _sort_h

int *memory = NULL;

void set_memory_elem(int, unsigned int);
int get_memory_elem(unsigned int);
void memory_initialize();
void memory_realloc(unsigned int address);

#endif