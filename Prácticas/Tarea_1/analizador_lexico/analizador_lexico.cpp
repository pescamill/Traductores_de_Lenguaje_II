// analizador_lexico.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include "lexico.h"

#include <iostream>
#include <string>



using namespace std;

int main(int argc, char *argv[]) {
	
	Lexico lexico;
	lexico.entrada("a4j3aa hola1 hola 3 222.2 ++ - 32.2 33.3");

	cout << "Resultado del Analisis Lexico" << endl << endl;
	cout << "Simbolo\t\tTipo" << endl;

	while (lexico.simbolo.compare("$") != 0) {
		lexico.sigSimbolo();
		cout << "\t\t" << lexico.tipoAcad(lexico.tipo) << endl;
	}

	cin.get();

	return 0;
}

