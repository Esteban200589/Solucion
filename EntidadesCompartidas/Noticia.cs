﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public abstract class Noticia
    {
        private string codigo;
        private DateTime fecha;
        private string titulo;
        private string cuerpo;
        private int importancia;
        private List<Periodista> periodistas;
        private Usuario usuario;

        public string Codigo 
        {
            get { return codigo; }
            set {
                if (value.Length != 6)
                    throw new Exception("El codigo debe ser contener 6 digitos ");
                else
                    codigo = value;
            }
        }
        public DateTime Fecha 
        {
            get { return fecha; } 
            set {
                fecha = value;
            }
        }
        public string Titulo 
        {
            get { return titulo; }
            set {
                if (value.Length > 50)
                    throw new Exception("El titulo no puede tener mas de 50 caracteres");
                else
                    titulo = value; 
            }
        }
        public string Cuerpo 
        {
            get { return cuerpo; }
            set { 
                cuerpo = value; 
            }
        }
        public int Importancia 
        {
            get { return importancia; }
            set {
                if (value <= 1 || value >= 5)
                    throw new Exception("La Importancia debe tener entre 1 y 5 caracteres de largo");
                else
                    importancia = value; 
            }
        }
        public List<Periodista> Periodistas 
        {
            get { return periodistas; }
            set {
                if (value == null && value.Count <= 1)
                    throw new Exception("La Noticia Nacional debe contener al menos un periodista");
                else
                    periodistas = value;
            }
        }
        public Usuario Usuario 
        {
            get { return usuario; }
            set {
                if (value == null)
                    throw new Exception("La Noticia debe contener un Usuario");
                else
                    usuario = value;
            }
        }


        public virtual string TipoNoticia
        {
            get { return "No tienen"; }
        }


        public Noticia(string pCode, DateTime pFecha, string pTitle, string pBody, int pImp, 
            List<Periodista> pPer, Usuario pUser)
        {
            Codigo = pCode;
            Fecha = pFecha;
            Titulo = pTitle;
            Cuerpo = pBody;
            Importancia = pImp;
            Periodistas = pPer;
            Usuario = pUser;
        }

        
    }
}