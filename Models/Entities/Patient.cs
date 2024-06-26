﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Patient
    {
        //Atributos paciente
        private uint _id;//dbfilled only on creation
        private string _name;
        private string _domicilio;
        private string _tipoSangre;
        private string _ciudad;
        private string _estado;
        private string _nacionalidad;
        private string _estadoCivil;
        private char _sexo;
        private int _edad;
        private string _telefono;

        //controll info
        private DateTime _registered;

        private bool _active;

        //Contactos extras
        private uint? _idCercano;
        private uint? _idFiador;//Contpaq SDK comming soon here?

        //id initialization
        public Patient(uint id)
        {
            _registered = DateTime.Now.ToUniversalTime();
            _id = id;
            _idCercano = null;
            _idFiador = null;
            //initiate w default values for notnull compatibility
            _name = "";
            _domicilio = "";
            _tipoSangre = "";
            _ciudad = "";
            _estado = "";
            _nacionalidad = "";
            _estadoCivil = "";
            _sexo = 'N';
            _edad = 0;
            _telefono = "";
        }

        public Patient()
        {
        }

        //methods
        public uint Id
        {
            get => _id;
            set => _id = value;
        }
        public string Nombre
        {
            get => _name;
            set => _name = value;
        }
        public string Domicilio
        {
            get => _domicilio;
            set => _domicilio = value;
        }

        public string TipoSangre
        {
            get => _tipoSangre;
            set => _tipoSangre = value;
        }

        public string Ciudad
        {
            get => _ciudad;
            set => _ciudad = value;
        }

        public string Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public string Nacionalidad
        {
            get => _nacionalidad;
            set => _nacionalidad = value;
        }

        public string EstadoCivil
        {
            get => _estadoCivil;
            set => _estadoCivil = value;
        }

        public char Sexo
        {
            get => _sexo;
            set => _sexo = value;
        }

        public int Edad
        {
            get => _edad;
            set => _edad = value;
        }

        public string Telefono
        {
            get => _telefono;
            set => _telefono = value;
        }

        public DateTime Registered
        {
            get => _registered.ToUniversalTime();
        }

        public bool Status
        {
            get => _active;
            set => _active = value;
        }

        //dejo estos dos como la otra forma de declararse tambien
        public uint? IdCercano
        {
            get { return _idCercano; }
            set { _idCercano = value; }
        }

        public uint? IdFiador
        {
            get { return _idFiador; }
            set { _idFiador = value; }
        }

        public static implicit operator bool(Patient patient)
        {
            return patient._name != null && patient._name != "";
        }

        public void Update(Patient patient)
        {
            //Update changes
            _name = patient._name;
            _domicilio = patient._domicilio;
            _tipoSangre = patient._tipoSangre;
            _ciudad = patient._ciudad;
            _estado = patient._estado;
            _nacionalidad = patient._nacionalidad;
            _estadoCivil = patient._estadoCivil;
            _sexo = patient._sexo;
            _edad = patient._edad;
            _telefono = patient._telefono;
            _registered = patient._registered;
            _active = patient._active;
            _idCercano = patient._idCercano;
            _idFiador = patient._idFiador;

        }

        public DateTime RegisteredDate { get => _registered.Date; set => _registered = value.ToUniversalTime(); }
        public TimeSpan RegisteredHour { get  { return _registered.TimeOfDay; } set => _registered = DateTime.Today.Add(value).ToUniversalTime();}

    }
}
