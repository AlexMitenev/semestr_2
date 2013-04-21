typedef struct
{
    int error;
	int eof;
	char* path;
	char* string;
}str;

typedef enum
{
    LD,
	LDC,
	ST,
	LDI,
	STI,
	ADD,
	SUB,
	MUL,
	DIV,
	MOD,
	CMP,
	JMP,
	BR,
	HLT,
	LABEL
}opcode;

typedef struct 
{
    opcode opcode;
	struct
	{ 
	    int element;
		unsigned int address;
		char* label;
	}arg;
	char* label_def;
}cmd;

typedef struct
{
    cmd* code;
	int error;
}pars;

pars parser(char*);