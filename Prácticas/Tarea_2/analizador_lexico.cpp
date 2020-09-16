// analizador_lexico.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include "lexico.h"

#include <iostream>
#include <string>



using namespace std;

int main(int argc, char *argv[]) {
	
	Lexico lexico;
	lexico.entrada(";;,;int float (){}{{}} & && &&& void if while < <= >=> <<= return else a a+1 !!= a4j3aa | || ||| === hola1 888 22.2 2*3; 8/1 \"hola mundo\"  \  yes asi 2+2 toma +-+");
	cout << "Resultado del Analisis Lexico" << endl << endl;
	cout << "Simbolo\t\t\t\t\t\tTipo" << endl;

	while (lexico.simbolo.compare("$") != 0) {
		lexico.sigSimbolo();
		cout << "[" << lexico.simbolo << "]" << "\t\t\t\t\t\t" << lexico.tipoAcad(lexico.tipo) << endl;
	}

	cin.get();

	return 0;
}

