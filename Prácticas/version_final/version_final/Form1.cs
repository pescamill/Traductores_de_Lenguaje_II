using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Practica_1
{
    public partial class Form1 : Form
    {
        //Declaracion
        int idx;
        int estado = 0;
        string cadena;
        string token;
        string[] tipos = {"int","void","char","float"};
        string[] estructuras = { "if", "while", "return", "else" };
        int[,] Tabla = new int[84, 40];
        int[,] Reglas = new int[43,2];
        Stack<Nodo> Pila;
        Queue<Nodo> Cola;
        Nodo nodo;
        List<ElementoTabla> tab;
        List<string> errores;
        public Form1()
        {
            InitializeComponent();
            Cargar();
        }

        private void Copiar_Click(object sender, EventArgs e)
        {
            Pila = new Stack<Nodo>();
            Cola = new Queue<Nodo>();
            tab = new List<ElementoTabla>();
            errores = new List<string>();
            dataGridView1.Rows.Clear();
            cadena = A1.Text;
            cadena += '$';
            idx = 0;
            while(idx < cadena.Length)
            {
               token = "";
               estado = 0;
               while(estado != 20)
                {

                    if(estado == 0)
                    {
                        
                        Verificacion(); 
                    }
                    if(estado == 1)
                    {
                        if (string.Equals(token,tipos[0]) || string.Equals(token, tipos[1]) || string.Equals(token, tipos[2]) || string.Equals(token, tipos[3]))
                        {
                            estado = 0;
                        }
                        else if(string.Equals(token,estructuras[0]))
                        {
                            estado = 9;
                        }
                        else if (string.Equals(token, estructuras[1]))
                        {
                            estado = 10;
                        }
                        else if (string.Equals(token, estructuras[2]))
                        {
                            estado = 11;
                        }
                        else if (string.Equals(token, estructuras[3]))
                        {
                            estado = 12;
                        }
                        else if ((cadena[idx] >= 'a' && cadena[idx] <= 'z') || (cadena[idx] >= 'A' && cadena[idx] <= 'Z') || cadena[idx] == '_')
                        {
                            token += cadena[idx];
                            idx++;
                        }
                        else
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            MostrarDatos();
                            estado = 20;
                        }
                    }
                    if((estado >= 2 && estado <= 7) || estado == 18 || estado == 16 || estado == 14)
                    {
                        Nodo nuevo = new Nodo();
                        nuevo.estado = estado;
                        nuevo.simbolo = token;
                        Cola.Enqueue(nuevo);
                        MostrarDatos();
                        idx++;
                        estado = 20;
                    }
                    if(estado == 8)
                    {
                        if(cadena[idx+1]=='=')
                        {
                            estado = 17;
                           
                        }
                        else
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            MostrarDatos();
                            idx++;
                            estado = 20;
                        }
                    }
                    if(estado == 9)
                    {
                        Nodo nuevo = new Nodo();
                        nuevo.estado = estado;
                        nuevo.simbolo = token;
                        Cola.Enqueue(nuevo);
                        MostrarDatos();
                        estado = 20;
                    }
                    if (estado == 10)
                    {
                        Nodo nuevo = new Nodo();
                        nuevo.estado = estado;
                        nuevo.simbolo = token;
                        Cola.Enqueue(nuevo);
                        MostrarDatos();
                        estado = 20;
                    }
                    if (estado == 11)
                    {
                        Nodo nuevo = new Nodo();
                        nuevo.estado = estado;
                        nuevo.simbolo = token;
                        Cola.Enqueue(nuevo);
                        MostrarDatos();
                        estado = 20;
                    }
                    if (estado == 12)
                    {
                        Nodo nuevo = new Nodo();
                        nuevo.estado = estado;
                        nuevo.simbolo = token;
                        Cola.Enqueue(nuevo);
                        MostrarDatos();
                        estado = 20;
                    }
                    if (estado == 13)
                    {
                        if((cadena[idx] >= '0' && cadena[idx] <= '9') || cadena[idx] == '.')
                        {
                            token += cadena[idx];
                            idx++;
                        }
                        else
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            MostrarDatos();
                            estado = 20;
                        }
                    }
                    if(estado == 15)
                    {
                        if(cadena[idx] == '&' && cadena[idx+1]=='&')
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            token += cadena[idx + 1];
                            idx+=2;
                            MostrarDatos();
                            estado = 20;
                        }
                        else if (cadena[idx] == '|' && cadena[idx + 1] == '|')
                        {
                            MessageBox.Show("Error");
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            token += cadena[idx + 1];
                            idx += 2;
                            MostrarDatos();
                            estado = 20;
                        }
                        //Error
                        else
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            estado = 14;
                            token += "*";
                            idx++;
                            MostrarDatos();
                            estado=20;
                        }
                        

                    }
                    if (estado == 17)
                    {
                        if (cadena[idx + 1] == '=')
                        {
                            
                            token += cadena[idx+1];
                            idx++;
                        }
                        else
                        {
                            Nodo nuevo = new Nodo();
                            nuevo.estado = estado;
                            nuevo.simbolo = token;
                            Cola.Enqueue(nuevo);
                            MostrarDatos();
                            idx++;
                            estado = 20;
                        }
                    }
                }
            }
            Validacion();
            
        }

        public void MostrarDatos()
        {
            DataGridViewRow fila = new DataGridViewRow();
            fila.CreateCells(dataGridView1);
            fila.Cells[0].Value = estado;
            fila.Cells[1].Value = token;
            dataGridView1.Rows.Add(fila);
        }
        public void Verificacion()
        {
            if (string.Equals(token,tipos[0]) || string.Equals(token,tipos[1]) || string.Equals(token,tipos[2]) || string.Equals(token,tipos[3]))
            {
                Nodo nuevo = new Nodo();
                nuevo.estado = estado;
                nuevo.simbolo += token;
                Cola.Enqueue(nuevo);
                MostrarDatos();
                estado = 20;
            }
            else if ((cadena[idx]>='a' && cadena[idx]<='z') || (cadena[idx]>='A' && cadena[idx]<='Z') || cadena[idx]=='_')
            {
                estado = 1;
            }
            else if (cadena[idx] == ';')
            {
                estado = 2;
                token += cadena[idx];
            }
            else if (cadena[idx] == ',')
            {
                estado = 3;
                token += cadena[idx];

            }
            else if (cadena[idx] == '(')
            {
                estado = 4;
                token += cadena[idx];

            }
            else if (cadena[idx] == ')')
            {
                estado = 5;
                token += cadena[idx];

            }
            else if (cadena[idx] == '{')
            {
                estado = 6;
                token += cadena[idx];

            }
            else if (cadena[idx] == '}')
            {
                estado = 7;
                token += cadena[idx];

            }
            else if (cadena[idx] == '=')
            {
                estado = 8;
                token += cadena[idx];

            }
            else if((cadena[idx]>='0' && cadena[idx]<='9') || cadena[idx]=='.')
            {
                estado = 13;
            }
            else if(cadena[idx] == '+' || cadena[idx] == '-')
            {
                estado = 14;
                token += cadena[idx];
            }
            else if (cadena[idx] == '&' || cadena[idx] == '|')
            {
                estado = 15;
                token += cadena[idx];
            }
            else if(cadena[idx] == '*' || cadena[idx] == '/')
            {
                estado = 16;
                token += cadena[idx];
            }
            else if(cadena[idx] == '>' || cadena[idx] == '<')
            {
                estado = 17;
                token += cadena[idx];
            }
            else if (cadena[idx] == '$')
            {
                estado = 18;
                token += cadena[idx];
            }
            else if(cadena[idx] == ' ')
            {
                idx++;
            }
            else if(cadena[idx] == '\n')
            {
                idx++;
            }
        }
        void Cargar()
        {  
            string[] lines = System.IO.File.ReadAllLines("GR2slrTable.txt");
            int row = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] partes = lines[i].Split('\t');
                int column = 0;
                for (int c = 1; c < partes.Length; c++)
                {
                    Tabla[row, column] = Convert.ToInt32(partes[c]);
                    column++;
                }
                row++;
            }
            int columna = 0;
            lines = System.IO.File.ReadAllLines("GR2slrRulesId.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] partes = lines[i].Split('\t');
                for (int c = 0; c < partes.Length; c++)
                {
                    Reglas[columna, c] = Convert.ToInt32(partes[c]);
                }
                columna++;
            }
        }

        void Validacion()
        {
            Nodo nuevo = new Nodo();
            nuevo.estado = 0;
            Pila.Push(nuevo);
            int valorTabla = 1;
            int izquierda = 0;
            bool Valida = false;
            while(valorTabla != -1 && valorTabla != 0)
            {
                valorTabla = Tabla[Pila.Peek().estado, Cola.Peek().estado];
                if (valorTabla >= 0)
                {
                    Pila.Push(Cola.Dequeue());
                    nuevo = new Nodo();
                    nuevo.estado = valorTabla;
                    Pila.Push(nuevo);
                }
                else if (valorTabla < -1)
                {
                    int regla = (valorTabla * -1) - 1;
                    izquierda = Reglas[regla, 0];
                    ObtenerRegla(regla);
                    valorTabla = Tabla[Pila.Peek().estado, izquierda];
                    nodo.estado = izquierda;
                    Pila.Push(nodo);
                    nuevo = new Nodo();
                    nuevo.estado = valorTabla;
                    Pila.Push(nuevo);
                    
                }
                else if (valorTabla == -1)
                {
                    Valida = true;
                    break;
                }
            }
            if (Valida && errores.Count == 0)
                MessageBox.Show("Valido");
            else
                MessageBox.Show("Invalido");
            nuevo = nodo;
            int numero = 1;
            ListErrors.Items.Clear();
            foreach( string error in errores )
            {
                ListErrors.Items.Add(numero.ToString()+". "+error);
                numero++;
            }

        }
        
        void ObtenerRegla(int _numeroRegla)
        {
            switch (_numeroRegla)
            {
                case 0: //Programa -> Definiciones
                    nodo = new programa(Pila);
                    break;
                case 2: //<Definiciones> ::= <Definicion> <Definiciones>
                case 15: //<DefLocales> ::= <DefLocal> <DefLocales> 
                case 19: //<Sentencias> ::= <Sentencia> <Sentencias>
                case 29: //<Argumentos> ::= <Expresion> <ListaArgumentos> 
                    Pila.Pop(); //Quita estado
                    Nodo aux = Pila.Pop(); //Quita Definiciones, deflocales, sentencias, o listaArgumentos
                    Pila.Pop(); //Quita estado
                    nodo = Pila.Pop(); //Quita Definicion, DefLocal, Sentencia, Expresion
                    nodo.siguiente = aux;
                    break;
                case 3: //Quita Definicion -> DefVar
                case 4: //Quita Definicion -> DefFunc
                case 16: //Quita DefLocal -> DefVar
                case 17: //Quita DefLocal -> Sentencia
                case 32: //Quita Expresion -> LlamadaFunc
                case 33: //Quita Expresion -> id
                case 34: //Quita Expresion -> constante
                case 36: //Quita SentenciaBloque -> Sentencia
                case 37: //Quita SentenciaBloque -> Bloque
                    Pila.Pop(); //Quita estado
                    nodo = Pila.Pop(); //Quita DefVar
                    break;
                case 5:// <DefVar> ::= tipo id <ListaVar> ;
                    nodo = new DefVar(Pila);
                    break;
                case 7: //<ListaVar> ::= , id <ListaVar>
                    Pila.Pop(); //Quita estado
                    Nodo lvar = Pila.Pop(); //Quita ListaVar
                    Pila.Pop(); //Quita estado
                    nodo = new Id(Pila.Pop().simbolo); //Quita Id
                    nodo.siguiente = lvar;
                    Pila.Pop(); //Quita estado;
                    Pila.Pop();//Quita ,
                    break;
                case 8: //<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc>
                    nodo = new DefFunc(Pila);
                    nodo.validatipos(tab,errores);
                    break;
                case 10: //<Parametros> ::= tipo id <ListaParam>
                    nodo = new Parametros(Pila);
                    break;
                case 12://<ListaParam> ::= , tipo id <ListaParam>
                    nodo = new Parametros(Pila);
                    Pila.Pop(); //Quita estado
                    Pila.Pop(); //Quita la ,
                    break;
                case 13://<BloqFunc> ::= { <DefLocales> }
                case 27: //<Bloque> ::= { <Sentencias>}
                case 38://<Expresion> ::= ( <Expresion> )
                    Pila.Pop(); //Quita estado
                    Pila.Pop(); //Quita ) o }
                    Pila.Pop(); //Quita estado
                    nodo = Pila.Pop(); //Quita DefLocales
                    Pila.Pop(); //Quita estado
                    Pila.Pop(); //Quita ( o {
                    break;
                case 20: //<Sentencia> ::= id = <Expresion> ;
                    nodo = new Asignacion(Pila);
                    break;
                case 21://<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
                    nodo = new If(Pila);
                    break;
                case 22: //<Sentencia> ::= while ( <Expresion> ) <Bloque>
                    nodo = new While(Pila);
                    break;
                case 23: //<Sentencia> ::= return <Expresion> ;
                    nodo = new Return(Pila);
                    break;
                case 24://<Sentencia> ::= <LlamadaFunc> ; 
                    Pila.Pop();//Quita estado
                    Pila.Pop();//Quita ;
                    Pila.Pop();//Quira estado
                    nodo = Pila.Pop();
                    break;
                case 26://<Otro> ::= else <SentenciaBloque> 
                    Pila.Pop();//Quita estado
                    nodo = Pila.Pop(); //Quita SentenciaBloque
                    Pila.Pop();//Quita estado
                    Pila.Pop();//Quita else
                    break;
                case 31:// <ListaArgumentos> ::= , <Expresion> <ListaArgumentos> 
                    Pila.Pop(); //Quita estado
                    aux = Pila.Pop(); //Quita ListaArgumentos
                    Pila.Pop(); //Quita estado
                    nodo = Pila.Pop(); //Quita Expresion
                    Pila.Pop(); //Quita estado
                    Pila.Pop(); //Quita ,
                    nodo.siguiente = aux;
                    break;
                case 35:
                    nodo = new LlamadaFunc(Pila);
                    break;
                case 39:
                case 40:
                case 41:
                case 42:
                    nodo = new Operacion2(Pila);
                    break;
                default:
                    nodo = new Nodo();
                    break;
            }
        }
    }


}
