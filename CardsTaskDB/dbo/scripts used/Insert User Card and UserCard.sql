DECLARE @CardID [int];
DECLARE @UserID [int];

INSERT INTO CardsTaskDB.dbo.Cards values (1111111111111118, '2023-06-15', 
	(SELECT ct.CardTypeID FROM CardsTaskDB.dbo.CardTypes ct WHERE ct.Type = 'Visa'), 
	(SELECT po.PaymentOrganizationID FROM CardsTaskDB.dbo.PaymentOrganizations po WHERE po.OrganizationName = 'Uber')) SET @CardID = SCOPE_IDENTITY();

INSERT INTO CardsTaskDB.dbo.Users values('test3', 'test3') SET @UserID = SCOPE_IDENTITY();

PRINT @CardID;
PRINT @UserID;
INSERT INTO CardsTaskDB.dbo.User_Card values
	(@CardID,@UserID);