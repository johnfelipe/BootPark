﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Boot_Park.Model.BootPark;
using Boot_Park.Model;

namespace Boot_Park.Controller.BootPark
{
    public class ParametrizacionCOD
    {
        private ParticularOAD particular = new ParticularOAD();
        private VehiculoOAD vehiculo = new VehiculoOAD();
        private EtiquetaOAD etiquetas = new EtiquetaOAD();

        private UsuarioOAD usuario = new UsuarioOAD();
        private EtiquetaUsuarioOAD etiquetausuario = new EtiquetaUsuarioOAD();

        private General general = new General();

        #region GESTION DE PATICULARES
        public DataTable consultarParticulares() {
                return particular.consultarParticulares();
            }

            public bool registrarParticulares() {
                return particular.registrarParticulares();
            }

            public bool registrarParticular(string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.registrarParticular(general.nextPrimaryKey("BOOTPARK.PARTICULAR","PART_ID"), identificacion, nombre, apellido, registradoPor);
            }

            public bool actualizarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor) {
                return particular.actualizarParticular(id, identificacion, nombre, apellido, registradoPor);
            }

            public bool eliminalParticular(string id) {
                return particular.eliminarParticular(id);
            }
        #endregion

        #region GESTION DE VEHICULOS
        public DataTable consultarVehiculos()
        {
            return vehiculo.consultarVehiculos();
        }

        public bool registrarVehiculos()
        {
            return vehiculo.registrarVehiculos();
        }

        public bool registrarVehiculo(string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            return vehiculo.registrarVehiculo(general.nextPrimaryKey("BOOTPARK.VEHICULO", "VEHI_ID"), observacion, placa, modelo, marca, color, registradoPor);
        }

        public bool actualizarVehiculo(string id, string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            return vehiculo.actualizarVehiculo(id, observacion, placa, modelo, marca, color, registradoPor);
        }

        public bool eliminalVehiculo(string id)
        {
            return vehiculo.eliminarVehiculo(id);
        }
        #endregion

        #region GESTION DE ETIQUETAS
            public DataTable consultarEtiquetas()
            {
                return etiquetas.consultarEtiquetas();
            }

            public DataTable consultarEtiquetas(string estado)
            {
                return etiquetas.consultarEtiquetas(estado);
            }

        public DataTable consultarEtiquetasSinAsignar(string tipo, string usuario) {
            return etiquetausuario.consultarEtiquetasDisponibles(tipo, usuario);
        }

        public bool registrarEtiquetas()
            {
                return etiquetas.registrarEtoquetas();
            }

            public bool registrarEtiqueta(string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
            {
                return etiquetas.registrarEtiqueta(general.nextPrimaryKey("BOOTPARK.ETIQUETA","ETIQ_ID"), tipo, etiqueta, descripcion, observacion, estado, registradoPor);
            }

            public bool actualizarEtiqueta(string id, string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
            {
                return etiquetas.actualizarEtiqueta(id, tipo, etiqueta, descripcion, observacion, estado, registradoPor);
            }

            public bool eliminalEtiqueta(string id, string tipo)
            {
                return etiquetas.eliminarEtiqueta(id, tipo);
            }
        #endregion

        #region ETIQUETAUSUARIO

            public DataTable consultarEtiquetaUsuario() {
                return null;
            }

            public bool registrarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor) {
                return etiquetausuario.registrarEtiquetaUsuario(id, tipo, usuario, motivo, caducidad, registradoPor);
            }

            public bool actualizarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor) {
                return etiquetausuario.actualizarEtiquetaUsuario(id, tipo, usuario, motivo, caducidad, registradoPor);
            }

            public bool eliminarEtiquetaUsuario(string id, string tipo, string usuario)
            {
                return etiquetausuario.eliminarEtiquetaUsuario(id, tipo, usuario);
            }

        #endregion

        #region USUARIO
            public DataTable consultarUsuarios() {
                return usuario.consultarUsuarios();
            }
        #endregion
    }
}