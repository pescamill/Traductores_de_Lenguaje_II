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

	case TipoSimbolo::OPADIC:
		cad = "Op. Adicion";
		break;

	case TipoSimbolo::OPMULT:
		cad = "Op. Multiplicacion";
		break;

	case TipoSimbolo::PESOS:
		cad = "Fin de la Entrada";
		break;

	case TipoSimbolo::ENTERO:
		cad = "Entero";
		break;

	case TipoSimbolo::REAL:
		cad = "Real";
		break;

	case TipoSimbolo::ERROR:
		cad = "Error: Caracter no aceptado";
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
			if (esLetra(c)) {	
				while (esLetra(c) or esDigito(c)) {
					cout << c;
					c = sigCaracter();
				}
				aceptacion(0);
			}
		else
			if (esDigito(c)) {
				while (esDigito(c)) {
					cout << c;
					c = sigCaracter();
				}
				if (esPunto(c)) {
					cout << c;
					c = sigCaracter();
				while (esDigito(c)) {
					cout << c;
					c = sigCaracter();

				}
				}
				aceptacion(5);
			}
		else
			if (c == '$') aceptacion(3);
		else {
				cout << c;
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

	case 3:
		tipo = TipoSimbolo::PESOS;
		break;

	case 5:
		tipo = TipoSimbolo::REAL;
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
	this->estado = estado;
	simbolo += c;
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