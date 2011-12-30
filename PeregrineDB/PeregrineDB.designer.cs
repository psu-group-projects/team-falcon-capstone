﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParegrineDB
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PeregrineDB")]
	public partial class PeregrineDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertProcess(Process instance);
    partial void UpdateProcess(Process instance);
    partial void DeleteProcess(Process instance);
    partial void InsertLogRel(LogRel instance);
    partial void UpdateLogRel(LogRel instance);
    partial void DeleteLogRel(LogRel instance);
    partial void InsertJob(Job instance);
    partial void UpdateJob(Job instance);
    partial void DeleteJob(Job instance);
    partial void InsertMessage(Message instance);
    partial void UpdateMessage(Message instance);
    partial void DeleteMessage(Message instance);
    #endregion
		
		public PeregrineDBDataContext() : 
				base(global::ParegrineDB.Properties.Settings.Default.PeregrineDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PeregrineDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PeregrineDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PeregrineDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PeregrineDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Process> Processes
		{
			get
			{
				return this.GetTable<Process>();
			}
		}
		
		public System.Data.Linq.Table<LogRel> LogRels
		{
			get
			{
				return this.GetTable<LogRel>();
			}
		}
		
		public System.Data.Linq.Table<Job> Jobs
		{
			get
			{
				return this.GetTable<Job>();
			}
		}
		
		public System.Data.Linq.Table<Message> Messages
		{
			get
			{
				return this.GetTable<Message>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetTable1")]
		public ISingleResult<Process> GetTable1()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<Process>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Process")]
	public partial class Process : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ProcessID;
		
		private string _ProcessName;
		
		private int _State;
		
		private EntitySet<LogRel> _LogRels;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnProcessIDChanging(int value);
    partial void OnProcessIDChanged();
    partial void OnProcessNameChanging(string value);
    partial void OnProcessNameChanged();
    partial void OnStateChanging(int value);
    partial void OnStateChanged();
    #endregion
		
		public Process()
		{
			this._LogRels = new EntitySet<LogRel>(new Action<LogRel>(this.attach_LogRels), new Action<LogRel>(this.detach_LogRels));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProcessID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
			set
			{
				if ((this._ProcessID != value))
				{
					this.OnProcessIDChanging(value);
					this.SendPropertyChanging();
					this._ProcessID = value;
					this.SendPropertyChanged("ProcessID");
					this.OnProcessIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProcessName", DbType="NChar(15) NOT NULL", CanBeNull=false)]
		public string ProcessName
		{
			get
			{
				return this._ProcessName;
			}
			set
			{
				if ((this._ProcessName != value))
				{
					this.OnProcessNameChanging(value);
					this.SendPropertyChanging();
					this._ProcessName = value;
					this.SendPropertyChanged("ProcessName");
					this.OnProcessNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Int NOT NULL")]
		public int State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Process_LogRel", Storage="_LogRels", ThisKey="ProcessID", OtherKey="ProcessID")]
		public EntitySet<LogRel> LogRels
		{
			get
			{
				return this._LogRels;
			}
			set
			{
				this._LogRels.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LogRels(LogRel entity)
		{
			this.SendPropertyChanging();
			entity.Process = this;
		}
		
		private void detach_LogRels(LogRel entity)
		{
			this.SendPropertyChanging();
			entity.Process = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LogRel")]
	public partial class LogRel : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MessageID;
		
		private int _ProcessID;
		
		private System.Nullable<int> _JobID;
		
		private EntityRef<Process> _Process;
		
		private EntityRef<Job> _Job;
		
		private EntityRef<Message> _Message;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMessageIDChanging(int value);
    partial void OnMessageIDChanged();
    partial void OnProcessIDChanging(int value);
    partial void OnProcessIDChanged();
    partial void OnJobIDChanging(System.Nullable<int> value);
    partial void OnJobIDChanged();
    #endregion
		
		public LogRel()
		{
			this._Process = default(EntityRef<Process>);
			this._Job = default(EntityRef<Job>);
			this._Message = default(EntityRef<Message>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MessageID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MessageID
		{
			get
			{
				return this._MessageID;
			}
			set
			{
				if ((this._MessageID != value))
				{
					if (this._Message.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMessageIDChanging(value);
					this.SendPropertyChanging();
					this._MessageID = value;
					this.SendPropertyChanged("MessageID");
					this.OnMessageIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProcessID", DbType="Int NOT NULL")]
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
			set
			{
				if ((this._ProcessID != value))
				{
					if (this._Process.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnProcessIDChanging(value);
					this.SendPropertyChanging();
					this._ProcessID = value;
					this.SendPropertyChanged("ProcessID");
					this.OnProcessIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobID", DbType="Int")]
		public System.Nullable<int> JobID
		{
			get
			{
				return this._JobID;
			}
			set
			{
				if ((this._JobID != value))
				{
					if (this._Job.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnJobIDChanging(value);
					this.SendPropertyChanging();
					this._JobID = value;
					this.SendPropertyChanged("JobID");
					this.OnJobIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Process_LogRel", Storage="_Process", ThisKey="ProcessID", OtherKey="ProcessID", IsForeignKey=true)]
		public Process Process
		{
			get
			{
				return this._Process.Entity;
			}
			set
			{
				Process previousValue = this._Process.Entity;
				if (((previousValue != value) 
							|| (this._Process.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Process.Entity = null;
						previousValue.LogRels.Remove(this);
					}
					this._Process.Entity = value;
					if ((value != null))
					{
						value.LogRels.Add(this);
						this._ProcessID = value.ProcessID;
					}
					else
					{
						this._ProcessID = default(int);
					}
					this.SendPropertyChanged("Process");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Job_LogRel", Storage="_Job", ThisKey="JobID", OtherKey="JobID", IsForeignKey=true)]
		public Job Job
		{
			get
			{
				return this._Job.Entity;
			}
			set
			{
				Job previousValue = this._Job.Entity;
				if (((previousValue != value) 
							|| (this._Job.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Job.Entity = null;
						previousValue.LogRels.Remove(this);
					}
					this._Job.Entity = value;
					if ((value != null))
					{
						value.LogRels.Add(this);
						this._JobID = value.JobID;
					}
					else
					{
						this._JobID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Job");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Message_LogRel", Storage="_Message", ThisKey="MessageID", OtherKey="MessageID", IsForeignKey=true)]
		public Message Message
		{
			get
			{
				return this._Message.Entity;
			}
			set
			{
				Message previousValue = this._Message.Entity;
				if (((previousValue != value) 
							|| (this._Message.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Message.Entity = null;
						previousValue.LogRel = null;
					}
					this._Message.Entity = value;
					if ((value != null))
					{
						value.LogRel = this;
						this._MessageID = value.MessageID;
					}
					else
					{
						this._MessageID = default(int);
					}
					this.SendPropertyChanged("Message");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Job")]
	public partial class Job : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _JobID;
		
		private string _JobName;
		
		private System.Nullable<int> _PlannedCount;
		
		private System.Nullable<int> _CompletedCount;
		
		private System.Nullable<double> _PercentComplete;
		
		private EntitySet<LogRel> _LogRels;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnJobIDChanging(int value);
    partial void OnJobIDChanged();
    partial void OnJobNameChanging(string value);
    partial void OnJobNameChanged();
    partial void OnPlannedCountChanging(System.Nullable<int> value);
    partial void OnPlannedCountChanged();
    partial void OnCompletedCountChanging(System.Nullable<int> value);
    partial void OnCompletedCountChanged();
    partial void OnPercentCompleteChanging(System.Nullable<double> value);
    partial void OnPercentCompleteChanged();
    #endregion
		
		public Job()
		{
			this._LogRels = new EntitySet<LogRel>(new Action<LogRel>(this.attach_LogRels), new Action<LogRel>(this.detach_LogRels));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int JobID
		{
			get
			{
				return this._JobID;
			}
			set
			{
				if ((this._JobID != value))
				{
					this.OnJobIDChanging(value);
					this.SendPropertyChanging();
					this._JobID = value;
					this.SendPropertyChanged("JobID");
					this.OnJobIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobName", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string JobName
		{
			get
			{
				return this._JobName;
			}
			set
			{
				if ((this._JobName != value))
				{
					this.OnJobNameChanging(value);
					this.SendPropertyChanging();
					this._JobName = value;
					this.SendPropertyChanged("JobName");
					this.OnJobNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlannedCount", DbType="Int")]
		public System.Nullable<int> PlannedCount
		{
			get
			{
				return this._PlannedCount;
			}
			set
			{
				if ((this._PlannedCount != value))
				{
					this.OnPlannedCountChanging(value);
					this.SendPropertyChanging();
					this._PlannedCount = value;
					this.SendPropertyChanged("PlannedCount");
					this.OnPlannedCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompletedCount", DbType="Int")]
		public System.Nullable<int> CompletedCount
		{
			get
			{
				return this._CompletedCount;
			}
			set
			{
				if ((this._CompletedCount != value))
				{
					this.OnCompletedCountChanging(value);
					this.SendPropertyChanging();
					this._CompletedCount = value;
					this.SendPropertyChanged("CompletedCount");
					this.OnCompletedCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PercentComplete", DbType="Float")]
		public System.Nullable<double> PercentComplete
		{
			get
			{
				return this._PercentComplete;
			}
			set
			{
				if ((this._PercentComplete != value))
				{
					this.OnPercentCompleteChanging(value);
					this.SendPropertyChanging();
					this._PercentComplete = value;
					this.SendPropertyChanged("PercentComplete");
					this.OnPercentCompleteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Job_LogRel", Storage="_LogRels", ThisKey="JobID", OtherKey="JobID")]
		public EntitySet<LogRel> LogRels
		{
			get
			{
				return this._LogRels;
			}
			set
			{
				this._LogRels.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LogRels(LogRel entity)
		{
			this.SendPropertyChanging();
			entity.Job = this;
		}
		
		private void detach_LogRels(LogRel entity)
		{
			this.SendPropertyChanging();
			entity.Job = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Messages")]
	public partial class Message : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MessageID;
		
		private string _Message1;
		
		private System.DateTime _Date;
		
		private int _Category;
		
		private int _Prority;
		
		private EntityRef<LogRel> _LogRel;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMessageIDChanging(int value);
    partial void OnMessageIDChanged();
    partial void OnMessage1Changing(string value);
    partial void OnMessage1Changed();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnCategoryChanging(int value);
    partial void OnCategoryChanged();
    partial void OnProrityChanging(int value);
    partial void OnProrityChanged();
    #endregion
		
		public Message()
		{
			this._LogRel = default(EntityRef<LogRel>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MessageID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MessageID
		{
			get
			{
				return this._MessageID;
			}
			set
			{
				if ((this._MessageID != value))
				{
					this.OnMessageIDChanging(value);
					this.SendPropertyChanging();
					this._MessageID = value;
					this.SendPropertyChanged("MessageID");
					this.OnMessageIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Message", Storage="_Message1", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Message1
		{
			get
			{
				return this._Message1;
			}
			set
			{
				if ((this._Message1 != value))
				{
					this.OnMessage1Changing(value);
					this.SendPropertyChanging();
					this._Message1 = value;
					this.SendPropertyChanged("Message1");
					this.OnMessage1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category", DbType="Int NOT NULL")]
		public int Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					this.OnCategoryChanging(value);
					this.SendPropertyChanging();
					this._Category = value;
					this.SendPropertyChanged("Category");
					this.OnCategoryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prority", DbType="Int NOT NULL")]
		public int Prority
		{
			get
			{
				return this._Prority;
			}
			set
			{
				if ((this._Prority != value))
				{
					this.OnProrityChanging(value);
					this.SendPropertyChanging();
					this._Prority = value;
					this.SendPropertyChanged("Prority");
					this.OnProrityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Message_LogRel", Storage="_LogRel", ThisKey="MessageID", OtherKey="MessageID", IsUnique=true, IsForeignKey=false)]
		public LogRel LogRel
		{
			get
			{
				return this._LogRel.Entity;
			}
			set
			{
				LogRel previousValue = this._LogRel.Entity;
				if (((previousValue != value) 
							|| (this._LogRel.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._LogRel.Entity = null;
						previousValue.Message = null;
					}
					this._LogRel.Entity = value;
					if ((value != null))
					{
						value.Message = this;
					}
					this.SendPropertyChanged("LogRel");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591