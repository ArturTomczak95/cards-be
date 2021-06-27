
CREATE DATABASE CardsTaskDB;

GO

CREATE TABLE CardsTaskDB.dbo.PaymentOrganizations(
	PaymentOrganizationID INT IDENTITY(1,1) PRIMARY KEY,
	OrganizationName varchar(500),
);

CREATE TABLE CardsTaskDB.dbo.CardTypes(
	CardTypeID INT IDENTITY(1,1) PRIMARY KEY,
	[Type] varchar(200),
);

CREATE TABLE CardsTaskDB.dbo.Cards(
	CardID INT IDENTITY(1,1) PRIMARY KEY,
	CardNumber BIGINT UNIQUE,
	ValidThru datetime,
	PaymentOrganizationID INT references CardsTaskDB.dbo.PaymentOrganizations,
	CardTypeID INT references CardsTaskDB.dbo.CardTypes,
);

CREATE TABLE CardsTaskDB.dbo.Users(
	UserID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName varchar(200),
	LastName varchar(200)
);

CREATE TABLE CardsTaskDB.dbo.User_Card(
	UserCardID INT IDENTITY(1,1) PRIMARY KEY,
	UserID INT references CardsTaskDB.dbo.Users,
	CardID INT references CardsTaskDB.dbo.Cards UNIQUE
);

INSERT INTO CardsTaskDB.dbo.PaymentOrganizations values 
('Bolt'),
('CircleK'),
('KFC');

INSERT INTO CardsTaskDB.dbo.CardTypes values 
('MasterCard'),
('Visa');

INSERT INTO CardsTaskDB.dbo.Users values
('Katy', 'Johnson'),
('John', 'Smith');

INSERT INTO CardsTaskDB.dbo.Cards values
(1111111111111111, '2023-06-15', 1, 1),
(1111111111111112, '2022-05-17', 2, 2);

INSERT INTO CardsTaskDB.dbo.User_Card values
(1,1),
(2,2);