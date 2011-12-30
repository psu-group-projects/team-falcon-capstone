﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PeregrineDBTesting.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PeregrineDBTesting.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  StoredProcedure [dbo].[ShowProcesses]    Script Date: 12/28/2011 16:36:10 ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///CREATE PROCEDURE [dbo].[ShowProcesses]
        ///AS
        ///    SET NOCOUNT ON;
        ///SELECT ProcessID, ProcessName, State from dbo.Process
        ///
        ///
        ///GO.
        /// </summary>
        internal static string CreateSP_ShowProcesses_sql {
            get {
                return ResourceManager.GetString("CreateSP_ShowProcesses_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Job]    Script Date: 12/28/2011 16:15:44 ******/
        ///IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[Job]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[Job]
        ///GO
        ///
        ///USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Job]    Script Date: 12/28/2011 16:15:44 ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///CREATE TABLE [dbo].[Job](
        ///	[JobID] [int] NOT NULL,
        ///	[JobName] [nchar](10) NOT NULL,
        ///	[PlannedCount] [int] NUL [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreateJobTable_sql {
            get {
                return ResourceManager.GetString("DropCreateJobTable_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ///IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_LogRel_Job]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[LogRel]&apos;))
        ///ALTER TABLE [dbo].[LogRel] DROP CONSTRAINT [FK_LogRel_Job]
        ///GO
        ///
        ///IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_LogRel_Message]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[LogRel]&apos;))
        ///ALTER TABLE [dbo].[LogRel] DROP CONSTRAINT [FK_LogRel_Message]
        ///GO
        ///
        ///IF  EXISTS (SELECT * FROM sys.foreign_keys WH [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreateLogRelTable_sql {
            get {
                return ResourceManager.GetString("DropCreateLogRelTable_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Messages]    Script Date: 12/28/2011 16:17:24 ******/
        ///IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[Messages]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[Messages]
        ///GO
        ///
        ///USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Messages]    Script Date: 12/28/2011 16:17:24 ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///CREATE TABLE [dbo].[Messages](
        ///	[MessageID] [int] NOT NULL,
        ///	[Message] [nvarchar](50) NOT  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreateMessagesTable_sql {
            get {
                return ResourceManager.GetString("DropCreateMessagesTable_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [master]
        ///GO
        ///
        ////****** Object:  Database [PeregrineDB]    Script Date: 12/28/2011 16:18:08 ******/
        ///IF  EXISTS (SELECT name FROM sys.databases WHERE name = N&apos;PeregrineDB&apos;)
        ///DROP DATABASE [PeregrineDB]
        ///GO
        ///
        ///USE [master]
        ///GO
        ///
        ////****** Object:  Database [PeregrineDB]    Script Date: 12/28/2011 16:18:08 ******/
        ///CREATE DATABASE [PeregrineDB] ON  PRIMARY 
        ///( NAME = N&apos;PeregrineDB&apos;, FILENAME = N&apos;C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\PeregrineDB.mdf&apos; , SIZE = 2048KB , MAXSIZE  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreatePeregrineDB_sql {
            get {
                return ResourceManager.GetString("DropCreatePeregrineDB_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Process]    Script Date: 12/28/2011 16:18:32 ******/
        ///IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[Process]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[Process]
        ///GO
        ///
        ///USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[Process]    Script Date: 12/28/2011 16:18:32 ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///CREATE TABLE [dbo].[Process](
        ///	[ProcessID] [int] NOT NULL,
        ///	[ProcessName] [nchar](15) NOT NULL [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreateProcessTable_sql {
            get {
                return ResourceManager.GetString("DropCreateProcessTable_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[sysdiagrams]    Script Date: 12/28/2011 16:12:12 ******/
        ///IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[sysdiagrams]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[sysdiagrams]
        ///GO
        ///
        ///USE [PeregrineDB]
        ///GO
        ///
        ////****** Object:  Table [dbo].[sysdiagrams]    Script Date: 12/28/2011 16:12:12 ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///SET ANSI_PADDING ON
        ///GO
        ///
        ///CREATE TABLE [dbo].[sysdiagrams](
        ///	[name] [sysname]  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropCreateSysdiagramsTable_sql {
            get {
                return ResourceManager.GetString("DropCreateSysdiagramsTable_sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PeregrineDB.
        /// </summary>
        internal static string OldDBName {
            get {
                return ResourceManager.GetString("OldDBName", resourceCulture);
            }
        }
    }
}