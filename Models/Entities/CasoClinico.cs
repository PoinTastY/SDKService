﻿using System;

namespace StareMedic.Models.Entities
{
    public class CasoClinico
    {
        private string _id;

        private readonly uint _idDB;

        private string _name;

        private uint _idPacient;

        private uint _idDoctor;

        private uint _idHabitacion;

        private uint _idDiagnostico;

        private bool _status;

        private DateTime _fechaIngreso;

        private DateTime _fechaAlta;

        public CasoClinico(uint id)
        {
            _idDB = id;
            _fechaIngreso = DateTime.Now.ToUniversalTime();
            _status = true;
            _idDoctor = 0;
            _idHabitacion = 0;
            _idPacient = 0;
        }

        public CasoClinico() { }
        //Total account?
        //TO-DO: mplement a static method to assign all the values to the object?

        public uint IdDB
        {
            get => _idDB;
        }

        public string Id
        {
            //the set, will be from a special format method, provided by the repo on build time
            get => _id;
            set => _id = value;
        }

        public string Nombre
        {
            get => _name;
            set => _name = value;
        }

        public uint IdPaciente
        {
            get => _idPacient;
            set => _idPacient = value;
        }

        public uint IdDoctor
        {
            get => _idDoctor;
            set => _idDoctor = value;
        }

        public uint IdHabitacion
        {
            get => _idHabitacion;
            set => _idHabitacion = value;
        }

        public uint IdDiagnostico//thisone will only be setted through the case overview, nowhere else
        {
            get => _idDiagnostico;
            set => _idDiagnostico = value;
        }

        public DateTime FechaIngreso()
        {
            return _fechaIngreso.ToUniversalTime();
        }

        public void FechaAltaSet(DateTime x)
        {
            _fechaAlta = x.ToUniversalTime();
        }

        public DateTime FechaAlta()//change this to traditional method, split get and set individually
        {
            return _fechaAlta;
        }

        public bool Activo
        {
            get => _status;
            set => _status = value;
        }

        public void Update(CasoClinico caso)
        {
            _name = caso.Nombre;
            _idDoctor = caso.IdDoctor;
            _idHabitacion = caso.IdHabitacion;
            _fechaAlta = caso.FechaAlta();
        }

        public static implicit operator bool(CasoClinico x)
        {
            return (x._name != null || x._name != "") && x._idDoctor != 0 && x._idHabitacion != 0 && x._idPacient != 0;
        }

        public DateTime FechaAltaDate { get { return _fechaAlta.Date; } set { _fechaAlta = value.ToUniversalTime(); } }
        public TimeSpan FechaAltaHour { get { return _fechaAlta.TimeOfDay; } set { _fechaAlta = DateTime.Today.Add(value).ToUniversalTime(); } }
        public DateTime FechaIngresoDate { get { return _fechaIngreso.Date; } set { _fechaIngreso = value.ToUniversalTime(); } }
        public TimeSpan FechaIngresoHour { get { return _fechaIngreso.TimeOfDay; } set { _fechaIngreso = DateTime.Today.Add(value).ToUniversalTime(); } }

        //maybe add close case method in here instead of repo?
    }
}