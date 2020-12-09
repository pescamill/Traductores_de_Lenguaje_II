using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
namespace Practica_1
{
    public class Nodo
    {
        public string simbolo;
        public Nodo siguiente;
        public int estado;
        public static string ambito;
        public static char tipoDato;
        public static string cadenapa;
        public Nodo()
        {
            simbolo = "";
            siguiente = null;
        }
        public char dimetipo(Nodo tipo)
        {
            if (tipo.simbolo == "int")
                return 'i';
            else if (tipo.simbolo == "float")
                return 'f';
            return 'v';
        }

        public virtual void validatipos(List <ElementoTabla> tab, List<string> errores)
        {
            if (siguiente != null) siguiente.validatipos(tab, errores);
            if (!string.IsNullOrEmpty(simbolo))
            {
                if (simbolo[0] >= '0' && simbolo[0] <= '9')
                {
                    bool flotante = false;
                    for (int i = 0; i < simbolo.Length; i++)
                        if (simbolo[i] == '.')
                            flotante = true;
                    if (!flotante)
                        tipoDato = 'i';
                    else
                        tipoDato = 'f';
                }
                else
                    tipoDato = buscartipo2(tab, simbolo, ambito);
            }
        }


        public bool existe(List<ElementoTabla> Tab, string _id, char _tipo, string _ambito, string _cadParam)
        {
            for (int i = 0; i < Tab.Count; i++)
            {
                if (Tab[i].id == _id)
                    return true;
            }
            return false;
        }
        public bool existe(List<ElementoTabla> Tab, string _id, char _tipo, string _ambito)
        {
            for (int i = 0; i < Tab.Count; i++)
            {
                if (Tab[i].id == _id && Tab[i].ambito == _ambito)
                    return true;
            }
            return false;
        }
        public char buscartipo(List<ElementoTabla> Tab, string _id)
        {
            for(int i=0; i< Tab.Count; i++)
            {
                if (Tab[i].id == _id)
                    return Tab[i].tipo;
            }
            return '\0';
        }
        public char buscartipo2(List<ElementoTabla> Tab, string _id,string _ambito)
        {
            for (int i = 0; i < Tab.Count; i++)
            {
                if (Tab[i].id == _id && Tab[i].ambito == _ambito)
                    return Tab[i].tipo;
            }
            return '\0';
        }
    }
    public class programa : Nodo{
        public programa(Stack<Nodo> pila)
        {
            pila.Pop(); // Quita el estado
            siguiente = pila.Pop();
        }
    }

    public class Id : Nodo
    {
        public Id(String _simbolo)
        {
            simbolo = _simbolo;
        }
        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            tab.Add(new ElementoTabla(simbolo,tipoDato,ambito));
            if (siguiente != null) siguiente.validatipos(tab, errores);
        }
    }

    public class Tipo : Nodo
    {
        public Tipo(string _simbolo)
        {
            simbolo = _simbolo;
        }
    }

    public class DefVar : Nodo
    {
        Nodo tipo;
        Nodo id;
        Nodo lvar;
        public DefVar(Stack<Nodo> pila)
        {
            pila.Pop();//Quita estado
            pila.Pop();//Quita ;
            pila.Pop();//Quita estado
            lvar = pila.Pop(); //Quita ListVar
            pila.Pop(); // Quita estado
            id = new Id(pila.Pop().simbolo); // Quita id
            pila.Pop(); //Quita estado
            tipo = new Tipo(pila.Pop().simbolo); //Quita tipo
        }

        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            tipoDato = dimetipo(tipo);
            if (!existe(tab, id.simbolo, tipoDato, ambito))
                tab.Add(new ElementoTabla(id.simbolo, tipoDato, ambito));
            else
                errores.Add("La variable "+id.simbolo+" de la funcion "+ambito+" ya existe");
            if (lvar != null)
                lvar.validatipos(tab, errores);
            if (siguiente != null)
                siguiente.validatipos(tab,errores);
        }

    }

    public class DefFunc : Nodo
    {
        public Nodo tipo;
        public Nodo id;
        public Nodo parametros;
        public Nodo blocFunc;
        public static string varlocal;
        public DefFunc(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            blocFunc = pila.Pop(); //Quita bloquefunc
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita )
            pila.Pop(); //Quita estado
            parametros = pila.Pop(); //Quita parametros
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita (
            pila.Pop(); //Quita  estado
            id = new Id(pila.Pop().simbolo); //Quita id
            pila.Pop(); //Quita estado
            tipo = new Tipo(pila.Pop().simbolo); //Quita tipo
        }
        public override void validatipos (List<ElementoTabla> tabla, List<string> errores)
        {
            tipoDato = dimetipo(tipo);
            char tipodato = tipoDato;
            ambito = id.simbolo;
            cadenapa = "";
            if (parametros != null) parametros.validatipos(tabla,errores);
            if (!existe(tabla, id.simbolo, tipoDato, ambito, cadenapa))
                tabla.Add(new ElementoTabla(id.simbolo, tipodato, ambito, cadenapa));
            else
                errores.Add("La funcion " + id.simbolo + " ya existe");
            cadenapa = "";
            if (blocFunc != null) blocFunc.validatipos(tabla, errores);
            ambito = "";
            if (siguiente != null) siguiente.validatipos(tabla, errores);
        }
    }

    public class Parametros: Nodo
    {
        Nodo tipo;
        Nodo id;
        Nodo _listaParamaetros;
        public Parametros(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            _listaParamaetros = pila.Pop(); // Quita listaParametros
            pila.Pop(); // Quita estado
            id = new Id(pila.Pop().simbolo); // Quita id
            pila.Pop(); //Quita estado
            tipo = new Tipo(pila.Pop().simbolo); 
        }
        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            tipoDato = dimetipo(tipo);
            if (!existe(tab, id.simbolo, tipoDato, ambito))
            {
                tab.Add(new ElementoTabla(id.simbolo, tipoDato, ambito));
            }
            else
                errores.Add("La variable " + id.simbolo + " ya fue declarada");
            cadenapa += tipo.simbolo[0];
            if (_listaParamaetros != null) _listaParamaetros.validatipos(tab, errores);
            if (siguiente != null) siguiente.validatipos(tab, errores);
        }
    }

    public class Asignacion: Nodo
    {
        Nodo id;
        Nodo expresion;
        public Asignacion(Stack<Nodo> pila) {
            pila.Pop(); 
            pila.Pop(); 
            pila.Pop(); 
            expresion = pila.Pop();
            pila.Pop();
            pila.Pop(); 
            pila.Pop(); 
            id = new Id(pila.Pop().simbolo);
        }

        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            tipoDato = buscartipo2(tab, id.simbolo, ambito);
            char tipo1 = tipoDato;
            char tipo2;
            bool flotante = false;
            for(int i = 0; i < expresion.simbolo.Length; i++)
            {
                if (expresion.simbolo[i] == '.')
                    flotante = true;
            }
            if (!flotante)
                tipo2 = 'i';
            else
                tipo2 = 'f';
            if (expresion != null) expresion.validatipos(tab, errores);
            tipo2 = tipoDato;
            if (tipo1 != tipo2)
                errores.Add("El tipo de dato de " + id.simbolo + " en la funcion " + ambito + " es diferente de la expresion");
            if (siguiente != null) siguiente.validatipos(tab,errores);
        }
    }

    public class While : Nodo
    {
        Nodo expresion;
        Nodo bloque;
        public While(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            bloque = pila.Pop(); //Quita bloque
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita )
            pila.Pop(); //Quita estado
            expresion = pila.Pop(); //Quita expresion
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita (
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita While
        }
        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            if (bloque != null) bloque.validatipos(tab, errores);
            if (siguiente != null) siguiente.validatipos(tab, errores);
        }
    }

    public class If : Nodo
    {
        Nodo otro;
        Nodo _sentenciaBloque;
        Nodo expresion;
        public If(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            otro = pila.Pop(); //Quita otro;
            pila.Pop(); //Quita estado
            _sentenciaBloque = pila.Pop(); //Quita sentenciaBloque
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita )
            pila.Pop(); //Quita estado
            expresion = pila.Pop(); //Quita expresion
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita (
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita if
        }
        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            if (_sentenciaBloque != null) _sentenciaBloque.validatipos(tab,errores);
            if (otro != null) otro.validatipos(tab,errores);
            if (siguiente != null) siguiente.validatipos(tab, errores);
        }
    }

    public class Operacion1 : Nodo
    {
        Nodo expresion;
        public Operacion1(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita )
            pila.Pop(); //Quita estado
            expresion = pila.Pop(); //Quita expresion
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita )
        }
    }

    public class Operacion2 : Nodo
    {
        Nodo izq;
        Nodo der;
        public Operacion2(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            der = pila.Pop(); //Quita expresion derecho;
            pila.Pop(); //Quita estado
            simbolo = pila.Pop().simbolo; //Quita simbolo
            pila.Pop(); //Quita estado
            izq = pila.Pop(); //Quita expresion izquierda
        }

        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            char tipodato = tipoDato;
            izq.validatipos(tab,errores);
            char tipodato1 = tipoDato;
            der.validatipos(tab, errores);
            char tipodato2 = tipoDato;
            if (tipodato == tipodato1 && tipodato1 == tipodato2)
            {
                if (siguiente != null) siguiente.validatipos(tab, errores);
            }
            
        }
    }

    public class Return : Nodo
    {
        Nodo expresion;
        public Return(Stack<Nodo> pila)
        {
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita ;
            pila.Pop(); //Quita estado
            expresion = pila.Pop(); //Quita expresion
            pila.Pop(); //Quita estado
            pila.Pop(); //Quita return
        }
        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            if (expresion.simbolo[0] >= '0' && expresion.simbolo[0] <= '9') 
            {
                bool flotante = false;
                for (int i = 0; i < expresion.simbolo.Length; i++)
                {
                    if (expresion.simbolo[i] == '.')
                        flotante = true;
                }
                 if (!flotante)
                    tipoDato = 'i';
                else
                    tipoDato = 'f';
            }
            if (expresion.simbolo[0] == '+')
                tipoDato = 'i';
            else
                tipoDato = buscartipo2(tab, expresion.simbolo, ambito);
            char tipodato = buscartipo(tab, ambito);
            if (tipoDato != tipodato)
                errores.Add("El tipo de dato que regresa "+expresion.simbolo+" no es el mismo que el de la funcion "+ambito);

        }
    }

    public class LlamadaFunc : Nodo
    {
        Nodo id;
        Nodo argumentos;
        public LlamadaFunc(Stack<Nodo> pila)
        {
            pila.Pop();//Quita estado
            pila.Pop();//Quita )
            pila.Pop(); //Quita estado
            argumentos = pila.Pop();// Quita argumentos
            pila.Pop();//Quita estado
            pila.Pop();//Quita (
            pila.Pop(); //Quita estado
            id = new Id(pila.Pop().simbolo);
        }

        public override void validatipos(List<ElementoTabla> tab, List<string> errores)
        {
            Nodo aux = new Nodo();
            tipoDato = buscartipo(tab, id.simbolo);
            aux = argumentos;
            string cadena="";
            if (!string.IsNullOrEmpty(aux.simbolo)) {
                while (aux != null && string.Compare(aux.simbolo, "") == 1)
                {
                    if (aux.simbolo[0] >= '0' && aux.simbolo[0] <= '9')
                    {
                        bool flotante = false;
                        for (int i = 0; i < aux.simbolo.Length; i++)
                            if (aux.simbolo[i] == '.')
                                flotante = true;
                        if (!flotante)
                            cadena+= "i";
                        else
                            cadena+= "f";
                    }
                    else
                    {
                        char tipo2;
                        tipo2 = buscartipo2(tab, aux.simbolo, ambito);
                        cadena += tipo2;

                    }
                    aux = aux.siguiente;
                }
            }
            if(existFunc(tab,errores,id.simbolo,ambito,cadena))
            {
                id.validatipos(tab, errores);
            }
            if (siguiente != null)
                siguiente.validatipos(tab,errores);
            
        }

        public bool existFunc(List<ElementoTabla> tab, List<string> errores, string _id, string _ambito, string _cadenapa)
        {
            bool existe = false;
            foreach(ElementoTabla et in tab)
            {
                if(et.id == _id)
                {
                    existe = true;
                    if (et.parametro == _cadenapa)
                        return true;
                    else
                        errores.Add("Los parametros de la funcion "+_id+" son incorrectos");
                }
            }
            if (!existe)
                errores.Add("La funcion "+_id+" no existe");
            return false;
        }
    }

    public class ElementoTabla
    {
        public string id;
        public char tipo;
        public string ambito;
        public string parametro;
        public ElementoTabla(string _id, char _tipo, string _ambito, string _parametros)
        {
            id = _id;
            tipo = _tipo;
            ambito = _ambito;
            parametro = _parametros;
        }
        public ElementoTabla(string _id, char _tipo, string _ambito)
        {
            id = _id;
            tipo = _tipo;
            ambito = _ambito;
            parametro = "";
        }
    }
}
