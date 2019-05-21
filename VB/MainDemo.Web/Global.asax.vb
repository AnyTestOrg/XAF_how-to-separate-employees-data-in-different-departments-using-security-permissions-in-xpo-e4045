﻿Imports System
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Web
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Web

Namespace MainDemo.Web
    Public Class [Global]
        Inherits System.Web.HttpApplication

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            AddHandler ASPxWebControl.CallbackError, New EventHandler(AddressOf Application_Error)
            WebApplication.DefaultPage = "Default.aspx"
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = True
        End Sub

        Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            WebApplication.SetInstance(Session, New MainDemoAspNetApplication())

            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If

            DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.Register()
            WebApplication.Instance.ConnectionString = DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.ConnectionString
            WebApplication.Instance.Setup()
            WebApplication.Instance.Start()
        End Sub

        Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            Dim filePath As String = HttpContext.Current.Request.PhysicalPath

            If Not String.IsNullOrEmpty(filePath) AndAlso (filePath.IndexOf("Images") >= 0) AndAlso Not System.IO.File.Exists(filePath) Then
                HttpContext.Current.Response.[End]()
            End If
        End Sub

        Protected Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            ErrorHandling.Instance.ProcessApplicationError()
        End Sub

        Protected Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
            WebApplication.LogOff(Session)
            WebApplication.DisposeInstance(Session)
        End Sub

        Protected Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Private Sub InitializeComponent()
        End Sub
    End Class
End Namespace
