﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardGameConsoleTestApp.DBML
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="CardGame")]
	public partial class CardDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCardName(CardName instance);
    partial void UpdateCardName(CardName instance);
    partial void DeleteCardName(CardName instance);
    partial void InsertCard(Card instance);
    partial void UpdateCard(Card instance);
    partial void DeleteCard(Card instance);
    partial void InsertCardTrigger(CardTrigger instance);
    partial void UpdateCardTrigger(CardTrigger instance);
    partial void DeleteCardTrigger(CardTrigger instance);
    partial void InsertCardTriggerParam(CardTriggerParam instance);
    partial void UpdateCardTriggerParam(CardTriggerParam instance);
    partial void DeleteCardTriggerParam(CardTriggerParam instance);
    partial void InsertMethod(Method instance);
    partial void UpdateMethod(Method instance);
    partial void DeleteMethod(Method instance);
    #endregion
		
		public CardDataContext() : 
				base(global::CardGameConsoleTestApp.Properties.Settings.Default.CardGameConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CardDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CardDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CardDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CardDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CardName> CardNames
		{
			get
			{
				return this.GetTable<CardName>();
			}
		}
		
		public System.Data.Linq.Table<Card> Cards
		{
			get
			{
				return this.GetTable<Card>();
			}
		}
		
		public System.Data.Linq.Table<CardTrigger> CardTriggers
		{
			get
			{
				return this.GetTable<CardTrigger>();
			}
		}
		
		public System.Data.Linq.Table<CardTriggerParam> CardTriggerParams
		{
			get
			{
				return this.GetTable<CardTriggerParam>();
			}
		}
		
		public System.Data.Linq.Table<Method> Methods
		{
			get
			{
				return this.GetTable<Method>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardName")]
	public partial class CardName : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _CardId;
		
		private int _LanguageId;
		
		private string _Name;
		
		private EntityRef<Card> _Card;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCardIdChanging(int value);
    partial void OnCardIdChanged();
    partial void OnLanguageIdChanging(int value);
    partial void OnLanguageIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public CardName()
		{
			this._Card = default(EntityRef<Card>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardId", DbType="Int NOT NULL")]
		public int CardId
		{
			get
			{
				return this._CardId;
			}
			set
			{
				if ((this._CardId != value))
				{
					if (this._Card.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardIdChanging(value);
					this.SendPropertyChanging();
					this._CardId = value;
					this.SendPropertyChanged("CardId");
					this.OnCardIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LanguageId", DbType="Int NOT NULL")]
		public int LanguageId
		{
			get
			{
				return this._LanguageId;
			}
			set
			{
				if ((this._LanguageId != value))
				{
					this.OnLanguageIdChanging(value);
					this.SendPropertyChanging();
					this._LanguageId = value;
					this.SendPropertyChanged("LanguageId");
					this.OnLanguageIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardName", Storage="_Card", ThisKey="CardId", OtherKey="Id", IsForeignKey=true)]
		public Card Card
		{
			get
			{
				return this._Card.Entity;
			}
			set
			{
				Card previousValue = this._Card.Entity;
				if (((previousValue != value) 
							|| (this._Card.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Card.Entity = null;
						previousValue.CardNames.Remove(this);
					}
					this._Card.Entity = value;
					if ((value != null))
					{
						value.CardNames.Add(this);
						this._CardId = value.Id;
					}
					else
					{
						this._CardId = default(int);
					}
					this.SendPropertyChanged("Card");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Card")]
	public partial class Card : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _Cost;
		
		private int _Type;
		
		private int _Attack;
		
		private int _Health;
		
		private string _ImageUrl;
		
		private EntitySet<CardName> _CardNames;
		
		private EntitySet<CardTrigger> _CardTriggers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCostChanging(int value);
    partial void OnCostChanged();
    partial void OnTypeChanging(int value);
    partial void OnTypeChanged();
    partial void OnAttackChanging(int value);
    partial void OnAttackChanged();
    partial void OnHealthChanging(int value);
    partial void OnHealthChanged();
    partial void OnImageUrlChanging(string value);
    partial void OnImageUrlChanged();
    #endregion
		
		public Card()
		{
			this._CardNames = new EntitySet<CardName>(new Action<CardName>(this.attach_CardNames), new Action<CardName>(this.detach_CardNames));
			this._CardTriggers = new EntitySet<CardTrigger>(new Action<CardTrigger>(this.attach_CardTriggers), new Action<CardTrigger>(this.detach_CardTriggers));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cost", DbType="Int NOT NULL")]
		public int Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				if ((this._Cost != value))
				{
					this.OnCostChanging(value);
					this.SendPropertyChanging();
					this._Cost = value;
					this.SendPropertyChanged("Cost");
					this.OnCostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int NOT NULL")]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Attack", DbType="Int NOT NULL")]
		public int Attack
		{
			get
			{
				return this._Attack;
			}
			set
			{
				if ((this._Attack != value))
				{
					this.OnAttackChanging(value);
					this.SendPropertyChanging();
					this._Attack = value;
					this.SendPropertyChanged("Attack");
					this.OnAttackChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Health", DbType="Int NOT NULL")]
		public int Health
		{
			get
			{
				return this._Health;
			}
			set
			{
				if ((this._Health != value))
				{
					this.OnHealthChanging(value);
					this.SendPropertyChanging();
					this._Health = value;
					this.SendPropertyChanged("Health");
					this.OnHealthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImageUrl", DbType="VarChar(255)")]
		public string ImageUrl
		{
			get
			{
				return this._ImageUrl;
			}
			set
			{
				if ((this._ImageUrl != value))
				{
					this.OnImageUrlChanging(value);
					this.SendPropertyChanging();
					this._ImageUrl = value;
					this.SendPropertyChanged("ImageUrl");
					this.OnImageUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardName", Storage="_CardNames", ThisKey="Id", OtherKey="CardId")]
		public EntitySet<CardName> CardNames
		{
			get
			{
				return this._CardNames;
			}
			set
			{
				this._CardNames.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardTrigger", Storage="_CardTriggers", ThisKey="Id", OtherKey="CardId")]
		public EntitySet<CardTrigger> CardTriggers
		{
			get
			{
				return this._CardTriggers;
			}
			set
			{
				this._CardTriggers.Assign(value);
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
		
		private void attach_CardNames(CardName entity)
		{
			this.SendPropertyChanging();
			entity.Card = this;
		}
		
		private void detach_CardNames(CardName entity)
		{
			this.SendPropertyChanging();
			entity.Card = null;
		}
		
		private void attach_CardTriggers(CardTrigger entity)
		{
			this.SendPropertyChanging();
			entity.Card = this;
		}
		
		private void detach_CardTriggers(CardTrigger entity)
		{
			this.SendPropertyChanging();
			entity.Card = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardTrigger")]
	public partial class CardTrigger : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _CardId;
		
		private int _TriggerType;
		
		private int _MethodId;
		
		private EntitySet<CardTriggerParam> _CardTriggerParams;
		
		private EntityRef<Card> _Card;
		
		private EntityRef<Method> _Method;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCardIdChanging(int value);
    partial void OnCardIdChanged();
    partial void OnTriggerTypeChanging(int value);
    partial void OnTriggerTypeChanged();
    partial void OnMethodIdChanging(int value);
    partial void OnMethodIdChanged();
    #endregion
		
		public CardTrigger()
		{
			this._CardTriggerParams = new EntitySet<CardTriggerParam>(new Action<CardTriggerParam>(this.attach_CardTriggerParams), new Action<CardTriggerParam>(this.detach_CardTriggerParams));
			this._Card = default(EntityRef<Card>);
			this._Method = default(EntityRef<Method>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardId", DbType="Int NOT NULL")]
		public int CardId
		{
			get
			{
				return this._CardId;
			}
			set
			{
				if ((this._CardId != value))
				{
					if (this._Card.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardIdChanging(value);
					this.SendPropertyChanging();
					this._CardId = value;
					this.SendPropertyChanged("CardId");
					this.OnCardIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TriggerType", DbType="Int NOT NULL")]
		public int TriggerType
		{
			get
			{
				return this._TriggerType;
			}
			set
			{
				if ((this._TriggerType != value))
				{
					this.OnTriggerTypeChanging(value);
					this.SendPropertyChanging();
					this._TriggerType = value;
					this.SendPropertyChanged("TriggerType");
					this.OnTriggerTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MethodId", DbType="Int NOT NULL")]
		public int MethodId
		{
			get
			{
				return this._MethodId;
			}
			set
			{
				if ((this._MethodId != value))
				{
					if (this._Method.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMethodIdChanging(value);
					this.SendPropertyChanging();
					this._MethodId = value;
					this.SendPropertyChanged("MethodId");
					this.OnMethodIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CardTrigger_CardTriggerParam", Storage="_CardTriggerParams", ThisKey="Id", OtherKey="CardTriggerId")]
		public EntitySet<CardTriggerParam> CardTriggerParams
		{
			get
			{
				return this._CardTriggerParams;
			}
			set
			{
				this._CardTriggerParams.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardTrigger", Storage="_Card", ThisKey="CardId", OtherKey="Id", IsForeignKey=true)]
		public Card Card
		{
			get
			{
				return this._Card.Entity;
			}
			set
			{
				Card previousValue = this._Card.Entity;
				if (((previousValue != value) 
							|| (this._Card.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Card.Entity = null;
						previousValue.CardTriggers.Remove(this);
					}
					this._Card.Entity = value;
					if ((value != null))
					{
						value.CardTriggers.Add(this);
						this._CardId = value.Id;
					}
					else
					{
						this._CardId = default(int);
					}
					this.SendPropertyChanged("Card");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Method_CardTrigger", Storage="_Method", ThisKey="MethodId", OtherKey="Id", IsForeignKey=true)]
		public Method Method
		{
			get
			{
				return this._Method.Entity;
			}
			set
			{
				Method previousValue = this._Method.Entity;
				if (((previousValue != value) 
							|| (this._Method.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Method.Entity = null;
						previousValue.CardTriggers.Remove(this);
					}
					this._Method.Entity = value;
					if ((value != null))
					{
						value.CardTriggers.Add(this);
						this._MethodId = value.Id;
					}
					else
					{
						this._MethodId = default(int);
					}
					this.SendPropertyChanged("Method");
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
		
		private void attach_CardTriggerParams(CardTriggerParam entity)
		{
			this.SendPropertyChanging();
			entity.CardTrigger = this;
		}
		
		private void detach_CardTriggerParams(CardTriggerParam entity)
		{
			this.SendPropertyChanging();
			entity.CardTrigger = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardTriggerParam")]
	public partial class CardTriggerParam : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _CardTriggerId;
		
		private string _ParamName;
		
		private int _ParamValue;
		
		private EntityRef<CardTrigger> _CardTrigger;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCardTriggerIdChanging(int value);
    partial void OnCardTriggerIdChanged();
    partial void OnParamNameChanging(string value);
    partial void OnParamNameChanged();
    partial void OnParamValueChanging(int value);
    partial void OnParamValueChanged();
    #endregion
		
		public CardTriggerParam()
		{
			this._CardTrigger = default(EntityRef<CardTrigger>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardTriggerId", DbType="Int NOT NULL")]
		public int CardTriggerId
		{
			get
			{
				return this._CardTriggerId;
			}
			set
			{
				if ((this._CardTriggerId != value))
				{
					if (this._CardTrigger.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardTriggerIdChanging(value);
					this.SendPropertyChanging();
					this._CardTriggerId = value;
					this.SendPropertyChanged("CardTriggerId");
					this.OnCardTriggerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParamName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string ParamName
		{
			get
			{
				return this._ParamName;
			}
			set
			{
				if ((this._ParamName != value))
				{
					this.OnParamNameChanging(value);
					this.SendPropertyChanging();
					this._ParamName = value;
					this.SendPropertyChanged("ParamName");
					this.OnParamNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParamValue", DbType="Int NOT NULL")]
		public int ParamValue
		{
			get
			{
				return this._ParamValue;
			}
			set
			{
				if ((this._ParamValue != value))
				{
					this.OnParamValueChanging(value);
					this.SendPropertyChanging();
					this._ParamValue = value;
					this.SendPropertyChanged("ParamValue");
					this.OnParamValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CardTrigger_CardTriggerParam", Storage="_CardTrigger", ThisKey="CardTriggerId", OtherKey="Id", IsForeignKey=true)]
		public CardTrigger CardTrigger
		{
			get
			{
				return this._CardTrigger.Entity;
			}
			set
			{
				CardTrigger previousValue = this._CardTrigger.Entity;
				if (((previousValue != value) 
							|| (this._CardTrigger.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CardTrigger.Entity = null;
						previousValue.CardTriggerParams.Remove(this);
					}
					this._CardTrigger.Entity = value;
					if ((value != null))
					{
						value.CardTriggerParams.Add(this);
						this._CardTriggerId = value.Id;
					}
					else
					{
						this._CardTriggerId = default(int);
					}
					this.SendPropertyChanged("CardTrigger");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Method")]
	public partial class Method : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Class;
		
		private string _Name;
		
		private EntitySet<CardTrigger> _CardTriggers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnClassChanging(string value);
    partial void OnClassChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Method()
		{
			this._CardTriggers = new EntitySet<CardTrigger>(new Action<CardTrigger>(this.attach_CardTriggers), new Action<CardTrigger>(this.detach_CardTriggers));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Class", DbType="VarChar(127) NOT NULL", CanBeNull=false)]
		public string Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				if ((this._Class != value))
				{
					this.OnClassChanging(value);
					this.SendPropertyChanging();
					this._Class = value;
					this.SendPropertyChanged("Class");
					this.OnClassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(127) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Method_CardTrigger", Storage="_CardTriggers", ThisKey="Id", OtherKey="MethodId")]
		public EntitySet<CardTrigger> CardTriggers
		{
			get
			{
				return this._CardTriggers;
			}
			set
			{
				this._CardTriggers.Assign(value);
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
		
		private void attach_CardTriggers(CardTrigger entity)
		{
			this.SendPropertyChanging();
			entity.Method = this;
		}
		
		private void detach_CardTriggers(CardTrigger entity)
		{
			this.SendPropertyChanging();
			entity.Method = null;
		}
	}
}
#pragma warning restore 1591
