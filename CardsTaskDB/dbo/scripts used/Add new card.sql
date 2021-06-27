DECLARE @CardID [int];

INSERT INTO CardsTaskDB.dbo.Cards values (1111111111111119, '2023-06-15', 
	(SELECT ct.CardTypeID FROM CardsTaskDB.dbo.CardTypes ct WHERE ct.Type = 'Visa'), 
	(SELECT po.PaymentOrganizationID FROM CardsTaskDB.dbo.PaymentOrganizations po WHERE po.OrganizationName = 'Uber')) SET @CardID = SCOPE_IDENTITY();

INSERT INTO CardsTaskDB.dbo.User_Card VALUES((SELECT uc.UserID FROM CardsTaskDB.dbo.User_Card uc WHERE uc.UserCardID = 2), @CardID); 

