﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Boot_Park.View.Public.test.test" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TEST</title>
    <script src="../../../Content/js/socket.io.js"></script>
    <script type="text/javascript">
        var io = io.connect('http://localhost:2016');
    </script>
</head>
<body>

    <script type="text/javascript">
        var socket= new WebSocket('ws://127.0.0.1:2012');
        var card = 0;
        var user = 0;
        var i = 0;

        socket.onmessage = function (evt) {
            if (i == 0) {
                eval(evt.data);
                console.log(card);
                i++;
            } else {
                i = 0;
                var spli = evt.data.split(',');
                eval(spli[1])
                console.log(user);

                TEST.ValidaAspirante(user,
                  {
                      success: function (response) {
                          debugger;
                          if (response) // SI ES VERDADERO VALIDE EL TAG
                          {
                              var tag = TEST.DetectarTag(
                              {
                                  success: function (data) // TAG DETECTADO
                                  {
                                      if (data != '') {
                                          Ext.net.Notification.show({
                                              html: 'BIEN HECHO! El tag es:' + data,
                                              title: 'Notificación'
                                          });
                                          TEST.ValidarVehiculo(user, data,
                                          {
                                              success: function (resp) //LO QUE RESPONDE AL VALIDAR EL TAG
                                              {
                                                  if (resp) // SI ES VERDADERO
                                                  {
                                                      TEST.CargarAspirante(user);// CARGA EL ASPIRANTE
                                                      TEST.CargarVehiculo(data); // CARGA EL VEHICULO
                                                      TEST.RegistrarCirculacion(user, data); // REGISTRA LA CIRCULACION
                                                      io.emit('click');
                                                      //TEST.AbrirPuerta(); // ABRE LA PUERTA
                                                  } else {
                                                      alert('VEHICULO NO AUTORIZADO');
                                                  }
                                              }
                                          });
                                      }
                                  }
                              });

                          } else // COMO NO ES VERDADERO: PEDOR AL USUARIO QUE SE RETIRE
                          {
                              alert('El usuario no existe en nuestra base de datos.');
                          }
                      }
                  });
            }
        };

    </script>

    <ext:ResourceManager runat="server" />
    <form id="test" runat="server">
             
        <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="PUSUARIO" runat="server" Region="North" Title="Validación" Height="300" Icon="User" Padding="1" Layout="Column">
                    <Items>
                        <ext:Panel runat="server" ID="FOTO" ColumnWidth=".3">
                            <Items>
                                <ext:AbsoluteLayout ID="AbsoluteLayout1" runat="server">
                                    <Anchors>
                                        <ext:Anchor>
                                            <ext:Image runat="server" ImageUrl="../../../Content/images/user.png" Width="164px" Height="164px" X="110" Y="50" />
                                        </ext:Anchor>
                                    </Anchors>
                                </ext:AbsoluteLayout>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" ID="PDATOS" ColumnWidth=".7" Padding="2">
                            <Items>
                                <ext:GridPanel runat="server" ID="GPDATOS" AutoExpandColumn="NOMBRE" Height="200">
                                    <Store>
                                        <ext:Store ID="SDATOS" runat="server">
                                            <Reader>
                                                <ext:JsonReader>
                                                    <Fields>
                                                        <ext:RecordField Name="IDENTIFICACION" />
                                                        <ext:RecordField Name="NOMBRE" />
                                                         <ext:RecordField Name="APELLIDO" />
                                                        <ext:RecordField Name="TIPOUSUARIO" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn />
                                            <ext:Column ColumnID="CIDENTIFICACION" DataIndex="IDENTIFICACION" Header="CODIGO DEL USUARIO" Width="200" />
                                            <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" />
                                            <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" />
                                            <ext:Column ColumnID="CTIPOUSUARIO" DataIndex="TIPOUSUARIO" Header="Tipo de Usuario" />
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="PVEHICULO" runat="server" Layout="Fit" Region="Center" Height="300" Icon="Car" Padding="2">
                    <Items>
                        <ext:GridPanel runat="server" ID="GPVEHICULO" AutoExpandColumn="OBSERVACION">
                            <Store>
                                <ext:Store ID="SVEHICULOS" runat="server">
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="VEHICULO" />
                                                <ext:RecordField Name="PLACA" />
                                                <ext:RecordField Name="MODELO" />
                                                <ext:RecordField Name="OBSERVACION" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:RowNumbererColumn />
                                    <%--<ext:Column ColumnID="CVEHICULO" DataIndex="VEHICULO" Header="Vehiculo" />--%>
                                    <ext:Column ColumnID="CPLACA" DataIndex="PLACA" Header="Placa del Vehiculo" />
                                    <ext:Column ColumnID="CMODELO" DataIndex="MODELO" Header="Placa del Vehiculo" />
                                    <ext:Column ColumnID="COBSERVACION" DataIndex="OBSERVACION" Header="Observaciones" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
                     
                   
        <ext:Window runat="server" ID="AUTORIZA" Hidden="true" Height="300" Width="300" Modal="true">
            <Items>
                <ext:Label runat="server" ID="VALIDACION" Text="ACA VA SI SE ACEPTA O RECHAZA" />
            </Items>
        </ext:Window>
    </form>
</body>
</html>
