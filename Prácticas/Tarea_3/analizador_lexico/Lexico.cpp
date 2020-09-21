#include "lexico.h"

Lexico::Lexico(string fuente) {
	ind = 0;
	this->fuente = fuente;
}

Lexico::Lexico() {
	ind = 0;
}

string Lexico::tipoAcad(int tipo) {
	string cad = "";

	switch (tipo) {
	case TipoSimbolo::IDENTIFICADOR:
		cad = "Identificador";
		break;

	case TipoSimbolo::ENTERO:
		cad = "Entero";
		break;

	case TipoSimbolo::REAL:
		cad = "Real";
		break;

	case TipoSimbolo::CADENA:
		cad = "Cadena";
		break;

	case TipoSimbolo::TIPO:
		cad = "Tipo";
		break;

	case TipoSimbolo::OPADIC:
		cad = "Op. Adicion";
		break;

	case TipoSimbolo::OPMULT:
		cad = "Op. Multiplicacion";
		break;

	case TipoSimbolo::OPRELAC:
		cad = "Op. Relacional";
		break;

	case TipoSimbolo::OPOR:
		cad = "Op. Or";
		break;

	case TipoSimbolo::OPAND:
		cad = "Op. And";
		break;

	case TipoSimbolo::OPNOT:
		cad = "Op. Not";
		break;

	case TipoSimbolo::OPIGUALDAD:
		cad = "Op. Igualdad";
		break;

	case TipoSimbolo::SEMICOLON:
		cad = ";";
		break;

	case TipoSimbolo::COMA:
		cad = ",";
		break;

	case TipoSimbolo::ABREPAR:
		cad = "(";
		break;

	case TipoSimbolo::CIERRAPAR:
		cad = ")";
		break;

	case TipoSimbolo::ABRELLAVE:
		cad = "{";
		break;

	case TipoSimbolo::CIERRALLAVE:
		cad = "}";
		break;

	case TipoSimbolo::ASIGNACION:
		cad = "=";
		break;

	case TipoSimbolo::IF:
		cad = "if";
		break;
	
	case TipoSimbolo::WHILE:
		cad = "while";
		break;

	case TipoSimbolo::RETURN:
		cad = "return";
		break;
	
	case TipoSimbolo::ELSE:
		cad = "else";
		break;

	case TipoSimbolo::PESOS:
		cad = "Fin de la Entrada";
		break;

	case TipoSimbolo::ERROR:
		cad = "Error: caracter invalido o sintaxix incorrecta";
		break;
	}

	return cad;
}

void Lexico::entrada(string fuente) {
	ind = 0;
	this->fuente = fuente;
}

int Lexico::sigSimbolo() {

	estado = 0;
	continua = true;
	simbolo = "";

	//Inicio del Automata
	while (continua) {
		c = sigCaracter();
		switch (estado) {
		case 0:
			if (esLetra(c)) {							//Identificadores
				while (esLetra(c) || esDigito(c)) {
					simbolo += c;
					c = sigCaracter();
				}
					if (simbolo == "int") aceptacion(4);		//Tipos de datos
				else 
					if (simbolo == "float") aceptacion(4);
				else 
					if (simbolo == "void") aceptacion(4);
				else 
					if (simbolo == "if") aceptacion(19);		//Palabras reservadas
				else 
					if (simbolo == "while") aceptacion(20);
				else 
					if (simbolo == "return") aceptacion(21);
				else 
					if (simbolo == "else") aceptacion(22);
				else aceptacion(0);	
			}
		else
			if (esDigito(c)) {							//Enteros y Reales
				while (esDigito(c)) {	
					simbolo += c;
					c = sigCaracter();
				}
				if (esPunto(c)) {
					simbolo += c;
					c = sigCaracter();
					while (esDigito(c)) {
						simbolo += c;
						c = sigCaracter();
					}
					aceptacion(2);					//Es Real
					retroceso();
				}
				else  aceptacion(1);				//Es Entero
				
			}
		else
			if (c == '"') {							//Cadena
				simbolo += c;
				c = sigCaracter();
				while (c != '"') {
					simbolo += c;
					c = sigCaracter();
					if (c == '$')  break;  
				}

				if (c == '$') {						// Error: No se cerró la cadena
					aceptacion(-1); 
				}								

				else if (c == '"') {				// Cadena aceptada.
					aceptacion(3);
				}
			}
		else
			if (c == '+' || c == '-'){				//Operadores de adición
				aceptacion(5);
			}
		else
			if (c == '*' || c == '/') {				//Operadores de multiplicación
				aceptacion(6);
			}
		else 
			if (c == '<' || c == '>') {				//Operadores de relación			
				if (sigCaracter() == '=') {
					simbolo += c;
					c = sigCaracter();
					simbolo += '=';
					aceptacion(7);
				} else
				{
					simbolo += c;
					aceptacion(7);
				}
			}
		else
			if (c == '|') {
				if (sigCaracter() == '|') {
					simbolo += '||';
					aceptacion(8);
				}
				else {
					aceptacion(-1);
				}
			}
		else
			if (c == '&') {
				if (sigCaracter() == '&') {
					simbolo += '&&';
					aceptacion(9);
				}
				else {
					aceptacion(-1);
				}
			}
		else
			if (c == '!'){							
				if (sigCaracter() == '=') {		
					simbolo += '=';
					aceptacion(11);			//Operador de igualdad
					
				}
				else {
					simbolo += c;
					aceptacion(10);			//Operador NOT
				}							
			}
		else
			if (c == ';') {					//Semicolon
				aceptacion(12);
			}
		else
			if (c == ',') {
				aceptacion(13);
			}
		else
			if (c == '(') {					//Abre paréntesis
				aceptacion(14);
			}
		else
			if (c == ')') {					//Cierra paréntesis
				aceptacion(15);
			}
		else
			if (c == '}') {					//Abre llave
				aceptacion(16);
			}
		else 
			if (c == '{') {					//Cierra llave
				aceptacion(17);
			}
		else
			if (c == '=') {									
				if (sigCaracter() == '=') {	
					aceptacion(11);				// Operador de igualdad.
					simbolo += '=';	
				}
				else {
					aceptacion(18);				//Asignación
				}
			}
		else
			if (esEspacio(c)) {
			}
		else
			if (c == '$') aceptacion(23);
		else {								//Error
				simbolo = simbolo + c;
				aceptacion(-1);
			 }
			break;	
		}

	}
	//Fin del Automata

	switch (estado) {

	case 0:
		tipo = TipoSimbolo::IDENTIFICADOR;
		break;

	case 1:
		tipo = TipoSimbolo::ENTERO;
		break;

	case 2:
		tipo = TipoSimbolo::REAL;
		break;

	case 3:
		tipo = TipoSimbolo::CADENA;
		break;

	case 4:
		tipo = TipoSimbolo::TIPO;
		break;

	case 5:
		tipo = TipoSimbolo::OPADIC;
		break;

	case 6:
		tipo = TipoSimbolo::OPMULT;
		break;

	case 7:
		tipo = TipoSimbolo::OPRELAC;
		break;

	case 8:
		tipo = TipoSimbolo::OPOR;
		break;

	case 9:
		tipo = TipoSimbolo::OPAND;
		break;

	case 10:
		tipo = TipoSimbolo::OPNOT;
		break;

	case 11:
		tipo = TipoSimbolo::OPIGUALDAD;
		break;

	case 12:
		tipo = TipoSimbolo::SEMICOLON;
		break;

	case 13:
		tipo = TipoSimbolo::COMA;
		break;

	case 14:
		tipo = TipoSimbolo::ABREPAR;
		break;

	case 15:
		tipo = TipoSimbolo::CIERRAPAR;
		break;

	case 16:
		tipo = TipoSimbolo::ABRELLAVE;
		break;

	case 17:
		tipo = TipoSimbolo::CIERRALLAVE;
		break;

	case 18:
		tipo = TipoSimbolo::ASIGNACION;
		break;

	case 19:
		tipo = TipoSimbolo::IF;
		break;

	case 20:
		tipo = TipoSimbolo::WHILE;
		break;

	case 21:
		tipo = TipoSimbolo::RETURN;
		break;

	case 22:
		tipo = TipoSimbolo::ELSE;
		break;

	case 23:
		tipo = TipoSimbolo::PESOS;
		break;

	default:
		tipo = TipoSimbolo::ERROR;
	}

	return tipo;
}

char Lexico::sigCaracter() {
	if (terminado()) {
		return '$';
	}
	return fuente[ind++];
}

void Lexico::sigEstado(int estado) {
	if ((estado >= 0 && estado <= 4 && estado != 3) || estado == 7  
					|| (estado >= 19 && estado <= 22) || estado == 10) {
		if (terminado()) return;
		this->estado = estado;
		ind--;
	}
	else {
		this->estado = estado;
		simbolo += c;
	}
}

void Lexico::aceptacion(int estado) {
	sigEstado(estado);
	continua = false;
}

bool Lexico::terminado() {//fin de cadena
	return ind >= fuente.length();
}

bool Lexico::esLetra(char c) {
	return isalpha(c) || c == '_';
}

bool Lexico::esOpRelacional(char c) {
	return c == '<' || c == '>';
}

bool Lexico::esDigito(char c) {
	return isdigit(c);
}

bool Lexico::esEspacio(char c) {
	return c == ' ' || c == '\t';
}

bool Lexico::esPunto(char c) {
	return c == '.' || c == '\t';
}


void Lexico::retroceso() {
	if (c != '$') ind--;
	continua = false;
}