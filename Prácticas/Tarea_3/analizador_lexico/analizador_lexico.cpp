// analizador_lexico.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include "lexico.h"
#include "pila.h"

#include <iostream>
#include <string>

void gramaticaA();
void gramaticaB();

int tablaLR[5][24] = {		// E -> <id> + <id>
	{2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
	{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
	{0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
	{4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
	{4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -2}
};

int tablaLR_b[5][24] = {		// E -> <id> + E | <id>
	{2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
	{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
	{0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -3},
	{2, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
	{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -2}
};

using namespace std;

int main(int argc, char *argv[]) {

	gramaticaA();
	gramaticaB();

	return 0;
}

void gramaticaA(){
	Pila pila;
	int fila, columna, accion;
	fila = columna = accion = 0;
	bool aceptacion = false;
	Lexico lexico("hola+mundo");

	pila.push(TipoSimbolo::PESOS);
	pila.push(0);


	while (accion != -2) {
		lexico.sigSimbolo();
		fila = pila.top();
		columna = lexico.tipo;
		accion = tablaLR[fila][columna];
		pila.muestra();
		cout << "entrada: " << lexico.simbolo << endl;
		cout << "accion: " << accion << endl;

		if (accion == -2) break;

		pila.push(lexico.tipo);
		pila.push(accion);
	}

	pila.pop();
	pila.pop();
	pila.pop();
	pila.pop();
	pila.pop();
	pila.pop();

	fila = pila.top();
	columna = 3;
	accion = tablaLR[fila][columna];
	pila.push(3);
	pila.push(accion);
	pila.muestra();
	fila = pila.top();
	columna = lexico.tipo;
	accion = tablaLR[fila][columna];
	cout << "entrada: " << lexico.simbolo << endl;
	cout << "accion: " << accion << endl << endl;

	aceptacion = accion == -1;
	if (aceptacion) cout << "aceptacion" << endl;

}

void gramaticaB() {
	Pila pila;
	int fila, columna, accion;
	fila = columna = accion = 0;
	bool aceptacion = false;
	Lexico lexico("a+b+c+d+e+f");

	pila.push(TipoSimbolo::PESOS);
	pila.push(0);


	while (accion != -3) {
		lexico.sigSimbolo();
		fila = pila.top();
		columna = lexico.tipo;
		accion = tablaLR_b[fila][columna];
		pila.muestra();
		cout << "entrada: " << lexico.simbolo << endl;
		cout << "accion: " << accion << endl;

		if (accion == -3) break;  // E-> (vacio)
			

		pila.push(lexico.tipo);
		pila.push(accion);
	}

	
	pila.pop();
	pila.pop();
	pila.pop();
	pila.pop();
	pila.pop();

	fila = pila.top();
	columna = 3;
	accion = tablaLR_b[fila][columna];
	pila.push(3);
	pila.push(accion);
	pila.muestra();
	fila = pila.top();
	columna = lexico.tipo;
	accion = tablaLR_b[fila][columna];
	cout << "entrada: " << lexico.simbolo << endl;
	cout << "accion: " << accion << endl << endl;

	aceptacion = accion == -1;
	if (aceptacion) cout << "aceptacion" << endl;

}

