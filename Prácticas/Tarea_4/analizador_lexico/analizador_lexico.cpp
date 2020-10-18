// analizador_lexico.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include "lexico.h"
#include "pila.h"

#include <iostream>
#include <string>


using namespace std;

void ejemplo();

int main(int argc, char *argv[]) {
	ejemplo();
	return 0;
}

void ejemplo() {
	Pila pila;
	Alumno *alumno;
	alumno = new Licenciatura("345678", "Computacion", 200);
	pila.push(alumno);
	pila.push(new Bachillerato("456789", "Preparatoria 12"));
	pila.push(new Licenciatura("456789", "Informatica", 50));
	pila.muestra();
	cout << "*********************************" << endl;
	pila.pop();
	pila.muestra();
}