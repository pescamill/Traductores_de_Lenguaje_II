#ifndef _PILA
#define _PILA

#include <list>
#include <iostream>
#include <string>

using namespace std;

class Alumno {
protected:
	string codigo;
public:
	virtual void muestra() {}
};
class Bachillerato : public Alumno {
protected:
	string preparatoria;
public:
	Bachillerato(string codigo, string preparatoria) {
		this->codigo = codigo;
		this->preparatoria = preparatoria;
	}
	void muestra() {
		cout << "Alumno Bachillerato" << endl;
		cout << "Codigo: " << codigo << endl;
		cout << "Preparatoria: " << preparatoria << endl << endl;
	}
};
class Licenciatura : public Alumno {
protected:
	string carrera;
	int creditos;
public:
	Licenciatura(string codigo, string carrera, int creditos) {
		this->codigo = codigo;
		this->carrera = carrera;
		this->creditos = creditos;
	}
	void muestra() {
		cout << "Alumno Licenciatura" << endl;
		cout << "Codigo: " << codigo << endl;
		cout << "Carrera: " << carrera << endl;
		cout << "Creditos: " << creditos << endl << endl;
	}
};

class Pila {

private:
	list <Alumno*> lista;

public:
	void push(Alumno *x);
	Alumno* top();
	Alumno* pop();
	void muestra();
};


#endif